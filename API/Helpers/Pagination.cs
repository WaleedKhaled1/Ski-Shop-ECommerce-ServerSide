namespace API.Helpers
{
    public class Pagination<T> (int pagenumber,int pagesize,int totalcount,IEnumerable<T> data)
    {

        public int PageNumber { get; set; } = pagenumber;
        public int PageSize { get; set; }=pagesize;
        public int TotalCount { get; set; }=totalcount;

        public IEnumerable<T> Data { get; set; }=data;
    }
}
