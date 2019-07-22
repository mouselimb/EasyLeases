using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyLeasesDB.Models
{
    public partial class Realtors
    {
        public Realtors()
        {
            Homeowners = new HashSet<Homeowners>();
            Leases = new HashSet<Leases>();
            Renters = new HashSet<Renters>();
        }

        public long RealtorsId { get; set; }

        [Display(Name = "Name")]
        //[Required(ErrorMessage = "Name is Required!")]
        public string RealtorName { get; set; }

        [Display(Name = "Commission Rate")]
        [Required(ErrorMessage = "Commission Rate is Required!")]
        [DataType(DataType.Currency)]
        public decimal? CommissionRate { get; set; }

        [Display(Name = "Company")]
        public string RealtorCompany { get; set; }

        [Display(Name = "Phone #")]
        //[Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        [DataType(DataType.PhoneNumber)]
        public string RealtorPhoneNumber { get; set; }

        [Display(Name = "Username")]
        //[Required(ErrorMessage = "Username is Required!")]
        public string RealtorUserName { get; set; }

        [Display(Name = "Create Password")]
        //[Required(ErrorMessage = "Password is Required!")]
        [DataType(DataType.Password)]
        public string RealtorPassword { get; set; }

        public string RealtorReview { get; set; }

        public virtual ICollection<Homeowners> Homeowners { get; set; }
        public virtual ICollection<Leases> Leases { get; set; }
        public virtual ICollection<Renters> Renters { get; set; }
    }
}
