using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyLeasesDB.Models
{
    public partial class Properties
    {
        public Properties()
        {
            Leases = new HashSet<Leases>();
            Renters = new HashSet<Renters>();
        }
        public long PropertiesId { get; set; }

        public long? FkHomeownersId { get; set; }

        public DateTime? AvailableDate { get; set; }
        public decimal? PropertyRentAmount { get; set; }
        public string PropertyAddress { get; set; }
        public string PropertyCity { get; set; }
        public string PropertyState { get; set; }
        public string PropertyZip { get; set; }
        public int? PropertyFloors { get; set; }
        public int? PropertyBeds { get; set; }
        public int? PropertyBaths { get; set; }
        public int? PropertyLivingSqFt { get; set; }
        public bool? IsLeased { get; set; }

        public virtual Homeowners FkHomeowners { get; set; }
        public virtual ICollection<Leases> Leases { get; set; }
        public virtual ICollection<Renters> Renters { get; set; }
    }
}
