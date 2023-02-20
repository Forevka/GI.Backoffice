namespace GI.BackOffice.Models.ViewModelsData;

public class PaginationViewData
{
    public int PageCount => Convert.ToInt32(Math.Floor(TotalCount / Convert.ToDouble(PageSize)));
    public int TotalCount { get; set; }
    public int PageSize { get; set; }

    // determines how many page back/forward need to draw and insert ... between
    public int BackPageCount { get; set; } = 4;
    public int ForwardPageCount { get; set; } = 3;
}
