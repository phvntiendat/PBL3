using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.DTO
{
    class RegistrationDataAltView
    {
        public string regisId { get; set; }
        public string CMND_CCCD { get; set; }
        public int Dose { get; set; }
        public string vaccineName { get; set; }
        public DateTime regisDay { get; set; }
        public bool State { get; set; }
    }
}
