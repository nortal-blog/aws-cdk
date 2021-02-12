using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.S3;
using Src.Configs;

namespace Src.Modules {
    public class LambdaFunction {
        private readonly Stack stack;

        public LambdaFunction(Stack stack) {
            this.stack = stack;
        }

        public void Construct(LambdaConfiguration app) {
            var bucket = new Bucket(this.stack, "FunctionStore");

            var handler = new Function(this.stack, "Function", new FunctionProps {
                Runtime = Runtime.NODEJS_12_X,
                FunctionName = app.FunctionName,
                Code = Code.FromAsset(app.Resource),
                Handler = "index.handler",
                Environment = new Dictionary<string, string>{
                    { "BUCKET", bucket.BucketName }
                }
            });

            bucket.GrantReadWrite(handler);

        }

   }
}