using System.Collections.Generic;
using Amazon.CDK;
using Src.Configs;
using Src.Modules;

namespace Src
{
    public class ApiGatewayStack : Stack
    {
        internal ApiGatewayStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var appConfig = new AppConfiguration
            {
                AppId = "hello",
                AppName = "Hello",
                Lambdas = new List<LambdaConfiguration> {
                    new LambdaConfiguration {
                        ApiPath = "/api/{proxy+}",
                        FunctionName = "LambdaFunctionOne"
                    }                    
                }
            };

            var apiGatewayProxy = new HttpApiProxy(this);
            apiGatewayProxy.Construct(appConfig);
        }
    }
}
