namespace System.Reflection.Emit
{
    
    
    // Dynamically created for MoqRT - 04-Jun-2012
    public class MethodBuilder : System.Reflection.MethodInfo
    {
        public override MethodAttributes Attributes
        {
            get { throw new NotImplementedException(); }
        }

        public override MethodImplAttributes MethodImplementationFlags
        {
            get { throw new NotImplementedException(); }
        }

        public override ParameterInfo[] GetParameters()
        {
            throw new NotImplementedException();
        }

        public override Type DeclaringType
        {
            get { throw new NotImplementedException(); }
        }

        public override string Name
        {
            get { throw new NotImplementedException(); }
        }

        internal void SetImplementationFlags(int p)
        {
            throw new NotImplementedException();
        }

        internal ILGenerator GetILGenerator()
        {
            throw new NotImplementedException();
        }

        internal MethodImplAttributes GetMethodImplementationFlags()
        {
            throw new NotImplementedException();
        }

        internal void SetCustomAttribute(CustomAttributeBuilder attribute)
        {
            throw new NotImplementedException();
        }

        internal void SetParameters(Type[] paramTypes)
        {
            throw new NotImplementedException();
        }

        internal object DefineParameter(int p1, ParameterAttributes parameterAttributes, string p2)
        {
            throw new NotImplementedException();
        }

        internal void SetReturnType(Type returnType)
        {
            throw new NotImplementedException();
        }

        internal void SetSignature(Type returnType, object p1, object p2, Type[] parameters, object p3, object p4)
        {
            throw new NotImplementedException();
        }
    }
}
