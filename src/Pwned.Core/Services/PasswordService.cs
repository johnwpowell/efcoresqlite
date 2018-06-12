using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pwned.Core.Models;
using Pwned.Core.Data;

namespace Pwned.Core.Services
{
    public interface IPasswordService
    {
        Task<PasswordModel> GetPasswordAsync(string hash);
    }

    public class PasswordService : IPasswordService
    {
        private PwnedDbContext dbContext;
        private ILogger<PasswordService> logger;

        public PasswordService(PwnedDbContext dbContext, ILogger<PasswordService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<PasswordModel> GetPasswordAsync(string hash)
        {
            var password = await dbContext.Passwords.FindAsync(hash);

            if (password == null)
            {
                return null;
            }

            return new PasswordModel
            {
                Hash = password.Hash,
                Count = password.Count
            };
        }
    }
}