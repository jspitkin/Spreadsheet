using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SS;

namespace SpreadsheetGUI
{
    /// <summary>
    /// Tracks how many spreadsheets are running.
    /// Based off DemoApplicationContext class in Demo project for PS6
    /// </summary>
    class SpreadsheetApplicationContext : ApplicationContext
    {
        //Number of open forms
        private int formCount = 0;

        //Singleton ApplicationaContext
        private static SpreadsheetApplicationContext appContext;

        /// <summary>
        /// Private constructor for singleton pattern
        /// </summary>
        private SpreadsheetApplicationContext()
        {
        }

        /// <summary>
        /// Returns the one SpreadsheetApplicationContext
        /// </summary>
        /// <returns></returns>
        public static SpreadsheetApplicationContext getAppContext()
        {
            if (appContext == null)
            {
                appContext = new SpreadsheetApplicationContext();
            }
            return appContext;
        }

        /// <summary>
        /// Runs the form
        /// </summary>
        /// <param name="form">The form to be ran</param>
        public void RunForm(Form form)
        {
            //Increase the form count
            formCount++;

            form.FormClosed += (o, e) => { if (--formCount <= 0) ExitThread(); };

            //Run the form
            form.Show();
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            appContext.RunForm(new SpreadsheetForm());
            Application.Run(appContext);
        }
    }
}
