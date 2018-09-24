using armsim.Extra_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace armsim.Prototype
{
    class CPU
    {
        // these list keep track of all the instructions that get executed
        List<string> instLst = new List<string>(); // instruction list
        List<string> instAddressLst = new List<string>(); // instruction address list
        List<string> instDisLst = new List<string>(); // instruction disassembled list

        Memory memory;
        Registers registers;
        TraceLog traceLog;
        Subject subject;

        uint currentInstructionType;
        uint currentInstAddress;
        uint currentInstruction;

        public CPU(Memory mem, Registers reg)
        {
            // hook up references between CPU and RAM/Registers
            this.memory = mem;
            this.registers = reg;

            // initialize fields
            this.currentInstAddress = 0;
            this.currentInstruction = 0;
            this.currentInstructionType = 0;
        }

        public uint fetch()
        {
            //Get program counter

            //Read word

            //Set current instruction and address

            //Increase program counter

            //Return word
            return 0;
        }

        public void decode()
        {
            //Use the current instruction information
            //to decode it the right way

            //Decode special cases of code

            //Decode other normal cases of code
        }

        public bool execute()
        {
            //Execute the different cases of operations

            //Mul

            //Add

            //And other spcial Cases???
            return true;
        }

        /* Clear all three lists */
        internal void clearInstAddrDisLists()
        {
            instLst.Clear();
            instAddressLst.Clear();
            instDisLst.Clear();
        }
    }
}
