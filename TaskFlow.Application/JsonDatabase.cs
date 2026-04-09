using System.Text.Json;
using TaskFlow.Domain;

namespace TaskFlow.Application
{
    public class JsonDatabase : IJsonDatabase
    {
        private readonly string FilePath = "database.json";
        private readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true
        };

        public async Task<IEnumerable<TaskItem>> ReadAsync()
        {
            if (!File.Exists(FilePath))
                return new List<TaskItem>();

            var json = await File.ReadAllTextAsync(FilePath);
            if (string.IsNullOrEmpty(json)) return new List<TaskItem>();
            return JsonSerializer.Deserialize<IEnumerable<TaskItem>>(json, Options) ?? new List<TaskItem>();
        }

        public async Task<TaskItem> CreateAsync(TaskItem taskItem)
        {
            var allTasks = await ReadAsync();
            var allTasksList = allTasks.ToList();

            taskItem.ID = (allTasksList.Count > 0) ? (allTasksList.LastOrDefault().ID + 1) : 1;
            allTasksList.Add(taskItem);

            var json = JsonSerializer.Serialize(allTasksList, Options);
            await File.WriteAllTextAsync(FilePath, json);
            return taskItem;
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            var allTasks = await ReadAsync();
            return allTasks.Where(t => t.ID == id).FirstOrDefault();
        }

        public async Task RemoveAsync(int id)
        {
            var allTasks = await ReadAsync();

            var hasTask = allTasks.Where(t => t.ID == id);
            if (!hasTask.Any()) throw new Exception("Task não encontrado");

            var tasks = allTasks.Where(t => t.ID != id).ToList();
            var json = JsonSerializer.Serialize(tasks, Options);
            await File.WriteAllTextAsync(FilePath, json);
        }

        public async Task CompleteTask(int id)
        {
            var allTasks = await ReadAsync();
            var hasTask = allTasks.Where(t => t.ID == id);
            if (hasTask.Any() && hasTask.First().IsCompleted == true) return;

            hasTask.First().IsCompleted = true;
            var allTasksUpdated = allTasks.ToList();
            var json = JsonSerializer.Serialize(allTasksUpdated, Options);
            await File.WriteAllTextAsync(FilePath, json);            
        }
    }
}
