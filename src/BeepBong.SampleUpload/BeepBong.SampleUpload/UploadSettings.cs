using System;
using Eto.Forms;
using Eto.Drawing;

namespace BeepBong.SampleUpload
{
	public partial class UploadSettings : Dialog
	{
		public UploadSettings(IConfig config)
		{
            Config = config;
			InitializeComponent();

            URLInput.Text = Config.GetURL() ?? "";
            APIInput.Text = Config.GetAPI();

            SaveURLButton.Click += (sender, e) =>
            {
                var url = URLInput.Text;

                if (UrlProcessing.TeaTime(url))
                {
                    Config.SetURL(url);
                    MessageBox.Show("A very good afternoon from BBC2, where it's time to... Put the kettle on.", MessageBoxType.Information); //https://www.youtube.com/watch?v=SNbLkVl-xNY
                }
                else
                {
                    MessageBox.Show("Unable to poll URL. Check that you can access the site.", MessageBoxType.Warning);
                }
            };
        }
	}
}
