using System.Data;

namespace inventario.business.Abstractions.Data
{
    public interface IUnitOfWork
    {
        IDbConnection Connection { get; }

        void Commit();

        void Rollback();
    }
}
