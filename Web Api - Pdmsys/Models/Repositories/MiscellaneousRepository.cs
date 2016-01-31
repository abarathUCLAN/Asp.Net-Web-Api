using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.Interfaces;

namespace Web_Api___Pdmsys.Models.Repositories
{
    public class MiscellaneousRepository : IMiscellaneousRepository
    {

        pdmsysEntities db = new pdmsysEntities();

        public IQueryable GetProjectchangeRequests(int projectId)
        {
            var query = (from model in db.project_change_requests
                         where model.Project_FK == projectId
                         select new
                         {
                             id = model.Id,
                             name = model.name,
                             content = model.content
                         }).AsQueryable();

            return query;
        }

        public IQueryable GetProjectpresentations(int projectId)
        {
            var query = (from model in db.project_presentations
                         where model.Project_FK == projectId
                         select new
                         {
                             id = model.Id,
                             name = model.name,
                             content = model.content,
                             file = model.file
                         }).AsQueryable();

            return query;
        }

        public IQueryable GetProjectreports(int projectId)
        {
            var query = (from model in db.project_reports
                         where model.Project_FK == projectId
                         select new
                         {
                             id = model.Id,
                             month = model.month,
                             content = model.content
                         }).AsQueryable();

            return query;
        }

        public IQueryable GetProjectstyleguides(int projectId)
        {
            var query = (from model in db.project_style_guides
                         where model.Project_FK == projectId
                         select new
                         {
                             id = model.Id,
                             name = model.name,
                             content = model.content
                         }).AsQueryable();

            return query;
        }
    }
}