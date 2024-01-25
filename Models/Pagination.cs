namespace ProvaPub.Models
{
    public class Pagination<T> where T : class
    {
        public IEnumerable<T>? Result { get; set; }
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public int TotalResult { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext { get; set; }


        public void IsNextPage()
        {
            HasNext = TotalResult / TotalCount == CurrentPage ? false : true;
        }

        public void CountTotalPages()
        {
            TotalPages = Convert.ToInt32(Math.Round(Convert.ToDecimal(TotalResult) / Convert.ToDecimal(TotalCount)));
        }
    }
}
