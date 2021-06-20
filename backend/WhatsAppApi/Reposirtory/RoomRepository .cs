using System;
using WhatsAppApi.Data;
using WhatsAppApi.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WhatsAppApi.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context )
        {
            _context = context;
        }

        public async Task<RoomModel> Create(RoomModel room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return room;
        }
    }
}
