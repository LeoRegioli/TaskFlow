using TaskFlow.Domain;

namespace TaskFlow.Application
{
    public class TaskService(IJsonDatabase _jsonDatabase)
    {
        public async Task<TaskItem> CreateAsync(TaskItem task) => await _jsonDatabase.CreateAsync(task);

        public async Task<IEnumerable<TaskItem>> GetAllAsync() => await _jsonDatabase.ReadAsync();

        public async Task<TaskItem> GetByIdAsync(int id) => await _jsonDatabase.GetTaskByIdAsync(id);

        public async Task RemoveAsync(int id) => await _jsonDatabase.RemoveAsync(id);

        public async Task CompleteTask(int id) => await _jsonDatabase.CompleteTask(id);
    }
}