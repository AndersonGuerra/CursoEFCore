using CursoEFCore.Data;
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;
Console.WriteLine("Hello, World!");

static void InserirDados()
{
    var produto = new Produto
    {
        Descricao = "Produto Teste",
        CodigoBarras = "12341232132132",
        Valor = 10m,
        TipoProduto = TipoProduto.MercadoriaParaRevenda,
        Ativo = true
    };

    using var db = new ApplicationContext();
    db.Database.Migrate();
    db.Produtos.Add(produto);
    var registros = db.SaveChanges();
    Console.WriteLine($"Total de registro(s): {registros}");

}

static void InserirDadosEmMassa()
{
    var produto = new Produto
    {
        Descricao = "Produto Teste Massa",
        CodigoBarras = "12341232132132",
        Valor = 10m,
        TipoProduto = TipoProduto.MercadoriaParaRevenda,
        Ativo = true
    };
    var cliente = new Cliente
    {
        Nome = "Anderson Guerra",
        CEP = "1111111",
        Cidade = "Macapá",
        Estado = "AP",
        Telefone = "988887777"
    };
    using var db = new ApplicationContext();
    db.AddRange(produto, cliente);
    var registros = db.SaveChanges();
    Console.WriteLine($"Total de registro(s): {registros}");

}

static void ConsultarDados()
{
    using var db = new ApplicationContext();
    //var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
    var consultaPorMetodo = db.Clientes.AsNoTracking().Where(p => p.Id > 0).ToList();
    foreach (var cliente in consultaPorMetodo)
    {
        Console.WriteLine($"Consultando cliente: {cliente.Id}");
        db.Clientes.Find(cliente.Id);
    }
}

static void CadastrarPedido()
{
    using var db = new ApplicationContext();
    var cliente = db.Clientes.FirstOrDefault();
    var produto = db.Produtos.FirstOrDefault();

    var pedido = new Pedido
    {
        ClienteId = cliente.Id,
        IniciadoEm = DateTime.Now,
        FinalizadoEm = DateTime.Now,
        Observacao = "Teste de pedido",
        Status = StatusPedido.Analise,
        TipoFrete = TipoFrete.SemFrete,
        Itens = new List<PedidoItem>
        {
            new PedidoItem{
                ProdutoId = produto.Id,
                Desconto = 0,
                Quantidade = 1,
                Valor = 10,
            }
        }
    };
    db.Pedidos.Add(pedido);
    db.SaveChanges();
}

static void ConsultarPedidoCarregamentoAdiantado()
{
    using var db = new ApplicationContext();
    var pedidos = db.Pedidos.Include(p => p.Itens).ToList();
    Console.WriteLine(pedidos.Count);
}

static void AtualizarDados()
{
    using var db = new ApplicationContext();
    var cliente = db.Clientes.Find(1);
    cliente.Nome = "Cliente Alterado Passo 2";
    // var clienteAtualizado = new Cliente
    // {
    //     Id = 1,
    //     Nome = "Cliente Alterado Passo 2",
    //     C
    // };
    // db.Clientes.Update(cliente);
    // db.Entry(cliente).CurrentValues.SetValues(clienteAtualizado);
    db.SaveChanges();
}

static void RemoverRegistro()
{
    using var db = new ApplicationContext();
    var cliente = db.Clientes.Find(1);
    db.Clientes.Remove(cliente);
    //db.Remove(cliente);
    //db.Entry(cliente).State = EntityState.Deleted;
    db.SaveChanges();
}

// InserirDados();
// InserirDadosEmMassa();
// ConsultarDados();
// CadastrarPedido();
// ConsultarPedidoCarregamentoAdiantado();
// AtualizarDados();
RemoverRegistro();