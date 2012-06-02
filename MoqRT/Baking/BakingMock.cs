using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moq.Baking
{
    public sealed class BakingMock<T> : Mock<T>
        where T : class
    {
        internal BakingMock()
        {
        }

        public override T Object
        {
            get
            {
                throw new ObjectReferencedInBakingException();
            }
        }

        public override ISetup<T, TResult> Setup<TResult>(System.Linq.Expressions.Expression<Func<T, TResult>> expression)
        {
            return new BakingSetup<T, TResult>(expression);
        }
    }
}
