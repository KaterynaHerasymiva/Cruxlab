namespace Cruxlab;

public class Class1
{
    public string? Name { get; set; }
    public string? Description { get; set; }


    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        base.GetHashCode();

        return (Name + Description).GetHashCode();

    }
}

public abstract class A
{
    public A()
    {

    }

    public abstract void Do();

    public virtual void Do1()
    {

    }
}

public class B : A
{
    public B()
    {

    }

    public override void Do()
    {

    }

    public new virtual void Do1()
    {

    }
}

public class C : B
{
    public C()
    {

    }

    public override void Do()
    {

    }

    public override void Do1()
    {

    }
}
