using System.Text.Json.Serialization;

namespace TaskFlow.Domain
{
    public class TaskItem
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        
        [JsonPropertyName("description")]        
        public string? Description { get; set; }
        
        [JsonPropertyName("isCompleted")]
        public bool IsCompleted { get; set; }

        public TaskItem() { }
        public TaskItem(string title, string description)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("Título é requerido.");

            if (title.Trim().Length < 3)
                throw new ArgumentException("O Título deve ter pelo menos 3 caracteres.");

            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Descrição é requerido.");

            if (description.Trim().Length < 10)
                throw new ArgumentException("A descrição deve ter pelo menos 10 caracteres.");

            Title = title.Trim();
            Description = description.Trim();
        }

        public void Complete()
        {
            if (IsCompleted)
                throw new InvalidOperationException("Task já se encontra completado.");

            IsCompleted = true;
        }
    }
}
