using System;
using System.IO;
using System.Xml.Linq;
using BeepBong.DataAccess;
using Microsoft.EntityFrameworkCore;
using BeepBong.Application;

namespace BeepBong.App.XmlSeed
{
    static class Program
    {
        private static DbContextOptions<BeepBongContext> options;

        static void Main(string[] args)
        {
            bool import = false;
            bool export = false;

            options = new DbContextOptionsBuilder<BeepBongContext>()
                .UseSqlite("Data Source=../BeepBong.Web/BeepBong.db")
                .Options;

            if (args.Length < 2) {
                throw new ApplicationException("Missing file parameter");
            }

            if (!args[1].EndsWith(".xml", StringComparison.OrdinalIgnoreCase)) {
                throw new ApplicationException("Not an XML file");
            }

            switch (args[0]) {
                case "import":
                    import = true;
                    break;
                case "export":
                    export = true;
                    break;
                default:
                    break;
            }

            if (import) {
                // Create Database
                if(!File.Exists(args[1])) {
                    throw new ApplicationException("File not found");
                }

                CreateDatabase();

                ImportData(args[1]);
            }

            if (export) {
                ExportData(args[1]);
            }

            Console.WriteLine("Operation Complete");
            Console.ReadLine();
        }

        static void CreateDatabase() {
            using (var context = new BeepBongContext(options)) {
                context.Database.EnsureCreated();
                context.SaveChanges();
            }
        }

        static void ImportData(string file) {
            XDocument xml = XDocument.Load(file);

            XMLTranslate.ImportData(xml, options);
        }

        static void ExportData(string filePath) {
            XDocument xdoc = XMLTranslate.ExportData(options, true);

            xdoc.Save(filePath);
        }
    }
}
