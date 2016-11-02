using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static WinAPI.WinUser;
using static WinAPI.Kernel32;

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

				if (it.App.AllowOnlyOne)
					liv.BackColor = Color.PaleGreen;

				liv.SubItems.AddRange(new string[]{
					it.App.Name,
					TrackSystem.Utils.GetTime(GetTickCount64() - it.StartTime),
					TrackSystem.Utils.GetTime(it.AllTime + (GetTickCount64() - it.StartTime)),
					it.StartCount.ToString(),
				});

				lvTrackedApps.Items.Add(liv);
			}

			long pixels = TrackSystem.TrackingSystemState.MouseDistance;

			if (pixels > 1000000000000)
				tsslPixelDistance.Text = string.Format("{0:F2} Tpx", TrackSystem.TrackingSystemState.MouseDistance / 1000000000000.0);
			else if (pixels > 1000000000)
				tsslPixelDistance.Text = string.Format("{0:F2} Gpx", TrackSystem.TrackingSystemState.MouseDistance / 1000000000.0);
			else if (pixels > 1000000)
				tsslPixelDistance.Text = string.Format("{0:F2} Mpx", TrackSystem.TrackingSystemState.MouseDistance / 1000000.0);
			else if (pixels > 1000)
				tsslPixelDistance.Text = string.Format("{0:F2} Kpx", TrackSystem.TrackingSystemState.MouseDistance / 1000.0);
			else
				tsslPixelDistance.Text = string.Format("{0} px", TrackSystem.TrackingSystemState.MouseDistance);

			tsslPixelDistanceRaw.Text = pixels.ToString();

			tsslKeyStrokes.Text = TrackSystem.TrackingSystemState.KeyboardStrokes.ToString();
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
					ad.ApplicationUniqueID, ad.ApplicationRules, ad.ApplicationAllowOnlyOne);

				TrackSystem.TrackingSystemState.GrabAll();
			}
		}

		private delegate void _OpenCallback();

		private void fromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileTemplate();
		}

		internal static TrackSystem.Structs.App? OpenFileTemplate()
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

				TrackSystem.Structs.AppRuleSet ars = new TrackSystem.Structs.AppRuleSet();
				ars.Kind = TrackSystem.Structs.RuleSet.All;
				ars.Priority = TrackSystem.Structs.RulePriority.Medium;
				ars.UniqueId = "Inherit from file: " + Path.GetFileName(ofd.FileName);

				List<TrackSystem.Structs.AppRule> rules = new List<TrackSystem.Structs.AppRule>();

				rules.Add(new TrackSystem.Structs.AppRule(TrackSystem.Structs.AppRuleMatchTo.FileName,
					Path.GetFileName(ofd.FileName),
					TrackSystem.Structs.AppRuleAlgorithm.ExactInvariant));
				rules.Add(new TrackSystem.Structs.AppRule(TrackSystem.Structs.AppRuleMatchTo.FileMD5,
					md5, TrackSystem.Structs.AppRuleAlgorithm.Exact));

				if (!fvi.FileDescription.IsEmptyOrNull())
					rules.Add(new TrackSystem.Structs.AppRule(TrackSystem.Structs.AppRuleMatchTo.FileVersionDesc,
						fvi.FileDescription, TrackSystem.Structs.AppRuleAlgorithm.Exact));

				if (!fvi.CompanyName.IsEmptyOrNull())
					rules.Add(new TrackSystem.Structs.AppRule(TrackSystem.Structs.AppRuleMatchTo.FileVersionCompany,
						fvi.CompanyName, TrackSystem.Structs.AppRuleAlgorithm.Exact));

				if (!fvi.FileVersion.IsEmptyOrNull())
					rules.Add(new TrackSystem.Structs.AppRule(TrackSystem.Structs.AppRuleMatchTo.FileVersionFileVersion,
						fvi.FileVersion, TrackSystem.Structs.AppRuleAlgorithm.Exact));

				if (!fvi.ProductVersion.IsEmptyOrNull())
					rules.Add(new TrackSystem.Structs.AppRule(TrackSystem.Structs.AppRuleMatchTo.FileVersionProductVersion,
						fvi.ProductVersion, TrackSystem.Structs.AppRuleAlgorithm.Exact));

				ars.Rules = rules.ToArray();

				ad.ApplicationRules = new TrackSystem.Structs.AppRuleSet[]
				{
					ars
				};

				ad.ShowDialog();

				if (ad.IsValid)
				{
					var appinfo = TrackSystem.TrackingSystemState.AddNewDefinition(ad.ApplicationName,
						ad.ApplicationUniqueID, ad.ApplicationRules, ad.ApplicationAllowOnlyOne);

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
