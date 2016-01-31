using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Web_Api___Pdmsys.Models;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.helpers;
using Web_Api___Pdmsys.Models.Interfaces;
using Web_Api___Pdmsys.Models.Repositories;

namespace Web_Api___Pdmsys.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private AuthRepository _repo = null;
        pdmsysEntities db = new pdmsysEntities();
        static readonly IUserRepository _userrepo = new UserRepository();

        public UserController()
        {
            _repo = new AuthRepository();
        }

        [HttpGet]
        [Route("changeData")]
        public IHttpActionResult GetUser()
        {
            try
            {
                return Ok(_repo.GetUserdata());
            }
            catch (Exception e)
            {
                return Unauthorized();
            }
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityUser result = await _repo.RegisterUser(userModel);

            if (result.Id == null)
                return BadRequest();

            UserInfos info = new UserInfos();
            info.firstname = userModel.Firstname;
            info.lastname = userModel.Lastname;
            info.User_FK = result.Id;
            await db.SaveChangesAsync();
            db.UserInfos.Add(info);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("changeData")]
        public async Task<IHttpActionResult> ChangeUserData(UserdataChangeModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityUser user = await _userrepo.Find();

            UserInfos info = _userrepo.FindUserinfos(user.Id);


            _userrepo.ChangeUserData(model, user);

            return Ok();
        }

        [HttpPost]
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            return Ok();
        }


        [HttpPost]
        [Route("getUserByEmail")]
        public IHttpActionResult GetUserByEmail(EmailSearchModel email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable user = _userrepo.FindUserByUsername(email);

            foreach(var us in user)
            {
                List<Object> array = new List<object>();
                array.Add(us);
                return Ok(array);
            }
             return BadRequest();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}