using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections;

public class BinaryExample
{
    static void Main(string[] args)
    {
        Hashtable p1 = new Hashtable();
        p1.Add("Jim", "Lorem ipsum");
        p1.Add("Otto", "Dolor sit amet");

        FileStream fs = new FileStream("people.dat", FileMode.Create);

        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            formatter.Serialize(fs, p1);
        }
        catch (SerializationException f)
        {
            Console.WriteLine("Failed to serialize. Reason: " + f.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
        Hashtable p = new Hashtable();
        FileStream fs2 = new FileStream("people.dat", FileMode.Open);
        try
        {
            BinaryFormatter formatter2 = new BinaryFormatter();
            p = (Hashtable)formatter2.Deserialize(fs2);
        }
        catch (SerializationException f)
        {
            Console.WriteLine("Failed to deserialize. Reason: " + f.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }

        foreach (DictionaryEntry de in p)
        {
            Console.WriteLine("{0} lives at {1}.", de.Key, de.Value);
        }
    }
}