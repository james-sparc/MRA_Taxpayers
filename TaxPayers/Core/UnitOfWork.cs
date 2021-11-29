using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaxPayers.Data;

namespace TaxPayers.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }
        //saving data
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
        
    }
}
