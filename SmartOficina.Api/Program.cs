var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ApiKeyAttribute>();
}).AddJsonOptions(cfg =>
{
    cfg.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    cfg.JsonSerializerOptions.MaxDepth = 0;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfiguration(builder.Configuration);

builder.Services.RegisterContext(builder.Configuration);

builder.Services.RegisterServices(builder.Configuration);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
