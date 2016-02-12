using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.Interfaces;
using Web_Api___Pdmsys.Models.Repositories;

namespace Web_Api___Pdmsys.Controllers
{
    [Authorize]
    [RoutePrefix("api/functionalSpecification")]
    public class FunctionalSpecificationController : ApiController
    {

        private pdmsysEntities db = new pdmsysEntities();
        static readonly IFunctionalSpecificationRepository _repo = new FunctionalSpecificationRepository();

        [HttpGet]
        [Route("projectImplementation/{projectId}")]
        public IHttpActionResult GetProjectImplementation(int projectId)
        {
            project_implementations query = _repo.GetProjectImplementation(projectId);
            if (query == null)
                return BadRequest();
            return Ok(new { content = query.content });
        }

        [HttpPost]
        [Route("projectImplementation/{projectId}")]
        [MemberAndSpectatorActionFilter]
        public async Task<IHttpActionResult> PostProjectImplementation(project_implementations model, int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            bool updated = _repo.UpdateProjectImplementation(model, projectId);


            if (updated == true)
                return Ok();

            model.Project_FK = projectId;
            db.project_implementations.Add(model);

            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("projectImplementation/delete/{projectId}")]
        [AdminTypeActionFilter]
        public async Task<IHttpActionResult> DeleteProjectImplementation(int projectId)
        {
            var delete = from des in db.project_implementations
                         where des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_implementations.Remove(delete.FirstOrDefault<project_implementations>());
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("functionalRequirement/{projectId}")]
        public IQueryable GetProjectfunctionalRequirements(int projectId)
        {
            return _repo.GetProjectfunctionalRequirements(projectId);
        }

        [HttpPost]
        [Route("functionalRequirement/{projectId}")]
        [MemberAndSpectatorActionFilter]
        public async Task<IHttpActionResult> PostProjectfunctionalRequirement(project_functional_requirements model, int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Project_FK = projectId;
            db.project_functional_requirements.Add(model);

            await db.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPost]
        [Route("functionalRequirement/delete/{projectId}/{functionalRequirementId}")]
        [AdminTypeActionFilter]
        public async Task<IHttpActionResult> DeleteProjectfunctionalRequirement(int projectId, int functionalRequirementId)
        {
            var delete = from des in db.project_functional_requirements
                         where des.Id == functionalRequirementId && des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_functional_requirements.Remove(delete.FirstOrDefault<project_functional_requirements>());
            await db.SaveChangesAsync();

            return Ok();
        }
    }
}