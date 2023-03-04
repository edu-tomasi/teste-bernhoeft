using System.ComponentModel.DataAnnotations;

namespace inventario.data.Configurations
{
    internal record DatabaseConfig
    {
        [Required(AllowEmptyStrings = false)]
        public string ConnectionString { get; init; }
    }
}
