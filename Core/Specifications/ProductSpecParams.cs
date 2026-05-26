using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private List<string> _brands=[];

        public List<string> Brands
        {
            get { return _brands ; }

            set
            {
                _brands=value.SelectMany(b => b.Split(',')).ToList();
            }
        }

        private List<string> _types = [];

        public List<string> Types
        {
            get { return _types; }

            set
            {
                _types = value.SelectMany(t => t.Split(',')).ToList();
            }
        }

        public string? Sort { get; set; }

        private int MaxPageSize = 50;
        private int _PageSize { get; set; } = 6;

        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public int PageNumber { get; set; } = 1;

        private string ? _search { get; set; }

        public string Search
        {
            get =>_search?? "";
            set { _search = value.ToLower(); }
        }
    }
}
