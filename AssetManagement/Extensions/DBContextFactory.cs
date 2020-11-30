using AssetManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Extensions
{
    public class DBContextFactory
    {
        public IConfiguration Configuration { get; }
       public DBContextFactory(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public DataContext Create()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer("Server=DESKTOP-DA595U3\\SQLEXPRESS;Database=AssetBook;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            return new DataContext(options);
        }
    }
}
