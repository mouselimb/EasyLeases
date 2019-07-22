using System;
using System.Collections.Generic;

namespace EasyLeasesDB.Models
{
    public partial class Homeowners
    {
        public Homeowners()
        {
            Leases = new HashSet<Leases>();
            Properties = new HashSet<Properties>();
        }

        public long HomeownersId { get; set; }
        public long? FkRealtorsId { get; set; }
        public bool? IsRepresented { get; set; }
        public string HomeownerName { get; set; }
        public string HomeownerEmail { get; set; }
        public string HomeownerPhoneNumber { get; set; }
        public string HomeownerAddress { get; set; }
        public string HomeownerCity { get; set; }
        public string HomeownerState { get; set; }
        public string HomeownerZip { get; set; }
        public string HomeownerUserName { get; set; }
        public string HomeownerPassword { get; set; }

        public virtual Realtors FkRealtors { get; set; }
        public virtual ICollection<Leases> Leases { get; set; }
        public virtual ICollection<Properties> Properties { get; set; }
    }
}
