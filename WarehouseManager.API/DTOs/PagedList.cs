namespace WarehouseManager.API.DTOs;

public class PagedList<T>(List<T> data, int pageNumber, int pageSize, int totalRecords)
{
    public List<T> Data { get; set; } = data;
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
    public int TotalPages { get; set; } = (int)Math.Ceiling(totalRecords / (decimal)pageSize);
    public int TotalRecords { get; set; } = totalRecords;

    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}