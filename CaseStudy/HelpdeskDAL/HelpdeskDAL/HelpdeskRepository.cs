using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace HelpdeskDAL
{
    public class HelpdeskRepository<T> : IRepository<T> where T : HelpdeskEntity
    {
        private HelpdeskContext _db = null;

        public HelpdeskRepository(HelpdeskContext context = null)
        {
            _db = context != null ? context : new HelpdeskContext();
        }
        // Returns the list of Template
        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }
        // Returns list of template that matches the expression
        public List<T> GetByExpression(Expression<Func<T, bool>> match)
        {
            return _db.Set<T>().Where(match).ToList();
        }
        // Adds the template entity to set of template
        public T Add(T entity)
        {
            _db.Set<T>().Add(entity);
            _db.SaveChanges();
            return entity;
        }
        // Returns Enum value after the update of template
        public UpdateStatus Update(T updatedEntity)
        {
            // Decalre UpdateStatus value
            UpdateStatus operationStatus = UpdateStatus.Failed;

            try
            {
                // Get the entity that matches the expression
                HelpdeskEntity currentEntity = GetByExpression(ent => ent.Id == updatedEntity.Id).FirstOrDefault();
                _db.Entry(currentEntity).OriginalValues["Timer"] = updatedEntity.Timer;
                _db.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);

                // Save information
                if (_db.SaveChanges() == 1)
                    operationStatus = UpdateStatus.Ok;
            }
            catch (DbUpdateConcurrencyException dbx)
            {
                operationStatus = UpdateStatus.Stale;
                Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod().Name + dbx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod().Name + ex.Message);

            } // try/catch

            // Returns status
            return operationStatus;
        }
        // Return number of deleted employees, -1 if fail
        public int Delete(int id)
        {
            T currentEntity = GetByExpression(ent => ent.Id == id).FirstOrDefault();
            _db.Set<T>().Remove(currentEntity);
            return _db.SaveChanges();
        }
    }
}
