using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion_architecture.Domain
{
    public class WorkflowStepTemplate
    {
        public string Name { get; private set; }
        public int Order { get; private set; }
        public Guid UserId { get; private set; }
        public Guid RoleId { get; private set; }

        public WorkflowStepTemplate(string name, int order, Guid userId, Guid roleId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Order = order;
            UserId = userId;
            RoleId = roleId;
        }
    }
}
