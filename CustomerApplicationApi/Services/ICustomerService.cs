using System;
using System.Collections.Generic;

namespace CustomerApplicationApi.Services
{
    // Interface specifies behaviours and what they do but not how.
    // Generic types to maximize code reuse, type safety, and performance.
    public interface ICustomerService<TEntity, U> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(U id);
        long Add(TEntity b);
        long Update(U id, TEntity b);
        long Delete(U id);
    }
}
