using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Api___Pdmsys.Models.helpers
{
    public class UserdataChangeModel
    {
        [MaxLength(25)]
        [MinLength(2)]
        public string firstname { get; set; }
        [MaxLength(25)]
        [MinLength(2)]
        public string lastname { get; set; }
        [DataType(DataType.Password)]
        [MaxLength(100)]
        [MinLength(6)]
        public string password { get; set; }
    }
}