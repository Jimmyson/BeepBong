using BeepBong.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace BeepBong.SampleUpload
{
    internal class DataInteraction : IConfigReader
    {
        public IConfig Config { get; }
        public ObservableCollection<Item> FileCollection { get; }
        public List<ListItem> Tracks { get; private set; }
        public List<ListItem> Tracklists { get; private set; }

        public DataInteraction(IConfig config)
        {
            Config = config;
            FileCollection = new ObservableCollection<Item>();
            Tracklists = new List<ListItem>()
            {
                new ListItem()
                {
                    ID = null,
                    Value = "No Lists found.."
                }
            };
        }

        public void AddFileToCollection(IEnumerable<string> files)
        {
            foreach (var filePath in files)
            {
                FileCollection.Add(new Item
                {
                    ID = FileCollection.Count + 1,
                    FilePath = filePath,
                    FileName = filePath.Substring(filePath.LastIndexOf(Path.DirectorySeparatorChar) + 1)
                });
            }
        }

        public void ScanFile(Item i)
        {
            string hash = null;
            SampleCreateViewModel model = null;

            hash = AudioProcessing.ProcessFile(i.FilePath);
            model = AudioProcessing.CreateSample(i.FilePath);

            if (hash != null)
            {
                model.Fingerprint = hash;

                i.Scanned = true;

                i.Fingerprint = hash;
                i.Sample = model;
            }
            else
            {
                i.Status = "Unable to process the file.";
            }

            FileCollection[i.ID - 1] = i;
        }

        public void UploadSample(Item i)
        {
            i.Sample.TrackId = Guid.Parse(i.Track);

            i.Uploaded = UrlProcessing.SendSample(Config.GetURL(), i.Sample, Config.GetAPI(), out string statusMessage);
            i.Status = statusMessage;

            FileCollection[i.ID - 1] = i;
        }

        public void GetTracklists()
        {
            Tracklists = UrlProcessing.FetchTracklists(Config.GetURL());
        }

        public void GetTracks(string trackListId)
        {
            Tracks = UrlProcessing.FetchTracks(Config.GetURL(), trackListId);
        }

        public string GetURL()
        {
            return Config.GetURL();
        }

        public string GetAPI()
        {
            return Config.GetAPI();
        }

        public bool IsConfigSetup()
        {
            return Config.IsConfigSetup();
        }
    }
}
