using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Web_Api___Pdmsys;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.helpers;
using Web_Api___Pdmsys.Models.Interfaces;
using Web_Api___Pdmsys.Models.Repositories;

namespace Web_Api___Pdmsys.Controllers
{
    [Authorize]
    [RoutePrefix("api/projects")]
    public class ProjectsController : ApiController
    {
        private pdmsysEntities db = new pdmsysEntities();
        static readonly IProjectRepository _repo = new ProjectRepository();
        static readonly IUserRepository _userrepo = new UserRepository();
        static readonly IUserProjectRel _userprojectrel = new UserProjectRel();

        public IQueryable GetUserProjects()
        {
            return _userprojectrel.GetUsersProjects();
        }

        [HttpGet]
        [Route("rights/{projectId}")]
        public IHttpActionResult GetProjectrights(int projectId)
        {
            return Ok(_userprojectrel.GetProjectRightsByProjectId(projectId));
        }

        [HttpGet]
        [Route("getProjectName/{projectId}")]
        public IHttpActionResult GetProjectName(int projectId)
        {
            return Ok(new { name = _repo.GetProjectName(projectId) }); 
        }

        [HttpGet]
        [Route("getProjectMembers/{projectId}")]
        public IQueryable GetProjectMembers(int projectId)
        {
            return _userprojectrel.GetProjectMembersByProjectId(projectId);
        }

        [HttpPost]
        [Route("addMemberToProject/{projectId}")]
        public async Task<IHttpActionResult> AddMemberToProject(AddMemberModel model, int projectId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User_Project_Rel rel = new User_Project_Rel();
            rel.Project_FK = projectId;
            IdentityUser user = await _userrepo.FindUserByUsername(model.email);
            rel.User_FK = user.Id;
            rel.type = model.Type;

            db.User_Project_Rel.Add(rel);
            await db.SaveChangesAsync();

            List<Object> array = new List<object>();
            array.Add(new { id =user.Id });
            return Ok(array);
            
        }

        [HttpPost]
        [Route("removeProjectMember/{projectId}")]
        public IHttpActionResult PostProjects(EmailSearchModel email, int projectId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userprojectrel.RemoveRelByEmail(email.email);


            return Ok();
        }

        // DELETE: api/Project/5
        [ResponseType(typeof(Projects))]
        public async Task<IHttpActionResult> DeleteProjects(int id)
        {
            Projects projects = await db.Projects.FindAsync(id);
            if (projects == null)
            {
                return NotFound();
            }

            db.Projects.Remove(projects);
            await db.SaveChangesAsync();

            return Ok(projects);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectsExists(int id)
        {
            return db.Projects.Count(e => e.Id == id) > 0;
        }
    }
}