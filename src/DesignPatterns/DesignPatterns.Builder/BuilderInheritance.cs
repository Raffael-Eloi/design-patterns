namespace DesignPatterns.Builder;

public class Person
{
    public string Name;

    public string Position;

    public DateTime DateOfBirth;

    public class Builder : PersonBirthDateBuilder<Builder>
    {
        internal Builder() { }
    }

    public static Builder New => new Builder();

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    }
}

public abstract class PersonBuilder
{
    protected Person person = new Person();

    /*
     * The method is abstract, so it must be implemented in the derived class.
     * The build method was used 
    */

    public Person Build()
    {
        return person;
    }
}

/*
 * The SELF generic type parameter is used to return the derived class type.
 * the where SELF : PersonBuilder constraint is used to ensure that the derived class is a PersonBuilder.
 * class Example : PersonBuilder<Example> is a valid class declaration.
*/
public class PersonInfoBuilder<SELF> : PersonBuilder
  where SELF : PersonInfoBuilder<SELF>
{
    public SELF Called(string name)
    {
        person.Name = name;
        return (SELF)this;
    }
}

/*
 * The PersonJobBuilder class inherits from the PersonInfoBuilder class.
 * The method WorksAsA is used to set the Position property of the Person class.
*/

public class PersonJobBuilder<SELF>
  : PersonInfoBuilder<PersonJobBuilder<SELF>>
  where SELF : PersonJobBuilder<SELF>
{
    public SELF WorksAsA(string position)
    {
        person.Position = position;
        return (SELF)this;
    }
}

// here's another inheritance level
// note there's no PersonInfoBuilder<PersonJobBuilder<PersonBirthDateBuilder<SELF>>>!

public class PersonBirthDateBuilder<SELF>
  : PersonJobBuilder<PersonBirthDateBuilder<SELF>>
  where SELF : PersonBirthDateBuilder<SELF>
{
    public SELF Born(DateTime dateOfBirth)
    {
        person.DateOfBirth = dateOfBirth;
        return (SELF)this;
    }
}

internal class Program
{
    class SomeBuilder : PersonBirthDateBuilder<SomeBuilder>
    {

    }

    public static void MainBuilderInheritance(string[] args)
    {
        var me = Person.New
          .Called("Raffael")
          .WorksAsA("Software Engineer")
          .Born(DateTime.UtcNow)
          .Build();
        Console.WriteLine(me);
    }
}