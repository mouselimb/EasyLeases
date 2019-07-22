using System;
using System.Collections.Generic;

namespace EasyLeasesDB.Models
{
    public partial class Renters
    {
        public Renters()
        {
            Leases = new HashSet<Leases>();
        }

        public long RentersId { get; set; }
        public long? FkPropertiesId { get; set; }
        public long? FkRealtorsId { get; set; }
        public string RenterName { get; set; }
        public string RenterGender { get; set; }
        public string RenterSsn { get; set; }
        public decimal? RenterMonthlySalary { get; set; }
        public string RenterEmail { get; set; }
        public string RenterPhoneNumber { get; set; }
        public string RenterUserName { get; set; }
        public string RenterPassword { get; set; }

        public virtual Properties FkProperties { get; set; }
        public virtual Realtors FkRealtors { get; set; }
        public virtual ICollection<Leases> Leases { get; set; }
    }
}
