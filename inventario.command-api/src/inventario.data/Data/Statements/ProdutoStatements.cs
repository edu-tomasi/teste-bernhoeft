using inventario.business.Models;

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

        public static readonly string AlterarProduto = "";

        public static readonly string ListarProdutos = "";

        public static object ObterParametros(ProdutoModel produto) => new
        {
            produto.Id,
            produto.Nome,
            produto.Preco,
            produto.Descricao,
            produto.IdCategoria,
            produto.Ativo
        };
    }
}
