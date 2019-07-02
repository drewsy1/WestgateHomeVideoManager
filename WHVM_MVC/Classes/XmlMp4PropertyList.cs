using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using Castle.Core.Internal;

namespace WHVM_MVC.Classes
{
    public class XmlMp4PropertyList
    {
        private readonly XmlDocument _propertyList;
        private readonly XmlNode _dictNode;
        private readonly Dictionary<string, XmlNode> _plistDictionary = new Dictionary<string, XmlNode>();

        public XmlMp4PropertyList()
        {
            _propertyList = new XmlDocument();

            XmlDeclaration xmlDeclaration = _propertyList.CreateXmlDeclaration("1.0", "UTF-8", null);
            _propertyList.PrependChild(xmlDeclaration);

            XmlDocumentType documentType = _propertyList.CreateDocumentType("plist", "-//Apple//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", null);
            _propertyList.AppendChild(documentType);

            XmlNode plistNode = _propertyList.CreateNode(XmlNodeType.Element, "plist", null);
            XmlAttribute plistVers = (XmlAttribute)_propertyList.CreateNode(XmlNodeType.Attribute, "version", null);
            plistVers.Value = "1.0";
            plistNode.Attributes.Append(plistVers);
            _propertyList.AppendChild(plistNode);

            _dictNode = _propertyList.CreateNode(XmlNodeType.Element, "dict", null);
            plistNode.AppendChild(_dictNode);
        }

        private void CreateKeyArrayPair(string keyName)
        {
            XmlElement keyNode;

            if (!_plistDictionary.ContainsKey(keyName))
            {
                XmlText keyNodeText = _propertyList.CreateTextNode(keyName);
                keyNode = _propertyList.CreateElement("key");
                keyNode.AppendChild(keyNodeText);

                XmlNode arrayNode = _propertyList.CreateNode(XmlNodeType.Element, "array", null);

                _dictNode.AppendChild(keyNode);
                _dictNode.AppendChild(arrayNode);

                _plistDictionary.Add(keyName, arrayNode);
            }
            else
            {
                Debug.WriteLine("keyname '" + keyName + "' already exists.");
            }
        }

        private XmlNode CreateKeyStringDict(string keyName, string stringValue)
        {
            XmlNode keyStringDictNode = _propertyList.CreateNode(XmlNodeType.Element, "dict", null);

            XmlText keyNodeText = _propertyList.CreateTextNode(keyName);
            XmlElement keyNode = _propertyList.CreateElement("key");
            keyNode.AppendChild(keyNodeText);

            XmlText stringNodeText = _propertyList.CreateTextNode(stringValue);
            XmlElement stringNode = _propertyList.CreateElement("string");
            stringNode.AppendChild(stringNodeText);

            keyStringDictNode.AppendChild(keyNode);
            keyStringDictNode.InsertAfter(stringNode, keyNode);

            return keyStringDictNode;
        }

        private void AddDictValueToKeyArray(string parentKeyNodeString, string key, string value)
        {
            XmlText keyNodeText = _propertyList.CreateTextNode(parentKeyNodeString);
            XmlElement keyNode = _propertyList.CreateElement("key");
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
            if (!targetKeyStringElement.IsNullOrEmpty()) targetKeyStringElement.ParentNode.RemoveChild(targetKeyStringElement);
        }

        private XmlNode FindCastMember(string castMemberName)
        {
            XmlNode castNode = _propertyList.SelectSingleNode("/plist/dict/key[. = 'cast']");
            if (castNode.IsNullOrEmpty()) return null;
            XmlNode castArrayNode = castNode.NextSibling;
            XmlNode targetKeyStringElement = castArrayNode.SelectSingleNode("./dict[string = '" + castMemberName + "']");

            return targetKeyStringElement;
        }

        public void SetDirector(string directorName)
        {
            XmlNode directorNode = _propertyList.SelectSingleNode("/plist/dict/key[. = 'directors']");
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

        public XmlDocument GetXmlMp4PropertyList()
        {
            return _propertyList;
        }
    }
}