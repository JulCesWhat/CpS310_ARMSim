using armsim.Prototype;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace armsim.Unit_Tests
{
    public class TestDecodeExecuteSimI
    {
        public static void runTests()
        {
            Console.WriteLine("Testing TestDecodeExecuteSimI class methods");
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("Testing startSimpleTest()");
            startSimpleTest();

            return;

        }

        private static void startSimpleTest()
        {
            Memory mem = new Memory(); // 32 KB
            Registers regs = new Registers();
            CPU cpu = new CPU(mem, regs);


            //************************************ testing MOV *********************************//

            // At memory slot 0
            // WriteWord(memAddress, Value)
            mem.WriteWord(0, 0xe3a02030); // e3a02030 = MOV R2, #48
            cpu.fetch();
            cpu.decode();
            cpu.execute(); //adds a instruction string to CPUInstructionRowList
            Debug.Assert(regs.getRegNValue(2) == 48);
            Debug.Assert(cpu.getTopInstAddrDisString() == "0x00000000: e3a02030: mov r2, #48"); // a whole instruction string gets saved in a one row
        }
    }
}
