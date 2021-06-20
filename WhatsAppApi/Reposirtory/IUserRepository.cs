using System;
using System.Threading.Tasks;
using WhatsAppApi.Models;

namespace WhatsAppApi.Repositories
{
    public interface IUserRepository
    {

        Task<UserModel> Create(UserModel user);

        Task<UserModel> GetUserByUsername(string username);
        Task<UserModel> GetUserById(int id);

    }
}
