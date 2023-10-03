using System;
using System.Collections.Generic;
using Onion_architecture.Domain.BaseObjectsNamespace;
using Onion_architecture.Domain.Entities.Requests;
using Onion_architecture.Domain.Entities.Users;
using Onion_architecture.Domain.Entities.Workflows;

namespace Onion_architecture.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Role hr = new Role("HR");
                Role dev = new Role("Developer");

                Email email1 = new Email("test@gmail.com");
                Email email2 = new Email("test2@gmail.com");

                Password password1 = new Password("Test@123");
                Password password2 = new Password("Test2@123");

                User user = User.Create("Nik", email1, hr, password1);
                User user2 = User.Create("Nik2", email2, dev, password2);

                List<WorkflowStepTemplate> steps = CreateDefaultSteps(user.Id, user.RoleId);
                List<WorkflowStepTemplate> steps2 = CreateDefaultSteps(user2.Id, user2.RoleId);

                WorkflowTemplate workflowTemplate = new WorkflowTemplate(Guid.NewGuid(), "HR", steps);
                WorkflowTemplate workflowTemplate2 = new WorkflowTemplate(Guid.NewGuid(), "Developer", steps2);

                Document document = new Document(user.Email);
                Document document2 = new Document(user2.Email);
                
                string comment1 = "Comment1", comment2 = "Comment2";
                Request request = workflowTemplate.Create(user, document, comment1);
                Request request2 = workflowTemplate.Create(user2, document2, comment2);

                for (int i = 0; i < request.Workflow.Steps.Count; i++)
                    request.Approve();
                for (int i = 0; i < request2.Workflow.Steps.Count; i++)
                    request2.Approve();

                if (request2.IsApproved())
                    Console.WriteLine("Approved");
                else if (request2.IsRejected())
                    Console.WriteLine("Rejected");
                else
                    Console.WriteLine("Is pending");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            Console.ReadKey();
        }

        private static List<WorkflowStepTemplate> CreateDefaultSteps(Guid userId, Guid roleGuid)
        {
            return new List<WorkflowStepTemplate> {
                        new WorkflowStepTemplate("Online Interview", 1, userId, roleGuid),
                        new WorkflowStepTemplate("Interview with HR", 2, userId, roleGuid),
                        new WorkflowStepTemplate("Technical Task", 3, userId, roleGuid),
                        new WorkflowStepTemplate("Meeting with CEO", 4, userId, roleGuid), };
        }
    }
}
