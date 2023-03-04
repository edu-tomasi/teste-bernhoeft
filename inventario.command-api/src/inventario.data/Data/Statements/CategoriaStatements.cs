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
            UPDATE bernhoeft.dbo.Categoria
                SET Nome = @{nameof(CategoriaModel.Nome)}, Ativo = @{nameof(CategoriaModel.Ativo)}
            WHERE Id = @{nameof(CategoriaModel.Id)}";

        public static readonly string ListarCategorias = $@"
            SELECT {nameof(CategoriaModel.Id)},
                   {nameof(CategoriaModel.Nome)},
                   {nameof(CategoriaModel.Ativo)}
            FROM bernhoeft.dbo.Categoria WITH (NOLOCK)
            WHERE ({nameof(CategoriaModel.Id)} = @{nameof(CategoriaModel.Id)} 
                    OR @{nameof(CategoriaModel.Id)} IS NULL)
            AND ({nameof(CategoriaModel.Nome)} COLLATE Latin1_general_CI_AI LIKE @{nameof(CategoriaModel.Nome)} COLLATE Latin1_general_CI_AI 
                    OR @{nameof(CategoriaModel.Nome)} IS NULL)
            AND ({nameof(CategoriaModel.Ativo)} = @{nameof(CategoriaModel.Ativo)} 
                    OR @{nameof(CategoriaModel.Ativo)} IS NULL)";

        public static readonly string RemoverCategoria = $@"
            DELETE FROM bernhoeft.dbo.Categoria
            WHERE {nameof(CategoriaModel.Id)} = @{nameof(CategoriaModel.Id)}";

        public static object ObterParametros(CategoriaModel categoria) => new
        {
            categoria.Id,
            categoria.Nome,
            categoria.Ativo
        };

        public static string ObterFiltrosParaConsulta(object filtros)
        {

            return "";
        }
    }
}
