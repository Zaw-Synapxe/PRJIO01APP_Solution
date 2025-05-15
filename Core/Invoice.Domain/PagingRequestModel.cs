namespace Invoice.Domain
{
    public class PagingRequestModel
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; }
        private int pageSize;
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = value > maxPageSize ? maxPageSize : value;
            }
        }
        public string orderBy { get; set; }
        public string direction { get; set; }
    }
}
