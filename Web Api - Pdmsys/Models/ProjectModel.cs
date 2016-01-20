using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Api___Pdmsys.Models
{
    public class ProjectModel
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string description { get; set; }

        public string acronym { get; set; }
    }
}