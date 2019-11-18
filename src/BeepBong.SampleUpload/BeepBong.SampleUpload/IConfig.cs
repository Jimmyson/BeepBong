using System;
using System.Collections.Generic;
using System.Text;

namespace BeepBong.SampleUpload
{
    public interface IConfig : IConfigReader
    {
        void SetURL(string url);
        void SetAPI(string key);
    }

    public interface IConfigReader
    {
        string GetURL();
        string GetAPI();
    }
}
