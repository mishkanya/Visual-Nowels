using System.Collections.Generic;
using System.Xml;

public class DialogReader
{
    XmlNode MainNode;
    public List<Person> Persons = new List<Person>();
    public DialogReader(XmlDocument xmldoc){
        MainNode = xmldoc.GetElementsByTagName("Replicas")[0];
        foreach(XmlNode a in  MainNode.ChildNodes){
            Persons.Add(new Person(a));
        }
    }
}
