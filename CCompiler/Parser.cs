//Louis DESPLANCHE

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

            
            for(int i = 0; i< codeArray.Length; i++)
            {
                char c = codeArray[i];
                
                
                if (c == '{') bracketCounter++;
                else if (c == '}') bracketCounter--;

                //On verifie que l'on ne se trouve pas dans une fonction
                else if (bracketCounter == 0)
                {
                    //On regarde s'il s'agit d'une instruction d'inclusion
                    if (c == '#')
                    {
                        //On réinitialise le buffer
                        strBuffer = "";

                        //On ajoute 8 au pointer pour passer le #include
                        i += 8;

                        //On check le contenu de l'instruction
                        for (int y=0; i < codeArray.Length; i++)
                        {
                            c = codeArray[i];
                            if (c == '\n') break;
                            strBuffer += codeArray[i];

                        }

                        //On ajoute l'entrée à la liste des inclusions
                        includes.Add(strBuffer);

                        //On reinitialise le buffer
                        strBuffer = "";
                        //On arrete la boucle (par soucis d'optimisation)
                        continue;
                    }

                    //On ajoute au buffer uniquement les lettres
                    if (char.IsLetter(c)) strBuffer += c;
                    //Si on revient à la ligne le buffer se reinitialise
                    if (c == '\n') strBuffer = "";

                    //On va regarder dans le programme s'il n'y a pas un début de methode/variable
                    //Pour cela on repere les types
                    foreach(Type type in Type.TypesList)
                    {

                        if(strBuffer.StartsWith(type.LanguageName))
                        {
                            //Une fois qu'on a identifié le type on reinitialise le buffer
                            strBuffer = "";

                            //On ajoute 2 au pointeur puis on récupere le reste de la signature
                            i += 2;
                            for (int y = 0; i < codeArray.Length; i++)
                            {
                                c = codeArray[i];
                                if (c == ';' || c == ')') break;
                                strBuffer += codeArray[i];
                            }

                            //On sépare la signature des paramètres
                            string[] signature = strBuffer.Split('(');
                            //On créé une variable afin d'y entré le nom de la methode/variable globale
                            string varName = signature[0];

                            //Si la signature n'a qu'une partie alors il s'agit d'une variable dans la cas contraire c'est une methode
                            if (signature.Length == 1) instructions.Add(new Variable(varName, type, Statut.free));
                            if (signature.Length > 1)
                            {
                                //On cherche la liste de paramètres de la fontion
                                List<Variable> vParams = new List<Variable>();
                                foreach (string param in signature[1].Split(','))
                                {
                                    foreach (Type vType in Type.TypesList)
                                    {
                                        if(param.Replace(" ", "").StartsWith(vType.LanguageName))
                                        {
                                            //On ajoute a la liste des paramètres
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
            //Verfie si les fonctions sont bien fermé
            if (bracketCounter > 0) throw new Exception("Il manque }");
            if (bracketCounter < 0) throw new Exception("Il manque {");
        }
    }
}
