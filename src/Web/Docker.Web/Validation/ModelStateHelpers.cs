using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Dcoker.Web.Validation
{
    public static class ModelStateHelpers
    {
        public static string SerializeModelState(ModelStateDictionary modelState)
        {
            var errorList = modelState
                .Select(kvp => new ModelStateTransferValue
                {
                    Key = kvp.Key,
                    AttemptedValue = kvp.Value.AttemptedValue,
                    RawValue = kvp.Value.RawValue,
                    ErrorMessages = kvp.Value.Errors.Select(err => err.ErrorMessage).ToList(),
                });

            return JsonConvert.SerializeObject(errorList);
        }

        public static ModelStateDictionary DeserializeModelState(string serialisedErrorList)
        {
            var errorList = JsonConvert.DeserializeObject<List<ModelStateTransferValue>>(serialisedErrorList);
            var modelState = new ModelStateDictionary();

            foreach (var item in errorList)
            {
                // CheckBoxFor generates a checkbox and a hidden field with the default value
                // This results in string[] when serializing. Because RawValue is an object,
                // JSON.NET will deserialize it as JArray, which breaks the model binder.
                // To fix this, we will cast JArray back to string[].
                if (item.RawValue is JArray jArray)
                {
                    item.RawValue = jArray.ToObject<string[]>();
                }

                modelState.SetModelValue(item.Key, item.RawValue, item.AttemptedValue);
                foreach (var error in item.ErrorMessages)
                {
                    modelState.AddModelError(item.Key, error);
                }
            }

            return modelState;
        }
    }
}