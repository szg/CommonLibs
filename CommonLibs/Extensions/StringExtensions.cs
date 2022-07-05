using System.Text.RegularExpressions;

namespace System
{
#pragma warning disable CS1591
    public static class StringExtensions
    {
        /// <summary>
        /// 指示指定的字符串是 null 还是 <see cref="System.String.Empty"/> 字符串
        /// </summary>
        /// <param name="value"> 要测试的字符串</param>
        /// <returns>如果 true 参数为 value 或空字符串 ("")，则为 null；否则为 false。</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return (value == null || value.Length == 0);
        }

        /// <summary>
        /// 指示指定的字符串是 null、空还是仅由空白字符组成
        /// </summary>
        /// <param name="value">要测试的字符串。</param>
        /// <returns>如果 true 参数为 value 或 null，或者如果 System.String.Empty 仅由空白字符组成，则为 value。</returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            if (value == null) return true;

            for (int i = 0; i < value.Length; i++)
            {
                if (!Char.IsWhiteSpace(value[i])) return false;
            }

            return true;
        }

        /// <summary>
        /// 验证字符串是否为手机号码
        /// </summary>
        /// <param name="value">要测试的字符串。</param>
        /// <returns>true 是手机号码，false 不是手机号码</returns>
        public static bool IsMobile(this string value)
        {
            if (value == null) return false;

            var regex = new Regex(@"^1[3-9]+\d{9}");

            return regex.IsMatch(value);
        }
    }
#pragma warning restore CS1591
}
