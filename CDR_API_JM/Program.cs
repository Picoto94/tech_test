using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Adicione os serviços ao contêiner.
builder.Services.AddControllers();
// Aprenda mais sobre a configuração do Swagger/OpenAPI em https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure o serviço de aplicação
builder.Services.AddScoped<ICDRService, CDRService>();

// Configure o repositório
builder.Services.AddSingleton<ICDRRepository, CDRRepository>(); // Alteração para Scoped

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var repository = services.GetRequiredService<ICDRRepository>();
}

// Configure o pipeline de solicitação HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();