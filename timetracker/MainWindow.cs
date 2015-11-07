﻿using System;
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
		NotifyIcon OwnNotifyIcon;
		Timer FetchTimer;
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

			TrackSystem.TrackingSystemState = new TrackSystem();

			OwnNotifyIcon = new NotifyIcon();
			OwnNotifyIcon.Visible = true;
			OwnNotifyIcon.Icon = Icon;
			OwnNotifyIcon.Text = "TimeTracker v" + CurrentVersion;
			OwnNotifyIcon.DoubleClick += NotifyIconDoubleClickEvent;
			OwnNotifyIcon.ContextMenuStrip = cmsNotify;

			FetchTimer = new Timer();
			FetchTimer.Interval = 5 * 1000;
			FetchTimer.Tick += OnFetchTickEvent;
		}

		private void OnFetchTickEvent(object sender, EventArgs e)
		{
			OnFetchTick();
		}

		private void OnFetchTick()
		{
			var tapps = TrackSystem.TrackingSystemState.GetRunningAps();

			lvTrackedApps.Items.Clear();

			foreach (var it in tapps)
			{
				ListViewItem liv = new ListViewItem(it.PID.ToString());

				liv.SubItems.AddRange(new string[]{
					it.Name,
					TrackSystem.Utils.GetTime(TrackSystem.WinAPI.GetTickCount64() - it.StartTime),
					TrackSystem.Utils.GetTime(it.AllTime + (TrackSystem.WinAPI.GetTickCount64() - it.StartTime))
				});

				lvTrackedApps.Items.Add(liv);
			}
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
				TrackSystem.TrackingSystemState.Close();
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

			FetchTimer.Start();
			OnFetchTick();
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

		private void exitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AllowClose = true;
			Close();
		}

		private void listToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ApplicationDefinition adef = new ApplicationDefinition();

			adef.AppDefinitions = TrackSystem.TrackingSystemState.GetDefiniedApps();
			adef.ShowDialog();

			TrackSystem.TrackingSystemState.GrabAll();
		}

		private void definitionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddDefinition ad = new AddDefinition();

			ad.ShowDialog();

			if (ad.IsValid)
			{
				var appinfo = TrackSystem.TrackingSystemState.AddNewDefinition(ad.ApplicationName,
					ad.ApplicationUniqueID, ad.ApplicationRules);

				TrackSystem.TrackingSystemState.GrabAll();
			}
		}

		private void fromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();

			ofd.ShowDialog(this);
		}
	}
}
