using inventario.business.Abstractions.Data;
using System;
using System.Data;

namespace inventario.data.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private int _transactionCounter;
        private IDbConnection _connection;
        private bool _disposed;

        public UnitOfWork(IDbConnection connection)
        {
            _connection = connection;
        }

        public IDbConnection Connection
        {
            get
            {
                if (_connection.State is not ConnectionState.Open)
                {
                    _connection.Open();
                    if (_connection.State is not ConnectionState.Open)
                    {
                        throw new InvalidOperationException("Connection should be open.");
                    }
                }
                return _connection;
            }

            init
            {
                _connection = value;
            }
        }

        public IDbTransaction Transaction { get; protected set; }

        public void BeginTransaction()
        {
            if (_transactionCounter == 0)
            {
                Transaction = Connection.BeginTransaction();
            }

            _transactionCounter++;
        }

        public void Commit()
        {
            try
            {
                TryCommit();
                return;
            }
            catch (Exception)
            {
                Rollback();
                throw;
            }
        }

        public void Rollback()
        {
            if (Transaction is null)
            {
                return;
            }

            _transactionCounter = 0;
            Transaction.Rollback();
            ClearTransaction();
        }

        private void TryCommit()
        {
            if (Transaction is null || _transactionCounter < 0)
            {
                throw new InvalidOperationException("Não existem transações em aberto para efetuar o commit.");
            }

            _transactionCounter--;

            if (_transactionCounter > 0)
            {
                return;
            }

            Transaction.Commit();
            ClearTransaction();
        }

        private void ClearTransaction()
        {
            Transaction.Dispose();
            Transaction = null;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Transaction?.Dispose();
                Connection?.Dispose();
                _connection = null;
            }

            _disposed = true;
        }
    }
}
