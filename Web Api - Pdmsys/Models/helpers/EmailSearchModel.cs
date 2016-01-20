using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Api___Pdmsys.Models.helpers
{
    public class EmailSearchModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
    }
}