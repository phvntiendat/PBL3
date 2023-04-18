using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.DTO
{
    public class CreateDatabase : CreateDatabaseIfNotExists<PBL3Entities>
    {
        protected override void Seed(PBL3Entities context)
        {
            context.Accounts.AddRange(new Account[]
            {
                //new Account{CMND_CCCD = "0123", Username = "admin", Password = "admin", Fullname = "Quản trị viên", Permission = true},
                //new Account{CMND_CCCD = "1234", Username = "user", Password = "user", Fullname = "Nguyen User", Permission = false}
            });
            context.Citizens.AddRange(new Citizen[]
            {
                //new Citizen{CMND_CCCD = "1234",fullName = "Nguyen Van A",  address = "Da Nang", gender = true, phone = "911", birth = DateTime.Now.Date, vaccination = 1},
                //new Citizen{CMND_CCCD = "2222",fullName = "Nguyen Van B",  address = "Hue", gender = true, phone = "911", birth = DateTime.Now.Date, vaccination = 2},
                //new Citizen{CMND_CCCD = "3333",fullName = "Nguyen Van C",  address = "Da Nang", gender = true, phone = "911", birth = DateTime.Now.Date, vaccination = 2},
            });
            context.Registrations.AddRange(new Registration[]
            {
                //new Registration{regisId = "1AA",CMND_CCCD = "1234", Dose = 2, vaccineName = "astra", regisDay = DateTime.Now, State = false},
                //new Registration{regisId = "2AAA",CMND_CCCD = "1231", Dose = 1, vaccineName = "verocell", regisDay = DateTime.Now, State = true},
                //new Registration{regisId = "3AA",CMND_CCCD = "3454", Dose = 2, vaccineName = "astra", regisDay = DateTime.Now, State = false}
            });
            context.Vaccines.AddRange(new Vaccine[]
            {
                //new Vaccine{vaccineName = "astra", quantity = 101},
                //new Vaccine{vaccineName = "verocell", quantity = 202}
            });
            
        }
    }
}
