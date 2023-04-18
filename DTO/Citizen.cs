using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.DTO
{
    public class Citizen
    {
        [Key]
        [StringLength(50)]
        [Required]
        public string CMND_CCCD { get; set; }
        public string fullName { get; set; }
        public bool gender { get; set; }
        public DateTime birth { get; set; }
        [StringLength(50)]
        public string phone { get; set; }
        public string address { get; set; }
        public int vaccination { get; set; }

        public Citizen()
        {

        }
        public Citizen(string _cmnd, string _fullname, string _address, bool _gender, string _phone, DateTime _birth, int _vaccination)
        {
            this.CMND_CCCD = _cmnd;
            this.fullName = _fullname;
            this.gender = _gender;
            this.birth = _birth;
            this.phone = _phone;
            this.address = _address;
            this.vaccination = _vaccination;
        }
    }
}
