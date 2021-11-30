using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaxPayers.Core.Models
{
    public class Taxpayer
    {
        [Key][Required][MinLength(8)]
        public string TPIN { get; set; }
        [Required]
        [Display(Name = "Business Certificate Number")]
        public string BusinessCertificateNumber { get; set; }
        [Required]
        [Display(Name = "Trading Name")]
        public string TradingName { get; set; }
        [Required]
        [Display(Name = "Business RegistrationDate")]
        public DateTime BusinessRegistrationDate { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string MobileNumber { get; set; }

        [Required][EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Physical Location")]
        public string PhysicalLocation { get; set; }
        //[Required]
        public string Username { get; set; }
    }
}
