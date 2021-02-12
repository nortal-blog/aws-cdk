using Amazon.CDK;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.S3.Deployment;
using Src.Configs;

namespace Src.Modules {
    public class StaticWebsite {
        private readonly Stack stack;

        public StaticWebsite(Stack stack) {
            this.stack = stack;
        }

        public void Construct(AppConfiguration app) {
            var bucket = new Bucket(this.stack, "WebsiteBucket", new BucketProps {
                BucketName = app.ApplicationUrl,
                PublicReadAccess = true,
                RemovalPolicy = RemovalPolicy.DESTROY,
                WebsiteIndexDocument = "index.html"                
            });

            var deploy = new BucketDeployment(this.stack, "BucketDeployment", new BucketDeploymentProps{
                DestinationBucket = bucket,
                Sources = new [] { Source.Asset("./webpage")
            }});

        }

   }
}