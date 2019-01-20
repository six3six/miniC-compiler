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

        static void Main(string[] args)
        {
            new Parser(codeExemple);
        }
    }
}
