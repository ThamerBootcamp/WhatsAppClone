using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsAppApi.Models;

namespace WhatsAppApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoomModel> Rooms { get; set; }
        public DbSet<MessageModel> Messages { get; set; }


    }
}
