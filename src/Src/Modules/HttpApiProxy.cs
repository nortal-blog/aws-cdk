using Amazon.CDK;
using Amazon.CDK.AWS.APIGatewayv2;
using Amazon.CDK.AWS.APIGatewayv2.Integrations;
using Amazon.CDK.AWS.CertificateManager;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.Route53;
using Amazon.CDK.AWS.Route53.Targets;
using Src.Configs;
using HttpMethod = Amazon.CDK.AWS.APIGatewayv2.HttpMethod;

namespace Src.Modules {
    public class HttpApiProxy {
        private readonly Stack stack;

        public HttpApiProxy(Stack stack) {
            this.stack = stack;
        }

        public void ConstructServerlessApp(AppConfiguration app) {
            var httpApi = AddApiGateway(app.AppId);
            //Custom domain name for the gateway
            var domainName = AddApiGatewayCustomDomainName(app.GetApplicationUrl(), app.GetCertificateArn());
            //Map the api + domain + stage together
            var apiMapping = AddApiGatewayMapping(httpApi, domainName);

            //Add default route to S3 "/*"
            var defaultRoute = CreateDefaultRouteToS3Bucket(httpApi, app.GetS3BucketUrl());

            //Add Lambda integrations
            foreach (var lambda in app.Lambdas) {
                var apiRoute = CreateApiRouteForLambda(httpApi, lambda.FunctionName, lambda.GetLambdaArn(stack), lambda.ApiPath);
            }

            //Tie-up everything with the Route53 Alias
            var route53 = AddRoute53Alias(app.GetHostedZoneUrl(), app.GetApplicationUrl(), domainName);
        }

        private ARecord AddRoute53Alias(string hostedZoneUrl, string applicationUrl, DomainName domainName) {
            return new ARecord(stack, "Route53Alias", new ARecordProps {
                Zone = HostedZone.FromLookup(stack, "HostedZone", new HostedZoneProviderProps {
                    DomainName = hostedZoneUrl
                }),
                RecordName = applicationUrl,
                Target = RecordTarget.FromAlias(new ApiGatewayv2Domain(domainName))
            });
        }

        private HttpRoute CreateApiRouteForLambda(HttpApi httpApi, string appName, string lambdaArn, string apiPath) {
            return new HttpRoute(stack, $"{appName}ApiRoute", new HttpRouteProps {
                HttpApi = httpApi,
                Integration = new LambdaProxyIntegration(new LambdaProxyIntegrationProps {
                    //This is important. Default version 2.0 will not work without changing the backend code!
                    PayloadFormatVersion = PayloadFormatVersion.VERSION_1_0,
                    Handler = Function.FromFunctionArn(stack, $"{appName}", lambdaArn)
                }),
                RouteKey = HttpRouteKey.With(apiPath, HttpMethod.ANY)
            });
        }

        private HttpRoute CreateDefaultRouteToS3Bucket(HttpApi httpApi, string s3BucketUrl) {
            return new HttpRoute(stack, "S3DefaultRoute", new HttpRouteProps {
                HttpApi = httpApi,
                Integration = new HttpProxyIntegration(new HttpProxyIntegrationProps {
                    Method = HttpMethod.ANY,
                    Url = s3BucketUrl
                }),
                RouteKey = HttpRouteKey.DEFAULT
            });
        }

        private HttpApiMapping AddApiGatewayMapping(HttpApi httpApi, DomainName domainName) {
            return new HttpApiMapping(stack, "HttpApiMapping", new HttpApiMappingProps {
                Api = httpApi,
                DomainName = domainName,
                Stage = new LocalDefaultStage(httpApi.DefaultStage)
            });
        }

        private DomainName AddApiGatewayCustomDomainName(string applicationUrl, string certificateArn) {
            return new DomainName(stack, "DomainName", new DomainNameProps {
                DomainName = applicationUrl,
                Certificate = Certificate.FromCertificateArn(stack, "WildCardCertificate", certificateArn)
            });
        }

        private HttpApi AddApiGateway(string appId) {
            return new HttpApi(stack, "HttpApiGateway", new HttpApiProps {
                ApiName = $"{appId}-proxy-cdk",
                CreateDefaultStage = true,
                Description = $"{appId}-proxy via cdk",
                DisableExecuteApiEndpoint = true
            });
        }
    }

    public class LocalDefaultStage : IHttpStage {
        public LocalDefaultStage(HttpStage stage) {
            Node = stage.Node;
            Env = stage.Env;
            Stack = stage.Stack;
            StageName = stage.StageName;
        }

        public ConstructNode Node { get; }
        public IResourceEnvironment Env { get; }
        public Stack Stack { get; }
        public string StageName { get; }
    }
}