using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextReplacer
{
    public class ConsoleARGSSupport
    {
        public readonly string[] possibleArgs = 
        {"-help",
        "-path",
        "-itext",
        "-ntext"};
        private string[] argsValues;
        public ConsoleARGSSupport(string[] args)
        {
            if (args == null || args.Length == 0)
                Environment.Exit(-1);
            argsValues = GetFilteredArgs(args);
            if(argsValues.Contains("-help"))
            {
                Environment.Exit(0);
            }
        }
        public ReplaceProperties GetArgsValues()
        {
            ReplaceProperties properties = new ReplaceProperties();
            properties.Path = argsValues[IndexOf(possibleArgs, "-path")];
            properties.InitText = argsValues[IndexOf(possibleArgs, "-itext")];
            properties.ReplacementText = argsValues[IndexOf(possibleArgs, "-ntext")];
            return properties;
        }
        private string[] GetFilteredArgs(string[] args)
        {
            string[] filteredArgs = new string[possibleArgs.Length];
            int count = 0;
            foreach(string arg in possibleArgs)
            {
                if(args.Contains(arg) && args.Length>(IndexOf(args, arg)+1))
                {
                    filteredArgs[count] = args[IndexOf(args, arg) + 1];

                    if (arg == "-help")
                    {
                        Console.WriteLine("[-help]\n[-path <text>] #Path to the directory\n[-itext <text>] #Text that should be replaced\n[-ntext <text>] #Replacement text");
                        Environment.Exit(0);
                    }
                }
                count++;
            }
            return filteredArgs;
        }
        private static int IndexOf(string[] array, string value)
        {
            for(int i=0;i<array.Length;i++)
            {
                if (array[i] == value)
                    return i;
            }
            return -1;
        }
    }
}
