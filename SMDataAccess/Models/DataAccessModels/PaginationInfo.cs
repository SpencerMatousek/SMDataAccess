using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDataAccess.Models.DataAccessModels;
public class PaginationInfo(int currentPage, int totalCount, int pageSize)
{
    public int CurrentPage { get; set; } = currentPage;
    public int TotalCount { get; set; } = totalCount;
    public int PageSize { get; set; } = pageSize;
    public bool HasNextPage { get => TotalPages > CurrentPage; }
    public bool HasPreviousPage { get => CurrentPage > 1; }
    public int TotalPages { get => (int)Math.Ceiling((double)TotalCount / PageSize); }
    public List<int> PageIndicatorsToShow
    {
        get
        {
            int maxPagesToShow = 5;
            int startPage, endPage;

            if (TotalPages <= maxPagesToShow)
            {
                startPage = 1;
                endPage = TotalPages;
            }
            else
            {
                int middle = maxPagesToShow / 2;
                if (CurrentPage <= middle + 1)
                {
                    startPage = 1;
                    endPage = maxPagesToShow;
                }
                else if (CurrentPage + middle >= TotalPages)
                {
                    startPage = TotalPages - maxPagesToShow + 1;
                    endPage = TotalPages;
                }
                else
                {
                    startPage = CurrentPage - middle;
                    endPage = CurrentPage + middle;
                }
            }

            return Enumerable.Range(startPage, endPage - startPage + 1).ToList();
        }
    }
    public string? SearchQuery { get; set; } = null;
    public override string ToString()
    {
        var showingCount = TotalCount < PageSize ? TotalCount : CurrentPage * PageSize;
        return $"{((CurrentPage - 1) * PageSize) + 1} - {showingCount} of {TotalCount}";
    }
}