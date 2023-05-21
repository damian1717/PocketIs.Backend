using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using PocketIS.Infrastucture.Validation;
using PocketIS.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace PocketIS.Services
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of a razor view to string renderer
    /// </summary>
    public class RenderRazorToStringService : IRenderRazorToStringService
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="razorViewEngine"></param>
        /// <param name="tempDataProvider"></param>
        /// <param name="serviceProvider"></param>
        public RenderRazorToStringService(IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider)
        {
            Check.NotNull(_razorViewEngine = razorViewEngine, nameof(razorViewEngine));
            Check.NotNull(_tempDataProvider = tempDataProvider, nameof(tempDataProvider));
            Check.NotNull(_serviceProvider = serviceProvider, nameof(serviceProvider));
        }

        /// <inheritdoc />
        public async Task<string> RenderToStringAsync(string viewName, object model, bool htmlDecode = false)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                var viewResult = _razorViewEngine.FindView(actionContext, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext).ConfigureAwait(false);

                return htmlDecode ?
                    HttpUtility.HtmlDecode(sw.ToString())
                    : sw.ToString();
            }
        }
    }
}
