using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class GameXml : MonoBehaviour {

	// Use this for initialization
    XmlDocument doc = new XmlDocument();
    XmlNode root;
    string localFileString;

	void Start () {
        localFileString = Application.dataPath + @"/XML/GameXMLData.xml";
        if (File.Exists(localFileString))
        {
            doc.Load(localFileString);
            root = doc.DocumentElement;
            XmlNodeList levelList = doc.GetElementsByTagName("level");
            foreach (XmlNode level in levelList)
            {
                XmlNodeList levelcontent = level.ChildNodes;
                foreach (XmlNode items in levelcontent)
                {
                    if (items.Name == "name")
                    {
                        Debug.Log(items.InnerText);
                    }
                    if (items.Name == "tutorial")
                    {
                        Debug.Log(items.InnerText);
                    }
                    if (items.Name == "object")
                    {
                        Debug.Log(items.InnerText);
                    }
                }
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
