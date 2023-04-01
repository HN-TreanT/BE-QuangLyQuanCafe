using QuanLyQuanCafe.Models;
using Microsoft.EntityFrameworkCore;
using QuanLyQuanCafe.Services.CategoryServices;
using QuanLyQuanCafe.Services.CustomerServices;
using QuanLyQuanCafe.Services.ProvideServices;
using QuanLyQuanCafe.Services.TableFoodServices;
using QuanLyQuanCafe.Services.WorkShiftServices;
using QuanLyQuanCafe.Services.StaffServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//connect database
builder.Services.AddDbContext<CafeContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Cafe")));
builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    // build.WithOrigins("http://localhost:3000");
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
/*builder.Services.AddAutoMapper(typeof(Program).Assembly);*/
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICategoryService, CategoryServices>();
builder.Services.AddScoped<ICustomerServices, CustomerServices>();
builder.Services.AddScoped<IProviderService, ProviderServices>();
builder.Services.AddScoped<ITableFoodService, TableFoodServices>();
builder.Services.AddScoped<IWorkShiftService, WorkShiftService>();
builder.Services.AddScoped<IStaffService, StaffServices>();
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
