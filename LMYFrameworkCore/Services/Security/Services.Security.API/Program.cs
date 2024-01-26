using Framework.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Security.Core.Interfaces;
using Services.Security.Core.Services;
using Services.Security.Infrastructure;
using Services.Security.Infrastructure.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
builder.Services.AddDbContext<EFDBContext>(
              options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
          );

builder.Services.AddTransient<IGenericRepository<Users>, GenericRepository<Users, EFDBContext>>();
builder.Services.AddTransient<IGenericRepository<UserRoles>, GenericRepository<UserRoles, EFDBContext>>();
builder.Services.AddTransient<IGenericRepository<Roles>, GenericRepository<Roles, EFDBContext>>();
builder.Services.AddTransient<IGenericUnitOfwork<EFDBContext>, GenericUnitOfWork<EFDBContext>>();

builder.Services.AddTransient<IUserService, UserService>();

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
