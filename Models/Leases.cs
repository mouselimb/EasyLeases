using System;
using System.Collections.Generic;

namespace EasyLeasesDB.Models
{
    public partial class Leases
    {
        public long LeasesId { get; set; }
        public long? FkHomeownersId { get; set; }
        public long? FkPropertiesId { get; set; }
        public long? FkRealtorsId { get; set; }
        public long? FkRentersId { get; set; }
        public string LeaseNumber { get; set; }
        public DateTime? LeaseStartDate { get; set; }
        public DateTime? LeaseEndDate { get; set; }
        public decimal? LeaseAmount { get; set; }

        public virtual Homeowners FkHomeowners { get; set; }
        public virtual Properties FkProperties { get; set; }
        public virtual Realtors FkRealtors { get; set; }
        public virtual Renters FkRenters { get; set; }
    }
}
