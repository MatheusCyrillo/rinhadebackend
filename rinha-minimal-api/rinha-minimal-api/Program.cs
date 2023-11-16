using rinha_minimal_api.Helpers;
using rinha_minimal_api.Models;
using rinha_minimal_api.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
services.AddCors();
services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
services.AddSingleton<DataContext>();
services.AddScoped<IPessoaRepository, PessoaRepository>();

var app = builder.Build();

//{
//    using var scope = app.Services.CreateScope();
//    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
//    await context.Init();
//}
// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

#region Endpoints

app.MapPost("/pessoas", async (Pessoa pessoa/*, IPessoaRepository pessoaRepository*/) =>
{
    //pessoa.Id = Guid.NewGuid();
    return Results.Created($"/pessoas/{pessoa.Id}", pessoa);

    //var isSuccess = await pessoaRepository.Create(pessoa);

    //if (!isSuccess)
    //    return Results.UnprocessableEntity();

    return Results.Created($"/pessoas/{pessoa.Id}", pessoa);
});
//    .AddEndpointFilter(async (invocationContext, next)
//=>
//{
//    //var pessoa = invocationContext.GetArgument<Pessoa>(0);

//    //if (string.IsNullOrEmpty(pessoa.Apelido)
//    //|| string.IsNullOrEmpty(pessoa.Nome)
//    //|| string.IsNullOrEmpty(pessoa.Nascimento)
//    //|| pessoa.Apelido.Length > 32
//    //|| pessoa.Nome.Length > 1000
//    //|| !DateTime.TryParse(pessoa.Nascimento, out _)
//    //|| (pessoa.Stack != null && pessoa.Stack.Any(s => string.IsNullOrEmpty(s) || s.Length > 32)))
//    //    return Results.BadRequest();

//    return await next(invocationContext);
//});

app.MapGet("/pessoas/{id}", async (Guid id/*, IPessoaRepository pessoaRepository*/) =>
{
    return Results.Ok();

    //var pessoa = await pessoaRepository.Get(id);

    //if (pessoa == null)
    //    return Results.NotFound();

    //return Results.Ok(pessoa);
})
    .WithName("GetPessoaById");

app.MapGet("/pessoas", async (string t/*, IPessoaRepository pessoaRepository*/) =>
{
    return Results.Ok();

    //return Results.Ok(await pessoaRepository.GetPessoasByTermo(t));
});
//    .AddEndpointFilter(async (invocationContext, next)
//=>
//    {
//        var termo = invocationContext.GetArgument<string>(0);

//        if (termo == null)
//            return Results.BadRequest();

//        return await next(invocationContext);
//    });

app.MapGet("/contagem-pessoas", async (/*IPessoaRepository pessoaRepository*/) =>
{
    return Results.Ok(0);

    //return Results.Ok(await pessoaRepository.TotalCount());
});

#endregion


app.Run();
