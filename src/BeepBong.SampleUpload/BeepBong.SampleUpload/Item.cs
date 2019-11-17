using System;
using System.Collections.Generic;
using System.Text;

namespace BeepBong.SampleUpload
{
    public class Item
    {
        public int ID { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Track { get; set; }
        public string Fingerprint { get; set; }
        public bool Scanned { get; set; }
        public bool Uploaded { get; set; }
        public string Status { get; set; }
    }

    public class ListItem
    {
        public string ID { get; set; }
        public string Value { get; set; }
    }
}
