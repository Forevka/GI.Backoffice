using Clean.Site.Models.ViewData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Polly;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Clean.Site.Controllers
{
    public class AuthorController : UmbracoPageController, IVirtualPageController
    {
        private readonly IPublishedValueFallback _publishedValueFallback;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IUserService _userService;

        public AuthorController(
            ILogger<AuthorController> logger,
            ICompositeViewEngine compositeViewEngine, 
            IPublishedValueFallback publishedValueFallback,
            IUmbracoContextAccessor umbracoContextAccessor,
            IUserService userService)
            : base(logger, compositeViewEngine)
        {
            _publishedValueFallback = publishedValueFallback;
            _umbracoContextAccessor = umbracoContextAccessor;
            _userService = userService;
        }

        [Route("[controller]/{authorId}")]
        [HttpGet]
        public IActionResult Index(int authorId)
        {
            // CurrentPage (IPublishedContent) will be the content returned
            // from the FindContent method.

            // return the view with the IPublishedContent
            var author = _userService.GetUserById(authorId);

            ViewBag.Author = author;

            return View("~/Views/Author.cshtml", CurrentPage);
        }

        public IPublishedContent? FindContent(ActionExecutingContext actionExecutingContext)
        {
            IUmbracoContext context = _umbracoContextAccessor.GetRequiredUmbracoContext();

            var authorId = actionExecutingContext.ActionArguments.GetValue("authorId");

            if (authorId != null)
            {
                return context.Content?.GetById(Guid.Parse("c606cba8-dfeb-4db5-99be-e297710fe601"));
            }

            return null;
        }
    }
}