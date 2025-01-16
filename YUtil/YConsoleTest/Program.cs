using System.Text;

public partial class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Build");
        Console.WriteLine(GetMD5HashFromFile("/Users/yaoshuai/Desktop/资源/Build.zip"));
        Console.WriteLine("\n");

        Console.WriteLine("index");
        Console.WriteLine(GetMD5HashFromFile("/Users/yaoshuai/Desktop/资源/index.html.zip"));
        Console.WriteLine("\n");

        Console.WriteLine("PetH5");
        Console.WriteLine(GetMD5HashFromFile("/Users/yaoshuai/Desktop/资源/PetH5.zip"));
        Console.WriteLine("\n");

        Console.WriteLine("TemplateData");
        Console.WriteLine(GetMD5HashFromFile("/Users/yaoshuai/Desktop/资源/TemplateData.zip"));
        Console.WriteLine("\n");
    }
}
public partial class Program
{
    public static string GetMD5HashFromFile(string filepath)
    {
        try
        {
            FileStream file = new FileStream(filepath, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("ABBuildUtil-GetMD5HashFromFile() fail, error:" + ex.Message);
        }
    }
}