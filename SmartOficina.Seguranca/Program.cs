var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerConfiguration(builder.Configuration);

builder.Services.RegisterServices(builder.Configuration);

builder.Services.RegisterContext(builder.Configuration);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using var scope = app.Services.CreateScope();

scope.ServiceProvider.GetRequiredService<IdentityInitializer>().Initialize();

app.UseAuthorization();

app.MapControllers();

app.Run();
