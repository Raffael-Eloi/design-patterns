using System.Diagnostics;
using static System.Console;

namespace SOLID.Principles._0___SRP;

/*
 * SRP - Single Responsibility Principle
 * Every module or class should have responsibility over a single part of the functionality provided by the software
 * It should have one reason to change
*/

// just stores a couple of journal entries and ways of
// working with them
public class Journal
{
    private readonly List<string> entries = new List<string>();

    private static int count = 0;

    public int AddEntry(string text)
    {
        entries.Add($"{++count}: {text}");
        return count; // memento pattern!
    }

    public void RemoveEntry(int index)
    {
        entries.RemoveAt(index);
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, entries);
    }

    // breaks single responsibility principle
    public void Save(string filename, bool overwrite = false)
    {
        File.WriteAllText(filename, ToString());
    }

    public static void Load(string filename)
    {

    }

    public static void Load(Uri uri)
    {

    }

    /*
        The journal has more than one reason to change
        - Adding/removing entries
        - Saving/loading entries
    */
}

// handles the responsibility of persisting objects
public class Persistence
{
    public static void SaveToFile(Journal journal, string filename, bool overwrite = false)
    {
        if (overwrite || !File.Exists(filename))
            File.WriteAllText(filename, journal.ToString());
    }
}

public class Demo
{
    static void Main(string[] args)
    {
        var journal = new Journal();
        journal.AddEntry("Today was a good day.");
        journal.AddEntry("I fixed a bug.");
        WriteLine(journal);

        var persistence = new Persistence();
        var filename = @"C:\Users\raffa\source\repos\design-patterns\src\SOLID.Principles\0 - SRP\journal.txt";
        Persistence.SaveToFile(journal, filename);

        var p = new Process();
        p.StartInfo = new ProcessStartInfo(filename)
        {
            UseShellExecute = true
        };
        p.Start();
    }
}