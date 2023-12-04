using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextMessenger.DataLayer.DTOs.UserDTOs
{
    public class UserMainInfoDTO
    {
        public Guid UserId { get; set; }
        public string Nickname { get; set; } = null!;
    }
}
