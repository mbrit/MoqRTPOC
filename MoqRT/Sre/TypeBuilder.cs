namespace System.Reflection.Emit
{
    
    
    // Dynamically created for MoqRT - 04-Jun-2012
    public class TypeBuilder : System.Reflection.TypeInfo
    {
        public override TypeAttributes Attributes
        {
            get { throw new NotImplementedException(); }
        }

        public override Type GetGenericTypeDefinition()
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

        public override int GetArrayRank()
        {
            throw new NotImplementedException();
        }

        public override Type GetElementType()
        {
            throw new NotImplementedException();
        }

        public override Type[] GetGenericParameterConstraints()
        {
            throw new NotImplementedException();
        }

        public override Type[] GenericTypeArguments
        {
            get { throw new NotImplementedException(); }
        }

        public override Assembly Assembly
        {
            get { throw new NotImplementedException(); }
        }

        public override Type BaseType
        {
            get { throw new NotImplementedException(); }
        }

        public override bool ContainsGenericParameters
        {
            get { throw new NotImplementedException(); }
        }

        public override MethodBase DeclaringMethod
        {
            get { throw new NotImplementedException(); }
        }

        public override string FullName
        {
            get { throw new NotImplementedException(); }
        }

        public override GenericParameterAttributes GenericParameterAttributes
        {
            get { throw new NotImplementedException(); }
        }

        public override int GenericParameterPosition
        {
            get { throw new NotImplementedException(); }
        }

        public override Guid GUID
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsEnum
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsGenericParameter
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsGenericType
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsGenericTypeDefinition
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsSerializable
        {
            get { throw new NotImplementedException(); }
        }

        public override string Namespace
        {
            get { throw new NotImplementedException(); }
        }

        public override string AssemblyQualifiedName
        {
            get { throw new NotImplementedException(); }
        }

        public override Type MakeArrayType()
        {
            throw new NotImplementedException();
        }

        public override Type MakeArrayType(int rank)
        {
            throw new NotImplementedException();
        }

        public override Type MakeByRefType()
        {
            throw new NotImplementedException();
        }

        public override Type MakeGenericType(params Type[] typeArguments)
        {
            throw new NotImplementedException();
        }

        public override Type MakePointerType()
        {
            throw new NotImplementedException();
        }

        internal void DefineMethodOverride(MethodBuilder methodBuilder, MethodInfo MethodToOverride)
        {
            throw new NotImplementedException();
        }

        internal void SetCustomAttribute(CustomAttributeBuilder customAttributeBuilder)
        {
            throw new NotImplementedException();
        }

        internal FieldInfo DefineField(string name, Type fieldType, FieldAttributes atts)
        {
            throw new NotImplementedException();
        }

        internal static void AddInterfaceImplementation(Type inter)
        {
            throw new NotImplementedException();
        }

        internal static void SetParent(Type baseType)
        {
            throw new NotImplementedException();
        }

        internal ConstructorBuilder DefineConstructor(MethodAttributes methodAttributes, CallingConventions callingConventions, Type[] args)
        {
            throw new NotImplementedException();
        }

        internal EventBuilder DefineEvent(string name, EventAttributes attributes, Type type)
        {
            throw new NotImplementedException();
        }

        internal MethodBuilder DefineMethod(string name, MethodAttributes attributes)
        {
            throw new NotImplementedException();
        }

        internal TypeBuilder DefineNestedType(string name, TypeAttributes attributes, Type baseType, Type[] interfaces)
        {
            throw new NotImplementedException();
        }
    }
}
