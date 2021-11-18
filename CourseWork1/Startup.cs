using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CourseWork1.Startup))]
namespace CourseWork1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
