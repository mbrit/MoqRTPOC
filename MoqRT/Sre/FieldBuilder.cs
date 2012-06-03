namespace System.Reflection.Emit
{
    
    
    // Dynamically created for MoqRT - 04-Jun-2012
    public class FieldBuilder : System.Reflection.FieldInfo
    {

        public override Type DeclaringType
        {
            get { throw new NotImplementedException(); }
        }

        public override string Name
        {
            get { throw new NotImplementedException(); }
        }

        public override FieldAttributes Attributes
        {
            get { throw new NotImplementedException(); }
        }

        public override Type FieldType
        {
            get { throw new NotImplementedException(); }
        }

        public override object GetValue(object obj)
        {
            throw new NotImplementedException();
        }

        internal void SetCustomAttribute(CustomAttributeBuilder customAttributeBuilder)
        {
            throw new NotImplementedException();
        }
    }
}
