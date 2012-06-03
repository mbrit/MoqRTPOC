﻿//Copyright (c) 2007. Clarius Consulting, Manas Technology Solutions, InSTEDD
//http://code.google.com/p/moq/
//All rights reserved.

//Redistribution and use in source and binary forms, 
//with or without modification, are permitted provided 
//that the following conditions are met:

//    * Redistributions of source code must retain the 
//    above copyright notice, this list of conditions and 
//    the following disclaimer.

//    * Redistributions in binary form must reproduce 
//    the above copyright notice, this list of conditions 
//    and the following disclaimer in the documentation 
//    and/or other materials provided with the distribution.

//    * Neither the name of Clarius Consulting, Manas Technology Solutions or InSTEDD nor the 
//    names of its contributors may be used to endorse 
//    or promote products derived from this software 
//    without specific prior written permission.

//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND 
//CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
//INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF 
//MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
//DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
//CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
//SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
//BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR 
//SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
//INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
//WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
//NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE 
//OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF 
//SUCH DAMAGE.

//[This is the BSD license, see
// http://www.opensource.org/licenses/bsd-license.php]

using System;
using System.Reflection;
#if !NETFX_CORE
using System.Security.Permissions;
#endif
using Castle.DynamicProxy;
using Castle.DynamicProxy.Generators;
using Moq.Properties;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;
using System.IO;

namespace Moq.Proxy
{
	internal class CastleProxyFactory : IProxyFactory
	{
#if !NETFX_CORE
		private static readonly ProxyGenerator generator = CreateProxyGenerator();
#endif

		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "By Design")]
		static CastleProxyFactory()
		{
#if !NETFX_CORE
#pragma warning disable 618
			AttributesToAvoidReplicating.Add<SecurityPermissionAttribute>();
#pragma warning restore 618
#endif

#if !SILVERLIGHT
			AttributesToAvoidReplicating.Add<ReflectionPermissionAttribute>();
			AttributesToAvoidReplicating.Add<PermissionSetAttribute>();
			AttributesToAvoidReplicating.Add<System.Runtime.InteropServices.MarshalAsAttribute>();
#if !NET3x
			AttributesToAvoidReplicating.Add<System.Runtime.InteropServices.TypeIdentifierAttribute>();
#endif
#endif
		}

		public T CreateProxy<T>(ICallInterceptor interceptor, Type[] interfaces, object[] arguments)
		{
			var mockType = typeof(T);

			if (mockType.IsInterface())
			{
#if !NETFX_CORE
				return (T)generator.CreateInterfaceProxyWithoutTarget(mockType, interfaces, new Interceptor(interceptor));
#else

                var realInterceptorType = MoqRTRuntime.Baker.ProxyGenerator.GetType().Assembly().
                    GetType("Castle.DynamicProxy.IInterceptor[]");

                // get the method...
                var method = MoqRTRuntime.Baker.ProxyGenerator.GetType().GetMethod("CreateInterfaceProxyWithoutTarget", 
                    new Type[] { typeof(Type), typeof(Type[]), realInterceptorType });
                return (T)method.Invoke(MoqRTRuntime.Baker.ProxyGenerator, new object[] { mockType, interfaces, 
                    new Interceptor(interceptor) });

                throw new NotImplementedException();

#endif
			}

			try
			{
#if !NETFX_CORE
				return (T)generator.CreateClassProxy(mockType, interfaces, new ProxyGenerationOptions(), arguments, 
                    new Interceptor(interceptor));
#else
                throw new NotImplementedException();
#endif
            }
			catch (TypeLoadException e)
			{
				throw new ArgumentException(Resources.InvalidMockClass, e);
			}
			catch (MissingMethodException e)
			{
				throw new ArgumentException(Resources.ConstructorNotFound, e);
			}
		}

#if !NETFX_CORE
		private static ProxyGenerator CreateProxyGenerator()
		{
			return new ProxyGenerator();
		}
#endif

        private class Interceptor : IInterceptor
		{
			private ICallInterceptor interceptor;

			internal Interceptor(ICallInterceptor interceptor)
			{
				this.interceptor = interceptor;
			}

			public void Intercept(IInvocation invocation)
			{
				this.interceptor.Intercept(new CallContext(invocation));
			}
		}

		private class CallContext : ICallContext
		{
			private IInvocation invocation;

			internal CallContext(IInvocation invocation)
			{
				this.invocation = invocation;
			}

			public object[] Arguments
			{
				get { return this.invocation.Arguments; }
			}

			public MethodInfo Method
			{
				get { return this.invocation.Method; }
			}

			public object ReturnValue
			{
				get { return this.invocation.ReturnValue; }
				set { this.invocation.ReturnValue = value; }
			}

			public void InvokeBase()
			{
				this.invocation.Proceed();
			}

			public void SetArgumentValue(int index, object value)
			{
				this.invocation.SetArgumentValue(index, value);
			}
		}
	}
}