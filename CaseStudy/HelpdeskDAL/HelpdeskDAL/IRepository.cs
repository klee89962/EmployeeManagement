using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HelpdeskDAL
{
    // IRepository Interface
    public interface IRepository<T>
    {
        List<T> GetAll();
        List<T> GetByExpression(Expression<Func<T, bool>> match);
        T Add(T entity);
        UpdateStatus Update(T enity);
        int Delete(int i);
    }
}
