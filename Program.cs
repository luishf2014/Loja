/* PRIMEIRO CÓDIGO */
/* SEGUNDO CÓDIGO */
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using loja.data;
using loja.models;
using loja.services;
using Microsoft.AspNetCore.Authentication;
/* TERCEIRO CÓDIGO */
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);

/* PRIMEIRO CÓDIGO */
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LojaDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 26)))
);

/* SEGUNDO CÓDIGO */
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<FornecedorService>();
builder.Services.AddScoped<VendaService>();

/* Prova */
builder.Services.AddScoped<ServicoService>();
builder.Services.AddScoped<ContratoService>();

/* TERCEIRO CÓDIGO */
builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("abc"))
        };
    });

var app = builder.Build();

/* SEGUNDO CÓDIGO */
// Configurar as requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

/* PRIMEIRO CÓDIGO */
/**************************    PRODUTO    *****************************************/

/*
Endpoint para criar um novo produto na base de dados
*/
// app.MapPost("/createproduto", async (LojaDbContext dbContext, Produto newProduto) =>
// {
//     dbContext.Produtos.Add(newProduto);
//     await dbContext.SaveChangesAsync();
//     return Results.Created($"/createproduto/{newProduto.id}", newProduto);
// });


// /*
// Endpoint para consultar todos os produtos da base de dados
// */
// app.MapGet("/produtos", async (LojaDbContext dbContext) =>
// {
//     var produtos = await dbContext.Produtos.ToListAsync();
//     return Results.Ok(produtos);
// });


// /*
// Endpoint para consultar um produto específico da base de dados ATRAVÉS DO ID DO PRODUTO
// */
// app.MapGet("/produtos/{id}", async (int id, LojaDbContext dbContext) =>
// {
//     var produto = await dbContext.Produtos.FindAsync(id);
//     if (produto == null)
//     {
//         return Results.NotFound($"Produto com o ID {id} Não encontrado");
//     }
//     return Results.Ok(produto);
// });

// /*
// Endpoint do tipo PUT, para atualização dos produtos na base de dados
// */
// app.MapPut("/produtos/{id}", async (int id, LojaDbContext dbContext, Produto updateProduto) =>
// {

//     // Verifica se o produto existe na base, corforme o ID informado
//     // Se o produto existir na base, será retornado para dentro do objeto existingProduto
//     var existingProduto = await dbContext.Produtos.FindAsync(id);
//     if (existingProduto == null)
//     {
//         return Results.NotFound($"Produto com o ID {id} Não encontrado");
//     }

//     //Atualizar os dados existingProduto
//     existingProduto.Nome = updateProduto.Nome;
//     existingProduto.Preco = updateProduto.Preco;
//     existingProduto.Fornecedor = updateProduto.Fornecedor;


//     // Salva no banco de dados

//     await dbContext.SaveChangesAsync();


//     // Retorna para o CLIENTE que invocou o EndPoint
//     return Results.Ok(existingProduto);
// });

// /**************************    CLIENTE    *****************************************/

// /*
// EndPoint para criação de novos clientes
// */
// app.MapPost("/createcliente", async (LojaDbContext dbContext, Cliente newCliente) =>
// {
//     dbContext.Clientes.Add(newCliente);
//     await dbContext.SaveChangesAsync();
//     return Results.Created($"/createcliente/{newCliente.Id}", newCliente);
// });

// /*
// Endpoint para consultar todos os CLIENTES da base de dados
// */
// app.MapGet("/clientes", async (LojaDbContext dbContext) =>
// {
//     var clientes = await dbContext.Clientes.ToListAsync();
//     return Results.Ok(clientes);
// });


// /*
// Endpoint para consultar um CLIENTE específico da base de dados ATRAVÉS DO ID DO CLIENTE
// */
// app.MapGet("/clientes/{id}", async (int id, LojaDbContext dbContext) =>
// {
//     var cliente = await dbContext.Clientes.FindAsync(id);
//     if (cliente == null)
//     {
//         return Results.NotFound($"Cliente com o ID {id} Não encontrado");
//     }

//     return Results.Ok(cliente);
// });


// /*
// Endpoint do tipo PUT, para atualização dos CLIENTES na base de dados
// */
// app.MapPut("/clientes/{id}", async (int id, LojaDbContext dbContext, Cliente updateCliente) =>
// {
//     var existingCliente = await dbContext.Clientes.FindAsync(id);
//     if (existingCliente == null)
//     {
//         return Results.NotFound($"Cliente com o ID {id} Não encontrado");
//     }

//     existingCliente.Nome = updateCliente.Nome;
//     existingCliente.Cpf = updateCliente.Cpf;
//     existingCliente.Email = updateCliente.Email;

//     await dbContext.SaveChangesAsync();

