using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace armsim.Simulator_I
{
    public abstract class Instruction
    {

        /// <summary>
        /// Checks for special cases like MUL and then checks for instruction type
        /// from <instruction> and decodes it according to the instruction type.
        /// </summary>
        public abstract void decodeInstruction();

        public abstract void executeInstruction();
    }
}
