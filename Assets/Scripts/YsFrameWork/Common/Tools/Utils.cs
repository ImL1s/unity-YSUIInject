using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;
using System;

namespace YSFramework.Tools
{

    public class Utils
    {
        /// <summary>
        /// Converts the xml to string.
        /// </summary>
        /// <returns>The xml to string.</returns>
        /// <param name="xmlDoc">Xml document.</param>
        public static string ConvertXmlToString(XmlDocument xmlDoc)
        {
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            writer.Formatting = Formatting.Indented;
            xmlDoc.Save(writer);
            StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            string xmlString = sr.ReadToEnd();
            sr.Close();
            stream.Close();
            return xmlString;
        }

        /// <summary>
        /// Froms the string to xml document.
        /// </summary>
        /// <returns>The string to xml document.</returns>
        /// <param name="bs">Bs.</param>
        public static XmlDocument ConvertByteArrayToXmlDoc(byte[] bs)
        {
            XmlDocument doc = new XmlDocument();
            string text = UTF8ByteArrayToString(bs);
            doc.LoadXml(text);
            return doc;
        }

        /// <summary>
        /// Strings to UT f8 byte array.
        /// </summary>
        /// <returns>The to UT f8 byte array.</returns>
        /// <param name="pXmlString">P xml string.</param>
        public static byte[] StringToUTF8ByteArray(string _string)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(_string);
            return byteArray;
        }

        /// <summary>
        /// UTs the f8 byte array to string.
        /// </summary>
        /// <returns>The f8 byte array to string.</returns>
        /// <param name="characters">Characters.</param>
        public static string UTF8ByteArrayToString(byte[] _bytes)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(_bytes);
            return (constructedString);
        }



        /// <summary>
        /// Gets the local xml document.
        /// </summary>
        /// <returns>The local xml document.</returns>
        /// <param name="xmlfile">Xmlfile.</param>
        public static XmlDocument GetLocalXmlDoc(string xmlfile)
        {
            string url;
#if REMOTE
		url = Application.persistentDataPath + xmlfile;
		byte[] bs = File.ReadAllBytes(url);
		XmlDocument doc = ConvertByteArrayToXmlDoc(bs);	
#else
            XmlDocument doc = new XmlDocument();
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
            {
                url = Application.dataPath + "/StreamingAssets" + xmlfile;
                doc.Load(url);
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                url = "jar:file://" + Application.dataPath + "!/assets" + xmlfile;
                WWW www = new WWW(url);
                while (!www.isDone) { }
                try
                {
                    doc.LoadXml(www.text);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                    //error_msg += "LOAD " + xmlfile + " ERROR!" + "\n";
                }
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                url = Application.dataPath + "/Raw" + xmlfile;
                doc.Load(url);
            }
#endif
            return doc;
        }
    }
}
































//
//#if REMOTE
//Debug.Log("LOAD XML:" + xmlfile);
//string url = Application.persistentDataPath + xmlfile;
//byte[] bs = File.ReadAllBytes(url);
//XmlDocument doc = fromStringToXmlDoc(bs);		
//#else
//XmlDocument doc = new XmlDocument();
//if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
//	doc.Load(Application.dataPath +  "/StreamingAssets" + xmlfile);
//else
//{
//	#if UNITY_ANDROID
//	WWW www = new WWW("jar:file://" + Application.dataPath + "!/assets" + xmlfile);
//	while (!www.isDone) {}
//	try
//	{
//		/*
//				System.IO.StringReader stringReader = new System.IO.StringReader(www.text);
//				stringReader.Read(); // skip BOM
//				System.Xml.XmlReader reader = System.Xml.XmlReader.Create(stringReader);
//				*/
//		doc.LoadXml(www.text);
//		//doc.LoadXml(stringReader.ReadToEnd());
//	}
//	catch (Exception ex)
//	{
//		//error_msg += "LOAD " + xmlfile + " ERROR!" + "\n";
//	}
//	#elif UNITY_IPHONE
//	doc.Load(Application.dataPath + "/Raw" + xmlfile);
//	#endif
//}
//#endif	

