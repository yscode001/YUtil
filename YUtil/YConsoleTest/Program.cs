using YCSharp;

public class Program
{
    static void Main(string[] args)
    {
        List<int> list = new List<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.ForReverse(null, null, data =>
        {
            Console.WriteLine(data);
        });
        list.For(null, null, data =>
        {
            Console.WriteLine(data);
        });
        Console.WriteLine(1);
    }
}