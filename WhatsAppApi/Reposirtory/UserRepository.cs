using System;
using WhatsAppApi.Data;
using WhatsAppApi.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WhatsAppApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context )
        {
            _context = context;
        }

        public async Task<UserModel> Create(UserModel user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
        public async Task<UserModel> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
