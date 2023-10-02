using System;
using System.Collections.Generic;
using Onion_architecture.Domain.Entities.Request;
using Onion_architecture.Domain.Entities.User;
using Onion_architecture.Domain.Entities.Workflow;

namespace Onion_architecture.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Создаем объекты для тестирования
                User user = User.Create("Nik", "test@gmail.com", "HR", "Test@123");
                User user2 = User.Create("Nik2", "test2@gmail.com", "Developer", "Test2@123");

                List<WorkflowStepTemplate> steps = CreateDefaultSteps(user.Id, user.RoleId);
                List<WorkflowStepTemplate> steps2 = CreateDefaultSteps(user2.Id, user2.RoleId);

                // Создаем новый шаблон рабочего процесса
                WorkflowTemplate workflowTemplate = new WorkflowTemplate(Guid.NewGuid(), "HR", steps);
                WorkflowTemplate workflowTemplate2 = new WorkflowTemplate(Guid.NewGuid(), "Developer", steps2);

                // Создаем заявку
                Document document = new Document(user.Email);
                Document document2 = new Document(user2.Email);

                Request request = workflowTemplate.Create(user, document);
                Request request2 = workflowTemplate.Create(user2, document2);

                // Проходим по шагам workflow
                for (int i = 0; i < request.Workflow.Steps.Count; i++)
                    request.Approve();
                for (int i = 0; i < 3; i++)
                    request2.Approve();
                request2.Reject();

                // Проверяем статус заявки
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
