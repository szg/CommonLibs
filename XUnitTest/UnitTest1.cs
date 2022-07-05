using System;
using Xunit;

namespace XUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void PinYinTest()
        {
            string p1 = CommonLibs.Utilities.PinYinUtilitity.GetPinyin("你说的");
            string p2 = CommonLibs.Utilities.PinYinUtilitity.GetInitials("你说的");
            string p3 = CommonLibs.Utilities.PinYinUtilitity.GetChineseText("wo");

            var mongodbId = CommonLibs.IdGenerator.IdGen.GetMongodbId();
        }
    }
}
