using TaskFlow.Domain;

namespace TaskFlow.Application
{
    public interface IJsonDatabase
    {
        Task<IEnumerable<TaskItem>> ReadAsync();
        Task<TaskItem> CreateAsync(TaskItem taskItem);
        Task<TaskItem> GetTaskByIdAsync(int id);
        Task RemoveAsync(int id);
        Task CompleteTask(int id);
    }
}
