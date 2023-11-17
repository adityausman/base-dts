using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BASE_RFID_TMS.Startup))]
//[assembly: OwinStartupAttribute(typeof(BASE_RFID_TMS.Startup))]
namespace BASE_RFID_TMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
