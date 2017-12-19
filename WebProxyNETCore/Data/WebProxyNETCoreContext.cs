using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebProxyNETCore.Models
{
    public class WebProxyNETCoreContext : DbContext
    {
        public WebProxyNETCoreContext (DbContextOptions<WebProxyNETCoreContext> options)
            : base(options)
        {
        }

        public DbSet<WebProxyNETCore.Models.ConfiguracionProxyModel> ConfiguracionProxyModel { get; set; }
    }
}
