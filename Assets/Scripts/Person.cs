using System.Collections.Generic;
using System.Xml;

public class Person
{
    public string Name;
    public List<string> PersonReplics = new List<string>();
    public string HEXColor;
    public int ReplicsID = 0;
    public Person(){

    }
    public Person(XmlNode mainNode)
    {
        var nameAttribute = mainNode.Attributes["color"];
        if (nameAttribute != null) 
        HEXColor = nameAttribute.Value;

        Name = mainNode.Name;
        foreach(XmlNode a in  mainNode.ChildNodes){
            PersonReplics.Add(a.InnerXml);
        }
    }
}

