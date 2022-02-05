namespace IdeaHelper;

internal class Program
{
    private static void Main(string[] args)
    {
        DynamicObjectExample();
    }

    private static void DynamicObjectExample()
    {
        dynamic dynamicObj = new DynamicObjectExample();
        static double getPiNumber() => 3.14;
        dynamicObj.GetPiNumber = (Func<double>)getPiNumber;
        dynamicObj.PiNumber = 3.14;

        // If Method is called, 'TryInvokeMember' is triggered.
        var pi = dynamicObj.GetPiNumber();

        // If Property is called, 'TryGetMember' is triggered.
        double piProp = dynamicObj.PiNumber;

        Console.WriteLine($"Hello World! {pi}");
    }
}