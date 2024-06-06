using Microsoft.EntityFrameworkCore;
using loja.data;
using loja.models;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LojaDbContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 26))));

var app = builder.Build();

/**************************    PRODUTO    *****************************************/

/*
Endpoint para criar um novo produto na base de dados
*/
app.MapPost("/createproduto", async (LojaDbContext dbContext, Produto newProduto) =>
{
    dbContext.Produtos.Add(newProduto);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/createproduto/{newProduto.id}", newProduto);
});


/*
Endpoint para consultar todos os produtos da base de dados
*/
app.MapGet("/produtos", async (LojaDbContext dbContext) =>
{
    var produtos = await dbContext.Produtos.ToListAsync();
    return Results.Ok(produtos);
});


/*
Endpoint para consultar um produto específico da base de dados ATRAVÉS DO ID DO PRODUTO
*/
app.MapGet("/produtos/{id}", async (int id, LojaDbContext dbContext) =>
{
    var produto = await dbContext.Produtos.FindAsync(id);
    if (produto == null)
    {
        return Results.NotFound($"Produto com o ID {id} Não encontrado");
    }
    return Results.Ok(produto);
});

/*
Endpoint do tipo PUT, para atualização dos produtos na base de dados
*/
app.MapPut("/produtos/{id}", async (int id, LojaDbContext dbContext, Produto updateProduto) =>
{

    // Verifica se o produto existe na base, corforme o ID informado
    // Se o produto existir na base, será retornado para dentro do objeto existingProduto
    var existingProduto = await dbContext.Produtos.FindAsync(id);
    if (existingProduto == null)
    {
        return Results.NotFound($"Produto com o ID {id} Não encontrado");
    }

    //Atualizar os dados existingProduto
    existingProduto.Nome = updateProduto.Nome;
    existingProduto.Preco = updateProduto.Preco;
    existingProduto.Fornecedor = updateProduto.Fornecedor;


    // Salva no banco de dados

    await dbContext.SaveChangesAsync();


    // Retorna para o CLIENTE que invocou o EndPoint
    return Results.Ok(existingProduto);
});

/**************************    CLIENTE    *****************************************/

/*
EndPoint para criação de novos clientes
*/
app.MapPost("/createcliente", async (LojaDbContext dbContext, Cliente newCliente) =>
{
    dbContext.Clientes.Add(newCliente);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/createcliente/{newCliente.Id}", newCliente);
});

/*
Endpoint para consultar todos os produtos da base de dados
*/
app.MapGet("/clientes", async (LojaDbContext dbContext) =>
{
    var clientes = await dbContext.Clientes.ToListAsync();
    return Results.Ok(clientes);
});


/*
Endpoint para consultar um produto específico da base de dados ATRAVÉS DO ID DO CLIENTE
*/
app.MapGet("/clientes/{id}", async (int id, LojaDbContext dbContext) =>
{
    var cliente = await dbContext.Clientes.FindAsync(id);
    if (cliente == null)
    {
        return Results.NotFound($"Cliente com o ID {id} Não encontrado");
    }

    return Results.Ok(cliente);
});

/**/
app.MapPut("/clientes/{id}", async (int id, LojaDbContext dbContext, Cliente updateCliente) =>
{
    var existingCliente = await dbContext.Clientes.FindAsync(id);
    if (existingCliente == null)
    {
        return Results.NotFound($"Cliente com o ID {id} Não encontrado");
    }
    
    existingCliente.Nome = updateCliente.Nome;
    existingCliente.Cpf = updateCliente.Cpf;
    existingCliente.Email = updateCliente.Email;
    
    await dbContext.SaveChangesAsync();
    
    return Results.Ok(existingCliente);
});

/**************************    FORNECEDOR    *****************************************/

/*
EndPoint para criação de novos clientes
*/
app.MapPost("/createfornecedor", async (LojaDbContext dbContext, Fornecedor newFornecedor) =>
{
    dbContext.Fornecedores.Add(newFornecedor);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/createfornecedor/{newFornecedor.Id}", newFornecedor);
});

/*
Endpoint para consultar todos os produtos da base de dados
*/
app.MapGet("/fornecedores", async (LojaDbContext dbContext) =>
{
    var fornecedores = await dbContext.Fornecedores.ToListAsync();
    return Results.Ok(fornecedores);
});


/*
Endpoint para consultar um produto específico da base de dados ATRAVÉS DO ID DO CLIENTE
*/
app.MapGet("/fornecedores/{id}", async (int id, LojaDbContext dbContext) =>
{
    var fornecedor = await dbContext.Fornecedores.FindAsync(id);
    if (fornecedor == null)
    {
        return Results.NotFound($"Fornecedor com o ID {id} Não encontrado");
    }

    return Results.Ok(fornecedor);
});

/**/
app.MapPut("/fornecedores/{id}", async (int id, LojaDbContext dbContext, Fornecedor updateFornecedor) =>
{
    var existingFornecedor = await dbContext.Fornecedores.FindAsync(id);
    if (existingFornecedor == null)
    {
        return Results.NotFound($"Fornecedor com o ID {id} Não encontrado");
    }
    
    existingFornecedor.Nome = updateFornecedor.Nome;
    existingFornecedor.endereco = updateFornecedor.endereco;
    existingFornecedor.email = updateFornecedor.email;
    existingFornecedor.telefone = updateFornecedor.telefone;
    existingFornecedor.cnpj = updateFornecedor.cnpj;
    
    await dbContext.SaveChangesAsync();
    
    return Results.Ok(existingFornecedor);
});

app.MapGet("/", () => "Hello World!");

app.Run();
