using Umbraco.Cms.Core.Models.Membership;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Clean.Site.Models.ViewData
{
    public class AuthorContent
    {
        public IUser Author { get; set; }
        public IEnumerable<ContentPage> Content {get;set;}
    }
}
