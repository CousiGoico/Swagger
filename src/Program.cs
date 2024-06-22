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

        var swaggerToPostman = new SwaggerToPostman(swaggerFilePath);
        var methods = swaggerToPostman.ProcessMethods();
        var json = swaggerToPostman.CreatePostmanJson(swaggerClass, methods);

        File.WriteAllText(@"c:\temp\collection_swagger.json", json);
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