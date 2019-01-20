//Louis DESPLANCHE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCompiler
{
    class Program
    {
        static string codeExemple = @"
char letter;

int main(int argc)
{
    int age = 20;
  
    if (age >= 18)
    {
      printf (null);
    }
     
    return 0;
}";
        //Le programme commence ici
        static void Main(string[] args)
        {
            //Le programme tente de comprendre le programme
            new Parser(codeExemple);
        }
    }
}
