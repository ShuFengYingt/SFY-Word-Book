using SFY_Word_Book.Api.Context;
using Microsoft.EntityFrameworkCore;
using Arch.EntityFrameworkCore.UnitOfWork;
using SFY_Word_Book.Api.Context.Repository;
using SFY_Word_Book.Api.Service;
using SFY_Word_Book.Api.Serviece;
using SFY_Word_Book.Api.Extension;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SFYWordBookContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("SFYWordBookConnection");
    options.UseSqlite(connectionString);
}).AddUnitOfWork<SFYWordBookContext>()
.AddCustomRepository<UserInfo, UserInfoRepository>();

builder.Services.AddTransient<IUserInfoService, UserService>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddControllers();

#region Ìí¼ÓAutoMapper
var autoMapperProfile = new MapperConfiguration(config =>
{
    config.AddProfile(new AutoMapperProfile());
});
builder.Services.AddSingleton(autoMapperProfile.CreateMapper());
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseAuthorization();

//app.MapControllers();

app.Run();
