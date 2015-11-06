using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timetracker
{
	public partial class MainWindow : Form
	{
		bool AllowVisible = false;
		bool AllowClose = false;
		TrackSystem TrackingSystem;
		NotifyIcon OwnNotifyIcon;
		string CurrentVersion;

		public MainWindow()
		{
			InitializeComponent();

			{
				var ass = System.Reflection.Assembly.GetExecutingAssembly();
				var fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(ass.Location);
				var str = fvi.ProductVersion.Split('.');
				int it = str.Length - 1;

				while (it >= 0 && str[it] == "0")
					it--;

				for (int kt = 0; kt <= it; ++kt)
					CurrentVersion = str[kt] + '.';

				CurrentVersion = CurrentVersion.Trim('.');
			}

			TrackingSystem = new TrackSystem();

			OwnNotifyIcon = new NotifyIcon();
			OwnNotifyIcon.Visible = true;
			OwnNotifyIcon.Icon = Icon;
			OwnNotifyIcon.Text = "TimeTracker v" + CurrentVersion;
			OwnNotifyIcon.DoubleClick += NotifyIconDoubleClickEvent;
			OwnNotifyIcon.ContextMenuStrip = cmsNotify;
		}

		private void NotifyIconDoubleClickEvent(object sender, EventArgs e)
		{
			if (Visible)
			{
				AllowVisible = false;
				Hide();
			}
			else
			{
				AllowVisible = true;
				Show();
				WindowState = FormWindowState.Normal;
				Activate();
			}
		}

		protected override void SetVisibleCore(bool value)
		{
			if (!AllowVisible)
			{
				value = false;
				if (!IsHandleCreated)
					CreateHandle();
			}

			base.SetVisibleCore(value);
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (!AllowClose)
			{
				Hide();
				e.Cancel = true;
			} else
			{
				TrackingSystem.Close();
				OwnNotifyIcon.Dispose();
			}

			base.OnFormClosing(e);
		}

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case TrackSystem.WinAPI.WM_QUERYENDSESSION:
					m.Result = (IntPtr)1;
					break;

				case TrackSystem.WinAPI.WM_ENDSESSION:
					AllowClose = true;
					Close();
					m.Result = (IntPtr)1;
					break;

				default:
					base.WndProc(ref m);
					break;
			}
		}

		private void MainWindow_Resize(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Minimized)
			{
				WindowState = FormWindowState.Normal;
				AllowVisible = false;
				Hide();
			}
		}

		private void MainWindow_Load(object sender, EventArgs e)
		{
			Text = "TimeTracker v" + CurrentVersion;
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AllowClose = true;
			Close();
		}

		private void cmsNotify_Open_Click(object sender, EventArgs e)
		{
			if (Visible)
			{
				AllowVisible = false;
				Hide();
			}
			else
			{
				AllowVisible = true;
				Show();
				WindowState = FormWindowState.Normal;
				Activate();
			}
		}

		private void cmsNotify_Opening(object sender, CancelEventArgs e)
		{
			if (Visible)
				cmsNotify_Open.Text = "Hide";
			else
				cmsNotify_Open.Text = "Open";
		}
	}
}
