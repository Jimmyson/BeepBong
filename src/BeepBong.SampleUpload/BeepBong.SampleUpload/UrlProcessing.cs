using BeepBong.Application.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BeepBong.SampleUpload
{
    public static class UrlProcessing
    {
        private static string FetchItem(string url)
        {
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = client.GetAsync(new Uri(url)).Result;

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;

                return responseBody;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                client.Dispose();
            }
        }

        private static string PostSample(string url, object sample) // Object to Sample
        {
            HttpClient client = new HttpClient();

            try
            {
                StringContent request = new StringContent(JsonConvert.SerializeObject(sample));
                request.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = client.PostAsync(new Uri(url), request).Result;

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;

                return responseBody;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                client.Dispose();
            }
        }

        public class Tracklist {
            public string id;
            public string name;
        }

        public static List<ListItem> FetchTracklists()
        {

            var data = FetchItem("http://localhost:54026/api/Tracklist/IdList");

            var tls = JsonConvert.DeserializeObject<List<Tracklist>>(data);

            var list = new List<ListItem>()
            {
                new ListItem()
                {
                    ID = Guid.Empty.ToString(),
                    Value = "Select a Tracklist..."
                }
            };

            list.AddRange(tls.Select(t => new ListItem() { ID = t.id, Value = t.name }).ToList());

            return list;
        }

        public static List<ListItem> FetchTracks(string TracklistId)
        {
            var data = FetchItem("http://localhost:54026/api/Tracklist/" + TracklistId);

            var tl = JsonConvert.DeserializeObject<TrackListDetailViewModel>(data);

            var list = tl.Tracks.Select(t => new ListItem() { ID = t.TrackId.ToString(), Value = t.Name }).ToList();

            return list;
        }

        public static bool SendSample(SampleCreateViewModel sample)
        {
            try
            {
                PostSample("http://localhost:54026/api/Sample", sample);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
