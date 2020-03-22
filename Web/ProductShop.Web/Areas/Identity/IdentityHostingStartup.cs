using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ProductShop.Web.Areas.Identity.IdentityHostingStartup))]

namespace ProductShop.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
