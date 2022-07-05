namespace System
{
#pragma warning disable CS1591 
    public static class ConvertExtensions
    {
        public static Int32 ToInt32(this Object obj, int defaults = default)
        {
            return obj == null ? defaults : ((IConvertible)obj).ToInt32(null);
        }
        public static Int64 ToInt64(this Object obj, Int64 defaults = default)
        {
            return obj == null ? defaults : ((IConvertible)obj).ToInt64(null);
        }
        public static Boolean ToBoolean(this Object obj, Boolean defaults = default)
        {
            if (obj == null || obj.ToString().IsNullOrEmpty())
                return defaults;
            var str = obj.ToString().ToLower();
            if (str == "1" || str == "true" || str == "t") return true;
            return defaults;
        }
        public static Byte ToByte(this Object obj, Byte defaults = default)
        {
            return obj == null ? defaults : ((IConvertible)obj).ToByte(null);
        }
        public static Char ToChar(this Object obj, Char defaults = default)
        {
            return obj == null ? defaults : ((IConvertible)obj).ToChar(null);
        }
        public static DateTime ToDateTime(this Object obj, DateTime defaults = default)
        {
            return obj == null ? defaults : ((IConvertible)obj).ToDateTime(null);
        }
        public static Decimal ToDecimal(this Object obj, Decimal defaults = default)
        {
            return obj == null ? defaults : ((IConvertible)obj).ToDecimal(null);
        }
        public static Double ToDouble(this Object obj, Double defaults = default)
        {
            return obj == null ? defaults : ((IConvertible)obj).ToDouble(null);
        }
        public static short ToInt16(this Object obj, short defaults = default)
        {
            return obj == null ? defaults : ((IConvertible)obj).ToInt16(null);
        }
        public static sbyte ToSByte(this Object obj, sbyte defaults = default)
        {
            return obj == null ? defaults : ((IConvertible)obj).ToSByte(null);
        }
        public static float ToSingle(this Object obj, float defaults = default)
        {
            return obj == null ? defaults : ((IConvertible)obj).ToSingle(null);
        }
        public static ushort ToUInt16(this Object obj, ushort defaults = default)
        {
            return obj == null ? defaults : ((IConvertible)obj).ToUInt16(null);
        }
        public static uint ToUInt32(this Object obj, uint defaults = default)
        {
            return obj == null ? defaults : ((IConvertible)obj).ToUInt32(null);
        }
        public static ulong ToUInt64(this Object obj, ulong defaults = default)
        {
            return obj == null ? defaults : ((IConvertible)obj).ToUInt64(null);
        }


        public static Int32 ToSafeInt32(this Object obj, int defaults = default)
        {
            if (obj == null || obj.ToString().IsNullOrWhiteSpace())
            {
                return defaults;
            }
            if (obj is Int32 || obj is Int64 || obj is Byte || obj is Char || obj is Decimal || obj is Double || obj is Single || obj is UInt64 || obj is UInt32 || obj is UInt16 || obj is SByte || obj is Int16)
            {
                return Convert.ToInt32(obj);
            }
            if (Int32.TryParse(obj.ToString(), out var result))
            {
                return result;
            }
            return defaults;
        }
        public static Int64 ToSafeInt64(this Object obj, Int64 defaults = default)
        {
            if (obj == null || obj.ToString().IsNullOrWhiteSpace())
            {
                return defaults;
            }
            if (obj is Int32 || obj is Int64 || obj is Byte || obj is Char || obj is Decimal || obj is Double || obj is Single || obj is UInt64 || obj is UInt32 || obj is UInt16 || obj is SByte || obj is Int16)
            {
                return Convert.ToInt64(obj);
            }
            if (Int64.TryParse(obj.ToString(), out var result))
            {
                return result;
            }
            return defaults;
        }
        public static DateTime ToSafeDateTime(this Object obj, DateTime defaults = default)
        {
            if (obj == null || obj.ToString().IsNullOrWhiteSpace())
            {
                return defaults;
            }
            if (DateTime.TryParse(obj.ToString(), out var date))
            {
                return date;
            }
            return defaults;
        }
    }
#pragma warning restore CS1591
}
