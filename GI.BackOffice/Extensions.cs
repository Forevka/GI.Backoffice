using Lucene.Net.Search;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Numerics;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;
using Umbraco.Extensions;
using Umbraco.New.Cms.Core.Models;
using static Lucene.Net.Store.Lock;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public static string? GetCropUrlWebp(this MediaWithCrops mediaWithCrops, ImageCropName cropAlias, int quality = 80)
        {
            var cropName = ImageCropNamesConstants.FromEnum(cropAlias);
            if (string.IsNullOrEmpty(cropName))
                return mediaWithCrops.GetUrlWebp(quality);

            var cropConfig = mediaWithCrops.LocalCrops.Crops?.FirstOrDefault(x => x.Alias == cropName);
            var viewPort = cropConfig?.Coordinates;

            ///0x0:769x774//filters:quality(80)/filters:format(webp)/fit-in/300x300
            var query = $"/filters:quality({quality})/filters:format(webp)/";

            if (viewPort != null)
            {
                var width = mediaWithCrops.GetProperty("umbracoWidth")?.GetValue();
                var height = mediaWithCrops.GetProperty("umbracoHeight")?.GetValue();

                var startX = (int)Math.Round(Convert.ToDouble(width).Percent((int)Math.Round(viewPort.X1 * 100, 0)));
                var startY = (int)Math.Round(Convert.ToDouble(height).Percent((int)Math.Round(viewPort.Y1 * 100, 0)));

                var endX = Math.Abs(
                    Math.Max((int)Math.Round(Convert.ToDouble(width).Percent((int)Math.Round(viewPort.X2 * 100, 0))), -Convert.ToDouble(width))
                    - Convert.ToDouble(width)
                    );
                var endY = Math.Abs(
                    Math.Max((int)Math.Round(Convert.ToDouble(height).Percent((int)Math.Round(viewPort.Y2 * 100, 0))), -Convert.ToDouble(height)) 
                    - Convert.ToDouble(height)
                    );

                query = $"/{startX}x{startY}:{endX}x{endY}/" + query;
            }

            if (cropConfig != null)
            {
                query += $"fit-in/{cropConfig.Width}x{cropConfig.Height}/";
            }

            query += mediaWithCrops.Url().Trim("/");

            return CloudFrontUrl + query;
        }

        public static string? GetUrlWebp(this MediaWithCrops mediaWithCrops, int quality = 80)
        {
            return CloudFrontUrl + $"fit-in/filters:quality({quality})/filters:format(webp)/" + mediaWithCrops.Url().Trim("/");
        }

        public static double Percent(this double number, int percent)
        {
            return ((double)number * percent) / 100;
        }
    }

}