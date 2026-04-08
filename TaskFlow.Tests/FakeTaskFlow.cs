using Bogus;
using TaskFlow.Domain;

namespace TaskFlow.Tests
{
    public class FakeTaskFlow
    {
        public TaskItem GenerateFakeTaskItem()
        {
           return new Faker<TaskItem>()
                 .RuleFor(c => c.Title, f => f.Lorem.Sentence())
                 .RuleFor(c => c.Description, f => f.Lorem.Paragraph())
                 .Generate();
        }
    }
}
