using Kornel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kornel.Persistance
{
    public class DatabasrContext : DbContext
    {
       protected override void OnConfiguring(DbContextOptionsBuilder options)
       {
            options.UseSqlite("Filename=MyDatabase.db");
       }

        public DbSet<UserModel> Users { get; set; }
    }
}
