using Umbraco.Cms.Core.Models.PublishedContent;

namespace GI.BackOffice
{
    public static class Extensions
    {
        public static IOrderedEnumerable<IPublishedContent> Randomize(this IEnumerable<IPublishedContent> source)
        {
            Random rnd = new();
            return source.OrderBy(item => rnd.Next());
        }
    }
}