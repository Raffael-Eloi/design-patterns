namespace SOLID.Principles._3___ISP;

/*
 * ISP - Interface Segregation Principle
 * The interface-segregation principle (ISP) states that no client should be forced to depend on methods it does not use.
*/

public class Document
{
}

public interface IMachine
{
    void Print(Document d);
    void Fax(Document d);
    void Scan(Document d);
}

// ok if you need a multifunction machine
public class MultiFunctionPrinter : IMachine
{
    public void Print(Document d)
    {
        //
    }

    public void Fax(Document d)
    {
        //
    }

    public void Scan(Document d)
    {
        //
    }
}

public class OldFashionedPrinter : IMachine
{
    public void Print(Document d)
    {
        // yep
    }

    /*
     * The OldFashionedPrinter class is forced to implement the Fax and Scan methods even though it doesn't use them.
    */

    public void Fax(Document d)
    {
        throw new NotImplementedException();
    }

    public void Scan(Document d)
    {
        throw new NotImplementedException();
    }
}

/*
 * The solution is to create separate interfaces for each of the methods.
*/

public interface IPrinter
{
    void Print(Document d);
}

public interface IScanner
{
    void Scan(Document d);
}

public class Printer : IPrinter
{
    public void Print(Document d)
    {

    }
}

public class Photocopier : IPrinter, IScanner
{
    public void Print(Document d)
    {
        throw new NotImplementedException();
    }

    public void Scan(Document d)
    {
        throw new NotImplementedException();
    }
}

public interface IMultiFunctionDevice : IPrinter, IScanner //
{

}

public struct MultiFunctionMachine : IMultiFunctionDevice
{
    /*
     * The implentation of that is up to you
     * Here is just an example
    */

    // compose this out of several modules
    private IPrinter printer;
    private IScanner scanner;

    public MultiFunctionMachine(IPrinter printer, IScanner scanner)
    {
        if (printer == null)
        {
            throw new ArgumentNullException(paramName: nameof(printer));
        }
        if (scanner == null)
        {
            throw new ArgumentNullException(paramName: nameof(scanner));
        }
        this.printer = printer;
        this.scanner = scanner;
    }

    public void Print(Document d)
    {
        printer.Print(d);
    }

    public void Scan(Document d)
    {
        scanner.Scan(d);
    }
}