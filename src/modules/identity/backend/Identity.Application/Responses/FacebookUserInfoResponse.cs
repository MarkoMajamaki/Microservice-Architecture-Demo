using System;
using Newtonsoft.Json;

namespace Identity.Application;

public partial class FacebookUserInfoResponse
{
    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonProperty("picture")]
    public Picture Picture { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }
}

public partial class Picture
{
    [JsonProperty("data")]
    public Data Data { get; set; }
}

public partial class Data
{
    [JsonProperty("height")]
    public long Height { get; set; }

    [JsonProperty("is_silhouette")]
    public bool IsSilhouette { get; set; }

    [JsonProperty("url")]
    public Uri Url { get; set; }

    [JsonProperty("width")]
    public long Width { get; set; }
}

public partial class FacebookUserInfoResponse
{
    public static FacebookUserInfoResponse FromJson(string json) {
        return JsonConvert.DeserializeObject<FacebookUserInfoResponse>(json, Identity.Application.Converter.Settings);
    } 
}

public static class SerializeFacebookUserInfoResponse
{
    public static string ToJson(this FacebookUserInfoResponse self) {
        return JsonConvert.SerializeObject(self, Identity.Application.Converter.Settings);
    }
}