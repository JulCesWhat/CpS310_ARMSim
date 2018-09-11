﻿using armsim.Extra_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace armsim.Prototype
{
    class Memory
    {
        uint RAMSize;
        private byte[] RAM;

        // default constructor
        public Memory()
        {
            uint RAMDefaultSize = 32768; // default 32KB or 32768 bytes
            RAM = new byte[RAMDefaultSize];
            RAMSize = RAMDefaultSize;
        }

        // Constructor: creates RAM with specific size
        public Memory(uint _RAMsize)
        {
            RAM = new byte[_RAMsize];
            RAMSize = _RAMsize;
        }

        // TODO: for now return int.MinValue
        // returns integer from RAM if <address> is divisible by 4
        // returns int.MaxValue if <address> is not divisible by 4
        // returns int.MaxValue if it was not able to read instruction
        public uint ReadWord(uint address)
        {
            if (!(address % 4 == 0)) // address not divisible by 4
                return uint.MaxValue;

            if (RAMSize <= address)  // address is greater than the RAM size
                return uint.MaxValue;

            byte[] bytes = { RAM[address], RAM[address + 1], RAM[address + 2], RAM[address + 3] }; // collect bytes from RAM into buffer

            uint retNum = BitConverter.ToUInt32(bytes, 0); // convert 4 bytes into uint
            return retNum;
        }

        // returns 0 if it was able to write <dataValue> into RAM
        // returns -1 if <address> is not divisible by 4
        // returns -1 if not able to write
        public int WriteWord(uint address, uint dataValue)
        {
            int intSizeInBytes = 4;

            if (!(address % 4 == 0)) // address not divisible by 4
                return -1;

            if ((RAM.Length - address) < intSizeInBytes) // RAM to small to fit the integer
                return -1;

            if (RAMSize < address)  // address is greater than the RAM size
                return -1;

            // Write integer to RAM
            byte[] bytes = BitConverter.GetBytes(dataValue); // retuns four bytes from int
            RAM[address + 0] = bytes[0];
            RAM[address + 1] = bytes[1];
            RAM[address + 2] = bytes[2];
            RAM[address + 3] = bytes[3];

            return 0;
        }

        // returns short from RAM if <address> is divisible by 2
        // returns short.MaxValue if <address> is not divisible by 2
        // returns short.MaxValue if it was not able to read half instruction.
        public ushort ReadHalfWord(uint address)
        {
            if (!(address % 2 == 0)) // address not divisible by 2
                return ushort.MaxValue;

            if (RAMSize <= address)  // address is greater or equal than the RAM size
                return ushort.MaxValue;

            byte[] bytes = { RAM[address], RAM[address + 1] }; // collect bytes from RAM into buffer

            ushort retNum = BitConverter.ToUInt16(bytes, 0); // convert 2 bytes into short
            return retNum;
        }

        // returns 0 if it was able to write <dataValue> into RAM
        // returns -1 if <address> is not divisible by 2
        // returns -1 if not able to write
        public int WriteHalfWord(uint address, ushort dataValue)
        {

            int shortSizeInBytes = 2; // HalfWord size

            if (!(address % 2 == 0)) // address not divisible by 2
                return -1;

            if ((RAM.Length - address) < shortSizeInBytes) // RAM to small to fit the short
                return -1;

            if (RAMSize < address)  // address is greater than the RAM size
                return -1;

            // Write short to RAM
            byte[] bytes = BitConverter.GetBytes(dataValue); // retuns 2 bytes from short
            RAM[address + 0] = bytes[0];
            RAM[address + 1] = bytes[1];

            return 0;
        }

        public byte ReadByte(uint address)
        {
            return RAM[address];
        }

        // returns 0 if it was able to write <dataValue> into RAM
        // returns -1 if not able to write
        public int WriteByte(uint address, byte dataValue)
        {
            if (RAMSize <= address)  // address is greater or equal to the RAM size
                return -1;

            RAM[address] = dataValue;
            return 0;
        }

        // returns the RAM's checksum
        // doing this way so that we can use it in the unit test aswell
        public uint ComputeRAMChecksum(byte[] ram)
        {
            var memory = (ram == null ? RAM : ram);
            uint cksum = 0;
            for (uint i = 0; i < memory.Length; i++)
            {
                cksum += memory[i] ^ i;
            }

            return cksum;
        }

        //TODO: I need to implement this part
        // use ReadWord() to read the instruction at location addr, and return true if
        // bit is 1 in the instruction, or false if 0 (bit should be in the range [0..31])
        public bool TestFlag(uint address, uint bit)
        {
            if (31 < bit) // bit not in the range [0..31])
            {
                Console.WriteLine("Cant testFlag: bit is out of range");
                return false;
            }

            //Need to do more work here
            return false;
        }


        // use ReadWord() to read the instruction at location addr, and set the 
        // bit whose position is specified by bit to 1 if flag is true, or 0 if flag is false.
        // places instruction back to address in RAM
        // (bit should be in the range [0..31])
        // returns 0 if able to set flag
        // returns -1 if not able to set flag
        public int SetFlag(uint address, uint bit, bool flag)
        {
            if ((0 > bit) | ((31 < bit))) // bit not in the range [0..31])
                return -1;

            uint value = ReadWord(address);
            if (value == uint.MaxValue)
                return -1;

            bool flagIsSet = TestFlag(address, bit);

            // Don't change flag if it doesn't need to
            if (flag == flagIsSet)
            {
                //Console.WriteLine(GetIntBinaryString(num));
                return -1;
            }

            // Change flag at <bit> number
            if (flagIsSet == true)
            {
                // shift a 1 to the bit counting from the right side, and XOR the number
                int IntBit = (int)bit;
                int IntValue = (int)value;
                int chFlagVal = IntValue ^ (1 << IntBit);

                uint numWFlagChanged = (uint)chFlagVal;
                WriteWord(address, numWFlagChanged);
                
                return 0;
            }
            else // (flagIsSet == false)
            {
                // not the numbers extracted from <address>, then the resul XOR it with 1 at <bit>, then not the result to change the flag
                uint nottedValue = ~value;
                int IntNottedValue = (int)nottedValue;

                int IntBit = (int)bit;
                int IntValue = (int)value;
                int chFlagVal = ~(IntNottedValue ^ (1 << IntBit));

                uint numWFlagChanged = (uint)chFlagVal;
                WriteWord(address, numWFlagChanged);
                
                return 0;
            }
        }

        // FUNCTION: Loads a segment to RAM
        // RETURNS: 0 if succesful
        //          -1 if not successful due to registers size
        public int loadRAMfromDataArrayBytes(ref byte[] data, ref PHE headerTable)
        {

            // if data can't fit in RAM, complain and exit program
            if (RAM.Length < headerTable.p_filesz)
            {
                Console.WriteLine("The RAM registers size is too small. Please re-run the program with a bigger RAM memmory size");
                return -1;
            }
            else if (RAM.Length < headerTable.p_vaddr)
            {
                Console.WriteLine("The start program offset is greater than the RAM size.The RAM registers size is too small. Please re-run the program with a bigger RAM memmory size.");
                return -1;
            }
            else if ((RAM.Length - headerTable.p_vaddr) < headerTable.p_filesz)
            {
                Console.WriteLine("The RAM registers size is too small. Please re-run the program with a bigger RAM memmory size");
                return -1;
            }
            else
            {
                uint i, n = 0;
                for (i = headerTable.p_vaddr; n < headerTable.p_filesz; i++, n++)
                {
                    RAM[i] = data[n];
                }

                return 0;
            }
        }

        public void clearMemory()
        {
            Array.Clear(RAM, 0, RAM.Length);
        }
    }
}
