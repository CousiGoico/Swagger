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
        var swaggerFilePath = @"c:\temp\Swagger.json";
        var swaggerText = File.ReadAllText(swaggerFilePath);
        var swaggerClass = JsonConvert.DeserializeObject<SwaggerClass>(swaggerText);

        if (swaggerClass != null) {
            var swaggerToPostman = new SwaggerToPostman(swaggerFilePath);
            var methods = swaggerToPostman.ProcessMethods();
            var json = swaggerToPostman.CreatePostmanJson(swaggerClass, methods);

            File.WriteAllText(@"c:\temp\collection_swagger.json", json);
        }
        
    }   
}