using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsAppApi.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public int Sender { get; set; }
        public int Receiver { get; set; }
        public string Content { get; set; }
        public DateTime Createdat { get; set; }

        //[ForeignKey("Id")]
        public RoomModel Room { get; set; }
       // public int Room_ID { get; set; }
    }
}
