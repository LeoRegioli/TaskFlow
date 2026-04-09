using TaskFlow.Application;
using TaskFlow.Domain;

namespace TaskFlow.Tests
{
    public class TaskFlowTests
    {
        private readonly JsonDatabase _jsonDB;
        private readonly TaskService _service;

        public TaskFlowTests()
        {
            _jsonDB = new JsonDatabase();
            _service = new TaskService(_jsonDB);            
        }

        [Fact]
        public async Task TaskFlow_ListAllTaskItems_NotNull()
        {
            var service = await _service.GetAllAsync();
            Assert.NotNull(service);
        }

        [Fact]
        public async Task TaskFlow_ValidateCreationTask_NotNull()
        {
            var taskItem = new FakeTaskFlow().GenerateFakeTaskItem();
            var createTask = await _service.CreateAsync(taskItem);
            var isCreated = await _service.GetByIdAsync(createTask.ID);

            Assert.NotNull(isCreated);
        }

        [Fact]
        public async Task TaskFlow_CreateAndRemoveTaskItem_Bool()
        {
            var taskItem = new FakeTaskFlow().GenerateFakeTaskItem();
            var createTask = await _service.CreateAsync(taskItem);
            await _service.RemoveAsync(createTask.ID);
            var isCreated = await _service.GetByIdAsync(createTask.ID);

            Assert.Null(isCreated);
        }

        [Fact]
        public async Task TaskFlow_GetTaskItemById_NotNull()
        {
            var allTasks = await _service.GetAllAsync();
            var getTaskById = await _service.GetByIdAsync(1);

            Assert.NotNull(getTaskById);
        }

        [Fact]
        public async Task TaskFlow_SetTaskComplete_True()
        {
            await _service.CompleteTask(1);
            var getTaskById = await _service.GetByIdAsync(1);

            Assert.True(getTaskById.IsCompleted);
        }

        [Fact]
        public async Task TaskFlow_TaskItem_ValidateExceptions_RequiredTitle()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(new TaskItem("", "")));
            Assert.Equal("Título é requerido.", exception.Message);
        }

        [Fact]
        public async Task TaskFlow_TaskItem_ValidateExceptions_RequiredTitleLength()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(new TaskItem("Te", "TaskFlowTests")));
            Assert.Equal("O Título deve ter pelo menos 3 caracteres.", exception.Message);
        }

        [Fact]
        public async Task TaskFlow_TaskItem_ValidateExceptions_RequiredDescription()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(new TaskItem("Test Task", "")));
            Assert.Equal("Descrição é requerido.", exception.Message);
        }

        [Fact]
        public async Task TaskFlow_TaskItem_ValidateExceptions_RequiredDescriptionLength()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await _service.CreateAsync(new TaskItem("Test Task", "Test")));
            Assert.Equal("A descrição deve ter pelo menos 10 caracteres.", exception.Message);
        }
    }
}