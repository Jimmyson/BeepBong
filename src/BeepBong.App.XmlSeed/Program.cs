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

				foreach (var programme in xml.Elements().First().Elements()) {
					Console.WriteLine(programme.Attribute("name").Value);
					
					Programme p = new Programme() {
						Name = programme.Attribute("name").Value,
						Year = programme.Attribute("year").Value
					};

					foreach (var track in programme.Elements()) {
						Track t = new Track() {
							Name = track.Value
						};
						if (track.HasAttributes)
							t.Subtitle = track.Attribute("subtitle").Value;

						p.Tracks.Add(t);
					}

					Console.WriteLine("Tracks: " + p.Tracks.Count);

					using (var context = new BeepBongContext(options)) {
						context.Programmes.Add(p);
						context.SaveChanges();
					}
				}
				
				Console.ReadLine();
			}
        }
    }
}
