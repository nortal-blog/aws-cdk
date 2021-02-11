using Amazon.CDK;

namespace Src.Configs {
    public class LambdaConfiguration {
        public string ApiPath { get; set; }
        public string FunctionName { get; set; }

        public string GetLambdaArn(Stack stack) {
            return $"arn:aws:lambda:{stack.Region}:{stack.Account}:function:{FunctionName}";
        }
    }
}