using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCompiler
{
    public class Type
    {
        public string Name
        {
            get; set;
        }

        public string LanguageName
        {
            get; set;
        }

        public Type(string name, string languageName)
        {
            Name = name;
            LanguageName = languageName;
        }

        public static Type cInt = new Type("integer", "int");
        public static Type cByte = new Type("byte", "byte");
        public static Type cChar = new Type("charactere", "char");
        public static Type cLong = new Type("long", "long");
        public static Type cFloat = new Type("float", "float");
        public static Type cDouble = new Type("doule", "double");
        public static Type cVoid = new Type("void", "void");
        public static Type cString = new Type("string", "string");

        public static readonly Type[] TypesList = {
            cInt, cByte, cChar, cLong, cFloat, cDouble, cVoid, cString
        };
    }
}
