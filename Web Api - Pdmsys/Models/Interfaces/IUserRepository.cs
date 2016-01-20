using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web_Api___Pdmsys.Models.helpers;

namespace Web_Api___Pdmsys.Models.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityUser> Find();
        IEnumerable FindUserByUsername(EmailSearchModel email);
        Task<IdentityUser> FindUserByUsername(string userName);
    }
}