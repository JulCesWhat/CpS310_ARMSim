using armsim.Extra_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

            //Dumi data
            this.disassembledCombinedString = @"
0xf0000000:    0xe3a08000 .... 	:mov      r8,#0
0xf0000004:    0xe28f900c .... 	:add      r9,pc,#0xc
0xf0000008:    0xe8b900ff .... 	:ldmia    r9!,{r0-r7}
0xf000000c:    0xe8a800ff .... 	:stmia    r8!,{r0-r7}
0xf0000010:    0xe8b900ff .... 	:ldmia    r9!,{r0-r7}
0xf0000014:    0xe8a800ff .... 	:stmia    r8!,{r0-r7}
0xf0000018:    0xe59ff018 .... 	:ldr      pc,0xf0000038         ; = #0xf0000070
0xf000001c:    0xe59ff018 .... 	:ldr      pc,0xf000003c         ; = #0xf0000058
0xf0000020:    0xe59ff018 .... 	:ldr      pc,0xf0000040         ; = #0xf000005c
0xf0000024:    0xe59ff018 ....	:ldr      pc,0xf0000044         ; = #0xf0000060
0xf0000028:    0xe59ff018 .... 	:ldr      pc,0xf0000048         ; = #0xf0000064
0xf000002c:    0xe1a00000 .... 	:nop 	
0xf0000030:    0xe59ff018 .... 	:ldr      pc,0xf0000050         ; = #0xf0000068
0xf0000034:    0xe59ff018 .... 	:ldr      pc,0xf0000054         ; = #0xf000006c
0xf0000038:    0xf0000070 ...p 	:andnv    r0,r0,r0,ror r0
0xf000003c:    0xf0000058 ...X 	:andnv	  r0,r0,r8,asr r0";
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

            writeInfoToTraceLog();

            //Thread.Sleep(5000);

            //For right now it never ends
            return false;
        }

        /// Print a 3 line info to trace log file in this format:
        /// 
        /// step_number program_counter checksum nzcf r0 r1 r2 r3
        /// r4 r5 r6 r7 r8 r9
        /// r10sl r11fp r12 r13 r14
        /// 
        /// Example:
        /// 
        /// 000001 41414141 d41d8cd98f00b204e9800998ecf8427e 0101 0=01234567 1=01234567 2=01234567 3=01234567
        /// 4=01234567  5=01234567  6=01234567  7=01234567  8=01234567 9=01234567
        /// 10=01234567 11=01234567 12=01234567 13=01234567 14=01234567
        /// Tracelog for SIM II
        private void writeInfoToTraceLog()
        {
            if (traceLog.isEnabled() == false)
                return;

            // getting the needed values
            int traceCounter = traceLog.getTraceCounter();
            uint programCounter = currentInstAddress; // program_counter is the value of the program counter at the time the fetch began

            string machine_state = "[sys]";

            uint[] controlFlagsIntArray = registers.getControlFlagsIntArray();
            uint[] registersIntArray = registers.getRegistersIntArray();

            // converting values to string formatted to print them later
            string strTC = traceCounter.ToString().PadLeft(6, '0');
            string strPC = programCounter.ToString("X").PadLeft(8, '0');  // gives you hex

            // printing values to trace log by lines
            traceLog.WriteLineToLog(strTC + " " + strPC + " " + machine_state + " " +
                                    controlFlagsIntArray[0] + controlFlagsIntArray[1] + controlFlagsIntArray[2] + controlFlagsIntArray[3] +
                                    " " + memory.ComputeRAMChecksum(null) +
                                     " 0=" + registersIntArray[0].ToString("X").PadLeft(8, '0') +
                                     " 1=" + registersIntArray[1].ToString("X").PadLeft(8, '0') +
                                     " 2=" + registersIntArray[2].ToString("X").PadLeft(8, '0') +
                                     " 3=" + registersIntArray[3].ToString("X").PadLeft(8, '0'));

            traceLog.WriteLineToLog(
                                    "\t4=" + registersIntArray[4].ToString("X").PadLeft(8, '0') +
                                    "  " + "5=" + registersIntArray[5].ToString("X").PadLeft(8, '0') +
                                    "  " + "6=" + registersIntArray[6].ToString("X").PadLeft(8, '0') +
                                    "  " + "7=" + registersIntArray[7].ToString("X").PadLeft(8, '0') +
                                    "  " + "8=" + registersIntArray[8].ToString("X").PadLeft(8, '0') +
                                    " " + "9=" + registersIntArray[9].ToString("X").PadLeft(8, '0'));

            traceLog.WriteLineToLog(
                                     "\t10=" + registersIntArray[10].ToString("X").PadLeft(8, '0') +
                                     " 11=" + registersIntArray[11].ToString("X").PadLeft(8, '0') +
                                     " 12=" + registersIntArray[12].ToString("X").PadLeft(8, '0') +
                                     " 13=" + registersIntArray[13].ToString("X").PadLeft(8, '0') +
                                     " 14=" + registersIntArray[14].ToString("X").PadLeft(8, '0'));

            traceLog.updateStepCounterByOne();
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


        internal string getCPUDisassembledCombinedString()
        {
            return disassembledCombinedString;
        }

        internal string getDisassembledLastInstructionExecuted()
        {
            return lastDisString;
        }
        #endregion
    }
}
