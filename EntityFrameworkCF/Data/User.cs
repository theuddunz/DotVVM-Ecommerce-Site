using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCF.ViewModels
{
    public class User
    {
        [Key] //Rappresenta La Chiave Primarfia , di conseguenza la variabile sotto questo  [Key] diventare PK.
        public int UserID { get; set; }
        [Required(ErrorMessage = "The Username is necessary")]
        public string Username { get; set; }
        [Required(ErrorMessage = "The Password is necessary")]

        public string Password { get; set; }
        [Required(ErrorMessage = "The Email is necessary")]
        [DataType(DataType.EmailAddress,ErrorMessage ="This email it's not valid")]

        
        public string Email { get; set; }
        public string Country { get; set; }
       
    }
}