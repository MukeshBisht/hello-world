    public delegate Base Del(Derived d);

    public class Program
    {

        public static void Main()
        {
            Base b = new Derived();
            Base[] bs = [new Base()];
            bs[0] = new Derived();
            bs[0] = new Derived2();

            Derived[] ds = [new Derived()];
            // because covariant is supported 
            bs = ds;
            // covariant is not safe with arrays
            //bs[0] = new Derived2(); // Runtime error

            // contravariance is not supported with arrays
            // ds = bs;  // not allowed

            // covariant and contravariant is supported with delegates 
            Del del = new Del(Test1);
            del += Test2;
            del += Test3;

            del(new Derived());

            // covariant and contravariant with generics
            // covariant examples
            ICovariantGeneric<Derived> covariantGeneric = new CovariantGeneric<Derived>();
            ICovariantGeneric<Base> covariantGenericWithBase = covariantGeneric;


            // contravariant examples
            IContravariantGeneric<Base> contravariantGeneric = new ContravariantGeneric<Base>();
            IContravariantGeneric<Derived> contravariantGenericWithBase = contravariantGeneric; 

            Action<Base> action = (b) => Console.WriteLine(b.GetType().Name);
            Action<Derived> action1 = action;
            action1(new Derived());
        }

        public static Derived Test1(Derived a)
        {
            return new Derived();
        }

        public static Base Test2(Derived a)
        {
            return new Derived();
        }

        public static Base Test3(Base a)
        {
            return new Derived();
        }

    }
}


public class Base
{
    public void Print()
    {
        Console.WriteLine($"from { this.GetType().Name }");
    }
}

public class Derived : Base
{

}

public class Derived2 : Base
{

}

public interface ICovariantGeneric <out T>
{

}

public class CovariantGeneric<T> : ICovariantGeneric<T>
{

}

public interface  IContravariantGeneric <in T> {
    public void Print(T t);
}

public class ContravariantGeneric<T> : IContravariantGeneric<T>
{
    public void Print(T t)
    {
       Console.WriteLine(t.GetType().Name);
    }
}
