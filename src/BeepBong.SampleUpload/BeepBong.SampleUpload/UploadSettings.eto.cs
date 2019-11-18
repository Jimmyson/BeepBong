using System;
using Eto.Forms;
using Eto.Drawing;

namespace BeepBong.SampleUpload
{
	partial class UploadSettings : Dialog
	{
        public IConfig Config;

		void InitializeComponent()
		{
			Title = "BeepBong Settings";
			ClientSize = new Size(480, 140);
			Padding = 10;
            Location = new Point(600, 400);
            Resizable = false;

            // Input
            var URLInput = new TextBox { PlaceholderText = "Where do I look?", Text = Config?.GetURL() ?? "" };
            var SaveURLButton = new Button { Text = "Test" };
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


            var APIInput = new PasswordBox { Text = Config?.GetAPI() };
            var SaveAPIButton = new Button { Text = "Save" };
            SaveURLButton.Click += (sender, e) =>
            {
                var key = APIInput.Text;

                // if (UrlProcessing.TeaTime(key))
                // {
                //     Config.SetAPI(key);
                //     MessageBox.Show("A very good afternoon from BBC2, where it's time to... Put the kettle on.", MessageBoxType.Information); //https://www.youtube.com/watch?v=SNbLkVl-xNY
                // }
                // else
                // {
                //     MessageBox.Show("Unable to poll URL. Check that you can access the site.", MessageBoxType.Warning);
                // }
            };

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
                                                Control = URLInput
                                            },
                                            new TableCell
                                            {
                                                Control = SaveURLButton
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
                                                Control = APIInput
                                            },
                                            new TableCell
                                            {
                                                Control = SaveAPIButton
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
