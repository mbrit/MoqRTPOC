using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moq.Baking
{
    internal class BakingSetup<TMock, TResult> : ISetup<TMock, TResult>
        where TMock : class
    {
        private Expression<Func<TMock, TResult>> Expression { get; set; }

        internal BakingSetup(Expression<Func<TMock, TResult>> expression)
        {
            this.Expression = expression;
        }

        public void Returns(object value)
        {
            // track the call...
            using (var conn = MoqRTRuntime.GetBakingConnection())
            {
                BakingItem item = new BakingItem() 
                {
                    MethodItemId = MoqRTRuntime.RunningMethod.Id,
                    Expression = this.Expression.ToString()
                };
                conn.Insert(item);
            }
        }
    }
}
