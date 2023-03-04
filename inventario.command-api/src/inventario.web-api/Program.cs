using inventario.business.Models;
using inventario.data.Context;
using inventario.data.Data;
using Microsoft.Data.SqlClient;
using System;

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

            //produtoRepository.AdicionarAsync(new() {Id = Guid.NewGuid(), Nome = "Copo de Cerveja", Descricao = "Copo exclusivo para consumo de cerveja do tipo IPA.", IdCategoria = Guid.Parse("1B224916-8448-4F6D-858D-B31AE301352F"), Preco = 25.45m, Ativo = true }).GetAwaiter().GetResult();
            uow.BeginTransaction();

            ProdutoModel produto = new()
            {
                Id = Guid.Parse("30848BA8-09C1-4181-849C-0028A1B282D1"),
                Nome = "Copo Floripa Eco Festival 2022",
                Descricao = "Copo exclusivo para o evento Floripa Eco Festival, edição 2022",
                Preco = 9.99m,
                IdCategoria = Guid.Parse("1B224916-8448-4F6D-858D-B31AE301352F"),
                Ativo = true
            };

            produtoRepository.AlterarAsync(produto).GetAwaiter().GetResult();

            uow.Commit();

        }
    }
}