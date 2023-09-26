using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion_architecture.Domain
{
    public class Workflow
    {
        public Guid WorkflowTemplateId { get; private set; }
        public RoleType Type { get; private set; }
        public List<WorkflowStep> Steps { get; private set; }

        public Workflow(Guid workflowTemplateId, RoleType type, List<WorkflowStep> steps)
        {
            WorkflowTemplateId = workflowTemplateId;
            Type = type;
            Steps = steps ?? throw new ArgumentNullException(nameof(steps));
        }
    }
}
