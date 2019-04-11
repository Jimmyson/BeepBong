using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using BeepBong.Domain.Models;
using BeepBong.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.App.XmlSeed
{
    class Program
    {
        static void Main(string[] args)
        {
			var options = new DbContextOptionsBuilder<BeepBongContext>()
				.UseSqlite("Data Source=../BeepBong.Web/BeepBong.db")
				.Options;

			if (args.Length != 1) {
				throw new ApplicationException("Missing file parameter");
			}
			if (!args[0].ToLower().EndsWith(".xml")) {
				throw new ApplicationException("Not an XML file");
			}
			if(!File.Exists(args[0])) {
				throw new ApplicationException("File not found");
			} else {
            	XDocument xml = XDocument.Load(args[0]);

				// Programmes and Tracks
				foreach (var programme in xml.Element("root").Element("Programmes").Elements()) {
					//Console.WriteLine(programme.Attribute("name").Value);
					
					Programme p = new Programme() ;

					if (programme.HasAttributes) {
						foreach (var attribute in programme.Attributes()) {
							if (attribute.Name == "name")
								p.Name = attribute.Value;
							if (attribute.Name == "year")
								p.Year = attribute.Value;
							if (attribute.Name == "channel")
								p.Channel = attribute.Value;
							if (attribute.Name == "composer")
								p.AudioComposer = attribute.Value;
							if (attribute.Name == "library")
								p.IsLibraryMusic = bool.Parse(attribute.Value);
						};
					}

					foreach (var track in programme.Elements()) {
						Track t = new Track() {
							Name = track.Value
						};
						if (track.HasAttributes) {
							foreach (var attribute in track.Attributes()) {
							if (attribute.Name == "subtitle")
								t.Subtitle = attribute.Value;
							}
						}
						p.Tracks.Add(t);
					}

					//Console.WriteLine("Tracks: " + p.Tracks.Count);

					using (var context = new BeepBongContext(options)) {
						context.Programmes.Add(p);
						context.SaveChanges();
					}
				}

				// Libraries
				foreach (var library in xml.Element("root").Element("Libraries").Elements())
				{
					var l = new Library();

					if (library.HasAttributes) {
						foreach (var attribute in library.Attributes()) {
							if (attribute.Name == "name")
								l.AlbumName = attribute.Value;
							if (attribute.Name == "catalog")
								l.Catalog = attribute.Value;
						};
					}

					using (var context = new BeepBongContext(options)) {
						context.Library.Add(l);
						context.SaveChanges();
					} 
				}
				
				Console.ReadLine();
			}
        }
    }
}
