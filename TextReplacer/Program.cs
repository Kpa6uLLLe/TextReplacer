namespace TextReplacer
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("[-help]\n[-path <text>] #Path to the directory\n[-itext <text>] #Text that should be replaced\n[-ntext <text>] #Replacement text");  
            ConsoleARGSSupport support = new ConsoleARGSSupport(args);
            var properties = support.GetArgsValues();
            FileTextReplacer replacer = new FileTextReplacer(properties);
            replacer.MassReplace();
        }
    }
}