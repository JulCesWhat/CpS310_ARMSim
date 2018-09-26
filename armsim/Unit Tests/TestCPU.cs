using armsim.Prototype;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace armsim.Unit_Tests
{
    class TestCPU
    {
        public static void runtTests()
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("Testing CPU class methods");
            Console.WriteLine("------------------------------");

            Memory mem = new Memory();
            Registers reg = new Registers();

            CPU testCPU = new CPU(mem, reg);


            Console.WriteLine("Testing: fetch()...");
            Debug.Assert(testCPU.fetch() == 0);
            reg.updateProgramCounter(4);
            Debug.Assert(testCPU.fetch() == 0);

            Console.WriteLine("Testing: All cpu tests passed.\n");
        }
    }
}
