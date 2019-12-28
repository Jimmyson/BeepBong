using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;

namespace BeepBong.DataAccess
{
    public class Pagination<T>
    {
        public int PageIndex { get; }
        public int TotalPages { get; }
        public int TotalItems { get; }
        public List<T> Items { get; } = new List<T>();

        public Pagination(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalItems = count;

            this.Items.AddRange(items);
        }

        // public bool HasPreviousPage
        // {
        //     get => PageIndex > 1;
        // }

        // public bool HasNextPage
        // {
        //     get => PageIndex < TotalPages;
        // }

        public static async Task<Pagination<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new Pagination<T>(items, count, pageIndex, pageSize);
        }
    }
}