using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Api___Pdmsys.Models
{
    public class RegisterModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        [MaxLength(25)]
        [MinLength(2)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(25)]
        [MinLength(2)]
        public string Lastname { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}