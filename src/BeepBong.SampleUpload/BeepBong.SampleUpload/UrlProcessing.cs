using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BeepBong.SampleUpload
{
    public static class UrlProcessing
    {
        private static async Task<string> FetchItemAsync(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(new Uri(url));

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        private static async Task<string> PostSample(string url, object sample) // Object to Sample
        {
            HttpClient client = new HttpClient();

            StringContent request = new StringContent(JsonConvert.SerializeObject(sample));
            request.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await client.PostAsync(new Uri(url), request);

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        public class Tracklist {
            public Guid trackListId;
            public string name;
        }

        public static Dictionary<Guid, string> FetchTracklists()
        {

            //var data = FetchItem("http://localhost:5001/api/Tracklists/IdList");

            //var list = JsonConvert.DeserializeObject<List<Tracklist>>(data);

            var list = new List<Tracklist>()
            {
                new Tracklist{
                    trackListId = Guid.Empty,
                    name = "Select a Item"
                },
                new Tracklist
                {
                    trackListId = Guid.NewGuid(),
                    name = "fdfkld"
                },
                new Tracklist
                {
                    trackListId = Guid.NewGuid(),
                    name = "fdfkld"
                },
                new Tracklist
                {
                    trackListId = Guid.NewGuid(),
                    name = "fdfkld"
                },
                new Tracklist
                {
                    trackListId = Guid.NewGuid(),
                    name = "fdfkld"
                },
                new Tracklist
                {
                    trackListId = Guid.NewGuid(),
                    name = "fdfkld"
                },
            };

            return list.ToDictionary(d => d.trackListId, d => d.name);
        }

        public static List<ListItem> FetchTracks(string TracklistId)
        {
            return new List<ListItem>()
            {
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
                new ListItem() { ID = "hjsdskd", Value = "kjdksjdjdsd" },
            };
        }
    }
}
