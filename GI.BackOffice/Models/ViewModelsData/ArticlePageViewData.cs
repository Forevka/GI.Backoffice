using Umbraco.Cms.Core.Models.PublishedContent;

namespace GI.BackOffice.Models.ViewModelsData;

public class ArticlePageViewData
{
    public IEnumerable<IPublishedContent> Articles { get; set; }
    public int Page { get; set; }
    public int PageCount { get; set; }
}