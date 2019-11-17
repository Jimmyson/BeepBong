using System;
using Eto.Forms;
using Eto.Drawing;

namespace BeepBong.SampleUpload.Desktop
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			new Eto.Forms.Application(Eto.Platform.Detect).Run(new MainForm());
		}
	}
}