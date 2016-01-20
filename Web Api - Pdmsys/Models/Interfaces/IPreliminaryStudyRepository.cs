using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Api___Pdmsys.Models.data;

namespace Web_Api___Pdmsys.Models.Interfaces
{
    public interface IPreliminaryStudyRepository
    {
        project_descriptions GetProjectDescription(int projectId);
        bool UpdateProjectDescription(project_descriptions model, int projectId);
        IQueryable GetProjectRisks(int projectId);
        bool UpdateProjectEffortEstimation(string json, int projectId);
        project_effort_estimations getEffortEstimation(int projectId);
    }
}