using BeepBong.Application.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BeepBong.SampleUpload
{
    public static class UrlProcessing
    {
        private static string FetchItem(string url)
        {
            HttpClient client = new HttpClient();
            string responseBody = null;

            try
            {
                HttpResponseMessage response = client.GetAsync(new Uri(url)).Result;

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                responseBody = response.Content.ReadAsStringAsync().Result;
                response.EnsureSuccessStatusCode();

                return responseBody;
            }
            catch (Exception e)
            {
                if (responseBody != null && responseBody == "I'm a teapot") // Save the teapot from an accidental shatter...
                    return responseBody;

                throw e;
            }
            finally
            {
                client.Dispose();
            }
        }

        private static HttpStatusCode PostSample(string url, object sample, string key) // Object to Sample
        {
            HttpClient client = new HttpClient();

            try
            {
                StringContent request = new StringContent(JsonConvert.SerializeObject(sample));
                request.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = client.PostAsync(new Uri(url), request).Result;

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response.StatusCode;
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

        public static bool SendSample(string url, SampleCreateViewModel sample, string key, out string statusResponse)
        {
            url = url.TrimEnd('/');
            try
            {
                HttpStatusCode code = PostSample($"{url}/api/Sample", sample, key);

                switch (code)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.Created:
                    case HttpStatusCode.Accepted:
                    case HttpStatusCode.NoContent:
                    case HttpStatusCode.Found:
                        statusResponse = "Sample Successfully Uploaded";
                        return true;

                    // Error Codes to handle
                    case HttpStatusCode.BadRequest:
                        statusResponse = "Unable to process the sample";
                        return false;
                    case HttpStatusCode.Unauthorized:
                        statusResponse = "You do not have access";
                        return false;
                    case HttpStatusCode.Conflict:
                        statusResponse = "Record already exists";
                        return false;
                }

                statusResponse = "Something happened that wasn't expected. HTTP Code: " + code;
                return false;
            }
            catch (Exception e)
            {
                statusResponse = "Unable to send the sample to the server. " + e.Message;
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
