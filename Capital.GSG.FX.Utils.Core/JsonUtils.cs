using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Capital.GSG.FX.Utils.Core
{
    public static class JsonUtils
    {
        public static (bool IsValid, string ValidationMessage) IsValidJson(string value)
        {
            value = value.Trim();

            if ((value.StartsWith("{") && value.EndsWith("}")) || //For object
                (value.StartsWith("[") && value.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(value);
                    return (true, "");
                }
                catch (JsonReaderException ex)
                {
                    return (false, ex.Message);
                }
                catch (Exception ex) //some other exception
                {
                    return (false, ex.Message);
                }
            }
            else
                return (false, "The Json string should start with '{' or '['");
        }

        public static bool TryParseJson(string value, out JToken jToken)
        {
            value = value.Trim();

            if ((value.StartsWith("{") && value.EndsWith("}")) || //For object
                (value.StartsWith("[") && value.EndsWith("]"))) //For array
            {
                try
                {
                    jToken = JToken.Parse(value);
                    return true;
                }
                catch (Exception) { }
            }

            jToken = null;
            return false;
        }
    }
}
