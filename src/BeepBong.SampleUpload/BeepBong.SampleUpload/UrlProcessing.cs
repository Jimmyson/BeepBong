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
                //response.EnsureSuccessStatusCode();
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

        private static string PostSample(string url, object sample, string key) // Object to Sample
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

        public static List<ListItem> FetchTracklists(string url)
        {
            url = url.TrimEnd('/');
            var data = FetchItem($"{url}/api/Tracklist/IdList");

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

        public static List<ListItem> FetchTracks(string url, string TracklistId)
        {
            url = url.TrimEnd('/');
            var data = FetchItem($"{url}/api/Tracklist/" + TracklistId);

            var tl = JsonConvert.DeserializeObject<TrackListDetailViewModel>(data);

            var list = tl.Tracks.Select(t => new ListItem() { ID = t.TrackId.ToString(), Value = t.Name }).ToList();

            return list;
        }

        public static bool SendSample(string url, SampleCreateViewModel sample, string key)
        {
            url = url.TrimEnd('/');
            try
            {
                PostSample($"{url}/api/Sample", sample, key);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool TeaTime(string url)
        {
            url = url.TrimEnd('/');
            var data = FetchItem($"{url}/api/report/teapot");

            return data != null;
        }
    }
}
