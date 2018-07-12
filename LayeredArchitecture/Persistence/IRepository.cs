using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LayeredArchitecture.Persistence
{
	public interface IRepository<T> where T : class
    {
		T Get(int id);
		T Save(T item);
		T FindSingle(Expression<Func<T, bool>> predicate);
		T FindFirst(Expression<Func<T, bool>> predicate);
		IEnumerable<T> FindAll(Expression<Func<T, bool>> predicte);
		IQueryable<T> Table { get; }
    }
}
