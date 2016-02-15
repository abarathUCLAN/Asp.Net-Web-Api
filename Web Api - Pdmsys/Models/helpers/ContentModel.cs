using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Api___Pdmsys.Models.helpers
{
    public class ContentModel
    {
        [Required]
        [MinLength(5)]
        public string content { get; set; }
    }
}