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
            Connection = connection;
        }

        public IDbConnection Connection
        {
            get
            {
                if (_connection.State is not ConnectionState.Open)
                {
                    _connection.Open();
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
                Transaction = _connection.BeginTransaction();
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
            Connection.Close();
            //Connection.Dispose();
            Transaction.Dispose();
            Transaction = null;
        }

        public void Dispose()
        {
            Dispose(disposing: false);
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
            }

            _disposed = true;
        }
    }
}
