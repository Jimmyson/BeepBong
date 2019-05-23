// using Xunit;
// using BeepBong.Application;
// using BeepBong.DataAccess;
// using Microsoft.EntityFrameworkCore;
// using System.Xml.Linq;
// using System.Linq;

// namespace BeepBong.Application.Test
// {
//     public class XMLTest
//     {
//         private XDocument xdocSample = new XDocument(
//                                         new XElement("root", 
//                                             new XElement("Programmes",
//                                                 new XElement("Programme",
//                                                     new XAttribute("name", "Test"),
//                                                     new XAttribute("year", "2002"),
//                                                     new XElement("Track",
//                                                         new XAttribute("title", "Valid 1")
//                                                     )
//                                                 )
//                                             ),
//                                             new XElement("Libraries",
//                                                 new XElement("Library",
//                                                     new XAttribute("name", "Protected")
//                                                 )
//                                             )
//                                         )
//                                     );

//         [Fact]
//         public void ImportTest()
//         {
//         //Given
//             var options = new DbContextOptionsBuilder<BeepBongContext>()
//                 .UseInMemoryDatabase(databaseName: "XMLTestImport")
//                 .Options;

//         //When
//             XMLTranslate.ImportData(xdocSample, options);
//         //Then
//             using (var context = new BeepBongContext(options))
//             {
//                 Assert.Equal(1, context.Programmes.Count());
//                 Assert.Equal(1, context.Tracks.Count());
//                 Assert.Equal(1, context.Libraries.Count());
//             }
//         }

//         [Fact]
//         public void ExportTest()
//         {
//             var options = new DbContextOptionsBuilder<BeepBongContext>()
//                 .UseInMemoryDatabase(databaseName: "XMLTestExport")
//                 .Options;
//         //Given
//             XMLTranslate.ImportData(xdocSample, options);

//         //When
//             XDocument xdoc = XMLTranslate.ExportData(options);
        
//         //Then
//             Assert.NotNull(xdoc);
//             Assert.Equal(xdocSample.ToString(), xdoc.ToString());
//         }
//     }
// }