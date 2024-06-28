using YCSharp;

public class Program
{
    static void Main(string[] args)
    {
        List<string> list = new List<string>();
        list.Add("a");
        list.Add("b");
        list.Add("c");
        list.Add(null);
        list.Add("d");
        list.Add("e");
        list.Add("f");

        list.RemoveElement((element) =>
        {
            return element == "a";
        });
        list.For(null, (data) =>
        {
            Console.WriteLine(data);
        });
    }
}