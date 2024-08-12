using Gemini_Competition.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"firebasecredientials.json");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IFireStoreService>(s => new FireStoreService("geminicompetition-c65ca"));
builder.Services.AddTransient<ITasksService>(s => new TasksService());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
