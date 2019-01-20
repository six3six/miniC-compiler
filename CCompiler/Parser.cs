using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCompiler
{
    class Parser
    {
        readonly string aCode;

        private List<Instruction> instructions = new List<Instruction>();
        private List<string> includes = new List<string>();
        
        /**
         * Constructeur naturel
         */
        public Parser(string code)
        {
            aCode = code;
            IdentifySignature();
        }

        /** 
         * Dresse une liste des variables globales et des fonctions ainsi que leurs type et leurs paramètres
         */
        void IdentifySignature()
        {
            //Pour la vérification du nombre de brackets
            int bracketCounter = 0;

            //Variable ou les charactères vont etre stocké avant d'être traité
            string strBuffer = "";

            //Transforme la chaine en tableau pour être traité par la boucle
            char[] codeArray = aCode.ToArray();

            //
            for(int i = 0; i< codeArray.Length; i++)
            {
                char c = codeArray[i];

                if (c == '{') bracketCounter++;
                else if (c == '}') bracketCounter--;

                

                else if(bracketCounter == 0)
                {
                    if (c == '#')
                    {
                        strBuffer = "";

                        i += 8;
                        for (int y=0; i < codeArray.Length; i++)
                        {
                            c = codeArray[i];
                            if (c == '\n') break;
                            strBuffer += codeArray[i];

                        }

                        includes.Add(strBuffer);
                        strBuffer = "";
                        continue;
                    }
                    if (char.IsLetter(c)) strBuffer += c;
                    if (c == '\n') strBuffer = "";


                    foreach(Type type in Type.TypesList)
                    {
                        if(strBuffer.StartsWith(type.LanguageName))
                        {
                            strBuffer = "";
                            string varName = "";
                            i += 2;
                            for (int y = 0; i < codeArray.Length; i++)
                            {
                                c = codeArray[i];
                                if (c == ';' || c == ')') break;
                                strBuffer += codeArray[i];
                            }

                            string[] signature = strBuffer.Split('(');

                            varName = signature[0];

                            if (signature.Length == 1) instructions.Add(new Variable(varName, type, Statut.free));
                            if (signature.Length > 1)
                            {
                                List<Variable> vParams = new List<Variable>();
                                foreach (string param in signature[1].Split(','))
                                {
                                    foreach (Type vType in Type.TypesList)
                                    {
                                        if(param.Replace(" ", "").StartsWith(vType.LanguageName))
                                        {
                                            vParams.Add(new Variable(param.Replace(vType.LanguageName, ""), vType, Statut.constant));
                                            break;
                                        }
                                    }

                                }
                                instructions.Add(new Methode(varName, type, vParams.ToArray(), 0));
                            }

                            break;
                        }
                    }
                }
            }

            if (bracketCounter > 0) throw new Exception("Il manque }");
            if (bracketCounter < 0) throw new Exception("Il manque {");
        }
    }
}
