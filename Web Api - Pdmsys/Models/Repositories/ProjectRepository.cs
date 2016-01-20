using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.helpers;

namespace Web_Api___Pdmsys.Models.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private pdmsysEntities db = new pdmsysEntities();

        public void Add(Projects item)
        {
            
            db.Projects.Add(item);
            db.SaveChangesAsync();
        }

        public bool AddMemberToProject(MemberModel member)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MemberModel> GetProjectMembers()
        {
            throw new NotImplementedException();
        }

        public String GetProjectName(int id)
        {
            var query = from p in db.Projects
                        where p.Id == id
                        select new
                        {
                            name = p.name
                        };
            return query.AsQueryable().First().name;
        }

        public IQueryable<Projects> GetUserProjects()
        {
            return db.Projects;
        }

        public void RemoveProjectMember(string email)
        {
            throw new NotImplementedException();
        }
    }
}