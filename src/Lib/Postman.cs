using Newtonsoft.Json;

namespace Swagger{

    public class MethodLine {
        public string Name { get; set; } = string.Empty;
        public int Start { get; set; } = 0;
        public int End { get; set; } = 0;
    }

    public class MethodClass {
        public string path { get; set; } = string.Empty;
        public MethodDefinition? get { get; set; } = null;
        public MethodDefinition? post { get; set; } = null;
        public MethodDefinition? put { get; set; } = null;
        public MethodDefinition? update { get; set; } = null;
        public MethodDefinition? delete { get; set; } = null;
    }


    public class MethodDefinition {
        public string[]? tags { get; set; } = null;
        public string summary { get; set; } = string.Empty;
        public MethodParameter[]? parameters { get; set; } = null;
        public MethodSecurity[]? security { get; set; } = null;
    }

    public class MethodParameter {
        public string name { get; set; } = string.Empty;
        public string @in { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public bool required { get; set; } = false;
        public MethodSchema? schema { get; set; } = null; 
    }

    public class MethodSchema {
        public string type { get; set; } = string.Empty;
        public string format { get; set; } = string.Empty;
    }

    public class MethodResponses {
        public MethodResponse? Ok { get; set; } = null;
        public MethodResponse? BadRequest { get; set; } = null;
        public MethodResponse? Notfound { get; set; } = null;
        public MethodResponse? InternalServerError { get; set; } = null;
    }

    public class MethodResponse {
        public string description { get; set; } = string.Empty;
    }

    public class MethodContent {
        public MethodContentType? type { get; set; } = null;
    }

    public class MethodContentType { 

    }

    public class MethodSecurity {

    }

    public class PostmanCollection {

        #region Properties 

        [JsonProperty("info")]
        public PostmanInfo? Info { get; set; } = null;

        [JsonProperty("item")]
        public List<PostmanItem>? Item { get; set; } = null;

        #endregion
    }

    public class PostmanInfo {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        [JsonProperty("schema")]
        public string Schema { get; set; } = "\"https://schema.getpostman.com/json/collection/v2.1.0/collection.json\"";
    }

    public class PostmanItem {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("request")]
        public PostmanRequest? Request { get; set; } = null;

        [JsonProperty("response")]
        public List<PostmanRresponse>? Response { get; set; } = null;
    }

    public class PostmanRresponse { 

    }

    public class PostmanRequest {
        [JsonProperty("method")]
        public string Method { get; set; } = string.Empty;

        [JsonProperty("header")]
        public List<PostmanHeader>? Header { get; set; } = null;

        [JsonProperty("url")]
        public PostmanUrl? Url { get; set; } = null;

        [JsonProperty("auth")]
        public PostmanAuth? Auth { get; set; } = null;

        [JsonProperty("body")]
        public PostmanBody? Body { get; set; } = null;
    }

    public class PostmanBody
    {
        [JsonProperty("mode")]
        public string Mode { get; set; } = string.Empty;

        [JsonProperty("urlencoded")]
        public List<PostmanHeader>? Urlencoded { get; set; } = null;

        [JsonProperty("raw")]
        public string Raw { get; set; } = string.Empty;

        [JsonProperty("Options")]
        public string Options { get; set; } = string.Empty;
    }

    public class PostmanBodyOptions
    {
        [JsonProperty("raw")]
        public PostmanLanguage? Raw { get; set; } = null;
    }

    public class PostmanLanguage {
        [JsonProperty("language")]
        public string Language { get; set; } = string.Empty;
    }

    public class PostmanAuth {
        [JsonProperty("type")]
        public string Type { get; set; } = "noauth";
    }

    public class PostmanHeader {
        [JsonProperty("key")]
        public string Key { get; set; } = string.Empty;

        [JsonProperty("value")]
        public string Value { get; set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
    }

    public class PostmanUrl {
        [JsonProperty("raw")]
        public string Raw { get; set; } = string.Empty;

        [JsonProperty("protocol")]
        public string Protocol { get; set; } = string.Empty;

        [JsonProperty("host")]
        public List<string>? Host { get; set; } = null;

        [JsonProperty("path")]
        public List<string>? Path { get; set; } = null;

        [JsonProperty("query")]
        public List<PostmanQuery>? Query { get; set; } = null;
    }

    public class PostmanQuery {
        [JsonProperty("key")]
        public string Key { get; set; } = string.Empty;

        [JsonProperty("value")]
        public string Value { get; set; } = string.Empty;

        [JsonProperty("disabled")]
        public bool Disabled { get; set; } = false;
    }
}