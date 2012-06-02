using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Moq.Baked;

namespace MoqRTPoc.Baked.Instances
{
    // this would be done by using the original Moq IL generation - just done in C# for ease...
    public class BakedForPocTestTestDoMagic : BakedMock<IFoo>, IFoo
    {
        public override IFoo Object
        {
            get
            {
                return this;
            }
        }

        public string DoMagic(string buf)
        {
            if (buf == "a")
                return DoMagicForReturnsA();
            else if (buf == "b")
                return DoMagicForReturnsB();
            else
                throw new NotSupportedException();
        }

        // foo.Setup(magic => magic.DoMagic("a")).Returns("foo");
        private string DoMagicForReturnsA()
        {
            return "foo";
        }

        // foo.Setup(magic => magic.DoMagic("b")).Returns("foo");
        private string DoMagicForReturnsB()
        {
            return "bar";
        }
    }
}
