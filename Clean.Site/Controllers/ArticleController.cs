using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Clean.Site.Controllers
{
    public class ArticleController : UmbracoPageController, IVirtualPageController
    {
        private readonly IPublishedValueFallback _publishedValueFallback;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public ArticleController(
            ILogger<ArticleController> logger,
            ICompositeViewEngine compositeViewEngine, 
            IPublishedValueFallback publishedValueFallback,
            IUmbracoContextAccessor umbracoContextAccessor)
            : base(logger, compositeViewEngine)
        {
            _publishedValueFallback = publishedValueFallback;
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        [Route("[controller]")]
        [HttpGet]
        public IActionResult Index(int page = 1, int pageSize = 6)
        {
            // CurrentPage (IPublishedContent) will be the content returned
            // from the FindContent method.

            // return the view with the IPublishedContent
            var articlePage = CurrentPage?
                .Children()
                .Where(x => x.IsVisible())
                .OrderByDescending(x => x.CreateDate);


            return PartialView("~/Views/Partials/articles/grid/default.cshtml", articlePage);
        }

        public IPublishedContent? FindContent(ActionExecutingContext actionExecutingContext)
        {
            IUmbracoContext context = _umbracoContextAccessor.GetRequiredUmbracoContext();
            var content = context.Content?.GetById(Guid.Parse("c606cba8-dfeb-4db5-99be-e297710fe601"));

            return content;
        }
    }
}