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
    [RoutePrefix("api/requirementSpecification")]
    public class RequirementSpecificationController : ApiController
    {
        private pdmsysEntities db = new pdmsysEntities();
        static readonly IRequirementSpecificationRepository _repo = new RequirementSpecificationRepository();

        [HttpGet]
        [Route("projectResult/{projectId}")]
        public IHttpActionResult GetProjectResult(int projectId)
        {
            project_results query = _repo.GetProjectResult(projectId);
            if (query == null)
                return BadRequest();
            return Ok(new { content = query.content });
        }

        [HttpPost]
        [Route("projectResult/{projectId}")]
        public async Task<IHttpActionResult> PostProjectResult(project_results model, int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            bool updated = _repo.UpdateProjectResult(model, projectId);


            if (updated == true)
                return Ok();

            model.Project_FK = projectId;
            db.project_results.Add(model);

            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("projectResult/delete/{projectId}")]
        public async Task<IHttpActionResult> DeleteProjectResult(int projectId)
        {
            var delete = from des in db.project_results
                         where des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_results.Remove(delete.FirstOrDefault<project_results>());
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("projectIntroduction/{projectId}")]
        public IHttpActionResult GetProjectIntroduction(int projectId)
        {
            project_introductions query = _repo.GetProjectIntroduction(projectId);
            if (query == null)
                return BadRequest();
            return Ok(new { content = query.content });
        }

        [HttpPost]
        [Route("projectIntroduction/{projectId}")]
        public async Task<IHttpActionResult> PostProjectIntroduction(project_introductions model, int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            bool updated = _repo.UpdateProjectIntroduction(model, projectId);


            if (updated == true)
                return Ok();

            model.Project_FK = projectId;
            db.project_introductions.Add(model);

            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("projectIntroduction/delete/{projectId}")]
        public async Task<IHttpActionResult> DeleteProjectIntroduction(int projectId)
        {
            var delete = from des in db.project_introductions
                         where des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_introductions.Remove(delete.FirstOrDefault<project_introductions>());
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("projectUse/{projectId}")]
        public IHttpActionResult GetProjectUse(int projectId)
        {
            project_uses query = _repo.GetProjectUse(projectId);
            if (query == null)
                return BadRequest();
            return Ok(new { content = query.content });
        }

        [HttpPost]
        [Route("projectUse/{projectId}")]
        public async Task<IHttpActionResult> PostProjectUse(project_uses model, int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            bool updated = _repo.UpdateProjectUse(model, projectId);


            if (updated == true)
                return Ok();

            model.Project_FK = projectId;
            db.project_uses.Add(model);

            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("projectUse/delete/{projectId}")]
        public async Task<IHttpActionResult> DeleteProjectUse(int projectId)
        {
            var delete = from des in db.project_uses
                         where des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_uses.Remove(delete.FirstOrDefault<project_uses>());
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("productData/{projectId}")]
        public IHttpActionResult GetproductData(int projectId)
        {
            project_data query = _repo.GetProductData(projectId);
            if (query == null)
                return BadRequest();
            return Ok(new { content = query.content });
        }

        [HttpPost]
        [Route("productData/{projectId}")]
        public async Task<IHttpActionResult> PostproductData(project_data model, int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            bool updated = _repo.UpdateProductData(model, projectId);


            if (updated == true)
                return Ok();

            model.Project_FK = projectId;
            db.project_data.Add(model);

            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("productData/delete/{projectId}")]
        public async Task<IHttpActionResult> DeleteproductData(int projectId)
        {
            var delete = from des in db.project_data
                         where des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_data.Remove(delete.FirstOrDefault<project_data>());
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("projectQuality/{projectId}")]
        public IHttpActionResult GetProjectQuality(int projectId)
        {
            project_qualities query = _repo.GetProjectQuality(projectId);
            if (query == null)
                return BadRequest();
            return Ok(new { content = query.content });
        }

        [HttpPost]
        [Route("projectQuality/{projectId}")]
        public async Task<IHttpActionResult> PostProjectQuality(project_qualities model, int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            bool updated = _repo.UpdateProjectQuality(model, projectId);


            if (updated == true)
                return Ok();

            model.Project_FK = projectId;
            db.project_qualities.Add(model);

            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("projectQuality/delete/{projectId}")]
        public async Task<IHttpActionResult> DeleteProjectQuality(int projectId)
        {
            var delete = from des in db.project_qualities
                         where des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_qualities.Remove(delete.FirstOrDefault<project_qualities>());
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("needToHave/{projectId}")]
        public IQueryable GetProjectneedToHaves(int projectId)
        {
            return _repo.GetProjectneedToHaves(projectId);
        }

        [HttpPost]
        [Route("needToHave/{projectId}")]
        public async Task<IHttpActionResult> PostProjectneedToHave(project_need_to_haves model, int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Project_FK = projectId;
            db.project_need_to_haves.Add(model);

            await db.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPost]
        [Route("needToHave/delete/{projectId}/{needToHaveId}")]
        public async Task<IHttpActionResult> DeleteProjectneedToHave(int projectId, int needToHaveId)
        {
            var delete = from des in db.project_need_to_haves
                         where des.Id == needToHaveId && des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_need_to_haves.Remove(delete.FirstOrDefault<project_need_to_haves>());
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("niceToHave/{projectId}")]
        public IQueryable GetProjectniceToHaves(int projectId)
        {
            return _repo.GetProjectniceToHaves(projectId);
        }

        [HttpPost]
        [Route("niceToHave/{projectId}")]
        public async Task<IHttpActionResult> PostProjectniceToHave(project_nice_to_haves model, int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Project_FK = projectId;
            db.project_nice_to_haves.Add(model);

            await db.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPost]
        [Route("niceToHave/delete/{projectId}/{needToHaveId}")]
        public async Task<IHttpActionResult> DeleteProjectniceToHave(int projectId, int needToHaveId)
        {
            var delete = from des in db.project_nice_to_haves
                         where des.Id == needToHaveId && des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_nice_to_haves.Remove(delete.FirstOrDefault<project_nice_to_haves>());
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("actualState/{projectId}")]
        public IHttpActionResult GetactualState(int projectId)
        {
            project_actual_states query = _repo.GetactualState(projectId);
            if (query == null)
                return BadRequest();
            return Ok(new { content = query.content });
        }

        [HttpPost]
        [Route("actualState/{projectId}")]
        public async Task<IHttpActionResult> PostactualState(project_actual_states model, int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            bool updated = _repo.UpdateactualState(model, projectId);


            if (updated == true)
                return Ok();

            model.Project_FK = projectId;
            db.project_actual_states.Add(model);

            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("actualState/delete/{projectId}")]
        public async Task<IHttpActionResult> DeleteactualState(int projectId)
        {
            var delete = from des in db.project_actual_states
                         where des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_actual_states.Remove(delete.FirstOrDefault<project_actual_states>());
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("targetState/{projectId}")]
        public IHttpActionResult GettargetState(int projectId)
        {
            project_target_states query = _repo.GettargetState(projectId);
            if (query == null)
                return BadRequest();
            return Ok(new { content = query.content });
        }

        [HttpPost]
        [Route("targetState/{projectId}")]
        public async Task<IHttpActionResult> PosttargetState(project_target_states model, int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            bool updated = _repo.UpdatetargetState(model, projectId);


            if (updated == true)
                return Ok();

            model.Project_FK = projectId;
            db.project_target_states.Add(model);

            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("targetState/delete/{projectId}")]
        public async Task<IHttpActionResult> DeletetargetState(int projectId)
        {
            var delete = from des in db.project_target_states
                         where des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_target_states.Remove(delete.FirstOrDefault<project_target_states>());
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("nonFunctionalRequirement/{projectId}")]
        public IQueryable GetProjectnonFunctionalRequirements(int projectId)
        {
            return _repo.GetProjectnonFunctionalRequirements(projectId);
        }

        [HttpPost]
        [Route("nonFunctionalRequirement/{projectId}")]
        public async Task<IHttpActionResult> PostProjectnonFunctionalRequirement(project_non_functional_requirements model, int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Project_FK = projectId;
            db.project_non_functional_requirements.Add(model);

            await db.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPost]
        [Route("nonFunctionalRequirement/delete/{projectId}/{nonFunctionalRequirementId}")]
        public async Task<IHttpActionResult> DeleteProjectnonFunctionalRequirement(int projectId, int nonFunctionalRequirementId)
        {
            var delete = from des in db.project_non_functional_requirements
                         where des.Id == nonFunctionalRequirementId && des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_non_functional_requirements.Remove(delete.FirstOrDefault<project_non_functional_requirements>());
            await db.SaveChangesAsync();

            return Ok();
        }

    }
}