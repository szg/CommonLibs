using ProtoBuf;
using ProtoBuf.Meta;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Concurrent;

namespace System
{
    /// <summary>
    /// Google Protobuf-net 扩展
    ///
    /// 修改记录
    ///		2020.08.06 版本：1.0.0 sunnyfish 创建。
    ///		2020.08.07 版本：1.0.1 sunnyfish 增加支持继承类属性映射。
    ///
    /// <author>
    ///		<name>sunnyfish</name>
    ///		<date>2020.08.06</date>
    /// </author>
    /// </summary>
    public static class GoogleProtobufExtensions
    {
        private static object objLock = new object();

        /// <summary>
        /// 将实体转为base64字符串
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="obj">要转换的实体</param>
        /// <returns>返回转换后的字符串</returns>
        public static string ToProtobuf<T>(this T obj)
        {
            InitProtobufType(typeof(T));
            string strBuf = null;
            if (obj != null)
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    Serializer.Serialize<T>(mStream, obj);
                    byte[] datas = mStream.ToArray();
                    mStream.Position = 0;
                    strBuf = datas.ToBase64();
                }
            }
            return strBuf;
        }

        /// <summary>
        /// 将base64字符串转为实体
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="protobuf">base64 字符串</param>
        /// <returns>返回转换后的实体</returns>
        public static T FromProtobuf<T>(this string protobuf)
        {
            InitProtobufType(typeof(T));
            T obj = default(T);
            if (!string.IsNullOrEmpty(protobuf))
            {
                try
                {
                    byte[] binaryEntity = protobuf.ToBase64();
                    using (MemoryStream mStream = new MemoryStream(binaryEntity))
                    {
                        obj = Serializer.Deserialize<T>(mStream);
                    }
                }
                catch (ProtoException protoException)
                {
                    throw new Exception("ProtoException", protoException);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return obj;
        }

        /// <summary>
        /// 将实体转为 byte数组
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="obj">要转换的实体</param>
        /// <returns>返回转换后的byte数组</returns>
        public static byte[] ToProtobufBytes<T>(this T obj)
        {
            InitProtobufType(typeof(T));
            byte[] datas = new byte[0];
            if (obj != null)
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    Serializer.Serialize<T>(mStream, obj);
                    datas = mStream.ToArray();
                }
            }
            return datas;
        }

        /// <summary>
        /// 将byte数组转为实体
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="protobuf">byte数组</param>
        /// <returns>返回转换后的实体</returns>
        public static T FromProtobufBytes<T>(this byte[] protobuf)
        {
            InitProtobufType(typeof(T));
            T obj = default(T);
            if (protobuf.Length > 0)
            {
                using (MemoryStream mStream = new MemoryStream(protobuf))
                {
                    obj = ProtoBuf.Serializer.Deserialize<T>(mStream);
                }
            }
            return obj;
        }

        private static String ToBase64(this Byte[] data, Int32 offset = 0, Int32 count = 0, Boolean lineBreak = false)
        {
            if (data == null || data.Length < 1) return null;
            if (count <= 0)
                count = data.Length - offset;
            else if (offset + count > data.Length)
                count = data.Length - offset;

            return Convert.ToBase64String(data, offset, count, lineBreak ? Base64FormattingOptions.InsertLineBreaks : Base64FormattingOptions.None);
        }

        private static Byte[] ToBase64(this String data)
        {
            if (String.IsNullOrEmpty(data)) return null;

            return Convert.FromBase64String(data);
        }

        private static ConcurrentQueue<string> typeList = new ConcurrentQueue<string>();

        /// <summary>
        /// 将 类型 转化为 支持 protobuf-net 序列化的类型，也可以直接使用
        /// </summary>
        /// <param name="types">类型</param>
        public static void InitProtobufType(params Type[] types)
        {
            if (types == null || types.Length < 1)
            {
                throw new ArgumentNullException(nameof(types));
            }

            lock (objLock)
            {
                foreach (var type in types)
                {
                    AddTypeToModel(type, RuntimeTypeModel.Default);
                }
            }
        }

        private static void AddTypeToModel(Type type, RuntimeTypeModel typeModel)
        {
            if (typeList.Contains(type.FullName))
            {
                return;
            }

            typeList.Enqueue(type.FullName);

            if (typeModel.IsDefined(type))
            {
                return;
            }

            typeModel.IncludeDateTimeKind = true;

            var meta = typeModel.Add(type, true);

            AddMetaType(meta, type, typeModel);
        }

        private static void AddMetaType(MetaType meta, Type type, RuntimeTypeModel typeModel)
        {
            // 存在父类，先映射父类属性,并且父类
            if (!type.BaseType.FullName.Equals("System.Object") && !type.BaseType.FullName.Equals(type.FullName))
            {
                AddMetaType(meta, type.BaseType, typeModel);
            }

            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(x => x.GetSetMethod() != null);

            foreach (var property in properties)
            {
                meta.Add(property.Name);
            }

            var complexProperties = properties.Where(_ => !IsSimpleType(_.PropertyType)).OrderBy(_ => _.Name);

            // 复杂类型需要处理里面的每个简单类型，使用了递归操作
            foreach (var complexProperty in complexProperties)
            {
                if (complexProperty.PropertyType.IsGenericType)
                {
                    // Protobuf的顺序很重要，在序列化跟反序列化都需要保持一致的顺序，否则反序列化的时候就会出错
                    foreach (var genericArgumentType in complexProperty.PropertyType.GetGenericArguments().OrderBy(h => h.Name))
                    {
                        if (!IsSimpleType(genericArgumentType))
                        {
                            AddTypeToModel(genericArgumentType, typeModel);
                        }
                    }
                }
                else if (complexProperty.PropertyType.IsArray)
                {
                    AddTypeToModel(complexProperty.PropertyType.GetElementType(), typeModel);
                }
                else
                {
                    AddTypeToModel(complexProperty.PropertyType, typeModel);
                }
            }
        }

        /// <summary>
        /// 是否为简单类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsSimpleType(Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type);
            var newType = underlyingType ?? type;
            var simpleTypes = new List<Type>
                               {
                                   typeof(byte),
                                   typeof(sbyte),
                                   typeof(short),
                                   typeof(ushort),
                                   typeof(int),
                                   typeof(uint),
                                   typeof(long),
                                   typeof(ulong),
                                   typeof(float),
                                   typeof(double),
                                   typeof(decimal),
                                   typeof(bool),
                                   typeof(string),
                                   typeof(char),
                                   typeof(Guid),
                                   typeof(DateTime),
                                   typeof(DateTimeOffset),
                                   typeof(byte[]),
                                   typeof(string[])
                               };
            return simpleTypes.Contains(newType) ||
#if NET40
                newType.GetType()
#else
                newType.GetTypeInfo()
#endif
                .IsEnum;
        }
    }
}