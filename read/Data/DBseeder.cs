using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using read.Models;

namespace read.Data
{
    public static class DBseeder
    {
        public static void SeedDB(readContext context )
        {
            context.Database.EnsureCreated();
            context.AllBooks.Add(
            new RBooks() { Name = "Book", Id = 1, text = "try" }
            );
            //context.SaveChanges();

        }
    }
}
