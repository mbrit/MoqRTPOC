using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SemReflector
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var path = @"C:\Code\MoqRTPoc\MoqRT\Sre";
                if(!(Directory.Exists(path)))
                    Directory.CreateDirectory(path);

                // ignore...
                var ignoreMethods = new List<string>() { "ToString", "Equals", "GetHashCode", "GetType", "GetObjectData", 
                    "DefineResource" };
                var ignoreTypes = new List<string>() { "FlowControl", "OpCode", "OpCodes", "OpCodeType", "OperandType", 
                    "PackingSize", "StackBehavior" };

                var skip = new Dictionary<string, List<string>>();
                skip["ModuleBuilder"] = new List<string>() { "GetPEKind" };
                skip["AssemblyBuilder"] = new List<string>() { "Policy", "PermissionSet", "RuleSet", "GetFile", "GetFiles", 
                    "Save", "Evidence", "SecurityRuleSet" };
                skip["ConstructorBuilder"] = new List<string>() { "AddDeclarativeSecurity" };
                skip["DynamicILInfo"] = new List<string>() { "SetCode", "SetExceptions", "SetLocalSignature" };
                skip["FieldBuilder"] = new List<string>() { "GetValueDirect", "SetValueDirect" };
                skip["ILGenerator"] = new List<string>() { "MarkSequencePoint" };
                skip["MethodBuilder"] = new List<string>() { "AddDeclarativeSecurity" };
                skip["ModuleBuilder"] = new List<string>() { "ModuleHandle", "GetSignerCertificate", "GetSymWriter", "DefineDocument",
                    "GetPEKind" };
                skip["TypeBuilder"] = new List<string>() { "AddDeclarativeSecurity" };

                // find all the types that are sre...
                var asm = Assembly.Load("mscorlib");
                var dt = DateTime.Now.ToString("dd-MMM-yyyy");
                foreach (var type in asm.GetTypes().Where(v => v.Namespace != null && v.Namespace.StartsWith("System.Reflection.Emit")
                    && v.IsPublic && !(v.IsNested) && !(ignoreTypes.Contains(v.Name))))
                {
                    var ns = new CodeNamespace(type.Namespace);
                    var t = new CodeTypeDeclaration(type.Name);
                    ns.Types.Add(t);
                    if(type.IsAbstract)
                        t.Attributes |= MemberAttributes.Abstract;
                    if(type.IsValueType)
                        t.IsStruct = true;

                    if (!(type.IsEnum))
                    {
                        var skips = new List<string>();
                        if (skip.ContainsKey(type.Name))
                            skips = skip[type.Name];

                        // base...
                        if (type.BaseType != null && type.BaseType != typeof(object) && type.BaseType != typeof(ValueType))
                            t.BaseTypes.Add(new CodeTypeReference(type.BaseType));
                        foreach (var iface in type.GetInterfaces().Where(v => v.DeclaringType == type))
                            t.BaseTypes.Add(new CodeTypeReference(iface));

                        // comments...
                        t.Comments.Add(new CodeCommentStatement("Dynamically created for MoqRT - " + dt));

                        //// properties...
                        //foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        //    .Where(v => !(skips.Contains(v.Name))))
                        //{
                        //    var p = new CodeMemberProperty();
                        //    p.Name = prop.Name;
                        //    p.Type = new CodeTypeReference(prop.PropertyType);
                        //    p.Attributes = MemberAttributes.Public | MemberAttributes.Final;

                        //    bool ok = true;
                        //    if (prop.GetGetMethod().IsVirtual)
                        //    {
                        //        // is it any higher ones...
                        //        var found = false;
                        //        var walk = type.BaseType;
                        //        while (walk != null)
                        //        {
                        //            var upper = walk.GetProperties().Where(v => v.Name == prop.Name)
                        //                .FirstOrDefault();
                        //            if (upper != null)
                        //            {
                        //                if (upper.GetGetMethod().IsAbstract)
                        //                    found = true;
                        //                else 
                        //                    ok = false;
                        //                break;
                        //            }

                        //            walk = walk.BaseType;
                        //        }

                        //        // remove...
                        //        p.Attributes ^= MemberAttributes.Final;
                        //        if (found)
                        //            p.Attributes |= MemberAttributes.Override;
                        //    }

                        //    // if...
                        //    if (ok)
                        //    {
                        //        t.Members.Add(p);

                        //        if (prop.CanRead)
                        //            p.GetStatements.Add(GetThrowNotImplemented());
                        //        if (prop.CanWrite)
                        //            p.GetStatements.Add(GetThrowNotImplemented());
                        //    }
                        //}

                        // constructros...
                        foreach (var constructor in type.GetConstructors(BindingFlags.Instance | BindingFlags.Public))
                        {
                            var c = new CodeConstructor();
                            foreach (var parameter in constructor.GetParameters())
                                c.Parameters.Add(GetParameterExpression(parameter));

                            // add...
                            t.Members.Add(c);

                        }

                        //// methods...
                        //foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                        //    .Where(v => !(v.Name.StartsWith("get_")) && !(v.Name.StartsWith("set_")) && !(v.IsAbstract) &&
                        //    !(ignoreMethods.Contains(v.Name)) && !(v.Name.StartsWith("add_")) && !(v.Name.StartsWith("remove_")) && 
                        //    !(skips.Contains(v.Name))))
                        //{
                        //    var m = new CodeMemberMethod();
                        //    m.Name = method.Name;
                        //    m.ReturnType = new CodeTypeReference(method.ReturnType);
                        //    m.Attributes = MemberAttributes.Public;

                        //    bool ok = true;
                        //    bool found = false;
                        //    if (method.IsVirtual)
                        //    {
                        //        // is it any higher ones...
                        //        var walk = type.BaseType;
                        //        while (walk != null)
                        //        {
                        //            var upper = walk.GetMethods().Where(v => v.Name == method.Name).FirstOrDefault();
                        //            if (upper != null)
                        //            {
                        //                if (upper.IsAbstract)
                        //                    found = true;
                        //                else
                        //                    ok = false;
                        //                break;
                        //            }

                        //            walk = walk.BaseType;
                        //        }

                        //        // remove...
                        //        m.Attributes ^= MemberAttributes.Final;
                        //        if (found)
                        //            m.Attributes |= MemberAttributes.Override;
                        //    }

                        //    // parms...
                        //    if (ok)
                        //    {
                        //        t.Members.Add(m);

                        //        foreach (var parameter in method.GetParameters())
                        //            m.Parameters.Add(GetParameterExpression(parameter));

                        //        // throw...
                        //        m.Statements.Add(GetThrowNotImplemented());
                        //    }
                        //}
                    }
                    else
                    {
                        t.IsEnum = true;

                        foreach (var field in type.GetFields().Where(v => v.Name != "value__"))
                        {
                            var f = new CodeMemberField();
                            f.Name = field.Name;

                            // add...
                            t.Members.Add(f);
                        }
                    }

                    // save...
                    var provider = CodeDomProvider.CreateProvider("CSharp");
                    var options = new CodeGeneratorOptions();
                    options.BracingStyle = "C";
                    using (var writer = new StreamWriter(Path.Combine(path, type.Name + ".cs")))
                        provider.GenerateCodeFromNamespace(ns, writer, options);

                    // ok...
                    Console.WriteLine(type);

                }
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static CodeParameterDeclarationExpression GetParameterExpression(ParameterInfo parameter)
        {
            var p = new CodeParameterDeclarationExpression();
            p.Name = parameter.Name;
            p.Type = new CodeTypeReference(parameter.ParameterType);

            return p;
        }

        private static CodeThrowExceptionStatement GetThrowNotImplemented()
        {
            return new CodeThrowExceptionStatement(new CodeObjectCreateExpression(typeof(NotImplementedException)));
        }
    }
}
