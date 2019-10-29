using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace AspNetMVC.TagHelpers
{
    [HtmlTargetElement("image", TagStructure= TagStructure.WithoutEndTag)]
    public class ImageTaghelper:TagHelper
    {
        public string Url { get; set; }
        public string FallbackUrl { get; set; }
        private IHttpContextAccessor httpContextAccessor;

        public ImageTaghelper(IHttpContextAccessor contextAccessor)
        {
            httpContextAccessor = contextAccessor;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output )
        {
            output.TagName = "img";
            output.TagMode = TagMode.SelfClosing;
            HttpClient client = new HttpClient();
            if(Url.StartsWith("http")|| Url.StartsWith("https"))
            {
                var webUrl = new Uri(Url);
                client.BaseAddress = new Uri($"{webUrl.Scheme}://{webUrl.Host}");
                Url = webUrl.LocalPath;
            }
            else
            {
                var baseUrl = httpContextAccessor.HttpContext.Request.GetDisplayUrl();
                client.BaseAddress = new Uri(baseUrl);
            }

            using(HttpResponseMessage response = await client.GetAsync(Url))
            {
                if (response.IsSuccessStatusCode)
                    output.Attributes.SetAttribute("src", Url);
                else
                    output.Attributes.SetAttribute("src", FallbackUrl);
            }
        }
    }
}
