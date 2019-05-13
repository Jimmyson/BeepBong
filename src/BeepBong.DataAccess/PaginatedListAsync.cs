using Microsoft.EntityFrameworkCore;
using System.Threading;
using BeepBong.Domain;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;

namespace BeepBong.DataAccess
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; } 

        public PaginatedList(List<T> items, int count, int pageSize, int pageIndex)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get => (PageIndex > 1);
        }

        public bool HasNextPage
        {
            get => (PageIndex < TotalPages);
        }
        
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await ((IQueryable<T>)source).CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}