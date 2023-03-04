using inventario.business.Models;

namespace inventario.data.Data.Statements
{
    public class CategoriaStatements
    {
        public static readonly string InserirCategoria = $@"
            INSERT INTO bernhoeft.dbo.Categoria (
                Id, 
                Nome, 
                Ativo) 
            VALUES (
                @{nameof(CategoriaModel.Id)}, 
                @{nameof(CategoriaModel.Nome)}, 
                @{nameof(CategoriaModel.Ativo)})";

        public static readonly string AlterarCategoria = $@"
            UPDATE dbo.Categoria
                SET Nome = @{nameof(CategoriaModel.Nome)}, Ativo = @{nameof(CategoriaModel.Ativo)}
            WHERE Id = @{nameof(CategoriaModel.Id)}";

        public static readonly string ListarCategorias = $@"
            SELECT {nameof(CategoriaModel.Id)},
                   {nameof(CategoriaModel.Nome)},
                   {nameof(CategoriaModel.Ativo)}
            FROM bernhoeft.dbo.Categoria WITH (NOLOCK)
            WHERE Nome LIKE '@{nameof(CategoriaModel.Nome)}'
            AND Ativo = @{nameof(CategoriaModel.Ativo)}";

        public static object ObterParametros(CategoriaModel categoria) => new
        {
            categoria.Id,
            categoria.Nome,
            categoria.Ativo
        };
    }
}
