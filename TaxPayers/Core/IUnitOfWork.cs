using System.Security.Claims;
using System.Threading.Tasks;

namespace TaxPayers.Core
{
    public interface IUnitOfWork
    {
       
        Task CompleteAsync();
    }
}