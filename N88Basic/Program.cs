using System;
using System.IO;
using System.Linq;

namespace N88Basic
{
    class Program
    {
        public enum DumpArgs
        {
            action,
            tableFilePath,
            assembledFilePath,
            disassembledOutputFilePath,
        }

        public enum WriteArgs
        {
            action,
            tableFilePath,
            disassembledFilePath,
            assembledOutputFilePath,
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine($"Cannot have 0 arguments.");
                Environment.Exit(1);
                return;
            }

            var action = args[0];

            switch (action)
            {
                case "Dump":
                    {
                        Console.WriteLine($"Dumping");

                        var requiredLength = (int)Enum.GetValues(typeof(DumpArgs)).Cast<DumpArgs>().Max() + 1;
                        if (args.Length != requiredLength)
                        {
                            Console.WriteLine($"Required argument number: {requiredLength}. Received: {args.Length}");
                            Environment.Exit(1);
                            break;
                        }

                        var tableFilePath = args[(int)DumpArgs.tableFilePath];
                        var assembledFilePath = args[(int)DumpArgs.assembledFilePath];
                        var disassembledOutputFilePath = args[(int)DumpArgs.disassembledOutputFilePath];

                        var s = new N88Basic(tableFilePath).Disassembler(assembledFilePath);

                        File.WriteAllText(disassembledOutputFilePath, s);
                    }
                    break;
                case "Write":
                    {
                        Console.WriteLine($"Writing");

                        var requiredLength = (int)Enum.GetValues(typeof(WriteArgs)).Cast<WriteArgs>().Max() + 1;
                        if (args.Length != requiredLength)
                        {
                            Console.WriteLine($"Required argument number: {requiredLength}. Received: {args.Length}");
                            Environment.Exit(1);
                            break;
                        }

                        var tableFilePath = args[(int)WriteArgs.tableFilePath];
                        var disassembledFilePath = args[(int)WriteArgs.disassembledFilePath];
                        var assembledOutputFilePath = args[(int)WriteArgs.assembledOutputFilePath];

                        var b = new N88Basic(tableFilePath).Assembler(disassembledFilePath);

                        File.WriteAllBytes(assembledOutputFilePath, b);
                    }
                    break;
                default:
                    Console.WriteLine($"Invalid first parameter: {action}");
                    Environment.Exit(1);
                    break;
            }

            Console.WriteLine($"Finished successfully.");
        }
    }
}
