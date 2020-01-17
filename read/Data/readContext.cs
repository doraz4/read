using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//using read.Models;

namespace read.Models
{
    public class readContext : DbContext
    {
        public readContext (DbContextOptions<readContext> options)
            : base(options)
        {
        }

        public DbSet<read.Models.RBooks> AllBooks { get; set; }
    }
}
