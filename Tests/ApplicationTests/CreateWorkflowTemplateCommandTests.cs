using Application.WorkflowTemplates.Commands;
using Domain.Entities.WorkflowTemplates;
using NUnit.Framework;

namespace ApplicationTests;

[TestFixture]
public class CreateWorkflowTemplateCommandTests
{
    [Test]
    public void Constructor_ThrowsArgumentNullException_WhenNameIsNullTest()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CreateWorkflowTemplateCommand(null, new WorkflowStepTemplate[0]));
    }

    [Test]
    public void Constructor_ThrowsArgumentNullException_WhenStepsIsNullTest()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CreateWorkflowTemplateCommand("TestWorkflow", null));
    }

    [Test]
    public void Constructor_SetsProperties_WhenArgumentsAreValidTest()
    {
        // Arrange
        string name = "TestWorkflow";
        WorkflowStepTemplate[] steps = new WorkflowStepTemplate[2]
        {
            new WorkflowStepTemplate("Step1", 1, Guid.NewGuid(), Guid.NewGuid()),
            new WorkflowStepTemplate("Step2", 2, Guid.NewGuid(), Guid.NewGuid())
        };

        // Act
        var command = new CreateWorkflowTemplateCommand(name, steps);

        // Assert
        Assert.That(command.Name, Is.EqualTo(name));
        Assert.That(command.Steps, Is.EqualTo(steps));
    }
}