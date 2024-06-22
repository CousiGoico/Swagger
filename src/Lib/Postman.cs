using Newtonsoft.Json;

namespace Swagger{

    public class MethodLine {
        public string Name { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
    }

    public class MethodClass {
        public string path { get; set; }
        public MethodDefinition get { get; set; }
        public MethodDefinition post { get; set; }
        public MethodDefinition put { get; set; }
        public MethodDefinition update { get; set; }
        public MethodDefinition delete { get; set; }
    }


    public class MethodDefinition {
        public string[] tags { get; set; }
        public string summary { get; set; }
        public MethodParameter[] parameters { get; set; }
        //public MethodResponse[] responses { get; set; }
        public MethodSecurity[] security { get; set; }
    }

    public class MethodParameter {
        public string name { get; set; }
        public string @in { get; set; }
        public string description { get; set; }
        public bool required { get; set; }
        public MethodSchema schema { get; set; }

    }

    public class MethodSchema {
        public string type { get; set; }
        public string format { get; set; }
    }

    public class MethodResponses {
        public MethodResponse Ok { get; set; }
        public MethodResponse BadRequest { get; set; }
        public MethodResponse Notfound { get; set; }
        public MethodResponse InternalServerError { get; set; }
    }

    public class MethodResponse {
        public string description { get; set; }
    }

    public class MethodContent {
        public MethodContentType type { get; set; }
    }

    public class MethodContentType { 

    }

    public class MethodSecurity {

    }

    public class PostmanCollection {

        #region Properties 

        [JsonProperty("info")]
        public PostmanInfo Info { get; set; }

        [JsonProperty("item")]
        public List<PostmanItem> Item { get; set; } = new List<PostmanItem>();

        #endregion
    }

    public class PostmanInfo {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("schema")]
        public string Schema { get; set; } = "\"https://schema.getpostman.com/json/collection/v2.1.0/collection.json\"";
    }

    public class PostmanItem {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("request")]
        public PostmanRequest Request { get; set; } = new PostmanRequest();

        [JsonProperty("response")]
        public List<PostmanRresponse> Response { get; set; } = new List<PostmanRresponse>();
    }

    public class PostmanRresponse { 

    }

    public class PostmanRequest {
        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("header")]
        public List<PostmanHeader> Header { get; set; }

        [JsonProperty("url")]
        public PostmanUrl Url { get; set; }

        [JsonProperty("auth")]
        public PostmanAuth Auth { get; set; }

        [JsonProperty("body")]
        public PostmanBody Body { get; set; }
    }

    public class PostmanBody
    {
        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("urlencoded")]
        public List<PostmanHeader> Urlencoded { get; set; }

        [JsonProperty("raw")]
        public string Raw { get; set; }

        [JsonProperty("Options")]
        public string Options { get; set; }
    }

    public class PostmanBodyOptions
    {
        [JsonProperty("raw")]
        public PostmanLanguage Raw { get; set; }
    }

    public class PostmanLanguage {
        [JsonProperty("language")]
        public string Language { get; set; }
    }

    public class PostmanAuth {
        [JsonProperty("type")]
        public string Type { get; set; } = "noauth";
    }

    public class PostmanHeader {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class PostmanUrl {
        [JsonProperty("raw")]
        public string Raw { get; set; }

        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        [JsonProperty("host")]
        public List<string> Host { get; set; }

        [JsonProperty("path")]
        public List<string> Path { get; set; }

        [JsonProperty("query")]
        public List<PostmanQuery> Query { get; set; }
    }

    public class PostmanQuery {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }
    }
}