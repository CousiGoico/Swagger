using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Swagger
{
    public class SwaggerClass
    {
        #region Properties 

        [JsonPropertyName("openapi")]
        public string OpenAPI { get; set; }

        [JsonPropertyName("info")]
        public SwaggerInfo Info { get; set; }

        [JsonPropertyName("servers")]
        public List<SwaggerServer> Servers { get; set; }

        [JsonPropertyName("paths")]
        public SwaggerPaths Paths { get; set; }

        [JsonPropertyName("components")]
        public SwaggerComponents Components { get; set; }

        #endregion

        #region Static methods

        public static List<MethodClass> ProcessMethods() {
            var swaggerLines = File.ReadAllLines(@"c:\temp\Swagger.json");
            bool append = false;
            var methodLines = new List<string>();
            var methods = new List<MethodLine>();
            var methodList = new List<MethodClass>();
            foreach (var item in swaggerLines)
            {
                if (item.Contains("paths")) {
                    append = true;
                }
                if (item.ToLower().Contains("\"components\": {"))
                {
                    append = false;
                }
                if (append) {
                    methodLines.Add(item);
                }
            }
            for (var line = 0; line < methodLines.Count; line++) 
            {
                var item = methodLines[line];
                bool checkIsMethod = item.Trim().StartsWith("\"/") && item.Split("/").Length >= 1 && item.EndsWith(": {");
                if (checkIsMethod || item.Contains("/api/") || item.Contains("/app")) {
                    methods.Add(new MethodLine() { Name = item, Start = line + 1 });
                }
            }
            for (var methodNumber = 0; methodNumber < methods.Count - 1; methodNumber++)
            {
                var method = methods[methodNumber];
                method.End = methods[methodNumber + 1].Start - 2;
                string methodContent = string.Join(" ", methodLines.GetRange(method.Start, method.End - method.Start).ToArray());
                if (string.IsNullOrEmpty(methodContent) == false) {
                    Console.WriteLine($"MethodNumber: {methodNumber}");
                    MethodClass methodDefinition = JsonConvert.DeserializeObject<MethodClass>("{" + methodContent + "}");
                    methodDefinition.path = method.Name.Trim().Replace(": {", string.Empty).Replace("\"", string.Empty).Trim();
                    methodDefinition.path = methodDefinition.path.Replace("{", ":").Replace("}", string.Empty);
                    methodList.Add(methodDefinition);
                }
            }
            return methodList;
        }

        #endregion
    }

    public class SwaggerInfo
    {

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }
    }

    public class SwaggerServer {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class SwaggerPaths {     

    }

    public class SwaggerComponents {
        [JsonPropertyName("schemas")]
        public SwaggerSchemas Schemas { get; set; }
    }

    public class SwaggerSchemas {
        [JsonPropertyName("*")]
        public SwaggerType SwaggerType { get; set; }

        public string SwaggerTypeName { get; set; }
    }

    public class SwaggerType {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("properties")]
        public SwaggerProperties Properties { get; set; }
    }

    public class SwaggerProperties { 
    
    }



}
