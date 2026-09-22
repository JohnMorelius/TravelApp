 using Configuration.Options;
using Configuration.Extensions;
using TravelApp.Models.Extensions;
using DbRepos;
using Services;

var builder = WebApplication.CreateBuilder(args);

// NOTE: global cors policy needed for JS and React frontends
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddEndpointsApiExplorer();

#region Configuration bootstrap
// User Secrets are loaded via the Configuration assembly, not MyWebApi.
// This is required so that EF Core migrations (which load the assembly at
// design time) can find the same secrets as the running application.
var currentDir = Directory.GetCurrentDirectory();
var assembly = System.Reflection.Assembly.Load("Configuration");
builder.Configuration
    .SetBasePath(Path.Combine(currentDir, "../MyWebApi"))
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddUserSecrets(assembly);

builder.Services.Configure<AesEncryptionOptions>(
    options => builder.Configuration.GetSection(AesEncryptionOptions.Position).Bind(options));

builder.Services.Configure<JwtOptions>(
    options => builder.Configuration.GetSection(JwtOptions.Position).Bind(options));

builder.Services.Configure<DbConnectionSetsOptions>(
    options => builder.Configuration.GetSection(DbConnectionSetsOptions.Position).Bind(options));


// VersionOptions is populated from the assembly, not from a config section
builder.Services.Configure<VersionOptions>(options => VersionOptions.ReadFromAssembly(options));
#endregion

 
builder.Services.AddDatabaseConnections(builder.Configuration);
builder.Services.AddTravelAppDbContext();

builder.Services.AddScoped<SeedDbRepos>();
builder.Services.AddScoped<ISeedService, SeedServiceDb>();

builder.Services.AddScoped<SevardhetDbRepos>();
builder.Services.AddScoped<ISevardhetService, SevardhetServiceDb>();

builder.Services.AddScoped<InfoDbRepos>();
builder.Services.AddScoped<IInfoService, InfoServiceDb>();

builder.Services.AddScoped<AnvandareDbRepos>();
builder.Services.AddScoped<IAnvandareService, AnvandareServiceDb>();

builder.Services.Configure<MySecretOptions>(
    options => builder.Configuration.GetSection(MySecretOptions.Position).Bind(options));

builder.Services.Configure<MySecretOptions>(
    options => builder.Configuration.GetSection(MySecretOptions.Position).Bind(options));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "TravelApp API",
#if DEBUG  
        Version = "v1.0 DEBUG",
#else
        Version = "v1.0",
#endif
        Description = "This is the WebApi for the TravelApp training project."
            + $"<br>DataSet: {builder.Configuration["DatabaseConnections:UseDataSetWithTag"]}"
            + $"<br>DefaultDataUser: {builder.Configuration["DatabaseConnections:DefaultDataUser"]}"
    });
});

var app = builder.Build();

// Swagger enabled unconditionally for this training project
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TravelApp API v1.0");
});

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();
app.MapControllers();

app.Run();