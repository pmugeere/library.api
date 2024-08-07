using Library.Demo.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerDocs();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
