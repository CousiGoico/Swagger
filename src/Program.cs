

using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Newtonsoft.Json;

namespace Swagger;

class Program
{
    static void Main(string[] args)
    {
        ReadSwaggerJson();
        Console.WriteLine("Fichero generado correctamente");
    }

    static void ReadSwaggerJson() {
        var swaggerText = File.ReadAllText(@"c:\temp\Swagger.json");
        var swaggerClass = JsonConvert.DeserializeObject<SwaggerClass>(swaggerText);

        var methods = ProcessMethods();
        var json = createPostmanJson(swaggerClass, methods);
        //ProcessJson(swaggerText);
        //ProcessJsonByLines();

        File.WriteAllText(@"c:\temp\collection_swagger.json", json);
    }

    static string createPostmanJson(SwaggerClass swaggerClass, List<MethodClass> methods) {
        var postmanCollection = new PostmanCollection();
        postmanCollection.Info = new PostmanInfo() { Name = swaggerClass.Info?.Title };
        postmanCollection.Item = new List<PostmanItem>();
        methods.ForEach(method =>
        {
            string host = swaggerClass.Servers.FirstOrDefault()?.Url;
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

        });
        return JsonConvert.SerializeObject(postmanCollection);
    }

    static PostmanItem createPostmanItem(string verb, string path, MethodDefinition method, string host) {

        var listHeaders = new List<PostmanHeader>();
        //if (method.parameters != null && method.parameters.Any()) {
        //    method.parameters.ToList().ForEach(parameter => {
        //        listHeaders.Add(new PostmanHeader() { Key = parameter.name, Type = "Text" });
        //    });
        //}        

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

    static List<MethodClass> ProcessMethods() {
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
            if (item.Contains("/api/") || item.Contains("/app")) {
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

    static void ProcessJson(string swaggerText) {
        var swaggerTextAux = swaggerText.Substring(swaggerText.IndexOf("\"schemas\":") + "\"schemas\": {".Length);
        var method = swaggerTextAux.Substring(swaggerTextAux.IndexOf("\"") + 1);
        method = method.Substring(0, method.IndexOf("\""));

        Console.WriteLine($"Method: {method}");

    }

    static void ProcessJsonByLines() {
        var swaggerLines = File.ReadAllLines(@"c:\temp\Swagger.json");
        bool checkSchemas = false;
        bool checkPaths = false;
        int counterSpaces = 0;

        foreach (var line in swaggerLines)
        {

            if (checkPaths == true) {
                var letters = line.ToCharArray();
                int spaces = 0;
                for (var i = 0; i < letters.Length; i++) { 
                    var letter = letters[i];
                    if (Convert.ToInt32(letter) != 32) {
                        spaces = line.Substring(0, i).Length;
                        break;
                    }
                }
                Console.WriteLine($"Spaces: {spaces} - línea: {line}");
            }

            //if (checkSchemas == true) {
            //    Console.WriteLine(line);
            //    foreach (char item in line)
            //    {
            //        Console.Write(System.Convert.ToInt32(item));
            //    }

            //}

            if (line.Contains("\"schemas")) {
                checkSchemas = true;
                checkPaths = false;
            }
            if (line.Contains("\"paths"))
            {
                checkPaths = true;
            }
        }
    }
}