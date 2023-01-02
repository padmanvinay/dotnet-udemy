using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CharacterDbContext dbContext;

        public AuthRepository(CharacterDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<ServiceResponse<string>> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            ServiceResponse<int> serviceResponse = new ServiceResponse<int>();
            serviceResponse.Data = user.Id;
            return serviceResponse;
        }

        public Task<bool> UserExists(string username)
        {
            throw new NotImplementedException();
        }
    }
}