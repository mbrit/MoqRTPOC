using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moq.Baked
{
    public abstract class BakedMock<T> : Mock<T>
        where T : class
    {
        protected BakedMock()
        {
        }

        public override ISetup<T, TResult> Setup<TResult>(Expression<Func<T, TResult>> expression)
        {
            return new BakedSetup<T, TResult>(expression);
        }
    }
}
