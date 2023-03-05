using inventario.business.Models;
using inventario.business.Models.Request;
using System.Collections.Generic;

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
            WHERE ({nameof(CategoriaModel.Id)} = @{nameof(FilterCategoriaRequest.Id)} 
                    OR @{nameof(FilterCategoriaRequest.Id)} IS NULL)
            AND ({nameof(CategoriaModel.Nome)} COLLATE Latin1_general_CI_AI LIKE @{nameof(FilterCategoriaRequest.Nome)} COLLATE Latin1_general_CI_AI 
                    OR @{nameof(FilterCategoriaRequest.Nome)} IS NULL)
            AND ({nameof(CategoriaModel.Ativo)} = @{nameof(FilterCategoriaRequest.Ativo)} 
                    OR @{nameof(FilterCategoriaRequest.Ativo)} IS NULL)";

        public static readonly string RemoverCategoria = $@"
            DELETE FROM bernhoeft.dbo.Categoria
            WHERE {nameof(CategoriaModel.Id)} = @{nameof(CategoriaModel.Id)}";

        public static object ObterParametros(CategoriaModel categoria) => new
        {
            categoria.Id,
            categoria.Nome,
            categoria.Ativo
        };

        public static Dictionary<string, object> ObterParametrosParaListar(FilterCategoriaRequest request)
        {
            return new Dictionary<string, object>()
            {
                [nameof(FilterCategoriaRequest.Id)] = request.Id,
                [nameof(FilterCategoriaRequest.Nome)] = request.Nome is null ? null : $"%{request.Nome}%",
                [nameof(FilterCategoriaRequest.Ativo)] = request.Ativo
            };
        }
    }
}
