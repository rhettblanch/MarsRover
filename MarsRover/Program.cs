using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RunRover(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred:\n{0}", ex.Message);
            }

            Console.ReadKey();
        }

        static void RunRover(string[] args)
        {
            var options = new CommandLineOptions();
            var parser = new CommandLineParser();

            if (!parser.ParseArguments(args, options) || options.HasError)
            {
                Console.Error.Write(options.GetUsage());
                return;
            }

            Console.Out.Write(options.GetUsage());

            List<string> commandLines = new List<string>();

            if(!File.Exists(options.FileName)){
                throw new FileNotFoundException(string.Format("File {0} not found.",options.FileName));
            }

            using (StreamReader r = new StreamReader(options.FileName))
            {
                Console.WriteLine("Input:");
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    commandLines.Add(line);
                }
                Console.WriteLine("");
            }

            MarsRoverService marsRoverService = new MarsRoverService();
            
            List<string> output = marsRoverService.NavigateRovers(commandLines, options.DebugFlag);

            Console.WriteLine("Output:");
            foreach (string roverOutput in output)
            {
                Console.WriteLine(roverOutput);
            }
        }
    }
}
