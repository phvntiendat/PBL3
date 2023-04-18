using System;
using System.Data.Entity;
using System.Linq;
using PBL3.DTO;

namespace PBL3
{
    public class PBL3Entities : DbContext
    {
        public PBL3Entities()
            : base("name=PBL3Entities")
        {
            Database.SetInitializer<PBL3Entities>(new CreateDatabase());
        }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Citizen> Citizens { get; set; }
        public virtual DbSet<Vaccine> Vaccines { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
    }
}