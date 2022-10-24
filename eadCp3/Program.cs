using eadCp3.Config;
using eadCp3.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var stringConexao = "DATA SOURCE=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SID=orcl)));USER ID=RM88397; Password=220802";

builder.Services.AddDbContext<Context>
    (options => options.UseOracle(stringConexao));

var app = builder.Build();
app.UseSwagger();

app.MapPost("/AdicionarPaciente", async (Paciente paciente, Context context) =>
{
    context.Paciente.Add(paciente);
    await context.SaveChangesAsync();
});

app.MapDelete("/ExcluirPaciente/{id}", async (int id, Context context) =>
{
    var paciente = await context.Paciente.FirstOrDefaultAsync(p => p.Id == id);
    if(paciente != null)
    {
        context.Paciente.Remove(paciente);
        await context.SaveChangesAsync();
    }
    
});

app.MapGet("/ListarPaciente", async (Context context) =>
{
    return await context.Paciente.ToListAsync();
});

app.MapGet("/ObterPaciente/{id}", async (int id,Context context) =>
{
    return await context.Paciente.FirstOrDefaultAsync(p => p.Id == id);
});

app.UseSwaggerUI();
app.Run();
