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

        string disassembledCombinedString; // holds all the disassembled text
        string lastDisString; // holds last disassembled instruction 

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

            // initialize fields
            this.currentInstAddress = 0;
            this.currentInstruction = 0;
            this.currentInstructionType = 0;
            this.disassembledCombinedString = "";
            this.lastDisString = "";

            // initialize tracelog
            traceLog = new TraceLog();
        }

        // FUNCTION: Fetch the instruction and its address
        //           update fields <currentInstruction> and <currentInstAddress>
        // RETURNS:  an integer to be tested. i.e. return zero if we fetch a zero (this means that we are done        
        public uint fetch()
        {
            uint programCounter = registers.getProgramCounter();
            uint word = memory.ReadWord(programCounter);

            this.currentInstruction = word;
            this.currentInstAddress = programCounter;

            this.registers.incrementProgramCounterInAWordSize();
            return word;

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

            // add address, instruction, and disassembled instruction strings to each list
        //    string addr = "0x" + currentInstAddress.ToString("X").PadLeft(8, '0');
        //    string inst = currentInstruction.ToString("x").PadLeft(8, '0');


        //    disassembledCombinedString = disassembledCombinedString + "\r\n" + addr + "\t" + inst + "\t";


            return true;
        }

        /* Clear all three lists */
        internal void clearInstAddrDisLists()
        {
            instLst.Clear();
            instAddressLst.Clear();
            instDisLst.Clear();
        }

        #region Misellinious

        /* Trace Log Methods */
        internal bool isTraceLogEnabled() { return traceLog.isEnabled(); }
        internal void resetTraceCounterToOne() { traceLog.resetTraceCounterToOne(); }
        internal void turnOffTraceLog() { traceLog.turnOffTraceLog(); }
        internal void turnOnTraceLog() { traceLog.turnOnTraceLog(); }
        internal void traceLogFlush() { traceLog.flush(); }
        internal void traceLogClose() { traceLog.close(); }

        #endregion
    }
}
