using Microsoft.EntityFrameworkCore;
using UrlShortener.Api.Data;


var builder = WebApplication.CreateBuilder(args);
 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IShortLinkHandler, CreateShortLinkHandler>();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    var cs = builder.Configuration.GetConnectionString("AppDb")
             ?? throw new InvalidOperationException("Missing ConnectionStrings:AppDb");
    opt.UseNpgsql(cs);
});
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
