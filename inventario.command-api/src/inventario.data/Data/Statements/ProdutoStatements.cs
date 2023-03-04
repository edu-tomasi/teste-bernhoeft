﻿using inventario.business.Models;
using System;
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
            WHERE (p.Id = @{nameof(ProdutoModel.Id)} OR @{nameof(ProdutoModel.Id)} IS NULL)
            AND (p.Ativo = @{nameof(ProdutoModel.Ativo)} OR @{nameof(ProdutoModel.Ativo)} IS NULL)
            AND (p.Nome LIKE @{nameof(ProdutoModel.Nome)} OR @{nameof(ProdutoModel.Nome)} IS NULL)
            AND (p.Descricao LIKE @{nameof(ProdutoModel.Descricao)} OR @{nameof(ProdutoModel.Descricao)} IS NULL)
            AND (c.Nome = @CategoriaNome OR @CategoriaNome IS NULL)";

        public static object ObterParametros(ProdutoModel produto) => new
        {
            produto.Id,
            produto.Nome,
            produto.Preco,
            produto.Descricao,
            produto.IdCategoria,
            produto.Ativo
        };

        public static Dictionary<string, object> ObterParametrosParaListar(Guid? id, string nome, string descricao, string categoria, bool? ativo)
        {
            return new Dictionary<string, object>
            {
                [nameof(ProdutoModel.Id)] = id,
                [nameof(ProdutoModel.Ativo)] = ativo,
                [nameof(ProdutoModel.Nome)] = nome is null ? null : $"%{nome}%",
                [nameof(ProdutoModel.Descricao)] = descricao is null ? null : $"%{descricao}%",
                ["CategoriaNome"] = categoria is null ? null : $"%{categoria}%",
            };
        }
    }
}
