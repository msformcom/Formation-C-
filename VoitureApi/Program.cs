// Cr�ation du server web
using Microsoft.EntityFrameworkCore;
using Persistence.BDD.DAO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ConcessionContext>(builder =>
{
    // cette fonction me permet d'utiliser un builder pour specifier les options
    // UseSqlServer => Fonction extension qui
    // param�tre les options avec un provider pour SQLServer
    // Avec une chaine de connection pr�sente dans la config
    // avec le nom ConcessionConnectionString

    // builder.UseInMemoryDatabase(); pour utiliser le provider InMemory (� installer)
    // Parfait pour les tests

    builder.UseSqlServer("name=ConcessionConnectionString");
});


var app = builder.Build();

// Configure the HTTP request pipeline.


app.MapControllers();

app.Run();
