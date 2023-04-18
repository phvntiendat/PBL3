using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.DTO
{
    public class Account
    {
        [Key]
        [StringLength(50)]
        [Required]
        public string CMND_CCCD { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [StringLength(50)]
        public string Fullname { get; set; }
        public bool Permission { get; set; }

        public Account() { }
        public Account(string _CMND_CCCD, string _fullname, string _username, string _password, bool _permission)
        {
            this.CMND_CCCD = _CMND_CCCD;
            this.Username = _username;
            this.Password = _password;
            this.Fullname = _fullname;
            this.Permission = _permission;
        }

    }
}
