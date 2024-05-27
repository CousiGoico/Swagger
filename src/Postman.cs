 namespace Swagger{

class MethodLine {
    public string Name { get; set; }
    public int Start { get; set; }
    public int End { get; set; }
}

class MethodClass {
    public string path { get; set; }
    public MethodDefinition get { get; set; }
    public MethodDefinition post { get; set; }
    public MethodDefinition put { get; set; }
    public MethodDefinition update { get; set; }
    public MethodDefinition delete { get; set; }
}


class MethodDefinition {
    public string[] tags { get; set; }
    public string summary { get; set; }
    public MethodParameter[] parameters { get; set; }
    //public MethodResponse[] responses { get; set; }
    public MethodSecurity[] security { get; set; }
}

class MethodParameter {
    public string name { get; set; }
    public string @in { get; set; }
    public string description { get; set; }
    public bool required { get; set; }
    public MethodSchema schema { get; set; }

}

class MethodSchema {
    public string type { get; set; }
    public string format { get; set; }
}

class MethodResponses {
    public MethodResponse Ok { get; set; }
    public MethodResponse BadRequest { get; set; }
    public MethodResponse Notfound { get; set; }
    public MethodResponse InternalServerError { get; set; }
}

class MethodResponse {
    public string description { get; set; }
}

class MethodContent {
    public MethodContentType type { get; set; }
}

class MethodContentType { 

}

class MethodSecurity {

}

class PostmanCollection {

    [JsonProperty("info")]
    public PostmanInfo Info { get; set; }

    [JsonProperty("item")]
    public List<PostmanItem> Item { get; set; }

}

class PostmanInfo {
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("schema")]
    public string Schema { get; set; } = "\"https://schema.getpostman.com/json/collection/v2.1.0/collection.json\"";
}

class PostmanItem {
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("request")]
    public PostmanRequest Request { get; set; }

    [JsonProperty("response")]
    public List<PostmanRresponse> Response { get; set; }
}

class PostmanRresponse { 

}

class PostmanRequest {
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

class PostmanBody
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

class PostmanBodyOptions
{
    [JsonProperty("raw")]
    public PostmanLanguage Raw { get; set; }
}

class PostmanLanguage {
    [JsonProperty("language")]
    public string Language { get; set; }
}

class PostmanAuth {
    [JsonProperty("type")]
    public string Type { get; set; } = "noauth";
}

class PostmanHeader {
    [JsonProperty("key")]
    public string Key { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
}

class PostmanUrl {
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

class PostmanQuery {
    [JsonProperty("key")]
    public string Key { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }

    [JsonProperty("disabled")]
    public bool Disabled { get; set; }
}
}