namespace ManageBookStore.Models
{
    public class Paging<T>
    {
        public List<T> ListItem { get; set; }

        public int PageIndex { get; set; }

        public int ItemPerPage { get; set; }

        public int TotalPage { get; set; }

        public Paging() { }

        public Paging(List<T> listItem, int itemPerPage)
        {
            ListItem = listItem;
            ItemPerPage = itemPerPage;
        }

        public void CalTotalPage()
        {
            TotalPage = ListItem.Count / ItemPerPage;
            TotalPage = (ListItem.Count() % ItemPerPage == 0) ? TotalPage : (TotalPage + 1);
        }

        public List<T> GetListPageIndex(int pageIndex)
        {
            if (pageIndex <=0 || pageIndex > TotalPage)
            {
                pageIndex = 1;
            }
            int item = ItemPerPage;
            if (((ItemPerPage - 1) * pageIndex) >= ListItem.Count)
            {
                item = ListItem.Count-(pageIndex-1)*ItemPerPage;
            }
            return ListItem.Skip((pageIndex - 1) * ItemPerPage).Take(item).ToList();
        }
    }
}
