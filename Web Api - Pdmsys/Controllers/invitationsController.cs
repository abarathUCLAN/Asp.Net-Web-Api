using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
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
                    sendMail(invitation);
                    db.invitations.Add(invitation);
                }
            }
            await db.SaveChangesAsync();

            return Request.CreateResponse(HttpStatusCode.BadRequest, creationFailedInvitations);
        }

        [Route("addInvitationToProject/{projectId}")]
        [AdminTypeActionFilter]
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

            sendMail(invitations);

            return Ok(invitations);
        }

        [HttpPost]
        [Route("deleteInvitation/{projectId}")]
        [AdminTypeActionFilter]
        public IHttpActionResult RemoveInvitationByEmail(EmailSearchModel model, int projectId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repo.RemoveInvitationByEmail(model.email);

            return Ok();
        }

        private void sendMail(invitations inv)
        {
            MailMessage mailMessage = new MailMessage("support@pdmsys.com", "barath1058@gmail.com");

            mailMessage.Subject = "Yout got invited to Pdmsys! Check it out!";
            mailMessage.Body = "<body> Hello " + inv.firstname + " " + inv.lastname + "," +
                "<br> You have been invited to a project on Pdmsys, register yourself <a href = 'http://localhost:8080/pdmsys/#/invitation/'"+ inv.urlcode +">" +
            "here </a> and check your project out. If the link is not working, please copy http://localhost:8080/pdmsys/#/invitation/"+ inv.urlcode+" manually into your browser." +
            "<br>Kind Regards<br>Pdmsys - Admin</body>";

            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.mailgun.com", 587);

            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "yolo@sandbox08f1c44dd2074ddf94fc8c06d253f59d.mailgun.org",
                Password = "12345"
            };

            smtpClient.Send(mailMessage);
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