//     return Results.Ok(existingCliente);
// });

// /**************************    FORNECEDOR    *****************************************/

// /*
// EndPoint para criação de novos FORNECEDOR
// */
// app.MapPost("/createfornecedor", async (LojaDbContext dbContext, Fornecedor newFornecedor) =>
// {
//     dbContext.Fornecedores.Add(newFornecedor);
//     await dbContext.SaveChangesAsync();
//     return Results.Created($"/createfornecedor/{newFornecedor.Id}", newFornecedor);
// });

// /*
// Endpoint para consultar todos os FORNECEDORES da base de dados
// */
// app.MapGet("/fornecedores", async (LojaDbContext dbContext) =>
// {
//     var fornecedores = await dbContext.Fornecedores.ToListAsync();
//     return Results.Ok(fornecedores);
// });


// /*
// Endpoint para consultar um FORNECEDOR específico da base de dados ATRAVÉS DO ID DO FORNECEDOR
// */
// app.MapGet("/fornecedores/{id}", async (int id, LojaDbContext dbContext) =>
// {
//     var fornecedor = await dbContext.Fornecedores.FindAsync(id);
//     if (fornecedor == null)
//     {
//         return Results.NotFound($"Fornecedor com o ID {id} Não encontrado");
//     }

//     return Results.Ok(fornecedor);
// });


// app.MapPut("/fornecedores/{id}", async (int id, LojaDbContext dbContext, Fornecedor updateFornecedor) =>
// {
//     var existingFornecedor = await dbContext.Fornecedores.FindAsync(id);
//     if (existingFornecedor == null)
//     {
//         return Results.NotFound($"Fornecedor com o ID {id} Não encontrado");
//     }

//     existingFornecedor.Nome = updateFornecedor.Nome;
//     existingFornecedor.endereco = updateFornecedor.endereco;
//     existingFornecedor.email = updateFornecedor.email;
//     existingFornecedor.telefone = updateFornecedor.telefone;
//     existingFornecedor.cnpj = updateFornecedor.cnpj;

//     await dbContext.SaveChangesAsync();

//     return Results.Ok(existingFornecedor);
// });



// Add services to the container.
/**************************    PRODUTO    *****************************************/
/* Consultar os Produtos */
app.MapGet(
    "/produtos",
    async (ProductService productService) =>
    {
        var produtos = await productService.GetAllProdutosAsync();
        return Results.Ok(produtos);
    }
);

/* Consultar o produto atravez do id */
app.MapGet(
    "/produtos/{id}",
    async (int id, ProductService productService) =>
    {
        var produto = await productService.GetProdutoAsync(id);
        if (produto == null)
        {
            return Results.NotFound($"Produto com o ID {id} não encontrado");
        }
        return Results.Ok(produto);
    }
);

/* Criar o produto */
app.MapPost(
    "/createproduto",
    async (Produto produto, ProductService productService) =>
    {
        await productService.AddProductAsync(produto);
        return Results.Created($"/produtos/{produto.id}", produto);
    }
);

/* Atualizar o produto */
app.MapPut(
    "/produtos/{id}",
    async (int id, Produto produto, ProductService productService) =>
    {
        if (id != produto.id)
        {
            return Results.BadRequest("Produto não existe");
        }
        await productService.UpdateProductAsync(produto);
        return Results.Ok();
    }
);

/* Deletar Produto */
app.MapDelete(
    "/deleteprodutos/{id}",
    async (int id, ProductService productService) =>
    {
        await productService.DeleteProductAsync(id);
        return Results.Ok();
    }
);

/**************************    CLIENTE    *****************************************/
/* Consultar os Clientes */
app.MapGet(
    "/clientes",
    async (ClienteService clientService) =>
    {
        var clientes = await clientService.GetAllClientesAsync();
        return Results.Ok(clientes);
    }
);

/* Consultar o cliente atravez do id */
app.MapGet(
    "/clientes/{id}",
    async (int id, ClienteService clienteService) =>
    {
        var cliente = await clienteService.GetClienteAsync(id);
        if (cliente == null)
        {
            return Results.NotFound($"Cliente com o ID {id} não encontrado");
        }
        return Results.Ok(cliente);
    }
);

/* Criar o cliente */
app.MapPost(
    "/createcliente",
    async (Cliente cliente, ClienteService clienteService) =>
    {
        await clienteService.AddClienteAsync(cliente);
        return Results.Created($"/cliente/{cliente.Id}", cliente);
    }
);

/* Atualizar o cliente */
app.MapPut(
    "/clientes/{id}",
    async (int id, Cliente cliente, ClienteService clienteService) =>
    {
        if (id != cliente.Id)
        {
            return Results.BadRequest("Cliente não existe");
        }
        await clienteService.UpdateClienteAsync(cliente);
        return Results.Ok();
    }
);

