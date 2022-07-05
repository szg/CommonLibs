using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp461
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();
            WriteUser();

            WriteFlight();


            Console.ReadKey();
        }

        static void Init()
        {
            GoogleProtobufExtensions.InitProtobufType(typeof(CUser), typeof(User), typeof(NativeFlightShoppingModel));
        }

        static void WriteUser()
        {
            var user = new CUser { Name = "sunny", TName = "tsunny" };

            var pro = user.ToProtobuf<CUser>();

            var fromu = pro.FromProtobuf<CUser>();

            Console.WriteLine($"pro:{pro}");
        }

        static void WriteFlight()
        {
            var text = File.ReadAllText(@"d:\buf.txt", Encoding.UTF8);

            var model = text.FromProtobuf<NativeFlightShoppingModel>();
        }
    }
}
