using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp45
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();
            WriteUser();
            Console.ReadKey();
        }

        static void Init()
        {
            GoogleProtobufExtensions.InitProtobufType(typeof(Common.Model.User));
        }

        static void WriteUser()
        {
            var user = new Common.Model.User { };

            var pro = user.ToProtobuf();

            Console.WriteLine($"pro:{pro}");
            Console.ReadKey();
        }
    }
}
