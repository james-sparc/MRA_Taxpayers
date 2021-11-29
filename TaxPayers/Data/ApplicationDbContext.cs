using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaxPayers.Core.Models;

namespace TaxPayers.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           // modelBuilder.Entity<Taxpayer>(modelbulder => {
        //        modelbulder.HasNoKey();
        //    });
        }

       /// public DbSet<TaxPayers.Core.Models.Taxpayer> Taxpayer { get; set; }

     

    }
}
