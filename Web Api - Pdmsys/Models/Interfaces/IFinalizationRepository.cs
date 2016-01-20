using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Api___Pdmsys.Models.data;

namespace Web_Api___Pdmsys.Models.Interfaces
{
    interface IFinalizationRepository
    {
        IQueryable GetProjectprotocols(int projectId);
        project_manuals GetprojectManual(int projectId);
        bool UpdateprojectManual(project_manuals model, int projectId);
    }
}
