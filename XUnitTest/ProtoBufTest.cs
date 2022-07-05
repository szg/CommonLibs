using System;
using Xunit;

namespace XUnitTest
{
    public class ProtoBufTest
    {
        [Fact]
        public void BufTest()
        {
            //GoogleProtobufExtensions.InitProtobufType(typeof(FareInterface));
            var output = new FareInterfaceOutput
            {
                HeaderOut = "123",
                Result = "456"
            };
            var st = output.ToProtobuf();

            var interfact = new FareInterface
            {
                Output = new FareInterfaceOutput
                {
                    HeaderOut = "123",
                    Result = "456"
                }
            };

            var str = interfact.ToProtobuf();
            Assert.Null(str);
        }
    }

    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public class FareInterface
    {
        public FareInterfaceOutput Output { get; set; }
    }

    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public class FareInterfaceOutput
    {
        public string HeaderOut { get; set; }

        public string Result { get; set; }
    }
}