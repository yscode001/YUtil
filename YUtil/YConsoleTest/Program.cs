using YCSharp;

public class Program
{
    static void Main(string[] args)
    {
        List<string> list = new List<string>();
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        list.Add("5");
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");


        List<int> newlist = list.Convert(false, element =>
        {
            return element != "1";
        }, int.Parse);
        newlist.For(null, (data) =>
        {
            Console.WriteLine(data);
        });
    }
}