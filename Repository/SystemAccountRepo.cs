using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Presentation {


    public interface ISystemAccountRepo {
        Task<SystemAccount> FindByEmailPassword(String email, String password);
    } 
    
    public class SystemAccountRepo : ISystemAccountRepo {

        private readonly FUNewsManagementDbContext _context;

        public SystemAccountRepo(FUNewsManagementDbContext context) {
            _context = context;
        }

        public async Task<SystemAccount> FindByEmailPassword(String email, String password) {
            return await _context.SystemAccounts.FirstOrDefaultAsync(x => x.AccountEmail == email && x.AccountPassword == password);
        }

    }
}
