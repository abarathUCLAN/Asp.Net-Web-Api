using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Api___Pdmsys.Models.data;

namespace Web_Api___Pdmsys.Models.Interfaces
{
    public interface IFunctionalSpecificationRepository
    {
        project_implementations GetProjectImplementation(int projectId);
        bool UpdateProjectImplementation(project_implementations model, int projectId);
        IQueryable GetProjectfunctionalRequirements(int projectId);
    }
}