var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
        options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfiguration(builder.Configuration);

builder.Services.RegisterServices(builder.Configuration);

builder.Services.RegisterContext(builder.Configuration);

var tokenConfigurations = new TokenConfigurations();

new ConfigureFromConfigurationOptions<TokenConfigurations>(
    builder.Configuration.GetSection("TokenConfigurations"))
        .Configure(tokenConfigurations);

builder.Services.AddJwtSecurity(tokenConfigurations);

builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

using var scope = app.Services.CreateScope();

scope.ServiceProvider.GetRequiredService<IdentityInitializer>().Initialize();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
