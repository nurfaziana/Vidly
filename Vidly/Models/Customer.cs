using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly2.Models
{
    public class Customer
    { 
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter customer's name.")]   // 1. add data annotation
        [StringLength(255)]
        public string Name { get; set; }  
        public bool IsSubscribedToNewsLetter { get; set; }
        public MembershipType MembershipType { get; set; }

        [Display(Name = "Type of Membership")]
        public byte MembershipTypeId { get; set; }

        [Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }
    }
}