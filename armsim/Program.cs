using armsim.Extra_Classes;
using armsim.Prototype;
using armsim.Unit_Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace armsim
{
    class Program
    {
        static void Main(string[] args)
        {
            Options arguments = new Options(args);

            if (arguments.showTest)
            {
                Console.WriteLine("RUNNING UNIT TESTS ...");

                TestMemory.runTests();

                Console.WriteLine("All UNIT TESTS passed! Exiting ...");
                Environment.Exit(0);
            }

            Computer computer = new Computer(arguments);

            if (arguments.fileName != null)
            {
                int succesfullyOpenedFile = computer.loadSegmentsIntoRAM(arguments.fileName);
            }
            else
            {
                Console.WriteLine("File was not provided ...");
            }
        }
    }
}
