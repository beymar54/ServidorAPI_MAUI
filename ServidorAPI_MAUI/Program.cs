using ServidorAPI_MAUI.Contenido;
using Microsoft.EntityFrameworkCore;
using ServidorAPI_MAUI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(
    op => op.UseSqlite(builder.Configuration.GetConnectionString("MiConexionLocalSQLite"))
    );
var app = builder.Build();
app.MapGet("api/plato", async (AppDbContext contexto) => {
    var elementos = await contexto.Platos.ToListAsync();
    return Results.Ok(elementos);
});
app.MapPost("api/plato", async (AppDbContext contexto, Plato plato) => {
    var elementos = await contexto.Platos.AddAsync(plato);
    await contexto.SaveChangesAsync();
    return Results.Created($"api/plato/{plato.Id}", plato);
});
app.MapPut("api/plato/{id}", async (AppDbContext contexto, int id, Plato plato) => {
    var platoLocal = await contexto.Platos.FirstOrDefaultAsync(pl => pl.Id == id);
    if (platoLocal == null)
        return Results.NotFound();
    platoLocal.Nombre = plato.Nombre;
    platoLocal.Costo = plato.Costo;
    platoLocal.Ingredientes = plato.Ingredientes;
    await contexto.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("api/plato/{id}", async (AppDbContext contexto, int id) => {
    var platoLocal = await contexto.Platos.FirstOrDefaultAsync(pl => pl.Id == id);
    if (platoLocal == null)
        return Results.NotFound();
    contexto.Platos.Remove(platoLocal);
    await contexto.SaveChangesAsync();
    return Results.NoContent();
});
app.Run();