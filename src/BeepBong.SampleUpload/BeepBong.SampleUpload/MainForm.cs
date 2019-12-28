using System;
using Eto.Forms;
using Eto.Drawing;

namespace BeepBong.SampleUpload
{
	public partial class MainForm : Form
	{
		public MainForm(IConfig config)
		{
            di = new DataInteraction(config);

            InitializeComponent();
		}
	}
}
