using Microsoft.Extensions.Configuration;

namespace ApiTestingFrameworkDemo.Support
{
    public static class DictionaryExtension
    {
        public static Dictionary<string, string> GetDictionary(IConfiguration configuration, string sectionName)
        {
            var dictionary = new Dictionary<string, string>();
            var section = configuration.GetSection(sectionName);

            foreach (var child in section.GetChildren())
            {
                dictionary[child.Key] = child.Value;
            }

            return dictionary;
        }

        public static object Get(this Dictionary<object, object> dictionary, object key)
        {
            object value = null;
            if (dictionary.ContainsKey(key))
            {
                value = dictionary[key];
            }
            else
            {
                Console.WriteLine($"Key [{key.ToString()}] not found.");
            }
            return value;
        }

        public static T Get<T>(this Dictionary<object, object> dictionary, object key)
        {
            if (dictionary.TryGetValue(key, out object value))
            {
                if (value is T result)
                {
                    return result;
                }
                else
                {
                    Console.WriteLine($"Value for key [{key.ToString()}] is not of type {typeof(T).Name}.");
                }
            }
            else
            {
                Console.WriteLine($"Key [{key.ToString()}] not found.");
            }
            return default;
        }

        public static void AddResponseJson(this Dictionary<object, object> dictionary, HttpResponseMessage responseMessage)
        {

            var responseJson = responseMessage.Content.ReadAsStringAsync().Result;
            var responseStatus = (int)responseMessage.StatusCode;

            int responseCounter = (int)dictionary.Get("responseCounter");
            responseCounter++;
            if (dictionary.TryGetValue("response", out object responseValue))
            {
                dictionary.Add($"response{responseCounter}", responseValue);
                dictionary.Remove("response");
                dictionary.Add("response", responseJson);
            }
            else
            {
                dictionary.Add("response", responseJson);
            }
            dictionary.Remove("responseCounter");
            dictionary.Add("responseCounter", responseCounter);
            dictionary.Remove("statusCode");
            dictionary.Add("statusCode", responseStatus);
        }

        public static void AddOrReplace(this Dictionary<object, object> dictionary, string key, object value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        public static object GetResponseJson(this Dictionary<object, object> dictionary)
        {
            return dictionary.Get("response");
        }

        public static object GetResponseStatusCode(this Dictionary<object, object> dictionary)
        {
            return dictionary.Get("statusCode");
        }
    }
}
