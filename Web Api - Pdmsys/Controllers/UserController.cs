using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;
using Typesafe.Mailgun;
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
        private AuthRepository _repo = new AuthRepository();
        pdmsysEntities db = new pdmsysEntities();
        static readonly IUserRepository _userrepo = new UserRepository();
        static readonly IInvitationRepository _invrepo = new InvitationRepository();

        public UserController()
        {
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
        [AdminTypeActionFilter]
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

        [HttpPost]
        [Route("checkIfUrlCodeIsValid")]
        [AllowAnonymous]
        public IHttpActionResult CheckIfUrlCodeIsValid(UrlCodeModel code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_invrepo.checkIfUrlCodeIsValid(code.urlcode))
                return BadRequest();

            return Ok();

        }

        [HttpPost]
        [Route("registerUserWithUrlCode")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> RegisterUserWithUrlCode(UrlCodeModel code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityUser user = await _invrepo.registerUserWithUrlCode(code);

            if (user.Id == null)
                return BadRequest();

            return Ok();

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