using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
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

        public void ChangeUserData(UserdataChangeModel model, IdentityUser user)
        {
            String hashedNewPassword = _userManager.PasswordHasher.HashPassword(model.password);
            UserStore<IdentityUser> store = new UserStore<IdentityUser>(context);
            store.SetPasswordHashAsync(user, hashedNewPassword);
            store.UpdateAsync(user);

            UserInfos info = (from m in db.UserInfos
                             where m.User_FK == user.Id
                             select m).First<UserInfos>();

            if (model.firstname != null)
                info.firstname = model.firstname;

            if (model.lastname != null)
                info.lastname = model.lastname;

            db.Entry(info).State = EntityState.Modified;

            db.SaveChanges();

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

        public UserInfos FindUserinfos(string userid)
        {
            var query = from m in db.UserInfos
                        where m.User_FK == userid
                        select m;
            return query.First();
        }
    }
}