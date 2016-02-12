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
            try
            {
                return _userprojectrel.GetUsersProjects();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateProject(Projects project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (_repo.findProjectByName(project.name))
                return BadRequest();

            db.Projects.Add(project);
            await db.SaveChangesAsync();

            User_Project_Rel rel = new User_Project_Rel();
            rel.Project_FK = project.Id;
            IdentityUser user = await _userrepo.Find();
            rel.User_FK = user.Id;
            rel.type = 2;
            db.User_Project_Rel.Add(rel);

            await db.SaveChangesAsync();

            return Ok(project.Id);
        }

        [HttpGet]
        [Route("rights/{projectId}")]
        public IHttpActionResult GetProjectrights(int projectId)
        {
            int rights = _userprojectrel.GetProjectRightsByProjectId(projectId);
            if (rights == -1)
                return Unauthorized();
            return Ok(_userprojectrel.GetProjectRightsByProjectId(projectId));
        }

        [HttpGet]
        [Route("getProjectName/{projectId}")]
        [MemberAndSpectatorActionFilter]
        public IHttpActionResult GetProjectName(int projectId)
        {
            try
            {
                return Ok(new { name = _repo.GetProjectName(projectId) });
            }

            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("getProjectMembers/{projectId}")]
        [AdminTypeActionFilter]
        public IQueryable GetProjectMembers(int projectId)
        {
            return _userprojectrel.GetProjectMembersByProjectId(projectId);
        }

        [HttpGet]
        [Route("dashboard/{projectId}")]
        [MemberAndSpectatorActionFilter]
        public IHttpActionResult GetDashboardData(int projectId)
        {
            int finalizationPercent = calculatePercent(_repo.getFinalizationPercent(projectId), 3);
            int preliminaryStudyPercent = calculatePercent(_repo.getPreliminaryStudyPercent(projectId), 10);
            int requirementSpecificationPercent = calculatePercent(_repo.getRequirementSpecificationPercent(projectId), 2);
            int functionalSpecificationPercent = calculatePercent(_repo.getFunctionalSpecificationPercent(projectId), 3);
            return Ok(new
            {
                finalization = finalizationPercent,
                preliminaryStudy = preliminaryStudyPercent,
                requirementSpecification = requirementSpecificationPercent,
                functionalSpecification = functionalSpecificationPercent
            });
        }

        private int calculatePercent(int v, int max)
        {
            if (v == 0)
                return 0;
            float result = (100 / max) * v;
            return (int) result;
        }

        [HttpPost]
        [Route("addMemberToProject/{projectId}")]
        [AdminTypeActionFilter]
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
            array.Add(new { id = user.Id });
            return Ok(array);

        }

        [HttpPost]
        [Route("removeProjectMember/{projectId}")]
        [AdminTypeActionFilter]
        public IHttpActionResult PostProjects(EmailSearchModel email, int projectId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userprojectrel.RemoveRelByEmail(email.email);


            return Ok();
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