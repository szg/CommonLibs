using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp40
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
            GoogleProtobufExtensions.InitProtobufType(typeof(Common.Model.TEntity));
        }

        static void WriteUser()
        {
            var user = new Common.Model.User { Name = "sunny" };

            var pro = user.ToProtobuf();

            Console.WriteLine($"pro:{pro}");

            var en = pro.FromProtobuf<User>();

            Console.WriteLine(en?.Name);

            Console.ReadKey();
        }
    }
}
