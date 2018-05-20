using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi.ApiFolder
{

    [ModelBinder(typeof(DataSourceLoadOptionsMvcBinder))]
    public class AspNetDevextremeDataSourceLoader: DataSourceLoadOptionsBase {
    }
    internal class DataSourceLoadOptionsMvcBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            AspNetDevextremeDataSourceLoader sourceLoadOptions = new AspNetDevextremeDataSourceLoader();
            DataSourceLoadOptionsParser.Parse((DataSourceLoadOptionsBase)sourceLoadOptions, (Func<string, string>)(key =>
            {
                ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(key);
                if (valueProviderResult == null)
                    return (string)null;
                return valueProviderResult.FirstValue;
            }));
            return (object)sourceLoadOptions;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext) {
            AspNetDevextremeDataSourceLoader sourceLoadOptions = new AspNetDevextremeDataSourceLoader();
            DataSourceLoadOptionsParser.Parse((DataSourceLoadOptionsBase)sourceLoadOptions, (Func<string, string>)(key =>
            {
                ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(key);
                if (valueProviderResult == null)
                    return (string)null;
                return valueProviderResult.FirstValue;
            }));
            bindingContext.Model = sourceLoadOptions;
            bindingContext.Result= ModelBindingResult.Success(sourceLoadOptions);
            return Task.CompletedTask;
        }
    }
}
