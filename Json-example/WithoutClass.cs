using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Json_example
{
    public class WithoutClass
    {
        public static void Main()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\"));

            //načtení dokumentu bez třídy
            
            //XmlDocument
            //načtu xml soubor do XmlDocument
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(File.ReadAllText(path + "xml\\Brian.xml"));

            //vyberu první note z dokumentu
            XmlNode currNode = doc.DocumentElement.FirstChild;
            Console.WriteLine(currNode.OuterXml); //vrací text v node a jejich dětech
            Console.WriteLine(currNode.InnerText + "\n"); //vrací pouze text v dětech dané node

            //z node můžu přejít na další
            Console.WriteLine(currNode.NextSibling.OuterXml);
            Console.WriteLine(currNode.FirstChild.OuterXml);
            Console.WriteLine(currNode.ParentNode.OuterXml + "\n");

            //Newtonsoft
            JObject jobject = JObject.Parse(File.ReadAllText(path + "json\\brian.json"));
            Console.WriteLine(jobject["name"].Value<string>());
            JToken token = jobject.First;
            Console.WriteLine(token.ToString());
            Console.WriteLine(token.Next.ToString());
            Console.WriteLine(token.First.ToString());
           
        }
    }
}
