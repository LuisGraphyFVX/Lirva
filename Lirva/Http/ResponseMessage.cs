using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Linq;

namespace Lirva.Http
{
    public static class ResponseMessage
    {
        public static async Task<T> DeserializeJsonAsync<T>(this HttpResponseMessage httpResponseMessage, JsonSerializerOptions jsonSerializerOptions = null)
        {
            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(responseString, jsonSerializerOptions);
        }
        public static string GetCookies(this HttpResponseMessage httpResponseMessage, string name, RegexOptions options = RegexOptions.None)
        {
            string setCookieHeader = httpResponseMessage.Headers.ToString()
            .Split('\n')
            .FirstOrDefault(h => h.StartsWith("set-cookie: ", StringComparison.OrdinalIgnoreCase));

            if (setCookieHeader == null)
                return null;

            string cookie = Regex.Match(setCookieHeader, $"{name}=(.+?);", options).Groups[1].Value;

            return cookie == string.Empty ? null : cookie;
        }
    }
}
