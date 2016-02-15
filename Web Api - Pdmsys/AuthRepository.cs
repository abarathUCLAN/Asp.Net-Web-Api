using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Web_Api___Pdmsys.Models;
using Web_Api___Pdmsys.Models.data;

namespace Web_Api___Pdmsys
{
    public class AuthRepository : IDisposable
    {
        private PdmsysContext _ctx;

        private UserManager<IdentityUser> _userManager;

        private pdmsysEntities db = new pdmsysEntities();
        

        public AuthRepository()
        {
            _ctx = new PdmsysContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        public async Task<IdentityUser> RegisterUser(RegisterModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.email
            };
           
            var result = await _userManager.CreateAsync(user, userModel.Password);

            if (result.Succeeded == false)
                return null;

            return user;
        }

        public object GetUserdata()
        {
            IdentityUser user = _userManager.FindByName(ClaimsPrincipal.Current.Claims.ToList().First().Value);

            var query = from m in db.AspNetUsers
                        join u in db.UserInfos
                            on m.Id equals u.User_FK
                        where m.Id == user.Id
                        select new
                        {
                            firstname = u.firstname,
                            lastname = u.lastname,
                            email = m.UserName
                        };
            return query.First();                        
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}