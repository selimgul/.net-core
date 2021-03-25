using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Models
{
    public class FabrikaContext
       : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public FabrikaContext(DbContextOptions<FabrikaContext> options)
            : base(options)
        {
        }
    }
}
