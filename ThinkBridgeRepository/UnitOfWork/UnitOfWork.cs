using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkBridgeDataLayer;
using ThinkBridgeRepository.GenericRepository;

namespace ThinkBridgeRepository.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork,IDisposable
    {

        private readonly ThinkBridgeDBContext _context;
        private bool _disposed;
        private string _errorMessage = string.Empty;
        private DbContextTransaction _transaction;

        #region Delcare Repositories

        private GenericRepository<Inventory> inventory;
               
        #endregion
        #region
        public GenericRepository<Inventory> InventoryRepository
        {
            get
            {
                if(inventory==null)
                {
                    this.inventory = new GenericRepository<Inventory>(_context);
                }
                return inventory;
            }
        }

        #endregion


        public UnitOfWork()
        {
            _context = new ThinkBridgeDBContext();
        }
        public void Commit()
        {
            _transaction.Commit();
        }

        public void CreateTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void RollBack()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
                        //    catch (DbEntityValidationException dbExp)

                //foreach (var validationErrors in dbExp.EntityValidationErrors)
                //{
                //    foreach (var validationError in validationErrors.ValidationErrors)
                //    {
                //        _errorMessage += $"Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage} {Environment.NewLine}";
                //        throw new Exception(_errorMessage, dbExp);
                //    }
                //}
            }
        }
    }
}
