using IdeaHelper.TreeMaker;
using IdeaHelper.TreeMaker.Models;
using Newtonsoft.Json;

namespace IdeaHelper;

internal partial class Program
{
    private static void Main(string[] args)
    {
        BuildFlatListToTree();

        Console.ReadLine();
    }

    private static void BuildFlatListToTree()
    {
        var flatList = new List<FlatList>()
        {
            new FlatList() { Path = "~/Foo/Images/bar.jpg"},
            new FlatList() { Path = "~/Foo/Images/data.jpg"},
            new FlatList() { Path = "~/Foo/Images/mediaback.png"},
            new FlatList() { Path = "~/Foo/Videos/1-2.mp4"},
            new FlatList() { Path = "~/Foo/Videos/1-3.mp4"},
            new FlatList() { Path = "~/Foo/Videos/Clasic/museum.mp4"},
            new FlatList() { Path = "~/Foo/Documents/doc/virtual.doc"},
            new FlatList() { Path = "~/Foo/Documents/doc/real.doc"},
            new FlatList() { Path = "~/Foo/Documents/doc/intuitive.doc"},
            new FlatList() { Path = "~/Foo/Documents/doc/intuitive.doc"},
            new FlatList() { Path = "~/Foo/Documents/Pdf/analysis.pdf"},
            new FlatList() { Path = "~/Foo/aim.pdf"},
            new FlatList() { Path = "~/istanbul/Cafe/jpg/1.jpg"},
            new FlatList() { Path = "~/istanbul/Cafe/jpg/2.jpg"},
            new FlatList() { Path = "~/istanbul/Cafe/jpg/3.jpg"},
            new FlatList() { Path = "~/istanbul/Cafe/jpg/4.jpg"},
            new FlatList() { Path = "~/istanbul/Cafe/mp4/1.mp4"},
            new FlatList() { Path = "~/istanbul/Cafe/mp4/2.mp4"},
            new FlatList() { Path = "~/istanbul/Cafe/mp4/3.mp4"},
            new FlatList() { Path = "~/istanbul/Cafe/mp4/4.mp4"},
            new FlatList() { Path = "~/istanbul/Cafe/mp4/5.mp4"},
            new FlatList() { Path = "~/istanbul/Bar/jpg/terrace/1.png"},
            new FlatList() { Path = "~/istanbul/Bar/jpg/terrace/2.png"},
            new FlatList() { Path = "~/istanbul/Bar/jpg/terrace/3.png"},
            new FlatList() { Path = "~/istanbul/Bar/jpg/terrace/3.png"},
            new FlatList() { Path = "~/istanbul/architectural/plan.doc"},
        };

        var tree = BuildTreeByPath.Build<DefaultTreeModel, FlatList>(flatList);
        var json = JsonConvert.SerializeObject(tree, Formatting.Indented);

        using var reader = new StringReader(json);
        for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
        {
            Console.WriteLine(line);
        }
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