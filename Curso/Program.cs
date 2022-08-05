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

InserirDados();