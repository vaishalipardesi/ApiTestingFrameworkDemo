//using ApiTestingFrameworkDemo.Tests.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;

namespace ApiTestingFrameworkDemo.Tests.Helper
{
    public class JsonHelper
    {
        public string ReadJsonFile(Table table)
        {
            string inputParameters = null;
            foreach (var row in table.Rows)
            {
                inputParameters = ReadJsonFile(row["FileName"]);  // reading json file
            }
            return inputParameters;
        }

        public string ReadJsonFile(string fileName) // Passing string (file Path) 
        {
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var fullPathofFile = Path.Combine(projectDirectory, @"" + fileName);

            string inputData = File.ReadAllText(fullPathofFile);
            return inputData;
        }

        public string BuildRequestURL(string requestUrl, string jsonInputParams)
        {
            if (jsonInputParams == null)
            {
                return requestUrl;
            }
            else
            {
                IDictionary<string, string> jsonInputCSharp = JsonConvert.DeserializeObject<IDictionary<string, string>>(jsonInputParams);
                List<string> stringValues = new List<string>();
                foreach (var item in jsonInputCSharp)
                {
                    if (!string.IsNullOrWhiteSpace(item.Value))
                    {
                        stringValues.Add(item.Key + "=" + item.Value);
                    }
                }
                return string.Format("{0}?{1}", requestUrl, string.Join("&", stringValues));
            }
        }
    }
}

