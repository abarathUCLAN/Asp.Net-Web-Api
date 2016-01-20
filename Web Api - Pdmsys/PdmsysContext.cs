using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Api___Pdmsys
{
    public class PdmsysContext : IdentityDbContext<IdentityUser>
    {
        public PdmsysContext()
            : base("pdmsysEntities")
        {

        }

    }
}