using TaskFlow.Application;
using TaskFlow.Domain;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<IJsonDatabase, JsonDatabase>();

var app = builder.Build();

app.MapGet("/taskItem", async (TaskService tService) => await tService.GetAllAsync());
app.MapGet("/taskItem/{id}", async (int id, TaskService tService) => await tService.GetByIdAsync(id));
app.MapPut("/taskItem/{id}", async (int id, TaskService tService) => await tService.CompleteTask(id));
app.MapPost("/taskItem", async (TaskItem taskItem, TaskService tService) => await tService.CreateAsync(taskItem));
//app.MapDelete("/taskItem/{id}", async(int id, TaskService tService) => await tService.RemoveAsync(id));

app.Run();