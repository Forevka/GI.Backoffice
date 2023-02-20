namespace GI.BackOffice.Models.ViewModelsData;

public class BreadcrumbJumpViewData
{
    public string Url { get; set; }
    public string Title { get; set; }
}

public class BreadcrumbViewData
{
    public LinkedList<BreadcrumbJumpViewData> History { get; set; }
}
