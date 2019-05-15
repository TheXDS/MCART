using System;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Linq;

namespace xamlArt
{
    class Program
    {
        static void Main(string[] args)
        {
            Environment.CurrentDirectory = Path.GetFullPath(@"..\");
            using (var prj = new FileStream(Path.GetFullPath(@"..\WPF.csproj"), FileMode.Open))
            {
                var changes = false;
                var xmlPrj = new XmlDocument();
                xmlPrj.Load(prj);
                foreach (var j in new DirectoryInfo(Environment.CurrentDirectory).EnumerateFiles("*.xaml"))
                {
                    Console.WriteLine($"Empaquetando {j.Name}...");
                    var fileName = j.Name.Replace(".xaml", "_Xml.deflate");
                    using (var inputFile = j.Open(FileMode.Open))
                    using (var outputFile = new FileStream(Path.GetFullPath(@$"..\Resources\Icons\{fileName}"), FileMode.Create))
                    using (var deflate = new DeflateStream(outputFile, CompressionMode.Compress))
                    {
                        inputFile.CopyTo(deflate);
                    }

                    var noneFound = false;
                    var embedFound = false;
                    foreach (XmlNode node in xmlPrj.DocumentElement.ChildNodes)
                    {
                        if (node.Name == "ItemGroup" && node.Attributes.Count == 0)
                        {
                            noneFound = false;
                            foreach (XmlNode nones in node.ChildNodes)
                            {
                                if (nones.Name == "None")
                                {
                                    foreach (XmlAttribute nnattr in nones.Attributes)
                                    {
                                        if (nnattr.Name == "Remove" && nnattr.Value == @$"Resources\Icons\{fileName}")
                                        {
                                            noneFound = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (!noneFound)
                            {
                                var noneNode = xmlPrj.CreateElement("None");
                                noneNode.SetAttribute("Remove", @$"Resources\Icons\{fileName}");
                                node.AppendChild(noneNode);
                            }

                            embedFound = false;
                            foreach (XmlNode nones in node.ChildNodes)
                            {
                                if (nones.Name == "EmbeddedResource")
                                {
                                    foreach (XmlAttribute nnattr in nones.Attributes)
                                    {
                                        if (nnattr.Name == "Include" && nnattr.Value == @$"Resources\Icons\{fileName}")
                                        {
                                            embedFound = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (!embedFound)
                            {
                                var embedNode = xmlPrj.CreateElement("EmbeddedResource");
                                embedNode.SetAttribute("Include", @$"Resources\Icons\{fileName}");
                                node.AppendChild(embedNode);
                            }
                            break;
                        }
                    }

                    changes |= !noneFound | !embedFound;
                }
                if (changes)
                {
                    prj.SetLength(0);
                    Console.WriteLine("Actualizando archivo de proyecto...");
                    xmlPrj.Save(prj);
                }
            }
        }
    }
}
