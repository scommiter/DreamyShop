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

        //var result = productVariantsList.Select(
        //        variant => new ProductDto
        //        {
        //            ProductId = variant.ProductId,
        //            Quantity = variant.Quantity.ToString(),
        //            Price = variant.Price,
        //            //AttributeValue = string.Join(" ", productAttributeTextsList
        //            //                    .Where(d => productVariantValueTextsList.Any(pvvt => pvvt.ProductAttributeTextId == d.Id && pvvt.ProductId == d.ProductId))
        //            //                    .Select(pai => pai.Value))
        //            AttributeValue = string.Join(" ", productVariantValueTextsList
        //                                            .GroupBy(p => p.ProductVariantId)
        //                                            .Where(d => d.Key == variant.Id)
        //                                            .Where(v => v.All(variantV => productAttributeTextsList.Any(ppvt => ppvt.Id == variantV.ProductAttributeTextId)))
                                                    
        //        }
        //    );
        var result2 = productVariantValueTextsList
                .GroupBy(p => p.ProductVariantId)
                .Select(g => string.Join(" ", g.Select(p => productAttributeTextsList
                        .FirstOrDefault(ppvt => ppvt.Id == p.ProductAttributeTextId)?.Value))).ToList();

        var query = from pv in productVariantValueTextsList
                    join piv in productVariantValueIntsList on pv.ProductVariantId equals piv.ProductVariantId into pivGroup
                    from piv in pivGroup.DefaultIfEmpty()
                    group new { pv, piv } by pv.ProductVariantId;

        var result = (from pv in productVariantsList
                      join pvt1 in productVariantValueTextsList on pv.Id equals pvt1.ProductId into pvt1Group
                      from pvt1 in pvt1Group.DefaultIfEmpty()
                      join pai1 in productVariantValueIntsList on pv.Id equals pai1.ProductId into pai1Group
                      from pai1 in pai1Group.DefaultIfEmpty()
                      join pat1 in productAttributeTextsList on pvt1.ProductAttributeTextId equals pat1.Id into pat1Group
                      from pat1 in pat1Group.DefaultIfEmpty()
                      join pai2 in productAttributeIntsList on pai1.ProductAttributeIntId equals pai2.Id into pai2Group
                      from pai2 in pai2Group.DefaultIfEmpty()
                      select new
                      {
                          Attribute1 = pat1.Value,
                          Attribute2 = pat2.Value,
                          Attribute3 = pai2.Value,
                          Quantity = pv.Quantity,
                          Price = pv.Price
                      }).ToList();


        //var results = productVariantsList
        //    .Join(productVariantValueTextsList, pv => pv.Id, pAttriValueText => pAttriValueText.ProductVariantId, (pv, pAttriValueText) => new {ProductVariant = pv, ProductVariantValueText = pAttriValueText})
        //    .Join(productVariantValueIntsList, pv => pv.ProductVariant.Id, pAttriValueInt => pAttriValueInt.ProductVariantId, (pv, pAttriValueInt) => new { ProductVariant = pv, ProductVariantValueText = pAttriValueText })

        //Console.WriteLine(result.ToList());
        Console.Read();
    }
}
