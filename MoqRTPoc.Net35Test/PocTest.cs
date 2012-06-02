using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

#if !NETFX_CORE
using Microsoft.VisualStudio.TestTools.UnitTesting;
#else
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#endif

namespace MoqRTPoc
{
    [TestClass]
    public class PocTest
    {
        [TestMethod()]
        public void TestMagic()
        {
            var foo = Mock.Create<IFoo>();
            foo.Setup(magic => magic.DoMagic("a")).Returns("foo");
            foo.Setup(magic => magic.DoMagic("b")).Returns("bar");

            // go...
            Assert.AreEqual("foo", foo.Object.DoMagic("a"));
            Assert.AreEqual("bar", foo.Object.DoMagic("b"));
        }
    }
}
