using armsim.Prototype;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace armsim.Unit_Tests
{
    class TestMemory
    {
        public static void runTests()
        {
            Memory sim = new Memory(12);
            byte[] data = new byte[] { 14, 1, 2, 255 };

            // load data to RAM
            for (uint i = 0; i < data.Length; i++)
                sim.WriteByte(i, data[i]);

            Console.WriteLine("Testing Memory class methods");
            Console.WriteLine("------------------------------");

            // test checksum

            uint checksum = sim.ComputeRAMChecksum(new byte[] { 0x01, 0x82, 0x03, 0x84 });
            Console.WriteLine("Testing: ComputeRAMChecksum(byte[] memory)...");
            Debug.Assert(checksum == 268);
            Console.WriteLine("Testing: test passed.");


            // test ints
            uint wordNum1 = uint.MinValue;
            uint wordNum2 = 0xFFFFFC19; //- 999
            uint wordNum3 = 0;
            uint wordNum4 = 999;
            uint wordNum5 = uint.MaxValue;
            Console.WriteLine("Testing: WriteWord(uint address, uint dataValue)...");
            Debug.Assert(sim.WriteWord(0, wordNum1) == 0);
            Debug.Assert(sim.WriteWord(1, wordNum2) == -1);
            Debug.Assert(sim.WriteWord(2, wordNum3) == -1);
            Debug.Assert(sim.WriteWord(3, wordNum4) == -1);
            Debug.Assert(sim.WriteWord(4, wordNum5) == 0);
            Debug.Assert(sim.WriteWord(5, wordNum1) == -1);
            Debug.Assert(sim.WriteWord(6, wordNum2) == -1);
            Debug.Assert(sim.WriteWord(7, wordNum3) == -1);
            Debug.Assert(sim.WriteWord(8, wordNum4) == 0);
            Debug.Assert(sim.WriteWord(9, wordNum5) == -1);
            Debug.Assert(sim.WriteWord(10, wordNum1) == -1);
            Debug.Assert(sim.WriteWord(11, wordNum2) == -1);
            Debug.Assert(sim.WriteWord(12, wordNum3) == -1);
            Debug.Assert(sim.WriteWord(13, wordNum4) == -1);
            Debug.Assert(sim.WriteWord(14, wordNum5) == -1);
            Debug.Assert(sim.WriteWord(15, wordNum1) == -1);
            Debug.Assert(sim.WriteWord(16, wordNum2) == -1);
            Console.WriteLine("Testing: test passed.");

            Console.WriteLine("Testing: ReadWord(uint address)...");
            Debug.Assert(sim.ReadWord(0) == wordNum1);
            Debug.Assert(sim.ReadWord(1) == uint.MaxValue);
            Debug.Assert(sim.ReadWord(2) == uint.MaxValue);
            Debug.Assert(sim.ReadWord(3) == uint.MaxValue);
            Debug.Assert(sim.ReadWord(4) == wordNum5);
            Debug.Assert(sim.ReadWord(5) == uint.MaxValue);
            Debug.Assert(sim.ReadWord(6) == uint.MaxValue);
            Debug.Assert(sim.ReadWord(7) == uint.MaxValue);
            Debug.Assert(sim.ReadWord(8) == wordNum4);
            Debug.Assert(sim.ReadWord(9) == uint.MaxValue);
            Debug.Assert(sim.ReadWord(10) == uint.MaxValue);
            Debug.Assert(sim.ReadWord(11) == uint.MaxValue);
            Debug.Assert(sim.ReadWord(12) == uint.MaxValue);
            Debug.Assert(sim.ReadWord(13) == uint.MaxValue);
            Debug.Assert(sim.ReadWord(14) == uint.MaxValue);
            Debug.Assert(sim.ReadWord(15) == uint.MaxValue);
            Debug.Assert(sim.ReadWord(16) == uint.MaxValue);
            Console.WriteLine("Testing: test passed.");


            // test shorts
            ushort shortNum1 = ushort.MinValue;
            ushort shortNum2 = 999;
            ushort shortNum3 = 0;
            ushort shortNum4 = 0xFC19; //- 999
            ushort shortNum5 = ushort.MaxValue;
            Console.WriteLine("Testing: WriteHalfWord(uint address, short dataValue)...");
            Debug.Assert(sim.WriteHalfWord(0, shortNum1) == 0);
            Debug.Assert(sim.WriteHalfWord(1, shortNum2) == -1);
            Debug.Assert(sim.WriteHalfWord(2, shortNum3) == 0);
            Debug.Assert(sim.WriteHalfWord(3, shortNum4) == -1);
            Debug.Assert(sim.WriteHalfWord(4, shortNum5) == 0);
            Debug.Assert(sim.WriteHalfWord(5, shortNum1) == -1);
            Debug.Assert(sim.WriteHalfWord(6, shortNum2) == 0);
            Debug.Assert(sim.WriteHalfWord(7, shortNum3) == -1);
            Debug.Assert(sim.WriteHalfWord(8, shortNum4) == 0);
            Debug.Assert(sim.WriteHalfWord(9, shortNum1) == -1);
            Debug.Assert(sim.WriteHalfWord(10, shortNum2) == 0);
            Debug.Assert(sim.WriteHalfWord(11, shortNum3) == -1);
            Debug.Assert(sim.WriteHalfWord(12, shortNum4) == -1);
            Debug.Assert(sim.WriteHalfWord(13, shortNum5) == -1);
            Debug.Assert(sim.WriteHalfWord(14, shortNum1) == -1);
            Debug.Assert(sim.WriteHalfWord(15, shortNum2) == -1);
            Debug.Assert(sim.WriteHalfWord(16, shortNum3) == -1);
            Console.WriteLine("Testing: test passed.");

            Console.WriteLine("Testing: ReadHalfWord(uint address)...");
            Debug.Assert(sim.ReadHalfWord(0) == shortNum1);
            Debug.Assert(sim.ReadHalfWord(1) == ushort.MaxValue);
            Debug.Assert(sim.ReadHalfWord(2) == shortNum3);
            Debug.Assert(sim.ReadHalfWord(3) == ushort.MaxValue);
            Debug.Assert(sim.ReadHalfWord(4) == shortNum5);
            Debug.Assert(sim.ReadHalfWord(5) == ushort.MaxValue);
            Debug.Assert(sim.ReadHalfWord(6) == shortNum2);
            Debug.Assert(sim.ReadHalfWord(7) == ushort.MaxValue);
            Debug.Assert(sim.ReadHalfWord(8) == shortNum4);
            Debug.Assert(sim.ReadHalfWord(9) == ushort.MaxValue);
            Debug.Assert(sim.ReadHalfWord(10) == shortNum2);
            Debug.Assert(sim.ReadHalfWord(11) == ushort.MaxValue);
            Debug.Assert(sim.ReadHalfWord(12) == ushort.MaxValue);
            Debug.Assert(sim.ReadHalfWord(13) == ushort.MaxValue);
            Debug.Assert(sim.ReadHalfWord(14) == ushort.MaxValue);
            Debug.Assert(sim.ReadHalfWord(15) == ushort.MaxValue);
            Debug.Assert(sim.ReadHalfWord(16) == ushort.MaxValue);
            Console.WriteLine("Testing: test passed.");

            // test bytes
            byte byteNum1 = byte.MinValue;
            byte byteNum2 = 1;
            byte byteNum3 = 10;
            byte byteNum4 = 200;
            byte byteNum5 = byte.MaxValue;
            Console.WriteLine("Testing: WriteByte(uint address, byte dataValue)...");
            Debug.Assert(sim.WriteByte(0, byteNum1) == 0);
            Debug.Assert(sim.WriteByte(1, byteNum2) == 0);
            Debug.Assert(sim.WriteByte(2, byteNum3) == 0);
            Debug.Assert(sim.WriteByte(3, byteNum4) == 0);
            Debug.Assert(sim.WriteByte(4, byteNum5) == 0);
            Debug.Assert(sim.WriteByte(5, byteNum1) == 0);
            Debug.Assert(sim.WriteByte(6, byteNum2) == 0);
            Debug.Assert(sim.WriteByte(7, byteNum3) == 0);
            Debug.Assert(sim.WriteByte(8, byteNum4) == 0);
            Debug.Assert(sim.WriteByte(9, byteNum5) == 0);
            Debug.Assert(sim.WriteByte(10, byteNum1) == 0);
            Debug.Assert(sim.WriteByte(11, byteNum2) == 0);
            Debug.Assert(sim.WriteByte(12, byteNum3) == -1);
            Debug.Assert(sim.WriteByte(13, byteNum4) == -1);
            Debug.Assert(sim.WriteByte(14, byteNum5) == -1);
            Debug.Assert(sim.WriteByte(15, byteNum1) == -1);
            Debug.Assert(sim.WriteByte(16, byteNum2) == -1);
            Console.WriteLine("Testing: test passed.");

            Console.WriteLine("Testing: ReadByte(uint address)...");
            Debug.Assert(sim.ReadByte(0) == byteNum1);
            Debug.Assert(sim.ReadByte(1) == byteNum2);
            Debug.Assert(sim.ReadByte(2) == byteNum3);
            Debug.Assert(sim.ReadByte(3) == byteNum4);
            Debug.Assert(sim.ReadByte(4) == byteNum5);
            Debug.Assert(sim.ReadByte(5) == byteNum1);
            Debug.Assert(sim.ReadByte(6) == byteNum2);
            Debug.Assert(sim.ReadByte(7) == byteNum3);
            Debug.Assert(sim.ReadByte(8) == byteNum4);
            Debug.Assert(sim.ReadByte(9) == byteNum5);
            Debug.Assert(sim.ReadByte(10) == byteNum1);
            Debug.Assert(sim.ReadByte(11) == byteNum2);
            Console.WriteLine("Testing: test passed.");

            Console.WriteLine("Testing: All tests passed.");
        }
    }
}
