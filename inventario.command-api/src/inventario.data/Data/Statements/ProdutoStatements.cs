using inventario.business.Models;
using inventario.business.Models.Request;
using System.Collections.Generic;

namespace inventario.data.Data.Statements
{
    public class ProdutoStatements
    {
        public static readonly string InserirProduto = @$"
            INSERT INTO bernhoeft.dbo.Produto 
            VALUES (
                @{nameof(ProdutoModel.Id)}, 
                @{nameof(ProdutoModel.Nome)},
                @{nameof(ProdutoModel.Descricao)},
                @{nameof(ProdutoModel.Preco)}, 
                @{nameof(ProdutoModel.IdCategoria)},
                @{nameof(ProdutoModel.Ativo)})";

        public static readonly string AlterarProduto = $@"
            UPDATE bernhoeft.dbo.Produto
                SET {nameof(ProdutoModel.Nome)} = @{nameof(ProdutoModel.Nome)},
                    {nameof(ProdutoModel.Descricao)} = @{nameof(ProdutoModel.Descricao)},
                    {nameof(ProdutoModel.Preco)} = @{nameof(ProdutoModel.Preco)},
                    {nameof(ProdutoModel.IdCategoria)} = @{nameof(ProdutoModel.IdCategoria)},
                    {nameof(ProdutoModel.Ativo)} = @{nameof(ProdutoModel.Ativo)}
                WHERE {nameof(ProdutoModel.Id)} = @{nameof(ProdutoModel.Id)}";

        public static readonly string ListarProdutos = $@"
            SELECT  p.{nameof(ProdutoModel.Id)}, 
                    p.{nameof(ProdutoModel.Ativo)}, 
                    p.{nameof(ProdutoModel.Nome)}, 
                    p.{nameof(ProdutoModel.Descricao)}, 
                    p.{nameof(ProdutoModel.Preco)}, 
                    p.{nameof(ProdutoModel.IdCategoria)},
                    c.Id,
                    c.{nameof(ProdutoModel.Categoria.Nome)}, 
                    c.{nameof(ProdutoModel.Categoria.Ativo)}
            FROM bernhoeft.dbo.Produto p WITH (NOLOCK)
            INNER JOIN bernhoeft.dbo.Categoria c WITH (NOLOCK) ON p.{nameof(ProdutoModel.IdCategoria)} = c.{nameof(CategoriaModel.Id)}
            WHERE (p.{nameof(ProdutoModel.Id)} = @{nameof(FilterProdutoRequest.Id)} 
                    OR @{nameof(FilterProdutoRequest.Id)} IS NULL)
            AND (p.{nameof(ProdutoModel.Ativo)} = @{nameof(FilterProdutoRequest.Ativo)}
                    OR @{nameof(FilterProdutoRequest.Ativo)} IS NULL)
            AND (p.{nameof(ProdutoModel.Nome)} COLLATE Latin1_general_CI_AI LIKE @{nameof(FilterProdutoRequest.Nome)} COLLATE Latin1_general_CI_AI 
                    OR @{nameof(FilterProdutoRequest.Nome)} IS NULL)
            AND (p.{nameof(ProdutoModel.Descricao)} COLLATE Latin1_general_CI_AI LIKE @{nameof(FilterProdutoRequest.Descricao)} COLLATE Latin1_general_CI_AI 
                    OR @{nameof(FilterProdutoRequest.Descricao)} IS NULL)
            AND (p.{nameof(ProdutoModel.IdCategoria)} = @{nameof(FilterProdutoRequest.IdCategoria)} 
                    OR @{nameof(FilterProdutoRequest.IdCategoria)} IS NULL)
            AND (c.{nameof(ProdutoModel.Categoria.Nome)} LIKE @{nameof(FilterProdutoRequest.Categoria)} 
                    OR @{nameof(FilterProdutoRequest.Categoria)} IS NULL)";

        public static readonly string RemoverProduto = $@"
            DELETE FROM bernhoeft.dbo.Produto
            WHERE {nameof(ProdutoModel.Id)} = @{nameof(ProdutoModel.Id)}";

        public static object ObterParametros(ProdutoModel produto) => new
        {
            produto.Id,
            produto.Nome,
            produto.Preco,
            produto.Descricao,
            produto.IdCategoria,
            produto.Ativo
        };

        public static Dictionary<string, object> ObterParametrosParaListar(FilterProdutoRequest request)
        {
            return new Dictionary<string, object>
            {
                [nameof(FilterProdutoRequest.Id)] = request.Id,
                [nameof(FilterProdutoRequest.Ativo)] = request.Ativo,
                [nameof(FilterProdutoRequest.Nome)] = request.Nome is null ? null : $"%{request.Nome}%",
                [nameof(FilterProdutoRequest.Descricao)] = request.Descricao is null ? null : $"%{request.Descricao}%",
                [nameof(FilterProdutoRequest.IdCategoria)] = request.IdCategoria,
                [nameof(FilterProdutoRequest.Categoria)] = request.Categoria is null ? null : $"%{request.Categoria}%",
            };
        }
    }
}
