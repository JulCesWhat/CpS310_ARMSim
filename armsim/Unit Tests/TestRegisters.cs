using armsim.Prototype;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace armsim.Unit_Tests
{
    class TestRegisters
    {
        public static void runTests()
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("Testing Registers class methods");
            Console.WriteLine("------------------------------");


            Registers testRegisters = new Registers();

            Console.WriteLine("Testing: updateProgramCounter(int programCounter)...");
            Debug.Assert(testRegisters.getProgramCounter() == 0);
            testRegisters.updateProgramCounter(3);
            Debug.Assert(testRegisters.getProgramCounter() == 3);

            Console.WriteLine("Testing: clearRegisters()...");
            testRegisters.clearRegisters();
            uint[] intArray = testRegisters.getRegistersIntArray();
            Debug.Assert(intArray[0] == 0);
            Debug.Assert(intArray[5] == 0);
            Debug.Assert(intArray[10] == 0);
            Debug.Assert(intArray[14] == 0);

            Console.WriteLine("Testing: getRegisterNames()..");
            string[] registerNames = testRegisters.getRegisterNames();
            Debug.Assert(registerNames[0] == "R0");
            Debug.Assert(registerNames[5] == "R5");
            Debug.Assert(registerNames[10] == "SL");
            Debug.Assert(registerNames[15] == "PC");


            Console.WriteLine("Testing: All register tests passed.\n");

        }
    }
}
