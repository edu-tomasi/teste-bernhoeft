using inventario.data.Context;
using inventario.data.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Data.Common;

namespace inventario.web_api
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            using var connection = new SqlConnection("Server=(LocalDB)\\MSSQLLocalDB; Integrated Security=true ;");
            using var uow = new UnitOfWork(connection);
            
            connection.Open();
            var produtoRepository = new ProdutoRepository(uow);

            produtoRepository.AdicionarAsync(new() {Id = Guid.NewGuid(), Nome = "Copo de Cerveja", Descricao = "Copo exclusivo para consumo de cerveja do tipo IPA.", IdCategoria = Guid.Parse("1B224916-8448-4F6D-858D-B31AE301352F"), Preco = 25.45m, Ativo = true }).GetAwaiter().GetResult();


        }
    }
}