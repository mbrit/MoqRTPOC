namespace System.Reflection.Emit
{
    
    
    // Dynamically created for MoqRT - 04-Jun-2012
    public class ConstructorBuilder : System.Reflection.ConstructorInfo
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

        internal object DefineParameter(int p1, ParameterAttributes parameterAttributes, string p2)
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
    }
}
