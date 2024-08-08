using System.Text;
using static System.Console;

namespace DesignPatterns.Builder.CodingExercises;

/*
    Builder Coding Exercise

    You are asked to implement the Builder design pattern for rendering simple chunks of code.

    Sample use of the builder you are asked to create:

    var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
    Console.WriteLine(cb);
    The expected output of the above code is:

    public class Person
    {
      public string Name;
      public int Age;
    }
    Please observe the same placement of curly braces and use two-space indentation.

*/

public class ClassElement
{
    private const int IndentSize = 2;

    public ClassElement()
    {
        Name = string.Empty;
        Properties = new List<(string, string)>();
    }

    public string Name { get; set; }

    public List<(string, string)> Properties { get; set; }

    private string ToStringImplementation(int indent)
    {
        var sb = new StringBuilder();
        var i = new string(' ', IndentSize * indent);
        sb.Append($"{i}public class {Name}\n");
        sb.Append($"{{\n");

        foreach (var property in Properties)
        {
            sb.Append(new string(' ', IndentSize * (indent + 1)));
            sb.Append($"public {property.Item2} {property.Item1};\n");
        }

        sb.Append($"}}");
        return sb.ToString();
    }

    public override string ToString()
    {
        return ToStringImplementation(0);
    }
}

public class CodeBuilder
{
    private ClassElement ClassElement = new ClassElement();

    public CodeBuilder(string className)
    {
        ClassElement.Name = className;
    }

    public CodeBuilder AddField(string fieldName, string type)
    {
        ClassElement.Properties.Add((fieldName, type));
        return this;
    }

    public override string ToString()
    {
        return ClassElement.ToString();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var cb = new CodeBuilder("Person")
            .AddField("Name", "string")
            .AddField("Age", "int");
        WriteLine(cb);
    }
}