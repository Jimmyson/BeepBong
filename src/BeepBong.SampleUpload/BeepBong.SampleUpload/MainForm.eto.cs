using System;
using Eto.Forms;
using Eto.Drawing;
using System.Linq;

namespace BeepBong.SampleUpload
{
	partial class MainForm : Form
    {
        static string[] supportedExtensions = { ".mp3", ".m4a", ".wav", ".flac", ".aiff", ".aif", ".wma" };

        // Controls
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            MultiSelect = true,
            Filters = { new FileFilter("Audio Files", supportedExtensions) }
        };

        Button clearListButton = new Button { Text = "Clear List", Enabled = false };
        Button selectFileButton = new Button { Text = "Add Files..." };
        Button selectFolderButton = new Button { Text = "Folder" };
        Button scanFilesButton = new Button { Text = "Scan", Enabled = false };
        Button uploadEntityButton = new Button { Text = "Upload", Enabled = false };
        TextArea tracks = new TextArea { ReadOnly = true, Wrap = false };

        Button listReloadButton = new Button { Text = "Reload" };

        DropDown TracklistSelector = new DropDown
        {
            ItemKeyBinding = Binding.Property((ListItem i) => i.ID),
            ItemTextBinding = Binding.Property((ListItem i) => i.Value),
            SelectedIndex = 0
        };

        static ComboBoxCell TrackSelector = new ComboBoxCell
        {
            //DataStore = Tracks,
            ComboKeyBinding = Binding.Property((ListItem i) => i.ID),
            ComboTextBinding = Binding.Property((ListItem i) => i.Value),
            Binding = Binding.Property<Item, object>(p => p.Track),
        };

        GridView grid = new GridView
        {
            //DataStore = FileCollection,
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
                    DataCell = TrackSelector,
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
                new GridColumn
                {
                    HeaderText = "Status",
                    DataCell = new TextBoxCell { Binding = Binding.Property<Item, string>(p => p.Status) }
                },
            },
        };

        void InitializeComponent()
        {
            TracklistSelector.SelectedIndex = 0;

            Title = "BeepBong Sample Upload";
			ClientSize = new Size(860, 500);
			Padding = 10;

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
                                                    Control = TracklistSelector
                                                },
                                                listReloadButton
                                            }
                                        },
                                        tracks
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
                                                selectFileButton,
                                                selectFolderButton,
                                                new StackLayoutItem
                                                {
                                                    Expand = true
                                                },
                                                clearListButton
                                            }
                                        },
                                        new TableRow
                                        {
                                            ScaleHeight = true,
                                            Cells = {
                                                grid
                                            }
                                        },
                                        new StackLayout
                                        {
                                            Orientation = Orientation.Horizontal,
                                            VerticalContentAlignment = VerticalAlignment.Center,
                                            Spacing = 5,
                                            Items =
                                            {
                                                new StackLayoutItem
                                                {
                                                    Expand = true
                                                },
                                                scanFilesButton,
                                                uploadEntityButton
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
			//var clickMe = new Command { MenuText = "Click Me!", ToolBarText = "Click Me!" };
			//clickMe.Executed += (sender, e) => MessageBox.Show(this, "I was clicked!");

			var quitCommand = new Command { MenuText = "Quit", Shortcut = Eto.Forms.Application.Instance.CommonModifier | Keys.Q };
			quitCommand.Executed += (sender, e) => Eto.Forms.Application.Instance.Quit();

			var aboutCommand = new Command { MenuText = "About..." };
			aboutCommand.Executed += (sender, e) => new AboutDialog().ShowDialog(this);

            var preferenceCommand = new Command();
            preferenceCommand.Executed += (sender, e) => {
                //new UploadSettings(di.Config).ShowModal();
            };

			// create menu
			Menu = new MenuBar
			{
				Items =
				{
					// File submenu
					//new ButtonMenuItem { Text = "&File", Items = { clickMe } },
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
