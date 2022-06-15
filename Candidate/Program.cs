using Business.Services.Customers;
using Business.Services.Payments;
using Business.Services.Tokens;
using Business.Services.Transactions;
using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Customers;
using DataAccess.Abstract.Transactions;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Categories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
    options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ssZ";

});

builder.Services.AddScoped(typeof(IEntityRepository<>), typeof(EfRepositoryBase<>));
builder.Services.AddTransient<DbContext, CandidateContext>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionDal, EfTransactionDal>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerDal, EfCustomerDal>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

var TokenApiKey = builder.Configuration["TokenApiKey"];
var TokenEmail = builder.Configuration["TokenEmail"];
var TokenLang = builder.Configuration["TokenLang"];

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
