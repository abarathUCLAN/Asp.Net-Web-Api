using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Api___Pdmsys.Models.helpers
{
    public class UserdataChangeModel
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string password { get; set; }
    }
}