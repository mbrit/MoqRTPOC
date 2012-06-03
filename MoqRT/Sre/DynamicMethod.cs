namespace System.Reflection.Emit
{
    
    
    // Dynamically created for MoqRT - 04-Jun-2012
    public class DynamicMethod : System.Reflection.MethodInfo
    {
        
        private DynamicMethod(string name, System.Type returnType, System.Type[] parameterTypes, bool restrictedSkipVisibility)
        {
        }
        
        private DynamicMethod(string name, System.Type returnType, System.Type[] parameterTypes, System.Reflection.Module m, bool skipVisibility)
        {
        }
        
        private DynamicMethod(string name, System.Type returnType, System.Type[] parameterTypes, System.Type owner, bool skipVisibility)
        {
        }
        
        private DynamicMethod(string name, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, System.Type returnType, System.Type[] parameterTypes, System.Type owner, bool skipVisibility)
        {
        }
        
        private DynamicMethod(string name, System.Type returnType, System.Type[] parameterTypes)
        {
        }
        
        private DynamicMethod(string name, System.Type returnType, System.Type[] parameterTypes, System.Reflection.Module m)
        {
        }
        
        private DynamicMethod(string name, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, System.Type returnType, System.Type[] parameterTypes, System.Reflection.Module m, bool skipVisibility)
        {
        }
        
        private DynamicMethod(string name, System.Type returnType, System.Type[] parameterTypes, System.Type owner)
        {
        }

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
    }
}
