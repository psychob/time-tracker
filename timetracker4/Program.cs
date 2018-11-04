using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ninject;
using timetracker4.Forms;

namespace timetracker4
{
    static class Program
    {
        internal static StandardKernel DependencyInjection;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DependencyInjection = new StandardKernel();
            DependencyInjection.Load(Assembly.GetExecutingAssembly());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
