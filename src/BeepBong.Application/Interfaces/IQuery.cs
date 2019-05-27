using System;
using System.Linq;

namespace BeepBong.Application.Interfaces
{
    public interface IQuery<T>
    {
        IQueryable<T> GetQuery(Guid? id = null);
    }
}