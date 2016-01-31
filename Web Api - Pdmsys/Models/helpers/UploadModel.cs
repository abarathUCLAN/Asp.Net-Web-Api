using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Api___Pdmsys.Models.helpers
{
    public class UploadModel
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string content { get; set; }

    }
}