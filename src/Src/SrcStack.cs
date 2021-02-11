using System.Collections.Generic;
using Amazon.CDK;
using Src.Configs;
using Src.Modules;

namespace Src
{
    public class SrcStack : Stack
    {
        internal SrcStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var appConfig = new AppConfiguration
            {
                AppId = "hello",
                AppName = "Hello",
                Lambdas = new List<LambdaConfiguration> {
                    new LambdaConfiguration {
                        ApiPath = "/api/{proxy+}",
                        FunctionName = "helloNode"
                    }
                }
            };

            var apiGatewayProxy = new HttpApiProxy(this);
            apiGatewayProxy.ConstructServerlessApp(appConfig);
        }
    }
}
