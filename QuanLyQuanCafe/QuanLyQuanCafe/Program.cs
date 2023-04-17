using QuanLyQuanCafe.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using QuanLyQuanCafe.Services.CategoryServices;
using QuanLyQuanCafe.Services.CustomerServices;
using QuanLyQuanCafe.Services.ProvideServices;
using QuanLyQuanCafe.Services.TableFoodServices;
using QuanLyQuanCafe.Services.WorkShiftServices;
using QuanLyQuanCafe.Services.StaffServices;
using QuanLyQuanCafe.Services.ProductServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using QuanLyQuanCafe.Services.TokenServices;
using QuanLyQuanCafe.Services.ImportGoodsServices;
using QuanLyQuanCafe.Services.MaterialServices;
using QuanLyQuanCafe.Services.UseMaterialServices;
using QuanLyQuanCafe.Services.OrderServices;
using QuanLyQuanCafe.Services.OrderDetailServices;
using QuanLyQuanCafe.Services.PromotionServices;
using QuanLyQuanCafe.Services.PProductServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options=>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.IgnoreNullValues = true;
    options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//connect database
builder.Services.AddDbContext<CafeContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Cafe")));
builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
}));
//for Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<CafeContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])   
        
    )};
});
/*builder.Services.AddAutoMapper(typeof(Program).Assembly);*/
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICategoryService, CategoryServices>();
builder.Services.AddScoped<ICustomerServices, CustomerServices>();
builder.Services.AddScoped<IProviderService, ProviderServices>();
builder.Services.AddScoped<ITableFoodService, TableFoodServices>();
builder.Services.AddScoped<IWorkShiftService, WorkShiftService>();
builder.Services.AddScoped<IStaffService, StaffServices>();
builder.Services.AddTransient<ITokenService, TokenServices>();
builder.Services.AddScoped<IProductService, ProductServices>();
builder.Services.AddScoped<IImportGoodsService, ImportGoodsServices>();
builder.Services.AddScoped<IMaterialService, MaterialServices>();
builder.Services.AddScoped<IUseMaterialServices, UseMaterialServices>();
builder.Services.AddScoped<IOrderService, OrderServices>();
builder.Services.AddScoped<IOrderDtService, OrderDtServices>();
builder.Services.AddScoped<IPromotionService, PromotionServices>();
builder.Services.AddScoped<IPProductService, PProductServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}
app.UseCors("MyCors");
//truy cập localhost:7066/public/ + folder+ tên ảnh => truy cập ảnh trên server
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
    RequestPath = new PathString("/public")
});
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