/* Deletar Cliente */
app.MapDelete(
    "/deletecliente/{id}",
    async (int id, ClienteService clienteService) =>
    {
        await clienteService.DeleteClienteAsync(id);
        return Results.Ok();
    }
);

/**************************    FORNECEDORES    *****************************************/

/* Consultar os Fornecedores */
app.MapGet(
    "/fornecedor",
    async (FornecedorService fornecedorService) =>
    {
        var fornecedores = await fornecedorService.GetAllFornecedorAsync();
        return Results.Ok(fornecedores);
    }
);

/* Consultar o fornecedor atravez do id */
app.MapGet(
    "/fornecedores/{id}",
    async (int id, FornecedorService fornecedorService) =>
    {
        var fornecedor = await fornecedorService.GetFornecedorAsync(id);
        if (fornecedor == null)
        {
            return Results.NotFound($"fornecedor com o ID {id} não encontrado");
        }
        return Results.Ok(fornecedor);
    }
);

/* Criar o fornecedor */
app.MapPost(
    "/createfornecedor",
    async (Fornecedor fornecedor, FornecedorService fornecedorService) =>
    {
        await fornecedorService.AddFornecedoresAsync(fornecedor);
        return Results.Created($"/fornecedor/{fornecedor.Id}", fornecedor);
    }
);

/* Atualizar o fornecedor */
app.MapPut(
    "/fornecedores/{id}",
    async (int id, Fornecedor fornecedor, FornecedorService fornecedorService) =>
    {
        if (id != fornecedor.Id)
        {
            return Results.BadRequest("Fornecedor não existe");
        }
        await fornecedorService.UpdateFornecedorAsync(fornecedor);
        return Results.Ok();
    }
);

/* Deletar Fornecedor */
app.MapDelete(
    "/deletefornecedor/{id}",
    async (int id, FornecedorService fornecedorService) =>
    {
        await fornecedorService.DeleteFornecedorAsync(id);
        return Results.Ok();
    }
);

/*  TERCEIRO CÓDIGO */
/**************************    USUÁRIOS    *****************************************/
string GenerateToken(string data)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var secretKey = Encoding.ASCII.GetBytes("abcabcabcabcabcabcabcabcabcabcab"); //Esta chave será gravada em uma variável de ambiente
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Expires = DateTime.UtcNow.AddHours(1), // O token expira em 1 hora
        SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(secretKey),
            SecurityAlgorithms.HmacSha256Signature
        )
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token); //Converte o token em string
}

app.MapPost(
    "/login",
    async (HttpContext context) =>
    {
        // Receber o request
        using var reader = new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();

        // Deserializar o objeto
        var json = JsonDocument.Parse(body);
        var username = json.RootElement.GetProperty("username").GetString();
        var email = json.RootElement.GetProperty("email").GetString();
        var senha = json.RootElement.GetProperty("senha").GetString();

        // essa parte do código será completada com a service na proxima aula
        var token = "";
        if (senha == "1029")
        {
            token = GenerateToken(email); // o método generateToken será reiplementado em uma classe especializada
        }
        // retorna token
        await context.Response.WriteAsync(token);
    }
);

// Rota segura : Toda rota tem corpo de código parecido
app.MapGet(
    "/rotaSegura",
    async (HttpContext context) =>
    {
        // Verificar se o token está presente
        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Token não fornecido");
        }

        // Obter o token
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        // validar o token
        /* esta lógica será convertida em um método dentro de uma classe a ser reaproveitada */
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("abcabcabcabcabcabcabcabcabcabcabc");

        // Chave secreta (a mesma utilizada para gerar o token)
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        SecurityToken validateToken;
        try
        {
            // decodifica, verifica e valida o token
            tokenHandler.ValidateToken(token, validationParameters, out validateToken);
        }
        catch (Exception)
        {
            // Caso o teken seja inválido
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Token Invalido");
        }
        await context.Response.WriteAsync("**Autorizado");
    }
);

/* Etapa 8 */

/**************************    VENDAS    *****************************************/

// Rota para gravar uma venda
app.MapPost(
    "/createvenda",
    async (
        Venda venda,
        VendaService vendaService,
        ClienteService clienteService,
        ProductService productService
    ) =>
    {
        // Validar se o cliente e o produto existem
        var cliente = await clienteService.GetClienteAsync(venda.Cliente.Id);
        var produto = await productService.GetProdutoAsync(venda.Produto.id);

        // Atribuir cliente e produto à venda
        venda.Cliente = cliente;
        venda.Produto = produto;

        if (venda.Produto == null)
        {
            return Results.BadRequest("Produto não encontrado");
        }
        else if (venda.Cliente == null)
        {
            return Results.BadRequest("Cliente não existe");
        }

        // Adicionar a venda
        await vendaService.AddVendaAsync(venda);
        return Results.Created($"/vendas/{venda.Id}", venda);
    }
);

