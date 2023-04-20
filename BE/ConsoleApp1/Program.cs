using System.IO;
using ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        var productVariants = new ProductVariant();
        var productVariantValueTexts = new ProductVariantValueText();
        var productVariantValueInts = new ProductVariantValueInt();
        var productAttributeTexts = new ProductAttributeText();
        var productAttributeInts = new ProductAttributeInt();

        var productVariantsList = productVariants.GetProductVariants();
        var productVariantValueTextsList = productVariantValueTexts.GetProductVariantValueTexts();
        var productVariantValueIntsList = productVariantValueInts.GetProductVariantValueInts();
        var productAttributeTextsList = productAttributeTexts.GetProductAttributeTexts();
        var productAttributeIntsList = productAttributeInts.GetProductAttributeInts();
        IDictionary<string, List<string>> p = new Dictionary<string, List<string>>();
        p.Add("P1", new List<string>{ "Red", "Blue"});
        p.Add("P2", new List<string>{ "Red1", "Blue2"});
        p.Add("P3", new List<string>{ "Red2", "Blue3"});

        foreach (KeyValuePair<string, List<string>> kvp in p)
        {
            Console.WriteLine("Key: {0}", kvp.Key);
            foreach (var item in kvp.Value)
            {
                Console.WriteLine(item);
            }
        }
        Console.Read();
    }
    
    public class PV
    {
        public List<Option> Options { get; set; }
    }
    public class Option
    {
        public List<string> Value { get; set; }
    }
}
