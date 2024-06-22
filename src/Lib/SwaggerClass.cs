using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Swagger
{
    public class SwaggerClass
    {
        #region Properties 

        [JsonPropertyName("openapi")]
        public string OpenAPI { get; set; } = string.Empty;

        [JsonPropertyName("info")]
        public SwaggerInfo? Info { get; set; } = null;

        [JsonPropertyName("servers")]
        public List<SwaggerServer>? Servers { get; set; } = null;   

        [JsonPropertyName("paths")]
        public SwaggerPaths? Paths { get; set; } = null;

        [JsonPropertyName("components")]
        public SwaggerComponents? Components { get; set; } = null;

        #endregion
    }

    public class SwaggerInfo
    {

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;
    }

    public class SwaggerServer {
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }

    public class SwaggerPaths {     

    }

    public class SwaggerComponents {
        [JsonPropertyName("schemas")]
        public SwaggerSchemas? Schemas { get; set; } = null;
    }

    public class SwaggerSchemas {
        [JsonPropertyName("*")]
        public SwaggerType? SwaggerType { get; set; } = null;

        public string SwaggerTypeName { get; set; } = string.Empty;
    }

    public class SwaggerType {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("properties")]
        public SwaggerProperties? Properties { get; set; } = null;
    }

    public class SwaggerProperties { 
    
    }



}
