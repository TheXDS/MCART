/*
Program.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
 César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Linq;

namespace xamlArt
{
    class Program
    {
        private static string ResName(string fileName) => @$"Resources\Icons\{fileName}";
        private static bool GetRemove(XmlDocument xmlPrj, XmlNode itemGroup, string fileName) => GetResourceNode(xmlPrj, itemGroup, "None", "Remove", fileName);
        private static bool GetEmbeed(XmlDocument xmlPrj, XmlNode itemGroup, string fileName) => GetResourceNode(xmlPrj, itemGroup, "EmbeddedResource", "Include", fileName);
        private static XmlNode GetItemGroup(XmlDocument xmlProject, out bool changes)
        {
            foreach (XmlNode node in xmlProject.DocumentElement.ChildNodes)
            {
                if (node.Name == "ItemGroup" && node.Attributes.Count == 0)
                {
                    changes = false;
                    return node;
                }
            }
            var newItemGroup = xmlProject.CreateElement("ItemGroup");
            xmlProject.DocumentElement.AppendChild(newItemGroup);
            changes = true;
            return newItemGroup;
        }
        private static bool GetResourceNode(XmlDocument xmlPrj, XmlNode itemGroup, string kind, string op, string fileName)
        {
            foreach (XmlNode node in itemGroup.ChildNodes)
            {
                if (node.Name != kind) continue;
                if (node.Attributes.OfType<XmlAttribute>().Any(p => p.Name == op && p.Value.EndsWith(ResName(fileName)))) return false;
            }            
            var newNode = xmlPrj.CreateElement(kind);
            newNode.SetAttribute(op, ResName(fileName));
            itemGroup.AppendChild(newNode);
            return true;
        }
        private static string PackResource(FileInfo file)
        {
            Console.WriteLine($"Empaquetando {file.Name}...");
            var fileName = file.Name.Replace(".xaml", "_Xml.deflate");
            using var inputFile = file.Open(FileMode.Open);
            using var outputFile = new FileStream(Path.GetFullPath(@$"..\Resources\Icons\{fileName}"), FileMode.Create);
            using var deflate = new DeflateStream(outputFile, CompressionMode.Compress);
            inputFile.CopyTo(deflate);
            return fileName;
        }

        static void Main()
        {
            Environment.CurrentDirectory = Path.GetFullPath(@"..\");
            using var prj = new FileStream(Path.GetFullPath(@"..\WPF.csproj"), FileMode.Open);
            var xmlPrj = new XmlDocument();
            xmlPrj.Load(prj);
            var ig = GetItemGroup(xmlPrj, out var changes);

            foreach (var j in new DirectoryInfo(Environment.CurrentDirectory).EnumerateFiles("*.xaml"))
            {
                var f = PackResource(j);
                changes |= GetRemove(xmlPrj, ig, f) | GetEmbeed(xmlPrj, ig, f);
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
