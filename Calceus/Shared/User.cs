using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calceus.Shared
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public Role? Role { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [NotMapped]
        public bool IsNew { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;
    }
}