// Rota para consultar vendas detalhadas por produto
app.MapGet(
    "/vendas/produtos/{id}",
    async (
        int id,
        VendaService vendaService,
        ProductService productService,
        ClienteService clienteService
    ) =>
    {
        var vendas = await vendaService.GetVendasDetalhadasPorProdutoAsync(id);
        if (vendas == null)
        {
            return Results.NotFound("Vendas não encontradas");
        }
        return Results.Ok(vendas);
    }
);

// Rota para consultar vendas sumarizadas por produto
app.MapGet(
    "/vendas/produtos/{id}/sumarizada",
    async (int id, VendaService vendaService, ProductService productService) =>
    {
        var vendas = await vendaService.GetVendasSumarizadasPorProdutoAsync(id);
        return Results.Ok(vendas);
    }
);

// Rota para consultar vendas detalhadas por cliente
app.MapGet(
    "/vendas/clientes/{id}",
    async (int id, VendaService vendaService, ClienteService clienteService) =>
    {
        var vendas = await vendaService.GetVendasDetalhadasPorClienteAsync(id);
        return Results.Ok(vendas);
    }
);

// Rota para consultar vendas sumarizadas por cliente
app.MapGet(
    "/vendas/clientes/{id}/sumarizada",
    async (int id, VendaService vendaService, ClienteService clienteService) =>
    {
        var vendas = await vendaService.GetVendasSumarizadasPorClienteAsync(id);
        return Results.Ok(vendas);
    }
);

// ***************************** SERVIÇO ************************************

app.MapPost(
        "/servicos",
        async (HttpContext context, ServicoService servicoService) =>
        {
            var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
            var servico = JsonSerializer.Deserialize<Servico>(requestBody);

            if (servico == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Dados do serviço inválidos.");
                return;
            }

            // Definir a data de criação
            servico.DataCriacao = DateTime.UtcNow;

            await servicoService.AddServicoAsync(servico);

            context.Response.StatusCode = StatusCodes.Status201Created;
            await context.Response.WriteAsJsonAsync(servico);
        }
    )
    .RequireAuthorization(); // Requer autenticação JWT válida

// Atualizar serviço (requer autenticação JWT)
app.MapPut(
        "/servicos/{id}",
        async (HttpContext context, int id, Servico servico, ServicoService servicoService) =>
        {
            // Verificar se o ID na URL corresponde ao ID no corpo da requisição
            if (id != servico.Id)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(
                    "O ID do serviço na URL não corresponde ao ID fornecido no corpo da requisição."
                );
                return;
            }

            // Verificar se o serviço existe
            var existingServico = await servicoService.GetServicoAsync(id);
            if (existingServico == null)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync("Serviço não encontrado.");
                return;
            }

            // Atualizar os dados do serviço existente
            existingServico.Nome = servico.Nome;
            existingServico.Preco = servico.Preco;
            existingServico.Status = servico.Status;

            await servicoService.UpdateServicoAsync(existingServico);

            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsync("Serviço atualizado com sucesso.");
        }
    )
    .RequireAuthorization(); // Requer autenticação JWT válida

// Endpoint para consultar um serviço pelo Id
app.MapGet(
        "/servicos/{id}",
        async (int id, ServicoService servicoService) =>
        {
            var servico = await servicoService.GetServicoAsync(id);
            if (servico == null)
            {
                return Results.NotFound("Serviço não encontrado.");
            }

            return Results.Ok(servico);
        }
    )
    .RequireAuthorization();

// Endpoint para deletar um serviço pelo Id
app.MapDelete(
        "/servicos/{id}",
        async (int id, ServicoService servicoService) =>
        {
            var servico = await servicoService.GetServicoAsync(id);
            if (servico == null)
            {
                return Results.NotFound("Serviço não encontrado.");
            }

            await servicoService.DeleteServicoAsync(id);

            return Results.Ok();
        }
    )
    .RequireAuthorization();

// ***************************** CONTRATOS *************************************

// Endpoint para registro de contrato
app.MapPost(
    "/contratos",
    async (Contrato contrato, ContratoService contratoService) =>
    {
        await contratoService.RegistrarContratoAsync(contrato);
        return Results.Ok();
    }
);

// Endpoint para consulta de serviços contratados por cliente
app.MapGet(
    "/clientes/{clienteId}/servicos",
    async (int clienteId, ContratoService contratoService) =>
    {
        var servicosContratados = await contratoService.GetServicosContratadosPorClienteAsync(
            clienteId
        );
        return Results.Ok(servicosContratados);
    }
);

app.MapGet("/", () => "Hello World!");

app.Run();
