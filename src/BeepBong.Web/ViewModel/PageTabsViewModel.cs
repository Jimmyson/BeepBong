using BeepBong.DataAccess;

namespace BeepBong.Web.ViewModel
{
    public class PageTabsViewModel<T>
    {
        public PaginatedList<T> Pagination { get; set; }
		public string Id { get; set; }
    }
}