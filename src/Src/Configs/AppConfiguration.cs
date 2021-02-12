using System.Collections.Generic;
using Amazon.CDK;

namespace Src.Configs {
    public class AppConfiguration {
        public string AppName { get; set; }
        public string AppId { get; set; }
        public string Env { get; set; }
        public List<LambdaConfiguration> Lambdas { get; set; }

        public string S3BucketUrl => "http://hello.awsdevfi.nortal.com.s3-website.eu-north-1.amazonaws.com";

        public string HostedZoneUrl=> "awsdevfi.nortal.com";

        public string ApplicationUrl => $"hello.awsdevfi.nortal.com";

        public string CertificateArn => "arn:aws:acm:eu-north-1:594357352549:certificate/013468db-b270-427d-86cf-8399a8585c25";
    }
}