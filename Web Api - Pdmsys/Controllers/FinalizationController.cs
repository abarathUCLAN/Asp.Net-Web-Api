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
    [RoutePrefix("api/finalization")]
    public class FinalizationController : ApiController
    {
        private pdmsysEntities db = new pdmsysEntities();
        static readonly IFinalizationRepository _repo = new FinalizationRepository();

        [HttpGet]
        [Route("protocol/{projectId}")]
        public IQueryable GetProjectprotocols(int projectId)
        {
            return _repo.GetProjectprotocols(projectId);
        }

        [HttpPost]
        [Route("protocol/{projectId}")]
        [MemberAndSpectatorActionFilter]
        public async Task<IHttpActionResult> PostProjectprotocol(project_acceptance_protocols model, int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Project_FK = projectId;
            if (model.fulfilled == null)
                model.fulfilled = "false";

            db.project_acceptance_protocols.Add(model);

            await db.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPost]
        [AdminTypeActionFilter]
        [Route("protocol/delete/{projectId}/{protocolId}")]
        public async Task<IHttpActionResult> DeleteProjectprotocol(int projectId, int protocolId)
        {
            var delete = from des in db.project_acceptance_protocols
                         where des.Id == protocolId && des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_acceptance_protocols.Remove(delete.FirstOrDefault<project_acceptance_protocols>());
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("projectManual/{projectId}")]
        public IHttpActionResult GetprojectManual(int projectId)
        {
            project_manuals query = _repo.GetprojectManual(projectId);
            if (query == null)
                return BadRequest();
            return Ok(new { content = query.content });
        }

        [HttpPost]
        [Route("projectManual/{projectId}")]
        [MemberAndSpectatorActionFilter]
        public async Task<IHttpActionResult> PostprojectManual(project_manuals model, int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            bool updated = _repo.UpdateprojectManual(model, projectId);


            if (updated == true)
                return Ok();

            model.Project_FK = projectId;
            db.project_manuals.Add(model);

            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("projectManual/delete/{projectId}")]
        [AdminTypeActionFilter]
        public async Task<IHttpActionResult> DeleteprojectManual(int projectId)
        {
            var delete = from des in db.project_manuals
                         where des.Project_FK == projectId
                         select des;

            if (delete.Count() == 0)
                return BadRequest();

            db.project_manuals.Remove(delete.FirstOrDefault<project_manuals>());
            await db.SaveChangesAsync();

            return Ok();
        }
    }
}