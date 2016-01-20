using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.helpers;
using Web_Api___Pdmsys.Models.Interfaces;

namespace Web_Api___Pdmsys.Models.Repositories
{
	public class UserRepository : IUserRepository
	{
        private PdmsysContext context;

        private pdmsysEntities db;

        private UserManager<IdentityUser> _userManager;

        public UserRepository()
        {
            context = new PdmsysContext();
            db = new pdmsysEntities();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
        }

        public async Task<IdentityUser> Find()
        {
            IdentityUser user = await _userManager.FindByNameAsync(ClaimsPrincipal.Current.Claims.ToList().First().Value);

            return user;
        }

        public async Task<IdentityUser> FindUserByUsername(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public IEnumerable FindUserByUsername(EmailSearchModel email)
        {
            var query = from user in db.AspNetUsers
                        join info in db.UserInfos
                            on user.Id equals info.User_FK
                        where user.UserName == email.email
                        select new
                        {
                            firstname = info.firstname,
                            lastname = info.lastname,
                            email = user.UserName
                        };
            return query.ToList();
        }
    }
}