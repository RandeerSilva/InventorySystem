using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.UnitTests
{
    public static class DbSetMocker
    {
        public static DbSet<T> CreateMockDbSet<T>(IQueryable<T> data) where T : class
        {
            var dbSet = Substitute.For<DbSet<T>, IQueryable<T>>();
            ((IQueryable<T>)dbSet).Provider.Returns(data.Provider);
            ((IQueryable<T>)dbSet).Expression.Returns(data.Expression);
            ((IQueryable<T>)dbSet).ElementType.Returns(data.ElementType);
            ((IQueryable<T>)dbSet).GetEnumerator().Returns(data.GetEnumerator());
            return dbSet;
        }
    }
}
