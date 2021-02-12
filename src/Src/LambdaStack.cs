using System.Collections.Generic;
using Amazon.CDK;
using Src.Configs;
using Src.Modules;

namespace Src
{
    public class LambdaStack : Stack
    {
        internal LambdaStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var config = new LambdaConfiguration()
            {
                FunctionName = id,
                Resource = "./resources/function"
            };

            var lambda = new LambdaFunction(this);
            lambda.Construct(config);
        }
    }
}
