using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Linq;
using Environment = Amazon.CDK.Environment;

namespace Src
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            var stackProps = new StackProps
            {
                Env = new Environment
                {
                    Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
                    Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION")
                }
            };

            new SrcStack(app, "HelloApiGateway", stackProps);
            app.Synth();
        }
    }
}
