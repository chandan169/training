using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogApi.Models;
using System.Text;

namespace CatalogApi.CustomFormatters
{
    public class CsvOutputFormatter:TextOutputFormatter
    {
        public CsvOutputFormatter()
        { 
            //this.SupportedEncodings(SupportedEncodings())
        }

        protected override bool CanWriteType(Type type)
        {
            if(typeof(CatalogItem).IsAssignableFrom(type) || typeof(IEnumerable<CatalogItem>).IsAssignableFrom(type))
            {
                return true;
            }
            return false;
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            //write code to convert catalogitem type to csv
            var buffer = new StringBuilder();
            var response = context.HttpContext.Response;
            if(context.Object is CatalogItem)
            {
                var item = context.Object as CatalogItem;
                buffer.Append("Id,Name,Price,Quantity,ReorderLevel,MnufacturingDate,ImageUrl"+ Environment.NewLine);
                //buffer.Append(item.Id, item.Name, item.Price, item.Quantity, item.ReorderLevel, item.ManufacturingDate, item.ImageUrl);
            }
        }
    }
}
