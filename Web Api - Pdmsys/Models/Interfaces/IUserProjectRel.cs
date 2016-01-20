using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Api___Pdmsys.Models.data;
using Web_Api___Pdmsys.Models.helpers;

namespace Web_Api___Pdmsys.Models.Interfaces
{
    interface IUserProjectRel
    {
        IQueryable GetUsersProjects();
        int GetProjectRightsByProjectId(int projectId);
        IQueryable GetProjectMembersByProjectId(int projectId);
        void RemoveRelByEmail(string email);
    }
}
