using System.Web;

namespace Umbraco.Helpers
{
    public static class QueryStringHelper
    {
        public static int GetIntFromQueryString(HttpRequestBase request, string key, int fallbackValue)
        {
            var stringValue = request.QueryString[key];
            if(stringValue != null && !string.IsNullOrWhiteSpace(stringValue) && int.TryParse(stringValue, out var numericValue))
            {
                return numericValue;
            }

            return fallbackValue;
        }

    }
}