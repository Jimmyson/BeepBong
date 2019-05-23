using System;
using System.Collections.Generic;
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
        private static Dictionary<Guid, string> programmeIds = new Dictionary<Guid, string>();
        private static Dictionary<Guid, string> trackListIds = new Dictionary<Guid, string>();

        /// <summary>
        /// Import data into the specified BeepBong Database
        /// </summary>
        /// <param name="xml">Loaded XML document to be imported</param>
        /// <param name="options">Database options for a BeepBong database instance</param>
        public static void ImportData(XDocument xml, DbContextOptions<BeepBongContext> options)
        {
            EmptyDictionary();

            // Broadcasters, Channels and Programmes
            XElement broadcasting = xml.Element("BeepBongCollection").Element("Broadcasting");
            
            foreach (var broadcaster in broadcasting.Elements("Broadcaster"))
            {
                // Create Broadcaster
                Broadcaster b = null;
                if (broadcaster.HasAttributes)
                {
                    b = CreateBroadcasterEntity(broadcaster.Attributes());
                }

                if (broadcaster.HasElements)
                {
                    foreach (var channel in broadcaster.Elements("Channel"))
                    {
                        // Create Channel
                        Channel c = null;
                        if (channel.HasAttributes)
                        {
                            c = CreateChannelEntity(channel.Attributes());
                        }

                        if (channel.HasElements)
                        {
                            // Create Programme
                            foreach (var programme in channel.Elements("Programme"))
                            {
                                Programme p = null;
                                if (programme.HasAttributes)
                                {
                                    p = CreateProgrammeEntity(programme.Attributes());
                                    if (programme.Attribute("ref") != null)
                                    {
                                        Guid id = new Guid();
                                        programmeIds.Add(id, programme.Attribute("ref").Value);
                                        p.ProgrammeId = id;
                                    }
                                }

                                // Add Programme to Channel
                                c.Programmes.Add(p);
                            }
                        }

                        // Add Channel to Broadcaster
                        b.Channels.Add(c);
                    }
                }

                // Save Broadcaster and related entities
                using (var context = new BeepBongContext(options)) {
                    context.Broadcasters.Add(b);
                    context.SaveChanges();
                }
            }

            // Programmes w/o Channels
            foreach (var programme in broadcasting.Element("Programmes").Elements("Programme"))
            {
                if (programme.HasAttributes)
                {
                    Programme p = CreateProgrammeEntity(programme.Attributes());
                    if (programme.Attribute("ref") != null)
                    {
                        Guid id = new Guid();
                        programmeIds.Add(id, programme.Attribute("ref").Value);
                        p.ProgrammeId = id;
                    }

                    using (var context = new BeepBongContext(options)) {
                        context.Programmes.Add(p);
                        context.SaveChanges();
                    }
                }
            }

            // TrackList, Tracks, Samples and Libraries
            XElement audioCollection = xml.Element("BeepBongCollection").Element("AudioCollection");

            foreach (var trackList in audioCollection.Elements("TrackList"))
            {
                // Create TrackList
                TrackList tl = null;
                if (trackList.HasAttributes)
                {
                    tl = CreateTrackListEntity(trackList.Attributes());
                    if (trackList.Attribute("ref") != null)
                    {
                        Guid id = new Guid();
                        trackListIds.Add(id, trackList.Attribute("ref").Value);
                        tl.TrackListId = id;
                    }
                }
                
                if (trackList.HasElements)
                {
                    foreach (var track in trackList.Elements("Track"))
                    {
                        // Create Track
                        Track t = null;
                        if (track.HasAttributes)
                        {
                            t = CreateTrackEntity(track.Attributes());
                        }

                        if (track.HasElements)
                        {
                            // Create Samples
                            foreach (var sample in track.Elements("Sample"))
                            {
                                Sample s = CreateSampleEntity(sample.Attributes());

                                // Add Sample to Track
                                t.Samples.Add(s);
                            }
                        }
                        else
                        {
                            if (t == null)
                                t = new Track();
                            
                            //Fall back name value
                            if (string.IsNullOrEmpty(t.Name))
                                t.Name = track.Value;
                        }

                        // Add Track to TrackList
                        tl.Tracks.Add(t);
                    }
                }

                // Save TrackList and related entities
                using (var context = new BeepBongContext(options)) {
                    context.TrackLists.Add(tl);
                    context.SaveChanges();
                }
            }

            // Libraries
            foreach (var library in audioCollection.Element("Libraries").Elements())
            {
                Library l = null;
                if (library.HasAttributes)
                {
                    l = CreateLibraryEntity(library.Attributes());
                }

                using (var context = new BeepBongContext(options)) {
                    context.Libraries.Add(l);
                    context.SaveChanges();
                }
            }

            // Programme TrackList Links
            XElement progTrackList = xml.Element("BeepBongCollection").Element("ProgrammeTrackListLink");
            foreach (var link in progTrackList.Elements("Link"))
            {
                if (link.Attribute("programmeRef") != null && link.Attribute("trackListRef") != null)
                {
                    ProgrammeTrackList ptl = new ProgrammeTrackList() {
                        ProgrammeId = programmeIds.Where(l => l.Value == link.Attribute("programmeRef").Value).FirstOrDefault().Key,
                        TrackListId = trackListIds.Where(l => l.Value == link.Attribute("trackListRef").Value).FirstOrDefault().Key
                    };

                    using (var context = new BeepBongContext(options)) {
                        context.ProgrammeTrackLists.Add(ptl);
                        context.SaveChanges();
                    }
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
            EmptyDictionary();

            XDocument xdoc = null;

            // Establish a Database Connection
            using (var context = new BeepBongContext(options)) {
                // Prepare Dictionary
                int id = 0;
                foreach (var progId in context.Programmes.Select(p => p.ProgrammeId))
                {
                    programmeIds.Add(progId, "p"+(++id));
                }
                id = 0;
                foreach (var trackListId in context.TrackLists.Select(p => p.TrackListId))
                {
                    trackListIds.Add(trackListId, "tl"+(++id));
                }

                // Create the XML object
                xdoc = new XDocument(
                            new XElement("BeepBongCollection"),
                                new XElement("Broadcasting",
                                    context.Broadcasters.Select(
                                        b => new XElement("Broadcaster",
                                            (b.Name != null) ? new XAttribute("name", b.Name) : null,
                                            (b.Country != null) ? new XAttribute("country", b.Country) : null,
                                            (b.Channels.Any()) ? context.Channels
                                                        .Where(c => c.BroadcasterId == b.BroadcasterId)
                                                        .Select(c => new XElement("Channel",
                                                                (c.Name != null) ? new XAttribute("name", c.Name) : null,                                                         
                                                                (c.Programmes.Any()) ? context.Programmes
                                                                                        .Where(p => p.ChannelId == c.ChannelId)
                                                                                        .Select(p => new XElement("Programme",
                                                                                            new XAttribute("ref", programmeIds[p.ProgrammeId]),
                                                                                            (p.Name != null) ? new XAttribute("name", p.Name) : null, 
                                                                                            (p.AirDate != null) ? new XAttribute("year", p.AirDate) : null, 
                                                                                            (p.LogoLocation != null) ? new XAttribute("logo", p.LogoLocation) : null
                                                                                        )) : null                                                         
                                                            )) : null
                                        )
                                    ),
                                    context.Programmes
                                        .Where(p => p.ChannelId == null)
                                        .Select(p => new XElement("Programme",
                                            new XAttribute("ref", programmeIds[p.ProgrammeId]),
                                            (p.Name != null) ? new XAttribute("name", p.Name) : null, 
                                            (p.AirDate != null) ? new XAttribute("year", p.AirDate) : null, 
                                            (p.LogoLocation != null) ? new XAttribute("logo", p.LogoLocation) : null
                                        ))
                                ),
                                new XElement("AudioCollection",
                                    context.TrackLists.Select(
                                        tl => new XElement("TrackList",
                                            new XAttribute("ref", trackListIds[tl.TrackListId]),
                                            (tl.Name != null) ? new XAttribute("name", tl.Name) : null,
                                            (tl.Composer != null) ? new XAttribute("composer", tl.Composer) : null,
                                            (tl.Library == true) ? new XAttribute("library", tl.Library) : null,
                                            (tl.Tracks.Any()) ? context.Tracks
                                                                .Where(t => t.TrackListId == tl.TrackListId)
                                                                .Select(t => new XElement("Track",
                                                                        (t.Name != null) ? new XAttribute("name", t.Name) : null,
                                                                        (t.Variant != null) ? new XAttribute("subtitle", t.Variant) : null,
                                                                        (t.Description != null) ? new XAttribute("description", t.Description) : null,
                                                                        (t.Samples.Any()) ? context.Samples
                                                                                            .Where(s => s.TrackId == t.TrackId)
                                                                                            .Select(s => new XElement("Sample",
                                                                                                    (s.SampleRate > 0) ? new XAttribute("sampleRate", s.SampleRate) : null,
                                                                                                    (s.SampleCount > 0) ? new XAttribute("sampleCount", s.SampleCount) : null,
                                                                                                    (s.AudioChannelCount > 0) ? new XAttribute("channelCount", s.AudioChannelCount) : null,
                                                                                                    (s.BitRate > 0) ? new XAttribute("bitRate", s.BitRate) : null,
                                                                                                    new XAttribute("bitRateMode", s.BitRateMode),
                                                                                                    (s.Codec != null) ? new XAttribute("bitRate", s.Codec) : null,
                                                                                                    (s.Compression != CompressionEnum.None) ? new XAttribute("compression", s.Compression) : null,
                                                                                                    (s.Fingerprint != null) ? new XAttribute("fingerprint", s.Fingerprint) : null,
                                                                                                    (s.OtherAttributes != null) ? new XAttribute("other", s.OtherAttributes) : null,
                                                                                                    (s.Notes != null) ? new XAttribute("notes", s.Notes) : null,
                                                                                                    (s.Waveform != null) ? new XAttribute("waveform", s.Waveform) : null,
                                                                                                    (s.Spectrograph != null) ? new XAttribute("spectograph", s.Spectrograph) : null
                                                                                            )) : null

                                                                )) : null
                                        )
                                    ),
                                    new XElement("Libraries", context.Libraries
                                            .Select(l => new XElement("Library",
                                                (l.AlbumName != null) ? new XAttribute("name", l.AlbumName) : null,
                                                (l.Catalog != null) ? new XAttribute("catalog", l.Catalog) : null,
                                                (l.Label != null) ? new XAttribute("label", l.Label) : null,
                                                (l.MBID != null) ? new XAttribute("mbid", l.MBID) : null
                                        )
                                    ))
                                ),
                                new XElement("ProgrammeTrackListLink", context.ProgrammeTrackLists
                                            .Select(ptl => new XElement("Link",
                                                new XAttribute("programmeRef", programmeIds[ptl.ProgrammeId]),
                                                new XAttribute("trackListRef", trackListIds[ptl.TrackListId])
                                            ))
                                )
                );
            }

            return xdoc;
        }

        private static Broadcaster CreateBroadcasterEntity(IEnumerable<XAttribute> attributes)
        {
            Broadcaster b = new Broadcaster();
            foreach (var att in attributes)
            {
                if (att.Name == "name")
                    b.Name = att.Value;
                if (att.Name == "country")
                    b.Country = att.Value;
            }

            return b;
        }

        private static Channel CreateChannelEntity(IEnumerable<XAttribute> attributes)
        {
            Channel c = new Channel();
            foreach (var att in attributes)
            {
                if (att.Name == "name")
                    c.Name = att.Value;
            }

            return c;
        }

        private static Programme CreateProgrammeEntity(IEnumerable<XAttribute> attributes)
        {
            Programme p = new Programme();
            foreach (var attribute in attributes)
            {
                if (attribute.Name == "name")
                    p.Name = attribute.Value;
                if (attribute.Name == "year")
                    p.AirDate = new DateTime(int.Parse(attribute.Value), 1, 1);
                //if (attribute.Name == "logo")
                //    p.Logo = attribute.Value;
            }

            return p;
        }

        private static TrackList CreateTrackListEntity(IEnumerable<XAttribute> attributes)
        {
            TrackList tl = new TrackList();
            foreach (var att in attributes)
            {
                if (att.Name == "name")
                    tl.Name = att.Value;
                if (att.Name == "composer")
                    tl.Composer = att.Value;
                if (att.Name == "library")
                    tl.Library = Boolean.Parse(att.Value);
            }

            return tl;
        }

        private static Track CreateTrackEntity(IEnumerable<XAttribute> attributes)
        {
            Track t = new Track();
            foreach (var attribute in attributes)
            {
                if (attribute.Name == "name")
                    t.Name = attribute.Value;
                if (attribute.Name == "subtitle")
                    t.Variant = attribute.Value;
                if (attribute.Name == "description")
                    t.Description = attribute.Value;
                if (attribute.Name == "title")
                    t.Name = attribute.Value;
            }

            return t;
        }

        private static Sample CreateSampleEntity(IEnumerable<XAttribute> attributes)
        {
            Sample s = new Sample();
            foreach (var attribute in attributes)
            {
                if (attribute.Name == "sampleRate")
                    s.SampleRate = int.Parse(attribute.Value);
                if (attribute.Name == "sampleCount")
                    s.SampleCount = int.Parse(attribute.Value);
                if (attribute.Name == "channelCount")
                    s.AudioChannelCount = int.Parse(attribute.Value);
                if (attribute.Name == "bitRate")
                    s.BitRate = int.Parse(attribute.Value);
                if (attribute.Name == "bitRateMode")
                    s.BitRateMode = (BitRateModeEnum)Enum.Parse(typeof(BitRateModeEnum), attribute.Value);
                if (attribute.Name == "bitDepth")
                    s.BitDepth = int.Parse(attribute.Value);
                if (attribute.Name == "codec")
                    s.Codec = attribute.Value;
                if (attribute.Name == "compression")
                    s.Compression = (CompressionEnum)Enum.Parse(typeof(CompressionEnum), attribute.Value);
                if (attribute.Name == "fingerprint")
                    s.Codec = attribute.Value;
                if (attribute.Name == "other")
                    s.OtherAttributes = attribute.Value;
                if (attribute.Name == "notes")
                    s.Notes = attribute.Value;
                if (attribute.Name == "waveform")
                    s.Codec = attribute.Value;
                if (attribute.Name == "spectograph")
                    s.Codec = attribute.Value;
            }

            return s;
        }

        private static Library CreateLibraryEntity(IEnumerable<XAttribute> attributes)
        {
            Library l = new Library();
            foreach (var attribute in attributes)
            {
                if (attribute.Name == "name")
                    l.AlbumName = attribute.Value;
                if (attribute.Name == "catalog")
                    l.Catalog = attribute.Value;
                if (attribute.Name == "label")
                    l.Label = attribute.Value;
                if (attribute.Name == "mbid")
                    l.MBID = attribute.Value;
            }

            return l;
        }

        private static void EmptyDictionary()
        {
            programmeIds.Clear();
            trackListIds.Clear();
        }
    }
}