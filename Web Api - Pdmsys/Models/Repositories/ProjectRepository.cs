using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.helpers;

namespace Web_Api___Pdmsys.Models.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private PdmsysContext context;

        private pdmsysEntities db;

        private UserManager<IdentityUser> _userManager;

        public ProjectRepository()
        {
            context = new PdmsysContext();
            db = new pdmsysEntities();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
        }

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

        public IQueryable GetUserProjects()
        {
            IdentityUser user = _userManager.FindByName(ClaimsPrincipal.Current.Claims.ToList().First().Value);
            var query = (from m in db.User_Project_Rel
                        join p in db.Projects on m.Project_FK equals p.Id
                        where m.User_FK == user.Id
                        select new
                        {
                            id = p.Id,
                            name = p.name,
                            description = p.description,
                            acronym = p.acronym
                        }).AsQueryable();
            return query;

        }

        public void RemoveProjectMember(string email)
        {
            throw new NotImplementedException();
        }
    }
}