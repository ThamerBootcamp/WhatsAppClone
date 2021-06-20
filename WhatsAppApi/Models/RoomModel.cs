using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WhatsAppApi.Models
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string Name { get; set; } // uniqe

        //[ForeignKey("Owner_Id")]
       // public int Owner_Id { get; set; }
        [JsonIgnore]
        public UserModel Owner { get; set; }

        //[ForeignKey("Guest_Id")]
        //public int Guest_Id { get; set; }
        [JsonIgnore]
        public UserModel Guest { get; set; }

        // one to many : Room  ------<- Message
        public List<MessageModel> Messages { get; set; }
    }
}
