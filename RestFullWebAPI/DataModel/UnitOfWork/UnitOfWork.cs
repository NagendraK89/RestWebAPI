using DataModel.GenericRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        #region Private Veriables
        private RestFullWebAPIDbEntities _context = null;
        private GenericRepository<Token> _tokenRepo;
        private GenericRepository<User> _userRepo;
        private GenericRepository<Product> _productRepo;
        #endregion 

        #region Class constructor
        public UnitOfWork()
        {
            _context = new RestFullWebAPIDbEntities();
        }
        #endregion 
        #region region Public Repository Creation properties...
        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (this._productRepo == null)
                    this._productRepo = new GenericRepository<Product>(_context);
                return _productRepo;
            }
        }
        public GenericRepository<User> UserRepository
        {
            get 
            {
                if (this._userRepo == null)
                    this._userRepo = new GenericRepository<User>(_context);
                return _userRepo;
            
            }
        
        }

        public GenericRepository<Token> UserRepository
        {
            get
            {
                if (this._tokenRepo == null)
                    this._tokenRepo = new GenericRepository<Token>(_context);
                return _tokenRepo;

            }

        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }
        #endregion
        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
