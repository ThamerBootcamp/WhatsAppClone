﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WhatsAppApi.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Displayname { get; set; }
        public string Img { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        // one to many : User ------<- Room
        [InverseProperty("Owner")]
        public List<RoomModel> OwnerRooms { get; set; }
        [JsonIgnore]
        [InverseProperty("Guest")]
        public List<RoomModel> Guest_Rooms { get; set; }
    }
}
