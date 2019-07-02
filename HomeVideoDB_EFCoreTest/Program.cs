using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Castle.Core.Internal;
using HomeVideoDB_EFCoreTest.Models;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Query;
using TagLib;

namespace HomeVideoDB_EFCoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var db = new HomeVideoDBContext())
            //{
            //    var query = from b in db.Source
            //        orderby b.SourceId
            //        select b;

            //    Debug.WriteLine("All Sources in HomeVideoDB");

            //    foreach (var item in query)
            //    {
            //        Debug.WriteLine(item.SourceLabel);
            //        foreach (var Clip in item.Clips)
            //        {
            //            Debug.WriteLine("\t" + Clip.ClipId);
            //        }
            //    }

            //}
            XmlMp4PropertyList doc = new XmlMp4PropertyList();
            doc.AddCastMember("Beth Westgate");
            doc.RemoveCastMember("Beth Westgate");
            doc.AddCastMember("Drew Westgate");
            doc.SetDirector("Test");
            doc.SetDirector("Test2");

            var tfile = TagLib.File.Create(@"M:\Home Movies\1995-12-24_11-14-24_11-32-00_Christmas Eve, 1995 - Christmas Eve at the Frosts' House.mp4");
            TagLib.Mpeg4.AppleTag appleTags = (TagLib.Mpeg4.AppleTag )tfile.GetTag(TagLib.TagTypes.Apple);
            var test = appleTags.GetDashBox("\0\0\0\0com.apple.iTunes", "\0\0\0\0iTunMOVI");
            //var test2 = tfile.Tag.;

            TagLib.File f = TagLib.File.Create("test.mp4");
            TagLib.Mpeg4.AppleTag customTag = (TagLib.Mpeg4.AppleTag)f.GetTag(TagLib.TagTypes.Apple);
            customTag.SetDashBox("\0\0\0\0com.apple.iTunes", "\0\0\0\0iTunMOVI", "value");
            f.Save();
            
            Console.WriteLine(tfile);
        }
    }

    public class XmlMp4PropertyList
    {
        public readonly XmlDocument PropertyList;
        private readonly XmlNode _dictNode;
        private readonly Dictionary<string, XmlNode> _plistDictionary = new Dictionary<string, XmlNode>();

        public XmlMp4PropertyList()
        {
            PropertyList = new XmlDocument();

            XmlDeclaration xmlDeclaration = PropertyList.CreateXmlDeclaration("1.0", "UTF-8", null);
            PropertyList.PrependChild(xmlDeclaration);

            XmlDocumentType documentType = PropertyList.CreateDocumentType("plist", "-//Apple//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", null);
            PropertyList.AppendChild(documentType);

            XmlNode plistNode = PropertyList.CreateNode(XmlNodeType.Element, "plist", null);
            XmlAttribute plistVers = (XmlAttribute)PropertyList.CreateNode(XmlNodeType.Attribute, "version", null);
            plistVers.Value = "1.0";
            plistNode.Attributes.Append(plistVers);
            PropertyList.AppendChild(plistNode);

            _dictNode = PropertyList.CreateNode(XmlNodeType.Element, "dict", null);
            plistNode.AppendChild(_dictNode);
        }

        private XmlElement CreateKeyArrayPair(string keyName)
        {
            XmlElement keyNode;

            if (!_plistDictionary.ContainsKey(keyName))
            {
                XmlText keyNodeText = PropertyList.CreateTextNode(keyName);
                keyNode = PropertyList.CreateElement("key");
                keyNode.AppendChild(keyNodeText);

                XmlNode arrayNode = PropertyList.CreateNode(XmlNodeType.Element, "array", null);

                _dictNode.AppendChild(keyNode);
                _dictNode.AppendChild(arrayNode);

                _plistDictionary.Add(keyName, arrayNode);
            }
            else
            {
                Debug.WriteLine("keyname '" + keyName + "' already exists.");
                keyNode = null;
            }

            return keyNode;
        }

        private XmlNode CreateKeyStringDict(string keyName, string stringValue)
        {
            XmlNode keyStringDictNode = PropertyList.CreateNode(XmlNodeType.Element, "dict", null);

            XmlText keyNodeText = PropertyList.CreateTextNode(keyName);
            XmlElement keyNode = PropertyList.CreateElement("key");
            keyNode.AppendChild(keyNodeText);

            XmlText stringNodeText = PropertyList.CreateTextNode(stringValue);
            XmlElement stringNode = PropertyList.CreateElement("string");
            stringNode.AppendChild(stringNodeText);

            keyStringDictNode.AppendChild(keyNode);
            keyStringDictNode.InsertAfter(stringNode, keyNode);

            return keyStringDictNode;
        }

        private void AddDictValueToKeyArray(string parentKeyNodeString, string key, string value)
        {
            XmlText keyNodeText = PropertyList.CreateTextNode(parentKeyNodeString);
            XmlElement keyNode = PropertyList.CreateElement("key");
            keyNode.AppendChild(keyNodeText);

            if (!_plistDictionary.ContainsKey(parentKeyNodeString))
            {
                CreateKeyArrayPair(parentKeyNodeString);
            }

            XmlNode parentArrayNode = _plistDictionary[parentKeyNodeString];

            if (!parentArrayNode.SelectNodes("./dict[string='" + value + "']").IsNullOrEmpty()) return;
            XmlNode keyStringDict = CreateKeyStringDict(key, value);
            parentArrayNode.AppendChild(keyStringDict);
        }

        public void AddCastMember(string castMemberName)
        {
            AddDictValueToKeyArray("cast", "name", castMemberName);
        }

        public void RemoveCastMember(string castMemberName)
        {
            XmlNode targetKeyStringElement = FindCastMember(castMemberName);
            if(!targetKeyStringElement.IsNullOrEmpty()) targetKeyStringElement.ParentNode.RemoveChild(targetKeyStringElement);
        }

        private XmlNode FindCastMember(string castMemberName)
        {
            XmlNode castNode = PropertyList.SelectSingleNode("/plist/dict/key[. = 'cast']");
            if (castNode.IsNullOrEmpty()) return null;
            XmlNode castArrayNode = castNode.NextSibling;
            XmlNode targetKeyStringElement = castArrayNode.SelectSingleNode("./dict[string = '" + castMemberName + "']");

            return targetKeyStringElement;
        }

        public void SetDirector(string directorName)
        {
            XmlNode directorNode = PropertyList.SelectSingleNode("/plist/dict/key[. = 'directors']");
            XmlNode directorArrayNode = directorNode?.NextSibling;
            XmlNodeList targetDirectorChildNodes = directorArrayNode?.ChildNodes;

            if (targetDirectorChildNodes.IsNullOrEmpty())
            {
                AddDictValueToKeyArray("directors", "name", directorName);
            }
            else
            {
                XmlNode targetKeyStringElement = targetDirectorChildNodes[0].SelectSingleNode("./string");
                targetKeyStringElement.InnerText = directorName;
            }
        }
    }
}
