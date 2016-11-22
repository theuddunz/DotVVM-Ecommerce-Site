using System.Web.Hosting;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using DotVVM.Framework;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Routing;
using DotVVM.Framework.Controls.Bootstrap;

namespace EntityFrameworkCF
{
    public class DotvvmStartup : IDotvvmStartup
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            
            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
            config.AddBootstrapConfiguration();
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Admin", "admin", "Views/AdminPage.dothtml");
            config.RouteTable.Add("Register", "register", "Views/register.dothtml");
            config.RouteTable.Add("Index","","Views/index.dothtml");
            config.RouteTable.Add("LoginPage", "login","Views/LoginPage.dothtml");
            config.RouteTable.Add("ProfilePage", "user", "Views/ProfilePage.dothtml");
            config.RouteTable.Add("Cart", "cart", "Views/Cart.dothtml");
            // Uncomment the following line to auto-register all dothtml files in the Views folder
            // config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));    
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            // register custom resources and adjust paths to the built-in resources
        }
    }
}
