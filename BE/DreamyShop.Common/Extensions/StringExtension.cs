using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DreamyShop.Common.Extensions
{
    public static class StringExtension
    {
    public static string RemoveVietnameseDiacritics(this string text)
    {
        // Chuỗi chứa các ký tự có dấu tiếng Việt
        string vietnameseDiacritics = "àáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđÀÁẠẢÃÂẦẤẬẨẪĂẰẮẶẲẴÈÉẸẺẼÊỀẾỆỂỄÌÍỊỈĨÒÓỌỎÕÔỒỐỘỔỖƠỜỚỢỞỠÙÚỤỦŨƯỪỨỰỬỮỲÝỴỶỸĐ";

        // Chuỗi chứa các ký tự không dấu tương ứng
        string withoutDiacritics = "aaaaaaaaaaaaaaaaaeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyydAAAAAAAAAAAAAAAAAEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYD";

        // Sử dụng biểu thức chính quy để thay thế các ký tự có dấu
        Regex regex = new Regex($"[{vietnameseDiacritics}]");
        return regex.Replace(text, m => withoutDiacritics[vietnameseDiacritics.IndexOf(m.Value)].ToString());
    }


    /// <summary>
    /// Remove all white and space of text
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string RemoveAllWhiteSpace(this string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            return text.Replace(" ", String.Empty);
        }

        /// <summary>
        /// Trim and ToLower
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Standard(this string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            return text.Trim().ToLower();
        }
    }
}
