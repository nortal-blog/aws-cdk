using System.Collections.Generic;
using Amazon.CDK;

namespace Src.Configs {
    public class AppConfiguration {
        public string AppName { get; set; }
        public string AppId { get; set; }
        public string Env { get; set; }
        public List<LambdaConfiguration> Lambdas { get; set; }

        public string GetS3BucketUrl() {
            return $"http://hello.awsdevfi.nortal.com.s3-website.eu-north-1.amazonaws.com";
        }

        public string GetHostedZoneUrl() {
            return $"awsdevfi.nortal.com";
        }

        public string ApplicationUrl => $"hello.awsdevfi.nortal.com";

        public string GetCertificateArn() {
            return "arn:aws:acm:eu-north-1:594357352549:certificate/013468db-b270-427d-86cf-8399a8585c25";
        }
    }
}