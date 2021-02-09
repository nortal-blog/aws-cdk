using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Src
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new SrcStack(app, "SrcStack");
            app.Synth();
        }
    }
}
