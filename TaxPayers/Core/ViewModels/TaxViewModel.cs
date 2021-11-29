using System.Collections.Generic;
using TaxPayers.Core.Models;

namespace TaxPayers.Core.ViewModels
{
    public class TaxViewModel
    {
        public Taxpayer TaxPayer { get; set; }
        public IEnumerable<Taxpayer> Taxpayers { get; set; }
        
    }
}
