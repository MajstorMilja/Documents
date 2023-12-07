using DokumentCodeFirst.Data;
using DokumentCodeFirst.IService;
using DokumentCodeFirst.IService.sql;
using DokumentCodeFirst.Repository;
using DokumentCodeFirst.Repository.sql;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IdokumentRepository, DokumentRepository>();
builder.Services.AddScoped<IstavkeRepository, StavkeRepository>();
builder.Services.AddScoped<IproizvodRepository, ProizvodRepository>();
builder.Services.AddScoped<IklijentRepository,KlijentRepository>();
builder.Services.AddScoped<IKlijentService,KlijentService>();
builder.Services.AddScoped<IProizvodService, ProizvodService>();

// Add services to the container.
builder.Services.AddDbContext<SqlContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddMemoryCache();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigins",
    builder =>
    {
        builder.WithOrigins(
                            "http://localhost:4200"
                            )
                            .AllowAnyHeader()
                            .AllowAnyMethod();
    });
});

// UseCors

var app = builder.Build();
app.UseCors("AllowAngularOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SqlContext>();
    var isCreated = context.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
