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
		}
	}
}
