using ResortManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortManagement.Areas.Dashboard.Models
{
    public class AccommondationTypesViewModel
    {
        public IEnumerable<AccommodationTypes> accommodationTypes { get; set; }
        public string SearchTerm { get; set; }
        public int PageNo { get; set; }
        public Pager pager { get; set; }
    }
    public class Pager {
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }


        public Pager(int totalItems, int? page, int pageSize = 10)
        {
            if (pageSize == 0) pageSize = 10;

            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10 /*pageSize*/)
                {
                    startPage = endPage - 9 /*(pageSize-1)*/;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }
    }
}