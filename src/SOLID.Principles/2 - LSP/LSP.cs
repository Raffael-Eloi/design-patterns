﻿using static System.Console;

namespace SOLID.Principles._2___LSP;

/*
 * LSP - Liskov Substitution Principle
 * You should be able to substitute a base type for a subtype
*/

// using a classic example
public class Rectangle
{
    //public int Width { get; set; }
    //public int Height { get; set; }

    // The virtual keyword is used to modify a method, property, indexer, or event declaration and allow for it to be overridden in a derived class.

    public virtual int Width { get; set; }
    public virtual int Height { get; set; }

    public Rectangle()
    {

    }

    public Rectangle(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public override string ToString()
    {
        return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
    }
}

public class Square : Rectangle
{
    //public new int Width
    //{
    //  set { base.Width = base.Height = value; }
    //}

    //public new int Height
    //{ 
    //  set { base.Width = base.Height = value; }
    //}

    public override int Width // nasty side effects
    {
        set { base.Width = base.Height = value; }
    }

    public override int Height
    {
        set { base.Width = base.Height = value; }
    }
}

public class Demo
{
    static public int Area(Rectangle r) => r.Width * r.Height;

    static void MainLSP(string[] args)
    {
        Rectangle rc = new Rectangle(2, 3);
        WriteLine($"{rc} has area {Area(rc)}");

        // should be able to substitute a base type for a subtype
        /*Square*/
        Rectangle sq = new Square();
        sq.Width = 4;
        WriteLine($"{sq} has area {Area(sq)}");
    }
}