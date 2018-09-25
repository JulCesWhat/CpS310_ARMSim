using armsim.Extra_Classes;
using armsim.Prototype;
using armsim.Unit_Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace armsim
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Options arguments = new Options(args);

            if (arguments.showTest)
            {
                Console.WriteLine("RUNNING UNIT TESTS ...");

                TestMemory.runTests();

                Console.WriteLine("All UNIT TESTS passed! Exiting ...");
                Environment.Exit(0);
            }

            // subject and observers. Observers computer and form1 add themselves to the collection of observers and hold an object reference to it.
            Subject subject = new Subject();
            Computer computer = new Computer(subject, arguments);


            // preset to launch form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // create a form and launch it
            ArmSimForm form = new ArmSimForm(subject, computer, arguments);
            Application.Run(form);
        }
    }
}
