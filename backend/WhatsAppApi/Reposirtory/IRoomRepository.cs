using System;
using System.Threading.Tasks;
using WhatsAppApi.Models;

namespace WhatsAppApi.Repositories
{
    public interface IRoomRepository
    {

        Task<RoomModel> Create(RoomModel room);

        //Task<RoomModel> GetUserByUsername(string username);
        //Task<RoomModel> GetUserById(int id);

    }
}
