using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Api___Pdmsys.Models.helpers
{
    public class AddMemberModel
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
        public int Type { get; set; }
    }
}