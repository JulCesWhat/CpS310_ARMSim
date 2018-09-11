using armsim.Extra_Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace armsim.Prototype
{
    class Computer
    {
        Memory memory;
        DebugLog debugLog;

        public Computer(Options _arguments)
        {
            // initialize debug streams
            debugLog = new DebugLog();

            // initialize memory
            memory = new Memory(_arguments.memSize);
        }


        #region Helper methods

        ///  HELPER FUCNTION: to loadSegmentsIntoRAM(). Converts a byte array to a struct
        ///  RECIEVES: a byte array
        ///  RETURNS: a struct
        public T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T stuff = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(),
                typeof(T));
            handle.Free();
            return stuff;
        }

        /// FUNCTION: Open ELF file, read elf header, read each program header entr (PHE) and store segment(s) to RAM
        ///           if succesful elf header read, then
        ///             - clear memory and registers
        ///             - set programCounter with elf header entry point
        ///             - load PHEs to RAM
        /// RETURNS:
        ///           returns -1 if failed to load segments into ram
        ///           returns 0 if success
        public int loadSegmentsIntoRAM(string fileName)
        {
            try
            {
                using (FileStream strm = new FileStream(fileName, FileMode.Open))
                {

                    ELF elfHeader = new ELF();
                    byte[] data = new byte[Marshal.SizeOf(elfHeader)];

                    strm.Read(data, 0, data.Length); // Read ELF header data
                    elfHeader = ByteArrayToStructure<ELF>(data); // Convert to struct

                    debugLog.WriteLineToLog("IMPORTANT: Debug logging information for developers:                         " + DateTime.Now);
                    debugLog.WriteLineToLog("Loader: in loadSegmentsIntoRAM (): Opening " + fileName + "…");
                    debugLog.WriteLineToLog("Loader: in loadSegmentsIntoRAM (): Program entry address: " + elfHeader.e_entry);
                    debugLog.WriteLineToLog("Loader: in loadSegmentsIntoRAM (): Program header offset (location in file): " + elfHeader.e_phoff);
                    debugLog.WriteLineToLog("Loader: in loadSegmentsIntoRAM (): Size of Program header entry: " + elfHeader.e_phentsize);
                    debugLog.WriteLineToLog("Loader: in loadSegmentsIntoRAM (): Number of segments: " + elfHeader.e_phnum); //NumberelfHeader.e_entry.ToString("X4"));

                    // clear memory
                    memory.clearMemory();

                    // Read all program header entries and load segments to RAM
                    uint nextElfHeaderOffset = elfHeader.e_phoff;
                    int segmentNum = 0;
                    for (int i = 0; i < elfHeader.e_phnum; i++, segmentNum++) //  Read all program headers
                    {
                        // Read a program header
                        PHE headerTable = new PHE();
                        data = new byte[elfHeader.e_phentsize];
                        strm.Seek(nextElfHeaderOffset, SeekOrigin.Begin); // move position to start of next program header entry

                        strm.Read(data, 0, (int)elfHeader.e_phentsize); // Read ELF header data
                        headerTable = ByteArrayToStructure<PHE>(data);  // Convert to struct

                        // Load PHE to RAM
                        strm.Seek(headerTable.p_offset, SeekOrigin.Begin); // move position to start of segment
                        data = new byte[headerTable.p_filesz]; // resize data
                        strm.Read(data, 0, (int)headerTable.p_filesz); // Read segment to data

                        int succesfulLoad = memory.loadRAMfromDataArrayBytes(ref data, ref headerTable);
                        if (succesfulLoad == -1) // return if fail to load registers, mostlikely it doesn't fit in the RAM
                            return -1;

                        debugLog.WriteLineToLog("Loader: Segment " + segmentNum +
                                        " - vAddress = " + headerTable.p_vaddr +
                                        ", File Offset: " + headerTable.p_offset +
                                        ", Size: " + headerTable.p_filesz);
                        nextElfHeaderOffset += ((uint)elfHeader.e_phentsize);
                    }

                    uint cksum = memory.ComputeRAMChecksum(null);
                    Console.WriteLine("\nCurrent RAM hex digest: " + cksum);

                }

            }
            catch (Exception e)
            {
                debugLog.WriteLineToLog("Loader: in loadSegmentsIntoRAM(): File cannot be opened: " + e.Message);
                Console.WriteLine("For help try: armmemory.exe --help");
                return -1;
            }

            return 0;
        }

        #endregion
    }
}
