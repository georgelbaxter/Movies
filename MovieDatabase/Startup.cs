using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MovieDatabase.Startup))]

namespace MovieDatabase
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
