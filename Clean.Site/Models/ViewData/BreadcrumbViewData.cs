using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Clean.Site.Models.ViewData;

public class BreadcrumbJumpViewData
{
    public string Url { get; set; }
    public string Title { get; set; }
}

public class BreadcrumbViewData
{
    public LinkedList<BreadcrumbJumpViewData> History { get; set; }

    public static BreadcrumbViewData FormHistory(IPublishedContent model)
    {
        var parentHistory = new List<BreadcrumbJumpViewData>();

        if (model.ContentType.Alias == "contentPage")
        {
            parentHistory.Add(new()
            {
                Title = model.Value("title")?.ToString(),
                Url = model.Url(),
            });
        }
        else
        {
            parentHistory.Add(new()
            {
                Title = (model as IPageProperties).PageName,
                Url = model.Url(),
            });
        }

        var parent = model.Parent;
        while (parent != null)
        {
            parentHistory.Add(new BreadcrumbJumpViewData
            {
                Title = (parent as IPageProperties).PageName,
                Url = parent.Url(),
            });

            parent = parent.Parent;
        }

        parentHistory.Reverse();

        return new BreadcrumbViewData
        {
            History = new LinkedList<BreadcrumbJumpViewData>(parentHistory),
        };
    }
}
