//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;

//namespace WebApi.Helpers
//{
//    public class JsonNetResult : Json
//    {
//        private const string DateFormat = "yyyy-MM-dd HH:mm:ss";

//        public JsonNetResult(object data) : base(data) {
//            this.Value = data;
//        }
//        public override void ExecuteResult(ControllerContext context)
//        {
//            if (context == null)
//            {
//                throw new ArgumentNullException("context");
//            }

//            var response = context.HttpContext.Response;
//            if (!String.IsNullOrEmpty(this.ContentType))
//            {
//                response.ContentType = this.ContentType;
//            }
//            else
//            {
//                response.ContentType = "application/json";
//            }
//            if (this.ContentEncoding != null)
//            {
//                response.ContentEncoding = this.ContentEncoding;
//            }
//            if (this.Data != null)
//            {
//                // Using Json.NET serializer
//                var isoConvert = new IsoDateTimeConverter { DateTimeFormat = DateFormat };
//                response.Write(JsonConvert.SerializeObject(this.Data, isoConvert));
//            }
//        }
//    }
//    public class DateFormatConverter : IsoDateTimeConverter
//    {
//        public DateFormatConverter(string format)
//        {
//            this.DateTimeFormat = format;
//        }
//    }
//}

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

//public class JsonNetFilterAttribute : ActionFilterAttribute
//{
//    public override void OnActionExecuted(ActionExecutedContext filterContext)
//    {
//        if (filterContext.Result is JsonResult == false)
//        {
//            return;
//        }

//        filterContext.Result = new JsonNetResult(
//            (JsonResult)filterContext.Result);
//    }

//    private class JsonNetResult : JsonResult
//    {
//        public JsonNetResult(JsonResult jsonResult) : base(jsonResult) {
//            this.ContentType = jsonResult.ContentType;
//            this.Value = jsonResult.Value;
//        }

//        public override void ExecuteResult(ActionContext context)
//        {
//            if (context == null)
//            {
//                throw new ArgumentNullException("context");
//            }

//            var isMethodGet = string.Equals(
//                context.HttpContext.Request.Method,
//                "GET",
//                StringComparison.OrdinalIgnoreCase);

//            if (this.SerializerSettings.DateFormatHandling. == JsonRequestBehavior.DenyGet
//                && isMethodGet)
//            {
//                throw new InvalidOperationException(
//                    "GET not allowed! Change JsonRequestBehavior to AllowGet.");
//            }

//            var response = context.HttpContext.Response;

//            response.ContentType = string.IsNullOrEmpty(this.ContentType)
//                ? "application/json"
//                : this.ContentType;

//            if (this.ContentEncoding != null)
//            {
//                response.ContentEncoding = this.ContentEncoding;
//            }

//            if (this.Data != null)
//            {
//                response.Write(JsonConvert.SerializeObject(this.Data));
//            }
//        }
//    }
//}