using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureElasticPoolWithEFCore.DataContext
{
    public class MassRoverContext : DbContext
    {
        public MassRoverContext()
        {
        }

        public MassRoverContext(DbContextOptionsBuilder<MassRoverContext> optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            {
                if (optionsBuilder.IsConfigured == false)
                {
                    optionsBuilder.UseSqlServer("<developer database connection string>");
                }
                base.OnConfiguring(optionsBuilder);
            }
        }


        public DbSet<RoverEquipment> RoverEquipment { get; set; }
    }

    public class RoverEquipment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
