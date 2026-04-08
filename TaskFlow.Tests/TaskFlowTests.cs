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
        public async void TaskFlow_ListAllTaskItems_NotNull()
        {
            var service = await _service.GetAllAsync();
            Assert.NotNull(service);
        }

        [Fact]
        public async void TaskFlow_ValidateCreationTask_NotNull()
        {
            var taskItem = new FakeTaskFlow().GenerateFakeTaskItem();
            var createTask = await _service.CreateAsync(taskItem);
            var isCreated = await _service.GetByIdAsync(createTask.ID);

            Assert.NotNull(isCreated);
        }

        [Fact]
        public async void TaskFlow_CreateAndRemoveTaskItem_Bool()
        {
            var taskItem = new FakeTaskFlow().GenerateFakeTaskItem();
            var createTask = await _service.CreateAsync(taskItem);
            await _service.RemoveAsync(createTask.ID);
            var isCreated = await _service.GetByIdAsync(createTask.ID);

            Assert.Null(isCreated);
        }

        [Fact]
        public async void TaskFlow_GetTaskItemById_NotNull()
        {
            var allTasks = await _service.GetAllAsync();
            var idRandom = new Random().Next(1, allTasks.Count());
            var getTaskById = await _service.GetByIdAsync(idRandom);

            Assert.NotNull(getTaskById);
        }

        [Fact]
        public async void TaskFlow_SetTaskComplete_True()
        {
            var allTasks = await _service.GetAllAsync();
            var idRandom = new Random().Next(1, allTasks.Count());
            await _service.CompleteTask(idRandom);
            var getTaskById = await _service.GetByIdAsync(idRandom);

            Assert.True(getTaskById.IsCompleted);
        }

        [Fact]
        public async void TaskFlow_TaskItem_ValidateExceptions()
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(new TaskItem("Test Task", "TaskFlowTests")));
            Assert.Equal("Título é requerido.", exception.Exception?.Message);
            Assert.Equal("O Título deve ter pelo menos 3 caracteres.", exception.Exception?.Message);
            Assert.Equal("Descrição é requerido.", exception.Exception?.Message);
            Assert.Equal("A descrição deve ter pelo menos 10 caracteres.", exception.Exception?.Message);
        }
    }
}