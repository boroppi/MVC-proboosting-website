﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mvc_proboosting.Startup))]
namespace mvc_proboosting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
