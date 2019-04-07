using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BitSchool.Startup))]
namespace BitSchool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
