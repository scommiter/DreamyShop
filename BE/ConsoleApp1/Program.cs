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
        List<Student> student_List = new List<Student>() {
            new Student() { Stu_ID = 11, Stu_Name = "ABC", Age =18, Subject ="Arts" },
            new Student() { Stu_ID = 12, Stu_Name = "DEF", Age =19, Subject ="Arts" },
            new Student() { Stu_ID = 13, Stu_Name = "GHI", Age =18, Subject ="Arts" },
            new Student() { Stu_ID = 14, Stu_Name = "JKL", Age =19, Subject ="Science" },
            new Student() { Stu_ID = 15, Stu_Name = "JKL", Age =18, Subject ="Science" },
            new Student() { Stu_ID = 16, Stu_Name = "JKL", Age =19, Subject ="Arts" },
        };
        var result = from s in student_List
                     group s by new { s.Age, s.Subject };
        foreach (var ageGroups in result)
        {
            Console.WriteLine("Group: {0}", ageGroups.Key);

            foreach (Student s in ageGroups) // Each group has inner collection
                Console.WriteLine("Student Name: {0}", s.Stu_Name);
        }

        Console.Read();
    }
}
