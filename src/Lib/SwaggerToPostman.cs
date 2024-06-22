
using Newtonsoft.Json;

namespace Swagger {

    public class SwaggerToPostman {

        #region Properties

        private string swaggerFilePath {get;} = string.Empty;

        #endregion

        public SwaggerToPostman(string swaggerFilePath){
            this.swaggerFilePath = swaggerFilePath;
        }

        #region Methods

        public List<MethodClass> ProcessMethods() {
            var swaggerLines = File.ReadAllLines(this.swaggerFilePath);
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
                    methodContent = "{" + methodContent + "}";
                    MethodClass? methodDefinition = JsonConvert.DeserializeObject<MethodClass>(methodContent);
                    if (methodDefinition != null) {
                        methodDefinition.path = method.Name.Trim().Replace(": {", string.Empty).Replace("\"", string.Empty).Trim();
                        methodDefinition.path = methodDefinition.path.Replace("{", ":").Replace("}", string.Empty);
                        methodList.Add(methodDefinition);
                    }
                }
            }
            return methodList;
        }

        public string CreatePostmanJson(SwaggerClass swaggerClass, List<MethodClass> methods) {
            var postmanCollection = new PostmanCollection();
            postmanCollection.Info = new PostmanInfo() { Name = swaggerClass.Info == null ? string.Empty : swaggerClass.Info.Title };
            postmanCollection.Item = new List<PostmanItem>();
            methods.ForEach(method =>
            {
                if (swaggerClass.Servers != null) {
                    SwaggerServer? swaggerServer = swaggerClass.Servers.FirstOrDefault();
                    if (swaggerServer != null) {
                        string host = swaggerServer.Url;
                        if (method.get != null) {
                            postmanCollection.Item.Add(createPostmanItem("GET", method.path, method.get, host));
                        }
                        if (method.post != null)
                        {
                            postmanCollection.Item.Add(createPostmanItem("POST", method.path, method.post, host));
                        }
                        if (method.update != null)
                        {
                            postmanCollection.Item.Add(createPostmanItem("UPDATE", method.path, method.update, host));
                        }
                        if (method.put != null)
                        {
                            postmanCollection.Item.Add(createPostmanItem("PUT", method.path, method.put, host));
                        }
                        if (method.delete != null)
                        {
                            postmanCollection.Item.Add(createPostmanItem("DELETE", method.path, method.delete, host));
                        }

                    }
                }
            });
            return JsonConvert.SerializeObject(postmanCollection);
        }

        public static PostmanItem createPostmanItem(string verb, string path, MethodDefinition method, string host) {

            var listHeaders = new List<PostmanHeader>();    

            return new PostmanItem()
            {
                Name = path,
                Request = new PostmanRequest()
                {
                    Method = verb,
                    Url = new PostmanUrl()
                    {
                        Host = new List<string>() { host },
                        Path = path.Substring(1).Split("/").ToList(),
                        Protocol = string.Empty,
                        Raw = $"{host}{path}"
                    },
                    Header = listHeaders
                }
            };
        }

        #endregion

    }

}