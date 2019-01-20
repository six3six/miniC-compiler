//Louis DESPLANCHE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCompiler
{

    public class Instruction
    {
        public string Name { get; }
        public Type Type { get; }
        public Statut Statut { get; }
        public object Value { get; set; }

        /**
         * Constructor
         */
        public Instruction(string name, Type type, Statut statut)
        {
            Name = name;
            Type = type;
            Statut = statut;
            Value = new object();
        }

        /**
         * Constructor
         */
        public Instruction(string name, Type type, Statut statut, object value)
        {
            Name = name;
            Type = type;
            Statut = statut;
            Value = value;
        }
    }

    public class Variable : Instruction
    {
        public Variable(string name, Type type, Statut statut) : base(name, type, statut)
        {
        }

        public Variable(string name, Type type, Statut statut, object value) : base(name, type, statut, value)
        {
        }
    }

    public class Methode : Instruction
    {
        public Variable[] Parametres { get; }
        public int Pointer { get; }

        public Methode(string name, Type type, Variable[] parametres, int pointer) : base(name, type, Statut.constant)
        {
            Parametres = parametres;
            Pointer = pointer;
        }

    }

    public class MathOp : Methode
    {

        public MathOp(Type type, Variable[] parametres, int pointer) : base("MathOps", type, parametres, 0x00)
        {

        }
    }

    public enum Statut
    {
        free,
        constant
    }
}
