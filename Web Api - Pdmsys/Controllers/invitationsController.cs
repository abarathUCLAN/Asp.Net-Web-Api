using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.helpers;
using Web_Api___Pdmsys.Models.Interfaces;
using Web_Api___Pdmsys.Models.Repositories;

namespace Web_Api___Pdmsys.Controllers
{
    [Authorize]
    [RoutePrefix("api/invitations")]
    public class invitationsController : ApiController
    {
        private pdmsysEntities db = new pdmsysEntities();
        static readonly IInvitationRepository _repo = new InvitationRepository();

        public IQueryable Getinvitations(int id)
        {
            return _repo.GetInvitations(id);
        }

        // POST: api/invitations
        [Route("createInvitation/{projectId}")]
        public async Task<HttpResponseMessage> Postinvitations(invitations[] invitations, int projectId)
        {
            List<invitations> creationFailedInvitations = new List<invitations>();
            foreach (invitations invitation in invitations)
            {
                if (_repo.checkForAvailableEmail(invitation.email))
                    creationFailedInvitations.Add(invitation);
                else {
                    invitation.Project_FK = projectId;
                    invitation.urlcode = RandomString(20);
                    db.invitations.Add(invitation);
                }
            }
            await db.SaveChangesAsync();

            return Request.CreateResponse(HttpStatusCode.BadRequest, creationFailedInvitations);
        }

        [Route("addInvitationToProject/{projectId}")]
        public async Task<IHttpActionResult> AddInvitationToProject(invitations invitations, int projectId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            invitations.Project_FK = projectId;
            invitations.urlcode = RandomString(20);
            db.invitations.Add(invitations);
            await db.SaveChangesAsync();

            return Ok(invitations);
        }

        [HttpPost]
        [Route("deleteInvitation/{projectId}")]
        public IHttpActionResult RemoveInvitationByEmail(EmailSearchModel model, int projectId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repo.RemoveInvitationByEmail(model.email);


            return Ok();
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool invitationsExists(int id)
        {
            return db.invitations.Count(e => e.Id == id) > 0;
        }
    }
}