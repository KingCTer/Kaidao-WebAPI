using Microsoft.AspNetCore.Mvc;

namespace Kaidao.Web.Share.Query
{
    public class PaginationFilter
    {
        private int _pageIndex = 1;
        private int _pageSize = 10;
        private string _query = "";

        [FromQuery(Name = "pageIndex")]
        public int PageNumber
        {
            get { return _pageIndex; }
            set { _pageIndex = value < 1 ? 1 : value; }
        }

        [FromQuery(Name = "pageSize")]
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value < 0 || value > 200 ? 10 : value; }
        }

        [FromQuery(Name = "query")]
        public string Query
        {
            get { return _query; }
            set { _query = value; }
        }
    }
}