using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Swagger
{
    public class SwaggerClass
    {
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
