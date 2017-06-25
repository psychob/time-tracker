using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using timetracker.Entities;

using static timetracker.WinAPI.WinUser;
using static timetracker.WinAPI.Kernel32;
using System.Globalization;
using timetracker.BasePlugin;

namespace timetracker
{
	public partial class MainWindow : Form
	{
		bool AllowVisible = false;
		bool AllowClose = false;
		NotifyIcon OwnNotifyIcon;
		Timer FetchTimer;
		string CurrentVersion;
		DateTime StartTime = DateTime.Now;

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
					CurrentVersion += str[kt] + '.';

				CurrentVersion = CurrentVersion.Trim('.');
			}

			TrackSystem.TrackingSystemState = new TrackSystem();

			OwnNotifyIcon = new NotifyIcon();
			OwnNotifyIcon.Visible = true;
			OwnNotifyIcon.Icon = Properties.Resources.MainIcon;
			OwnNotifyIcon.Text = "TimeTracker v" + CurrentVersion;
			OwnNotifyIcon.DoubleClick += NotifyIconDoubleClickEvent;
			OwnNotifyIcon.ContextMenuStrip = cmsNotify;

			FetchTimer = new Timer();
			FetchTimer.Interval = 1000;
			FetchTimer.Tick += OnFetchTickEvent;
		}

		private void OnFetchTickEvent(object sender, EventArgs e)
		{
			OnFetchTick();
		}

		uint TickCount = 0;

		private void OnFetchTick()
		{
			if (TickCount % 5 == 0)
			{
				var tapps = TrackSystem.TrackingSystemState.GetRunningAps();

				lvTrackedApps.Items.Clear();

				foreach (var it in tapps)
				{
					ListViewItem liv = new ListViewItem(it.PID.ToString());

					if (it.Merged)
						liv.BackColor = Color.Blue;
					else if (it.App.AllowOnlyOne && it.Merged)
						liv.BackColor = Color.Yellow;
					else if (it.App.AllowOnlyOne)
						liv.BackColor = Color.PaleGreen;

					liv.SubItems.AddRange(new string[]{
					it.App.Name,
					TrackSystem.Utils.GetTime(GetTickCount64() - it.StartTime),
					TrackSystem.Utils.GetTime(it.AllTime + (GetTickCount64() - it.StartTime)),
					it.StartCount.ToString(),
				});

					lvTrackedApps.Items.Add(liv);
				}
			}

			TickCount++;

			var NetworkReciver = TrackSystem.TrackingSystemState.ReciverSpeed;
			var NetworkSent = TrackSystem.TrackingSystemState.SentSpeed;
			var TotalNetworkReciver = TrackSystem.TrackingSystemState.ReciverData;
			var TotalNetworkSent = TrackSystem.TrackingSystemState.SentData;

			var time = (DateTime.Now - StartTime);

			if (time.Days > 0)
				tsslTime.Text = time.ToString(@"d\d\:h\h\:m\m\:s\s", CultureInfo.InvariantCulture);
			else if (time.Hours > 0)
				tsslTime.Text = time.ToString(@"h\h\:m\m\:s\s", CultureInfo.InvariantCulture);
			else if (time.Minutes > 0)
				tsslTime.Text = time.ToString(@"m\m\:s\s", CultureInfo.InvariantCulture);
			else
				tsslTime.Text = time.ToString(@"s\s", CultureInfo.InvariantCulture);

			tsslPixelDistance.Text = "{0} px".FormatWith(0);
			tsslPixelDistanceRaw.Text = "{0} px/s".FormatWith(0);
			tsslKeyStrokes.Text = "{0} keys".FormatWith(0);
			tsslKeyPerSecond.Text = "{0} key/m".FormatWith(0);
			tsslMouseClickCount.Text = "{0} mclk".FormatWith(0);
			tsslMouseClickSpeed.Text = "{0} mclk/m".FormatWith(0);

			tsslNetworkReciver.Text = "↓ {0}B/s".FormatWith(NetworkReciver.ToMetric(1024, "i"));
			tsslNetSent.Text = "↑ {0}B/s".FormatWith(NetworkSent.ToMetric(1024, "i"));

			tsslNetRecivedTotal.Text = "↓ {0}B".FormatWith(TotalNetworkReciver.ToMetric(1024, "i"));
			tsslNetTotalSent.Text = "↑ {0}B".FormatWith(TotalNetworkSent.ToMetric(1024, "i"));
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
			}
			else
			{
				TrackSystem.TrackingSystemState.Close();
				OwnNotifyIcon.Dispose();
			}

			base.OnFormClosing(e);
		}

		protected override void WndProc(ref Message m)
		{
			switch ((WindowMessage)m.Msg)
			{
				case WindowMessage.WM_QUERYENDSESSION:
					m.Result = (IntPtr)1;
					break;

				case WindowMessage.WM_ENDSESSION:
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
					ad.ApplicationUniqueID, ad.ApplicationRules, ad.ApplicationAllowOnlyOne,
					ad.MergeSpawned);

				TrackSystem.TrackingSystemState.GrabAll();
			}
		}

		private delegate void _OpenCallback();

		private void fromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileTemplate();
		}

		internal static App? OpenFileTemplate()
		{
			OpenFileDialog ofd = new OpenFileDialog();

			ofd.Filter = "Exe Files (*.exe;*.com)|*.exe;*.com";
			ofd.CheckFileExists = true;

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				System.Diagnostics.FileVersionInfo fvi =
					System.Diagnostics.FileVersionInfo.GetVersionInfo(ofd.FileName);
				string md5 = TrackSystem.Utils.GetMD5(ofd.FileName);
				AddDefinition ad = new AddDefinition();

				if (fvi.ProductName != string.Empty)
					ad.ApplicationName = fvi.ProductName;
				else
					ad.ApplicationName = Path.GetFileName(ofd.FileName);

				ad.RunIntelligentNamer();

				AppRuleSet ars = new AppRuleSet();
				ars.Kind = RuleSet.All;
				ars.Priority = RulePriority.Medium;
				ars.UniqueId = "Inherit from file: " + Path.GetFileName(ofd.FileName);

				List<AppRule> rules = new List<AppRule>();

				rules.Add(new AppRule(AppRuleMatchTo.FileName,
					Path.GetFileName(ofd.FileName),
					AppRuleAlgorithm.ExactInvariant));
				rules.Add(new AppRule(AppRuleMatchTo.FileMD5,
					md5, AppRuleAlgorithm.Exact));

				if (!fvi.FileDescription.IsEmptyOrNull())
					rules.Add(new AppRule(AppRuleMatchTo.FileVersionDesc,
						fvi.FileDescription, AppRuleAlgorithm.Exact));

				if (!fvi.CompanyName.IsEmptyOrNull())
					rules.Add(new AppRule(AppRuleMatchTo.FileVersionCompany,
						fvi.CompanyName, AppRuleAlgorithm.Exact));

				if (!fvi.FileVersion.IsEmptyOrNull())
					rules.Add(new AppRule(AppRuleMatchTo.FileVersionFileVersion,
						fvi.FileVersion, AppRuleAlgorithm.Exact));

				if (!fvi.ProductVersion.IsEmptyOrNull())
					rules.Add(new AppRule(AppRuleMatchTo.FileVersionProductVersion,
						fvi.ProductVersion, AppRuleAlgorithm.Exact));

				ars.Rules = rules.ToArray();

				ad.ApplicationRules = new AppRuleSet[]
				{
					ars
				};

				ad.ShowDialog();

				if (ad.IsValid)
				{
					var appinfo = TrackSystem.TrackingSystemState.AddNewDefinition(ad.ApplicationName,
						ad.ApplicationUniqueID, ad.ApplicationRules, ad.ApplicationAllowOnlyOne,
						ad.MergeSpawned);

					TrackSystem.TrackingSystemState.GrabAll();

					return appinfo;
				}
			}

			return null;
		}

		private void saveDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TrackSystem.TrackingSystemState.SaveDatabase();
		}
	}
}
