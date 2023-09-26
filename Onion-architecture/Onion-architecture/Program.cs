using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Onion_architecture.Domain;

namespace Onion_architecture.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                EventStore eventStore = new EventStore();

                // Создаем объекты для тестирования
                User user = CreateUser("Nik", "test@gmail.com", RoleType.HR, "Test@123");
                User user2 = CreateUser("Nik2", "test2@gmail.com", RoleType.Developer, "Test2@123");

                List<WorkflowStepTemplate> steps = CreateDefaultSteps(user.Id, user.Role.Id);
                List<WorkflowStepTemplate> steps2 = CreateDefaultSteps(user2.Id, user2.Role.Id);

                // Создаем новый шаблон рабочего процесса
                WorkflowTemplate workflowTemplate = new WorkflowTemplate(Guid.NewGuid(), user.Role.Type, steps);
                WorkflowTemplate workflowTemplate2 = new WorkflowTemplate(Guid.NewGuid(), user2.Role.Type, steps2);

                // Создаем заявку
                Document document = new Document(user.Email);
                Document document2 = new Document(user2.Email);

                Request request = workflowTemplate.Create(user, document, eventStore);
                Request request2 = workflowTemplate.Create(user2, document2, eventStore);

                // Проходим по шагам workflow
                for (int i = 1; i <= request.Workflow.Steps.Count; i++)
                    request.Approve(user, i);
                for (int i = 1; i <= 3; i++)
                    request2.Approve(user2, i);

                // Проверяем статус заявки
                if (request.IsApproved())
                    Console.WriteLine("Approved");
                else if (request.IsRejected())
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

        private static User CreateUser(string name, string email, RoleType roleType, string password)
        {
            Email userEmail = new Email(email);
            Role role = new Role(roleType);
            Password userPassword = new Password(password);
            return new User(Guid.NewGuid(), name, userEmail, role.Id, role, userPassword);
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
