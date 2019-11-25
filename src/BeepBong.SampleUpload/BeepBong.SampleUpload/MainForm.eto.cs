using System;
using Eto.Forms;
using Eto.Drawing;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BeepBong.Application.ViewModels;

namespace BeepBong.SampleUpload
{
	partial class MainForm : Form
	{
        // Mutable Items
        ObservableCollection<Item> FileCollection;
        List<ListItem> Tracklists = new List<ListItem>()
        {
            new ListItem()
            {
                ID = null,
                Value = "No Lists found.."
            }
        };

        List<ListItem> Tracks;

        protected IConfig Config;

        // Controls
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            MultiSelect = true,
            Filters = {
                new FileFilter("Audio Files", ".mp3", ".m4a", ".wav", ".flac", ".aiff", ".aif", ".wma")
            }
        };

        Label statusLabel = new Label { Text = "I'll tell you things", TextColor = Color.FromGrayscale(float.Parse("0.5")) };
        Button clearListButton = new Button { Text = "Clear List", Enabled = false };
        Button selectFileButton = new Button { Text = "Add Files..." };
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

        /// <summary>
        /// Add files from the system to the File Collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HandleFiles(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.Ok)
            {
                MessageBox.Show(string.Concat(openFileDialog.Filenames));
                foreach (var filePath in openFileDialog.Filenames)
                {
                    FileCollection.Add(new Item
                    {
                        ID = FileCollection.Count + 1,
                        FilePath = filePath,
                        FileName = filePath.Substring(filePath.LastIndexOf(Path.DirectorySeparatorChar) + 1)
                    });
                }
            }
        }

        /// <summary>
        /// Process audio files in the collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async Task ScanFilesAsync(object sender, EventArgs e)
        {
            grid.Enabled = false;

            foreach (Item i in FileCollection.Where(f => !f.Scanned).ToList())
            {
                string hash = null;
                SampleCreateViewModel model = null;

                await Task.Run(() =>
                {
                    // MessageBox.Show("Scanned " + i.FileName);
                    hash = AudioProcessing.ProcessFile(i.FilePath);
                    model = AudioProcessing.CreateSample(i.FilePath);
                });

                if (hash != null)
                {
                    model.Fingerprint = hash;

                    i.Scanned = true;

                    i.Fingerprint = hash;
                    i.Sample = model;
                }
                else
                {
                    i.Status = "Unable to process the file.";
                }

                FileCollection[i.ID - 1] = i;
                grid.ReloadData(i.ID - 1);
            }

            grid.Enabled = true;
        }

        /// <summary>
        /// Process files in the collection to be sent to the cloud
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async Task UploadFilesAsync(object sender, EventArgs e)
        {
            grid.Enabled = false;

            foreach (Item i in FileCollection.Where(f => !f.Uploaded && f.Scanned && f.Track != null && f.Sample != null).ToList())
            {
                bool uploadSuccess = false;

                await Task.Run(() =>
                {
                    i.Sample.TrackId = Guid.Parse(i.Track);
                    //MessageBox.Show("Uploading " + i.FileName);
                    uploadSuccess = UrlProcessing.SendSample(Config.GetURL(), i.Sample, Config.GetAPI());
                });

                i.Status = (uploadSuccess ? "Sample Successfully Uploaded" : "Unable to send the sample");

                i.Uploaded = uploadSuccess;
                FileCollection[i.ID - 1] = i;
                grid.ReloadData(i.ID - 1);
            }

            grid.Enabled = true;
        }

        void InitializeComponent()
        {
            // Instancate Collections
            FileCollection = new ObservableCollection<Item>();
            grid.DataStore = FileCollection;

            if (Config.IsConfigSetup())
            {
                Tracklists = UrlProcessing.FetchTracklists(Config.GetURL());
                TracklistSelector.Enabled = true;
            }
            else
            {
                TracklistSelector.Enabled = false;
            }

            TracklistSelector.DataStore = Tracklists;
            TracklistSelector.SelectedIndex = 0;

            // Bind Actions
            clearListButton.Click += (sender, e) => FileCollection.Clear();
            selectFileButton.Click += HandleFiles;
            scanFilesButton.Click += (sender, e) => ScanFilesAsync(sender, e).ConfigureAwait(false); ;
            uploadEntityButton.Click += (sender, e) => UploadFilesAsync(sender, e).ConfigureAwait(false);

            listReloadButton.Click += (sender, e) =>
            {
                if (Config.GetURL() != null)
                {
                    Tracklists = UrlProcessing.FetchTracklists(Config.GetURL());
                    TracklistSelector.DataStore = Tracklists;
                    TracklistSelector.SelectedIndex = 0;
                    TracklistSelector.Enabled = true;
                }
                else
                {
                    TracklistSelector.Enabled = false;
                }
            };

            TracklistSelector.DropDownClosed += (sender, e) =>
            {
                if (TracklistSelector.SelectedKey != null && TracklistSelector.SelectedKey != Guid.Empty.ToString())
                {
                    Tracks = UrlProcessing.FetchTracks(Config.GetURL(), TracklistSelector.SelectedKey);
                    tracks.Text = string.Concat(Tracks.Select(t => t.Value + Environment.NewLine).ToList());

                    TrackSelector.DataStore = Tracks;
                }
            };

            FileCollection.CollectionChanged += (sender, e) =>
            {
                clearListButton.Enabled = (FileCollection.Count > 0);
                scanFilesButton.Enabled = (FileCollection.Count != 0);
                uploadEntityButton.Enabled = (FileCollection.Any(item => item.Scanned && item.Track != null));
            };

            grid.CellEdited += (sender, e) =>
            {
                uploadEntityButton.Enabled = (FileCollection.Any(item => item.Scanned && item.Track != null));
            };

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
                                                // new Button
                                                // {
                                                //     Text = "Folder"
                                                // },
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
                                                new Spinner
                                                {
                                                    Enabled = true
                                                },
                                                statusLabel,
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
                new UploadSettings(Config).ShowModal();
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
