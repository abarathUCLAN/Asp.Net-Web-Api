using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.helpers;

namespace Web_Api___Pdmsys.Models.Repositories
{
    public interface IProjectRepository
    {
        IQueryable GetUserProjects();
        String GetProjectName(int id);
        void Add(Projects item);
        bool AddMemberToProject(MemberModel member);
        IQueryable<MemberModel> GetProjectMembers();
        void RemoveProjectMember(String email);

    }
}