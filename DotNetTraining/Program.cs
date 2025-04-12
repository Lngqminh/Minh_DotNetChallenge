using System.Reflection;
using Application;
using Common.Application.Settings;
using Common.Loggers.Interfaces;
using Common.Loggers.SeriLog;
using DotNetTraining.AutoMappers;
using DotNetTraining.Repositories;
using DotNetTraining.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 1L * 1024 * 1024 * 1024; // Set limit to 1GB
});
// đăng ký AutoMapper
builder.Services.AddAutoMapper(typeof(Program));    
var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
var application = new Startup(builder, xmlPath, Assembly.GetExecutingAssembly());
application.Start();
