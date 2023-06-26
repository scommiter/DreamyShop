using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Common.Extensions
{
    public static class NumberExtension
    {
        public static string ConvertToVND(this double number)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            return number.ToString("#,###", cul.NumberFormat);
        }
    }
}
