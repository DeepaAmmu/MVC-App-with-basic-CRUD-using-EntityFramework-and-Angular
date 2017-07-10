using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace F2BMVCAngularApp.Models
{
    public class EmployeeViewModel
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int? Number { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [Required]
        public int? PhoneNumber { get; set; }
        
        public int AddressID { get; set; }
        [Required]
        public int? FlatNo { get; set; }
        [Required]
        public string street { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public int? Pincode { get; set; }
        
        public int StateId { get; set; }
        
        [Display(Name = "State")]
        public string StateName { get; set; }
        
        public int CityId { get; set; }
        
        [Display(Name = "City")]
        public string CityName { get; set; }


    }
    public class EmployeeModel
    {
        public List<EmployeeViewModel> EmployeeDetails = new List<EmployeeViewModel>();
        //Labels
        public EmployeeViewModel empViewModel { get; set; }
    }
}