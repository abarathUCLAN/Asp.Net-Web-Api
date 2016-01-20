using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IdentityResult> RegisterUser(RegisterModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.email
            };
           
            var result = await _userManager.CreateAsync(user, userModel.Password);

            UserInfos info = new UserInfos();
            info.firstname = userModel.Firstname;
            info.lastname = userModel.Lastname;
            info.User_FK = user.Id;

            db.UserInfos.Add(info);
            await db.SaveChangesAsync();

            return result;
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