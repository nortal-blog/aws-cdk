using System.Collections.Generic;
using Amazon.CDK;
using Src.Configs;
using Src.Modules;

namespace Src
{
    public class WebsiteStack : Stack
    {
        internal WebsiteStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var config = new WebsiteConfiguration()
            {
                DomainName = "hello.awsdevfi.nortal.com",
                Resource = "./resources/website"
            };

            var resource = new StaticWebsite(this);
            resource.Construct(config);
        }
    }
}
