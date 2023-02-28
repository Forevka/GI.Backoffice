using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Clean.Site.UmbracoHelpers
{
    public static class ArticlesSelectHelper
    {
        public static ArticleListPage GetContentNode(this UmbracoHelper Umbraco)
        {
            return (Umbraco.Content(Guid.Parse("c606cba8-dfeb-4db5-99be-e297710fe601")) as ArticleListPage)!;
        }

        public static (List<IPublishedContent> articlePage, int articlesCount) GetPagedContent(this UmbracoHelper Umbraco, int currentPage = 1, int pageSize = 6)
        {
            var articlePage = Umbraco.GetContentNode()
                !.SectionsToShow
                .SelectMany(x => x.Children)
                .Where(x => x.IsVisible())
                .OrderByDescending(x => x.CreateDate)
                .Skip((currentPage - 1) * pageSize)
                .Take(6)
                .ToList();

            var articlesCount = Umbraco.GetContentNode()
                !.SectionsToShow
                .SelectMany(x => x.Children)
                .Count(x => x.IsVisible());

            return (articlePage, articlesCount);
        }

        public static (List<IPublishedContent> articlePage, int articlesCount) GetPagedContentForSection(this UmbracoHelper Umbraco, string sectionName, int currentPage = 1, int pageSize = 6)
        {
            var articlePage = Umbraco.GetContentNode()
                !.SectionsToShow.Where(x => x.ContentType.Alias == sectionName)
                .SelectMany(x => x.Children)
                .Where(x => x.IsVisible())
                .OrderByDescending(x => x.CreateDate)
                .Skip((currentPage - 1) * pageSize)
                .Take(6)
                .ToList();

            var articlesCount = Umbraco.GetContentNode()
                !.SectionsToShow.Where(x => x.ContentType.Alias == sectionName)
                .SelectMany(x => x.Children)
                .Count(x => x.IsVisible());

            return (articlePage, articlesCount);
        }
    }
}
