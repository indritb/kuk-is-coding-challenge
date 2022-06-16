using NotesApp.Lib.UnitOfWork.Configuration;
using NotesApp.WebAPI.Configuration;
using NotesApp.WebAPI.Implementation.Configuration;

var builder = WebApplication.CreateBuilder(args);

//Initialize the WebHost
builder.WebHost.InitializeApp();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.SetupUnitOfWork();
builder.Services.SetupNotesDependencyInjection(builder.Configuration);
builder.Services.AddHealthChecks();
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
app.UseHealthChecks("/health");
app.Run();