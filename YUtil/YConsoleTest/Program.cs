using YCSharp;

public class Program
{
    static void Main(string[] args)
    {
        List<int> list = new List<int>();
        list.AddIfNotContain(1);
        list.AddIfNotContain(1);
        list.AddIfNotContain(1);
        list.AddIfNotContain(2);
        Console.WriteLine(list);
    }
}