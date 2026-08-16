using HR_Application;
using HR_Infrastructure;
using HR__Management_System.Extentions;
using HR__Management_System.EndPoints.DepartmentEndpoints;
using HR__Management_System.EndPoints.Employees;



var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices()
    .AddApiServices(builder.Configuration, builder.Host);

builder.Services.AddControllers();

var app = builder.Build();


app.UseApiMiddelwares();

app.MapDepartmentEndpoints();
app.MapEmployeeEndpoints();

app.Run();