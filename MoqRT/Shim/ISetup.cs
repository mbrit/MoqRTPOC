using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moq
{
    public interface ISetup<TMock, TResult>
        where TMock : class
    {
        void Returns(object value);
    }
}
