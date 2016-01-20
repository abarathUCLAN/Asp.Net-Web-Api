using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.Interfaces;
using Web_Api___Pdmsys.Models.helpers;

namespace Web_Api___Pdmsys.Models.Repositories
{
    public class UserProjectRel : IUserProjectRel
    {

        private pdmsysEntities db;

        private UserManager<IdentityUser> _userManager;

        private PdmsysContext context;

        public UserProjectRel()
        {
            context = new PdmsysContext();
            db = new pdmsysEntities();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
        }

        public IQueryable GetUsersProjects()
        {
            IdentityUser user = _userManager.FindByName(ClaimsPrincipal.Current.Claims.ToList().First().Value);
            var query = from rel in db.User_Project_Rel
                        join p in db.Projects on rel.Project_FK equals p.Id
                        where rel.User_FK == user.Id
                        select new
                        {
                            id = p.Id,
                            name = p.name, 
                            description = p.description,
                            acronym = p.acronym
                        };
            return query.AsQueryable();

        }

        public int GetProjectRightsByProjectId(int projectId)
        {
            IdentityUser user = _userManager.FindByName(ClaimsPrincipal.Current.Claims.ToList().First().Value);
            var query = from rel in db.User_Project_Rel
                        where rel.User_FK == user.Id && rel.Project_FK == projectId                        
                        select new
                        {
                            type = rel.type
                        };
            return query.ToList().First().type;
        }

        public IQueryable GetProjectMembersByProjectId(int projectId)
        {
            var query = from rel in db.User_Project_Rel
                        join user in db.AspNetUsers
                            on rel.User_FK equals user.Id
                        join info in db.UserInfos
                            on user.Id equals info.User_FK
                        where rel.Project_FK == projectId
                        select new
                        {
                            id = user.Id,
                            firstname = info.firstname,
                            lastname = info.lastname,
                            email = user.UserName,
                            type = rel.type
                        };
            return query.AsQueryable();
        }

        public void RemoveRelByEmail(string email)
        {
            IdentityUser user = _userManager.FindByName(email);

            var deleteRel = from rel in db.User_Project_Rel
                            where rel.User_FK == user.Id
                            select rel;

            db.User_Project_Rel.Remove(deleteRel.FirstOrDefault<User_Project_Rel>());
            db.SaveChanges();
        }
    }
}