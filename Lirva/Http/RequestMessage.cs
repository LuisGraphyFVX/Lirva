using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Lirva.Http
{
    public static class RequestMessage
    {
        public static void SetBasicAuthentication(this HttpRequestMessage httpRequestMessage, string username, string password)
        {
            var Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Credentials);
        }
        public static void SetBearerToken(this HttpRequestMessage httpRequestMessage, string token)
        {
            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
        public static void AddHeader(this HttpRequestMessage httpRequestMessage, string name, string value)
        {
            httpRequestMessage.Headers.TryAddWithoutValidation(name, value);
        }
        public static void AddHeader(this HttpRequestMessage httpRequestMessage, IDictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                httpRequestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }
    }
}
