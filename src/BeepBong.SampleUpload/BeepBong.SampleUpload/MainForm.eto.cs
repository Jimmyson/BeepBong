using System;
using Eto.Forms;
using Eto.Drawing;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace BeepBong.SampleUpload
{
    public class Item
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public object Track { get; set; }
        public string Fingerprint { get; set; }
        public bool Scanned { get; set; }
        public bool Uploaded { get; set; }
    }

	partial class MainForm : Form
	{
		void InitializeComponent()
		{
			Title = "BeepBong Sample Upload";
			ClientSize = new Size(860, 500);
			Padding = 10;

            var list = new List<object>()
            {
                "One",
                "Two",
                "Three",
            };

            var collection = new ObservableCollection<Item>();
            collection.Add(new Item
            {
                ID = 1,
                FileName = "Sample_Audio_320k.mp3",
                Track = "Two",
                Fingerprint = "hfjhfdfjnvds",
                Scanned = true,
                Uploaded = false
            });
            collection.Add(new Item
            {
                ID = 2,
                FileName = "Sample_Audio_320k.mp3",
                Track = "Two",
                Fingerprint = "hfjhfdfjnvds",
                Scanned = true,
                Uploaded = false
            });
            collection.Add(new Item
            {
                ID = 3,
                FileName = "Sample_Audio_320k.mp3",
                Track = "Two",
                Fingerprint = "hfjhfdfjnvds",
                Scanned = true,
                Uploaded = false
            });

            Content = new TableLayout()
            {
                Spacing = new Size(5, 0),
                Rows =
                {
                    new TableRow
                    {
                        Cells = {
                            // LEFT PANEL
                            new Panel
                            {
                                Content = new TableLayout()
                                {
                                    Width = 200,
                                    Spacing = new Size(0, 5),
                                    Rows =
                                    {
                                        new StackLayout
                                        {
                                            Spacing = 5,
                                            Orientation = Orientation.Horizontal,
                                            VerticalContentAlignment = VerticalAlignment.Center,
                                            Items =
                                            {
                                                new Label
                                                {
                                                    Text = "Tracklist"
                                                },
                                                new StackLayoutItem
                                                {
                                                    Expand = true,
                                                    Control = new DropDown
                                                    {
                                                    }
                                                }
                                            }
                                        },
                                        new ListBox()
                                    }
                                }
                            },
                            // RIGHT PANEL
                            
                            new Panel
                            {
                                Content = new TableLayout()
                                {
                                    Spacing = new Size(0, 5),
                                    Rows =
                                    {
                                        new StackLayout
                                        {
                                            Spacing = 5,
                                            Orientation = Orientation.Horizontal,
                                            VerticalContentAlignment = VerticalAlignment.Center,
                                            Items =
                                            {
                                                new Button
                                                {
                                                    Text = "File"
                                                },
                                                new Button
                                                {
                                                    Text = "Folder"
                                                },
                                                new StackLayoutItem
                                                {
                                                    Expand = true
                                                },
                                                new Button
                                                {
                                                    Text = "Clear List"
                                                }
                                            }
                                        },
                                        new TableRow
                                        {
                                            ScaleHeight = true,
                                            Cells = {
                                                new GridView
                                                {
                                                    DataStore = collection,
                                                    Columns =
                                                    {
                                                        new GridColumn
                                                        {
                                                            HeaderText = "ID",
                                                            DataCell = new TextBoxCell { Binding = Binding.Property<Item, string>(p => p.ID.ToString()) }
                                                        },
                                                        new GridColumn
                                                        {
                                                            HeaderText = "File Name",
                                                            DataCell = new TextBoxCell { Binding = Binding.Property<Item, string>(p => p.FileName) },
                                                            Editable = true
                                                        },
                                                        new GridColumn
                                                        {
                                                            HeaderText = "Track",
                                                            DataCell = new ComboBoxCell {
                                                                DataStore = list,
                                                                Binding = Binding.Property<Item, object>(r => r.Track)
                                                            },
                                                            Editable = true
                                                        },
                                                        new GridColumn
                                                        {
                                                            HeaderText = "Fingerprint",
                                                            DataCell = new TextBoxCell { Binding = Binding.Property<Item, string>(p => p.Fingerprint) }
                                                        },
                                                        new GridColumn
                                                        {
                                                            HeaderText = "Scanned",
                                                            DataCell = new CheckBoxCell { Binding = Binding.Property<Item, bool?>(p => p.Scanned) }
                                                        },
                                                        new GridColumn
                                                        {
                                                            HeaderText = "Uploaded",
                                                            DataCell = new CheckBoxCell { Binding = Binding.Property<Item, bool?>(p => p.Uploaded) }
                                                        },
                                                    },
                                                }
                                            }
                                        },
                                        new StackLayout
                                        {
                                            Orientation = Orientation.Horizontal,
                                            VerticalContentAlignment = VerticalAlignment.Center,
                                            Spacing = 5,
                                            Items =
                                            {
                                                new Label
                                                {
                                                    Text = "I tell you things",
                                                    TextColor = Color.FromGrayscale(float.Parse("0.5")),
                                                },
                                                new StackLayoutItem
                                                {
                                                    Expand = true
                                                },
                                                new Button
                                                {
                                                    Text = "Scan"
                                                },
                                                new Button
                                                {
                                                    Text = "Upload"
                                                }
                                            }
                                        },
                                    }
                                }
                            }
                        }
                    }
                }
            };

			// create a few commands that can be used for the menu and toolbar
			var clickMe = new Command { MenuText = "Click Me!", ToolBarText = "Click Me!" };
			clickMe.Executed += (sender, e) => MessageBox.Show(this, "I was clicked!");

			var quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
			quitCommand.Executed += (sender, e) => Application.Instance.Quit();

			var aboutCommand = new Command { MenuText = "About..." };
			aboutCommand.Executed += (sender, e) => new AboutDialog().ShowDialog(this);

            var preferenceCommand = new Command();
            preferenceCommand.Executed += (sender, e) => new UploadSettings().ShowModal();

			// create menu
			Menu = new MenuBar
			{
				Items =
				{
					// File submenu
					new ButtonMenuItem { Text = "&File", Items = { clickMe } },
					// new ButtonMenuItem { Text = "&Edit", Items = { /* commands/items */ } },
					// new ButtonMenuItem { Text = "&View", Items = { /* commands/items */ } },
				},
				ApplicationItems =
				{
					// application (OS X) or file menu (others)
					new ButtonMenuItem { Text = "&Preferences...", Command = preferenceCommand },
				},
				QuitItem = quitCommand,
				AboutItem = aboutCommand
			};

			// create toolbar			
			//ToolBar = new ToolBar { Items = { clickMe } };
		}
	}
}
