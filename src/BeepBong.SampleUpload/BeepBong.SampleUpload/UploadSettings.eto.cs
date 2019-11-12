using System;
using Eto.Forms;
using Eto.Drawing;

namespace BeepBong.SampleUpload
{
	partial class UploadSettings : Dialog
	{
		void InitializeComponent()
		{
			Title = "BeepBong Settings";
			ClientSize = new Size(480, 140);
			Padding = 10;
            Location = new Point(600, 400);
            Resizable = false;

            // Commands
            var closeCommand = new Command();
            closeCommand.Executed += (sender, e) => Close();

            Content = new TableLayout
            {
                Spacing = new Size(0, 5),
                Rows =
                {
                    new TableRow
                    {
                        Cells = {new GroupBox
                        {
                            Text = "BeepBong API Settings",
                            Content = new TableLayout
                            {
                                Spacing = new Size(5,5),
                                Rows =
                                {
                                    new TableRow
                                    {
                                        Cells = {
                                            new TableCell
                                            {
                                                Control = new Label
                                                { Text = "URL", VerticalAlignment = VerticalAlignment.Center }
                                            },
                                            new TableCell
                                            {
                                                ScaleWidth = true,
                                                Control = new TextBox { PlaceholderText = "Where do I look?" }
                                            },
                                            new TableCell
                                            {
                                                Control = new Button { Text = "Test" }
                                            }
                                        }
                                    },
                                    new TableRow
                                    {
                                        Cells = {
                                            new TableCell
                                            {
                                                Control = new Label { Text = "API Key", VerticalAlignment = VerticalAlignment.Center }
                                            },
                                            new TableCell
                                            {
                                                ScaleWidth = true,
                                                Control = new PasswordBox()
                                            },
                                            new TableCell
                                            {
                                                Control = new Button { Text = "Save" }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        }
                    },
                    null,
                    new TableRow
                    {
                        Cells = {
                            new StackLayout
                            {
                                Items =
                                {
                                    new StackLayoutItem
                                    {
                                        HorizontalAlignment = HorizontalAlignment.Right,
                                        Control = new Button
                                        {
                                            Text = "OK",
                                            Command = closeCommand
                                        }
                                    }
                                }
                            }
                        }
                    }
					// add more controls here
				}
			};
		}
	}
}
