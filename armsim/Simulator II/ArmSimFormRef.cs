using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace armsim.Simulator_II
{
    // Delegate to write a character into the terminal panel
    public delegate void WriteCharToTerminalDelegate(string strMessage);

    public delegate char dequeCharsQueueDelegate();

    public delegate bool checkCharsQueueContainsReturnDelegate();

    // ArmSimFormRef is has reference to ArmSimForm class and its methods.
    // The methods in this class are visible in all classes.
    public static class ArmSimFormRef
    {
        public static ArmSimForm mainwin;

        public static event WriteCharToTerminalDelegate OnWriteCharToTerminal;
        public static event dequeCharsQueueDelegate OnDequeCharsQueue;
        public static event checkCharsQueueContainsReturnDelegate OnCheckCharsQueueContainsReturn;

        // FUNCTION:  - Run event handler ArmSimFormRef_OnWriteCharToTerminal() in ArmSimForm class which
        //              Writes a char to terminal from field charQueue in ArmSimForm class
        public static void WriteCharToTerminal(string strMessage)
        {
            ThreadSafeWriteCharToTerminal(strMessage);
        }

        // HELPER FUNCTION to WriteCharToTerminal()
        // Thread that writes a character to terminal
        private static void ThreadSafeWriteCharToTerminal(string strMessage)
        {
            if (mainwin != null && mainwin.InvokeRequired)  // we are in a different thread to the main window
            {
                mainwin.Invoke(new WriteCharToTerminalDelegate(ThreadSafeWriteCharToTerminal), new object[] { strMessage });  // call self from main thread
            }
            else
                OnWriteCharToTerminal(strMessage);
        }


        public static char dequeCharsQueue()
        {
            return ThreadSafeDequeCharsQueue();
        }

        private static char ThreadSafeDequeCharsQueue()
        {
            if (mainwin != null && mainwin.InvokeRequired)  // we are in a different thread to the main window
            {
                return (char)mainwin.Invoke(new dequeCharsQueueDelegate(ThreadSafeDequeCharsQueue));  // call self from main thread
            }
            else
                return OnDequeCharsQueue();
        }



        public static bool checkCharsQueueContainsReturn()
        {
            return ThreadSafeCheckCharsQueueContainsReturn();
        }

        private static bool ThreadSafeCheckCharsQueueContainsReturn()
        {
            if (mainwin != null && mainwin.InvokeRequired)  // we are in a different thread to the main window
            {
                return (bool)mainwin.Invoke(new checkCharsQueueContainsReturnDelegate(ThreadSafeCheckCharsQueueContainsReturn));  // call self from main thread
            }
            else
                return OnCheckCharsQueueContainsReturn();
        }

    }
}
