using System.Linq;
using System.Xml.Linq;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Application
{
    /// <summary>
    /// Importing and Exporting BeepBong database via an XML document
    /// </summary>
    public static class XMLTranslate
    {
        /// <summary>
        /// Import data into the specified BeepBong Database
        /// </summary>
        /// <param name="xml">Loaded XML document to be imported</param>
        /// <param name="options">Database options for a BeepBong database instance</param>
        public static void ImportData(XDocument xml, DbContextOptions<BeepBongContext> options)
        {
            // Programmes and Tracks
            foreach (var programme in xml.Element("root").Element("Programmes").Elements())
            {
                Programme p = new Programme();

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
                        if (attribute.Name == "logo")
                            p.Logo = attribute.Value;
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
                        if (attribute.Name == "description")
                            t.Description = attribute.Value;

                        //Fall back name value
                        if (attribute.Name == "title" && string.IsNullOrEmpty(t.Name))
                            t.Name = attribute.Value;
                        }
                    }
                    p.Tracks.Add(t);
                }

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
                        if (attribute.Name == "label")
                            l.Label = attribute.Value;
                        if (attribute.Name == "mbid")
                            l.MBID = attribute.Value;
                    };
                }

                using (var context = new BeepBongContext(options)) {
                    context.Libraries.Add(l);
                    context.SaveChanges();
                } 
            }
        }

        /// <summary>
        /// XML document with the Database information
        /// </summary>
        /// <param name="options">Database options for a BeepBong database instance</param>
        /// <returns>An XDocument object with the Database, ready to be saved</returns>
        public static XDocument ExportData(DbContextOptions<BeepBongContext> options)
        {
            XDocument xdoc = null;

            // Establish a Database Connection
            using (var context = new BeepBongContext(options)) {
                // Prepare Database Programmes with Tracks
                var programmeData = context.Programmes
                                    .Include(p => p.Tracks)
                                    .Select(p => new {
                                        Name = p.Name,
                                        Year = p.Year,
                                        Channel = p.Channel,
                                        Composer = p.AudioComposer,
                                        Library = p.IsLibraryMusic,
                                        Logo = p.Logo,
                                        Tracks = p.Tracks.Select(t => new {
                                            Title = t.Name,
                                            Subtitle = t.Subtitle,
                                            Description = t.Description
                                        })
                                    })
                                    .ToList();

                // Prepare Library Items
                var libraryData = context.Libraries
                                        .Select(l => new {
                                            Name = l.AlbumName,
                                            Catalog = l.Catalog,
                                            Label = l.Label,
                                            MBID = l.MBID
                                        })
                                        .ToList();

                // Create the XML object
                xdoc = new XDocument(
                            new XElement("root", 
                                new XElement("Programmes", programmeData.Select(
                                    d => new XElement("Programme", 
                                            (d.Name != null) ? new XAttribute("name", d.Name) : null, 
                                            (d.Year != null) ? new XAttribute("year", d.Year) : null, 
                                            (d.Channel != null) ? new XAttribute("channel", d.Channel) : null, 
                                            (d.Composer != null) ? new XAttribute("composer", d.Composer) : null, 
                                            (d.Library == true) ? new XAttribute("library", d.Library) : null, 
                                            (d.Logo != null) ? new XAttribute("logo", d.Logo) : null, 
                                            (d.Tracks.Count() > 0) ? d.Tracks.Select(t => 
                                                new XElement("Track", 
                                                    (t.Title != null) ? new XAttribute("title", t.Title) : null, 
                                                    (t.Subtitle != null) ? new XAttribute("subtitle", t.Subtitle) : null, 
                                                    (t.Description != null) ? new XAttribute("description", t.Description) : null
                                                )
                                            ) : null
                                        )
                                    )
                                ),
                                new XElement("Libraries", libraryData.Select(
                                    ld => new XElement("Library",
                                        (ld.Name != null) ? new XAttribute("name", ld.Name) : null,
                                        (ld.Catalog != null) ? new XAttribute("catalog", ld.Catalog) : null,
                                        (ld.Label != null) ? new XAttribute("label", ld.Label) : null,
                                        (ld.MBID != null) ? new XAttribute("mbid", ld.MBID) : null
                                    )
                                ))
                            )
                        );
            }

            return xdoc;
        }
    }
}