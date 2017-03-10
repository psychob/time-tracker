using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timetracker
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			SetUpLogger();

			LogManager.GetLogger(typeof(Program)).Info("Application started");

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWindow());
		}

		private static void SetUpLogger()
		{
			Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

			PatternLayout patternLayout = new PatternLayout();
			patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
			patternLayout.ActivateOptions();

			FileAppender file = new FileAppender();
			file.AppendToFile = false;
			file.File = Application.UserAppDataPath + @"\Logs\" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss.fff") + ".log";
			file.Layout = patternLayout;
			file.LockingModel = new FileAppender.ExclusiveLock();
			file.ActivateOptions();
			file.Encoding = Encoding.UTF8;
			hierarchy.Root.AddAppender(file);

			OutputDebugStringAppender odsa = new OutputDebugStringAppender();
			odsa.Layout = patternLayout;
			odsa.ActivateOptions();
			hierarchy.Root.AddAppender(odsa);

			MemoryAppender memory = new MemoryAppender();
			memory.ActivateOptions();
			hierarchy.Root.AddAppender(memory);

			hierarchy.Root.Level = Level.All;
			hierarchy.Configured = true;
		}
	}
}
