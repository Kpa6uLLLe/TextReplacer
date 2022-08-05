using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextReplacer
{
    public class FileTextReplacer
    {
        private ReplaceProperties _properties;
        private string _log = "";
        private int _logIndex = 0;
        public FileTextReplacer(ReplaceProperties properties)
        {
            _properties = properties;
            if(!Directory.Exists(_properties.Path))
            {
                Console.WriteLine("Incorrect path");
                Environment.Exit(-1);
            }
            MassReplace();
        }
        public void MassReplace()
        {
            _log += $"{DateTime.Now} Started\nInitial folder: {_properties.Path}\n\n";
            MassReplace(_properties.Path, _properties.InitText, _properties.ReplacementText);
            Log();
        }
        private void Log()
        {
            string logsDirectoryPath = Path.Combine(Environment.CurrentDirectory, "Logs");
            if (!Directory.Exists(logsDirectoryPath))
            {
                Directory.CreateDirectory(logsDirectoryPath);
            }
            string logPath = Path.Combine(logsDirectoryPath, $"Log-{_logIndex}.txt");
            while (File.Exists(logPath))
            {
                _logIndex++;
                logPath = Path.Combine(Environment.CurrentDirectory, "Logs", $"Log-{_logIndex}.txt");
            }
            File.WriteAllText(logPath, _log);
            _log = "";
            Environment.Exit(0);
        }
        private void MassReplace(string? directoryPath, string initText, string newText)
        {
            if (!Directory.Exists(directoryPath))
                return;
            string[] filePaths = Directory.GetFiles(directoryPath);
            foreach(string filePath in filePaths)
            {
                ReplaceAllJoins(filePath, initText, newText);
            }
        }
        private void ReplaceAllJoins(string filePath, string initText, string newText)
        {
            string newFileText = "";
            if (!File.Exists(filePath))
                return;
            newFileText = File.ReadAllText(filePath);
            int replacementCounter = new Regex(initText).Matches(newFileText).Count;
            newFileText = newFileText.Replace(initText,newText);
            File.WriteAllText(filePath, newFileText);
            _log += 
                @$"Successful replacement on file {filePath} .
                  {replacementCounter} occurences of '{initText}' have been changed to '{newText}'
                  ";
            _log += "\n\n\n";
        }
    }
}
