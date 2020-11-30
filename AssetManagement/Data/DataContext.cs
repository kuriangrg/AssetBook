using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AssetManagement.Domain;

namespace AssetManagement.Data
{
    public class DataContext : IdentityDbContext
    {
        
        public DbSet<Asset>  Assets { get; set; }
        public DbSet<Metadata> Metadatas { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<Network> Networks { get; set; }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            
        }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Asset>().HasData(new Asset[] {
                new Asset{AssetId=1,AssetName="Dirico Parent",IsFolder=true}
            });
            modelBuilder.Entity<Network>().HasData(new Network[] {
                new Network{NetworkId=1,NetworkName="Facebook Landscape",MediaHeight=315,MediaWidth=600},
                 new Network{NetworkId=2,NetworkName="Twitter Portrait",MediaHeight=640,MediaWidth=360},

            });
        }
    }
}
