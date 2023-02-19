using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace GI.BackOffice
{
    public static class Extensions
    {
        public static string CloudFrontUrl = "https://d3w0rb8zugh4tl.cloudfront.net/";
        //fit-in/filters:quality(80)/filters:format(webp)/media/0zsbssxl/bg-top-4.png

        public static IOrderedEnumerable<IPublishedContent> Randomize(this IEnumerable<IPublishedContent> source)
        {
            Random rnd = new();
            return source.OrderBy(item => rnd.Next());
        }

        public static string? GetCropUrlWebp(this MediaWithCrops mediaWithCrops, string cropAlias, int quality = 80)
        {
            var cropConfig = mediaWithCrops.LocalCrops.Crops?.FirstOrDefault(x => x.Alias == cropAlias);
            if (cropConfig != null)
            {
                return CloudFrontUrl + $"fit-in/{cropConfig.Width}x{cropConfig.Height}/filters:quality({quality})/filters:format(webp)/" + mediaWithCrops.Url();
            }

            return GetUrlWebp(mediaWithCrops, quality);
        }

        public static string? GetUrlWebp(this MediaWithCrops mediaWithCrops, int quality = 80)
        {
            return CloudFrontUrl + $"fit-in/filters:quality({quality})/filters:format(webp)/" + mediaWithCrops.Url();
        }
    }

}