using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace GI.BackOffice
{
    public static class Extensions
    {
        public static IOrderedEnumerable<IPublishedContent> Randomize(this IEnumerable<IPublishedContent> source)
        {
            Random rnd = new();
            return source.OrderBy(item => rnd.Next());
        }

        public static string? GetCropUrlWebp(this MediaWithCrops mediaWithCrops, string cropAlias)
        {
            return mediaWithCrops.GetCropUrl(cropAlias);
        }

        public static string? GetUrlWebp(this MediaWithCrops mediaWithCrops)
        {
            return mediaWithCrops.Url();
        }
    }

}