namespace Facade;

public class Facade
{
    public void Operation1()
    {
        var class1 = new Class1();
        var class2 = new Class2();
        var class3 = new Class3();
        var class4 = new Class4(class2);
        
        class4.Operation5(class3);
        class1.Operation2();
        class3.Operation4();
    }
}