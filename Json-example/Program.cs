using Json_example;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;


public class Jsonexample
{
    public class Race
    {
        public int Id { get; set; }
        public Human[] Competitors { get; set; }
        public DateTime Start { get; set; }
        public Race() { }
        public Race(int id,Human[] competitor ,DateTime start)
        {
            id = Id;
            Competitors = competitor;
            Start = start;
        }

    }
    public class Human
    {
        
        [JsonPropertyName("name")]
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Nationality { get; set; }
        public int Birth { get; set; }
        public Human() { }

        public Human(string firstname, string lastname, string nationality, int birth)
        {
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Nationality = nationality;
            this.Birth = birth;
        }


    }
    static void Main(string[] args) 
    {
        string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(),@"..\..\..\"));


        //JSON (JavaScript Object Notation)
        //sezializuji pole čísel
        int[] Array = { 18, 456, 153, 178, 6542, 1231, 0, -5694, 1324, 1451, 465 };
        string jsonString = JsonSerializer.Serialize(Array);
        File.WriteAllText(path + "json\\Array.json", jsonString);
        int[] desArray = JsonSerializer.Deserialize<int[]>(jsonString);
        Print(desArray);

        //serializuji dictionary
        Dictionary<int,string> dic = new Dictionary<int, string>{ {1,"jedna" },{2,"dva" } };
        jsonString = JsonSerializer.Serialize(dic);
        File.WriteAllText(path + "json\\Dictionary.json", jsonString);
        Dictionary<int, string> desdic = JsonSerializer.Deserialize<Dictionary<int, string>>(jsonString);
        Print(desdic);

        //serializuji objekt
        Human Petr = new("Petr", "Lukavec", "CZE", 1978);
        jsonString = JsonSerializer.Serialize(Petr);
        File.WriteAllText(path + "json\\Petr.json", jsonString);
        Human desPetr = JsonSerializer.Deserialize<Human>(jsonString);
        Print(desPetr);

        //atribut nationality má nastaveno že bude ignorovaný pokud je null
        Human Brian = new("Brian", "lund", null, 1982);
        jsonString = JsonSerializer.Serialize(Brian);
        File.WriteAllText(path + "json\\Brian.json", jsonString);
        Human desBrian = JsonSerializer.Deserialize<Human>(jsonString);
        Print(desBrian);

        //objekt obsahující jiné objekty
        Race race = new(4, new Human[] { Petr, Brian }, DateTime.Now);
        jsonString = JsonSerializer.Serialize(race);
        File.WriteAllText(path + "json\\Race.json", jsonString);
        Race desRace = JsonSerializer.Deserialize<Race>(jsonString);
        Print(desRace);

        //pomocí JsonSerializerOptions můžeme nastavit serializaci
        var options = new JsonSerializerOptions { WriteIndented = true };
        jsonString = JsonSerializer.Serialize(Petr,options);
        File.WriteAllText(path + "json\\WriteIndented.json", jsonString);

        Console.WriteLine("\n\n\n");

        //XML (Extensible Markup Language)
        //zapíšu objekt do xml souboru
        XmlSerializer serializerAr = new XmlSerializer(typeof(int[]));
        using (var writer = new StreamWriter(path + "xml\\Array.xml"))
        {

            serializerAr.Serialize(writer, Array);
        }
        //XmlSerializer neumí serializovat hash table
        //zapíšu do souboru
        using (var reader = new StreamReader(path + "xml\\Array.xml"))
        {
            int[] h = (int[])serializerAr.Deserialize(reader);
            Print(h);
        }
        XmlSerializer serializer = new XmlSerializer(typeof(Human));
        using (var writer = new StreamWriter(path + "xml\\Petr.xml"))
        {

            serializer.Serialize(writer, Petr);
        }
        using (var reader = new StreamReader(path + "xml\\Petr.xml"))
        {
            Human h = (Human)serializer.Deserialize(reader);
            Print(h);
        }

        using (var writer = new StreamWriter(path + "xml\\Brian.xml"))
        {

            serializer.Serialize(writer, Brian);
        }
        using (var reader = new StreamReader(path + "xml\\Brian.xml"))
        {
            Human h = (Human)serializer.Deserialize(reader);
            Print(h);
        }

        XmlSerializer serializerRace = new XmlSerializer(typeof(Race));
        using (var writer = new StreamWriter(path + "xml\\Race.xml"))
        {

            serializerRace.Serialize(writer, race);
        }
        using (var reader = new StreamReader(path + "xml\\Race.xml"))
        {
            Race h = (Race)serializerRace.Deserialize(reader);
            Print(h);
        }

        Console.WriteLine("\n\n\n");
        WithoutClass.Main();
    }


    static void Print(int[] i) 
    {
        
        Console.Write("Array: ");
        foreach(var item in i) Console.Write($"{item} ");
        Console.WriteLine();
    }
    static void Print(Dictionary<int, string> i)
    {
        Console.Write("dictionary: ");
        foreach (var item in i) Console.Write(item);
        Console.WriteLine();
    }
    static void Print(Human i)
    {
        Console.Write("Human: ");
        Console.Write($"firstname: {i.Firstname} lastname: {i.Lastname} nationality: {i.Nationality} year: {i.Birth} ");
        Console.WriteLine();
    }
    static void Print(Race i)
    {
        Console.Write($"Race: id: {i.Id} start: {i.Start}");
        Console.Write("competitors: [");
        foreach (var h in i.Competitors)
        {
            
            Console.Write($"firstname: {h.Firstname} lastname: {h.Lastname} nationality: {h.Nationality} year: {h.Birth} ");
        }
        Console.Write("]");
        Console.WriteLine();
    }

}