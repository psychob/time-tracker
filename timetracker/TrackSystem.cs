﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

using timetracker.WinAPI.WMI;
using static timetracker.WinAPI.Kernel32;
using timetracker.Entities;
using timetracker.BasePlugin;

namespace timetracker
{
	public partial class TrackSystem
	{
		const string ApplicationDefinitions = "appdefinition3.xml";
		const string TrackedTimesCatalogue = "tracks3";

		internal static class Utils
		{
			public static bool IsStringMatch(string pattern, string subject,
				AppRuleAlgorithm algo)
			{
				switch (algo)
				{
					case AppRuleAlgorithm.Exact:
						return pattern == subject;

					case AppRuleAlgorithm.ExactInvariant:
						return pattern.Equals(subject, StringComparison.CurrentCultureIgnoreCase);

					case AppRuleAlgorithm.Near:
						return new Regex(GenerateNearCharacters(pattern)).IsMatch(subject);

					case AppRuleAlgorithm.NearInvariant:
						return new Regex(GenerateNearCharacters(pattern), RegexOptions.IgnoreCase).IsMatch(subject);

					case AppRuleAlgorithm.Regex:
						{
							try
							{
								return new Regex(pattern).IsMatch(subject);
							}
							catch (Exception)
							{
								return false;
							}
						}

					case AppRuleAlgorithm.RegexInvariant:
						{
							try
							{
								return new Regex(pattern, RegexOptions.IgnoreCase).IsMatch(subject);
							}
							catch (Exception)
							{
								return false;
							}
						}

					case AppRuleAlgorithm.StartsWith:
						return subject.StartsWith(pattern);

					case AppRuleAlgorithm.StartsWithInvariant:
						return subject.StartsWith(pattern, StringComparison.CurrentCultureIgnoreCase);

					case AppRuleAlgorithm.EndWith:
						return subject.EndsWith(pattern);

					case AppRuleAlgorithm.EndWithInvariant:
						return subject.EndsWith(pattern, StringComparison.CurrentCultureIgnoreCase);
				}

				throw new ArgumentException("Algorithm is not supported", nameof(algo));
			}

			private static string GenerateNearCharacters(string pattern)
			{
				// ? -> .{1}
				// * -> .*

				string out_str = "";
				int it = 0;

				while (it < pattern.Length)
				{
					int asterix = pattern.IndexOf("*", it);
					int exclam = pattern.IndexOf("?", it);

					int char_ = 0;

					if (asterix != -1 && exclam != -1)
						char_ = Math.Min(asterix, exclam);
					else if (asterix != -1)
						char_ = asterix;
					else if (exclam != -1)
						char_ = exclam;
					else
						char_ = -1;

					if (char_ == -1)
					{
						out_str += EscapeCharacters(pattern.Substring(it));
						it = pattern.Length;
					}
					else
					{
						if (char_ != it)
						{
							out_str += EscapeCharacters(pattern.Substring(it, char_ - it));
							it = char_;
						}

						switch (pattern[it])
						{
							case '?':
								out_str += ".{1}";
								break;

							case '*':
								out_str += ".*";
								break;
						}

						it++;
					}
				}

				return "^" + out_str + "$";
			}

			private static string EscapeCharacters(string str)
			{
				foreach (var it in new string[] { "\\", "[", "]", "(", ")", ".", "*", "?", "^", "$" })
					str = str.Replace(it, "\\" + it);

				return str;
			}

			public static string GetTime(ulong time)
			{
				ulong y = 0,
					  o = 0,
					  d = 0,
					  h = 0,
					  m = 0,
					  s = 0;

				const ulong s_time = 1000 * 1,
							m_time = 60 * s_time,
							h_time = 60 * m_time,
							d_time = 24 * h_time,
							o_time = 30 * d_time,
							y_time = 365 * d_time;

				string ret = "";

				if (time > y_time)
				{
					y = time / y_time;
					time -= y * y_time;
				}

				if (time > o_time)
				{
					o = time / o_time;
					time -= o * o_time;
				}

				if (time > d_time)
				{
					d = time / d_time;
					time -= d * d_time;
				}

				if (time > h_time)
				{
					h = time / h_time;
					time -= h * h_time;
				}

				if (time > m_time)
				{
					m = time / m_time;
					time -= m * m_time;
				}

				if (time > s_time)
				{
					s = time / s_time;
					time -= s * s_time;
				}

				if (y > 0)
					ret += y.ToString() + "y ";

				if (o > 0)
					ret += o.ToString() + "mo ";

				if (d > 0)
					ret += d.ToString() + "d ";

				if (h > 0)
					ret += h.ToString() + "h ";

				if (m > 0)
					ret += m.ToString() + "m ";

				if (s > 0)
					ret += s.ToString() + "s ";

				ret = ret.Trim();
				if (ret == string.Empty)
					return "0s";
				else
					return ret;
			}

			internal static string GetMD5(string path)
			{
				string md5_str = "";

				try
				{
					using (MD5 md5 = MD5.Create())
					{
						using (var stream = File.OpenRead(path))
						{
							md5_str = BitConverter.ToString(md5.ComputeHash(stream))
								.Replace("-", "").ToLower();
						}
					}
				}
				catch (Exception)
				{
					md5_str = "can't compute md5 hash!";
				}

				return md5_str;
			}

			internal static string ExtractString(AppRuleMatchTo matchTo, int pID,
				string name, string path, FileVersionInfo fileVersion,
				ref Dictionary<string, string> md5Cache)
			{
				switch (matchTo)
				{
					case AppRuleMatchTo.FileName:
						return name;

					case AppRuleMatchTo.FileNamePath:
						return path;

					case AppRuleMatchTo.FilePath:
						return Path.GetDirectoryName(path);

					case AppRuleMatchTo.FileVersionCompany:
						return fileVersion.CompanyName;

					case AppRuleMatchTo.FileVersionDesc:
						return fileVersion.FileDescription;

					case AppRuleMatchTo.FileVersionFileVersion:
						return fileVersion.FileVersion;

					case AppRuleMatchTo.FileVersionName:
						return fileVersion.ProductName;

					case AppRuleMatchTo.FileVersionProductVersion:
						return fileVersion.ProductVersion;

					case AppRuleMatchTo.FileMD5:
						if (md5Cache.ContainsKey(path))
							return md5Cache[path];
						else
						{
							string md5 = Utils.GetMD5(path);
							md5Cache[path] = md5;
							return md5;
						}
				}

				throw new ArgumentException("Enumeration argument is wrong!", nameof(matchTo));
			}
		}

		internal class Tracker : IDisposable
		{
			internal delegate void InternetChangeStateType(Guid guid, string Name, Win32_NetworkAdapter.NetConnectionStatus state);
			internal delegate void OSChangeType(ulong free, ulong all, ulong virtualFree, ulong virtualAll);
			internal delegate void ProcessorLoad(string Name, ulong Idle, ulong Kernel, ulong Work);
			internal delegate void OnNetworkBandwidthType(ulong R, ulong T);

			internal InternetChangeStateType OnInternetEvent;
			internal OSChangeType OnOsEvent;
			internal ProcessorLoad OnProcessorLoad;
			internal OnNetworkBandwidthType OnNetworkBandwidth;

			ManagementEventWatcher eventInternet;
			ManagementEventWatcher eventOS;
			ManagementEventWatcher eventProcessor;
			ManagementEventWatcher eventNetwork;

			internal ForegroundHook fHook = new ForegroundHook();
			internal NamechangeHook nHook = new NamechangeHook();
			internal MouseHook mHook = new MouseHook();

			public void Start()
			{
				InitializeTracking();
			}

			private void InitializeTracking()
			{
				const string NameSpace = @"\\.\root\CIMV2";
				const string NetChange = @"SELECT * FROM __InstanceModificationEvent WITHIN 600 WHERE TargetInstance ISA 'Win32_NetworkAdapter' AND TargetInstance.PhysicalAdapter = True";
				const string OperatingSystem = @"SELECT * FROM __InstanceModificationEvent WITHIN 5 WHERE TargetInstance ISA 'Win32_OperatingSystem'";
				const string ProcesSql = @"SELECT * FROM __InstanceModificationEvent WITHIN 5 WHERE TargetInstance ISA 'Win32_PerfFormattedData_PerfOS_Processor'";
				const string NetworkSql = @"SELECT * FROM __InstanceModificationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_PerfRawData_Tcpip_NetworkInterface'";

				eventInternet = new ManagementEventWatcher(NameSpace, NetChange);
				eventInternet.EventArrived += OnModificationInternetEvent;

				eventOS = new ManagementEventWatcher(NameSpace, OperatingSystem);
				eventOS.EventArrived += OnOSEvent;

				eventProcessor = new ManagementEventWatcher(NameSpace, ProcesSql);
				eventProcessor.EventArrived += ProcessorEvent;

				eventNetwork = new ManagementEventWatcher(NameSpace, NetworkSql);
				eventNetwork.EventArrived += networkEventArrived;
                
				fHook.Init();
				nHook.Init();
				mHook.Init();

				PullAllInternet();

				eventInternet.Start();
				eventOS.Start();
				eventProcessor.Start();
				eventNetwork.Start();
			}

			private void networkEventArrived(object sender, EventArrivedEventArgs e)
			{
				ManagementBaseObject mbo = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;

				ulong Recivied = (ulong)(UInt64)mbo.Properties["BytesReceivedPersec"].Value;
				ulong Send = (ulong)(UInt64)mbo.Properties["BytesSentPersec"].Value;

				OnNetworkBandwidth(Recivied, Send);
			}

			private void OnModificationInternetEvent(object sender, EventArrivedEventArgs e)
			{
				ManagementBaseObject mbo = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;

				string name = (string)mbo.Properties["Name"].Value;
				UInt16 status = (UInt16)mbo.Properties["NetConnectionStatus"].Value;
				Guid g = Guid.Parse((string)mbo.Properties["GUID"].Value);
				var n = Win32_NetworkAdapter.Convert(status);

				OnInternetEvent(g, name, n);
			}

			private void OnOSEvent(object sender, EventArrivedEventArgs e)
			{
				ManagementBaseObject mbo = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;

				ulong FreePhysicialMemory = (ulong)(UInt64)mbo.Properties["FreePhysicalMemory"].Value;
				ulong TotalVisibleMemorySize = (ulong)(UInt64)mbo.Properties["TotalVisibleMemorySize"].Value;
				ulong FreeVirtualMemory = (ulong)(UInt64)mbo.Properties["FreeVirtualMemory"].Value;
				ulong TotalVirtualMemorySize = (ulong)(UInt64)mbo.Properties["TotalVirtualMemorySize"].Value;

				OnOsEvent(FreePhysicialMemory, TotalVisibleMemorySize, FreeVirtualMemory, TotalVirtualMemorySize);
			}

			private void ProcessorEvent(object sender, EventArrivedEventArgs e)
			{
				ManagementBaseObject mbo = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;

				OnProcessorLoad((string)mbo.Properties["Name"].Value,
					(UInt64)mbo.Properties["PercentIdleTime"].Value,
					(UInt64)mbo.Properties["PercentPrivilegedTime"].Value,
					(UInt64)mbo.Properties["PercentProcessorTime"].Value);
			}

			public void PullAllInternet()
			{
				const string NameSpace = @"root\CIMV2";
				const string Query = @"SELECT Name,NetConnectionStatus,GUID FROM Win32_NetworkAdapter WHERE PhysicalAdapter = True";

				using (ManagementObjectSearcher mos = new ManagementObjectSearcher(NameSpace, Query))
				{
					foreach (ManagementObject mbo in mos.Get())
					{
						string name = (string)mbo.Properties["Name"].Value;
						var status = (UInt16)mbo.Properties["NetConnectionStatus"].Value;
						Guid g = Guid.Parse((string)mbo.Properties["GUID"].Value);
						var n = Win32_NetworkAdapter.Convert(status);

						OnInternetEvent(g, name, n);
					}
				}
			}

			public void Finish()
			{
				eventInternet.Stop();
				eventOS.Stop();
				eventProcessor.Stop();
				eventNetwork.Stop();

				fHook.DeInit();
				nHook.DeInit();
				mHook.DeInit();

				eventInternet.Dispose();
				eventOS.Dispose();
				eventProcessor.Dispose();
				eventNetwork.Dispose();
			}

			public void Dispose()
			{
				Finish();
			}
		}

		internal static TrackSystem TrackingSystemState = null;

		Tracker tracker = null;
		List<CurrentApps> currentApps = new List<CurrentApps>();
		List<App> definedApps = new List<App>();
		List<WaitStruct> waitedApps = new List<WaitStruct>();
		System.Timers.Timer waited_timer, valid_timer, tick_timer;
		object inOutLock = new object();
		bool valid_tick_running = false;
		bool waited_timer_running = false;

		public TrackSystem()
		{
			LoadState();
			BeginTracking();

			waited_timer = new System.Timers.Timer();
			waited_timer.Interval = 5 * 1000;
			waited_timer.Elapsed += OnWaitTick;
			waited_timer.AutoReset = true;
			waited_timer.Enabled = true;

			valid_timer = new System.Timers.Timer();
			valid_timer.Interval = 1 * 60 * 1000;
			valid_timer.Elapsed += OnValidTick;
			valid_timer.AutoReset = true;
			valid_timer.Enabled = true;

			tick_timer = new System.Timers.Timer();
#if DEBUG
			tick_timer.Interval = 1000;
#else
			tick_timer.Interval = 15 * 60 * 1000;
#endif

			tick_timer.Elapsed += OnTickTick;
			tick_timer.AutoReset = true;
			tick_timer.Enabled = true;
		}

		public void Close()
		{
			waited_timer.Enabled = false;
			valid_timer.Enabled = false;
			tick_timer.Enabled = false;

			ValidTick();
			WaitTick();

			FinishTracking();
			FinishProcess();
			SaveState();
		}

		private void OnValidTick(object sender, EventArgs e)
		{
			if (valid_tick_running)
				return;

			ValidTick();
		}

		private void ValidTick()
		{
			lock (inOutLock)
			{
				valid_tick_running = true;

				foreach (var it in currentApps)
				{
					if (PidNotRunning(it.PID))
					{
						StopApp(it.PID, it);
						currentApps.Remove(it);
						ValidTick();
						return;
					}
				}

				valid_tick_running = false;
			}
		}

		private void OnTickTick(object sender, EventArgs e)
		{
			AppendBinary(new PingEventType());
		}

		private bool PidNotRunning(int PID)
		{
			return GetProcessData(PID).Reason != ExeDataContainerReason.Valid;
		}

		private void OnWaitTick(object sender, EventArgs e)
		{
			if (waited_timer_running)
				return;

			WaitTick();
		}

		private void WaitTick()
		{
			lock (inOutLock)
			{
				waited_timer_running = true;

				var copy = waitedApps.ToList();
				waitedApps.Clear();

				foreach (var it in copy)
					NewProcessArrived(it.PID, it.Count, it.StartTime, it.ParentID);

				waited_timer_running = false;
			}
		}

		private void LoadState()
		{
			try
			{
				XmlReader xr = XmlReader.Create(ApplicationDefinitions);

				XmlSerializer xs = new XmlSerializer(definedApps.GetType());
				definedApps = (List<App>)xs.Deserialize(xr);
			}
			catch (Exception)
			{
			}

			if (!Directory.Exists(TrackedTimesCatalogue))
				Directory.CreateDirectory(TrackedTimesCatalogue);

			XmlWriterSettings xws = new XmlWriterSettings();
			xws.CloseOutput = true;
			xws.Encoding = Encoding.UTF8;
			xws.Indent = true;
			xws.IndentChars = "\t";

			string baseName = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss.fff");
			string bdFileName = baseName + ".bxml";

			string CurrentVersion = "";
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

			StreamBinary = File.Open(Path.Combine(TrackedTimesCatalogue, bdFileName),
				FileMode.Create, FileAccess.Write, FileShare.Read);

			{
				byte[] HEADER = "BINARY DATA FIL1".GetBytes();
				StreamBinary.Write(HEADER, 0, HEADER.Length);
			}

			AppendBinary(new VersionEventType(CurrentVersion));
		}

		private void SaveState()
		{
			StreamBinary.Close();

			SaveDatabase();
		}

		ManagementEventWatcher ProcessBeginWatcher;
		ManagementEventWatcher ProcessEndWatcher;

		public void BeginTracking()
		{
			ProcessBeginWatcher = Win32_Process.Watch(5, BaseClass.WatchType.Creation, CreateProcess);
			ProcessEndWatcher = Win32_Process.Watch(5, BaseClass.WatchType.Deletion, DestroyProcess);

			GrabAll();

			tracker = new Tracker();

			tracker.OnInternetEvent = InternetEvent;
			tracker.OnOsEvent = MemoryEvent;
			tracker.OnProcessorLoad = ProcessorLoad;
			tracker.OnNetworkBandwidth = NetworkBandwitch;

			tracker.fHook.foregroundChanged = ForegroundEvent;
			tracker.nHook.namechangeEvent = NamechangeEvent;

			tracker.mHook.mouseClickEvent = MouseClickEvent;
			tracker.mHook.mouseMoveEvent = MouseMoveEvent;
			tracker.mHook.mouseWheelMoveEvent = MouseWheelEvent;

			var x = System.Windows.Forms.Screen.PrimaryScreen;
			ResolutionChangeEvent(x.Bounds.Width, x.Bounds.Height);

			tracker.Start();
		}

		private void CreateProcess(Win32_Process obj)
		{
			int parentId = 0;

			if (obj.ParentProcessId.HasValue)
				parentId = (int)obj.ParentProcessId.Value;

			NewProcessArrived((int)obj.ProcessId, parentId);
		}

		private void DestroyProcess(Win32_Process obj)
		{
			ProcessDestoryed((int)obj.ProcessId);
		}

		private void ResolutionChangeEvent(int width, int height)
		{
			AppendBinary(new ResolutionChange(width, height));
		}

		private void InternetEvent(Guid g, string Name, Win32_NetworkAdapter.NetConnectionStatus state)
		{
			AppendBinary(new NetworkAdapterDefinition(Name, g));
		}

		private void NewProcessArrived(int pid, int ParentID)
		{
			NewProcessArrived(pid, 0, GetTickCount64(), ParentID);
		}

		private void NewProcessArrived(int pid, int retire, ulong processTime,
			int ParentID)
		{
			if (pid == 0 || pid == 4)
				return;

			lock (inOutLock)
			{
				// podany PID już jest już śledzony
				if (currentApps.Contains(cai => cai.PID == pid))
					return;

				// pobieramy dane o procesie
				var pd = GetProcessData(pid);

				// nie udało się pobrać danych?
				switch (pd.Reason)
				{
					case ExeDataContainerReason.IncompleteInformation:
						AddToWaitQueue(pid, processTime, retire, ParentID);
						return;

					case ExeDataContainerReason.ExceptionOccured:
						return;
				}

				var ipd = pd.Value;

				ScanForApplication(ipd.PID, ipd.Name, ipd.Path, ipd.FileVersion,
					processTime, ParentID);
			}
		}

		private ExeDataContainer GetProcessData(int pid)
		{
			try
			{
				ExeData ed = new ExeData();

				var pinfo = Process.GetProcessById(pid);

				ed.PID = pid;
				ed.Path = pinfo.MainModule.FileName;
				ed.Name = Path.GetFileName(ed.Path);
				ed.FileVersion = FileVersionInfo.GetVersionInfo(ed.Path);

				return new ExeDataContainer(ed);
			}
			catch (Win32Exception w32) when (w32.NativeErrorCode == 299)
			{
				return new ExeDataContainer(ExeDataContainerReason.IncompleteInformation);
			}
			catch (Exception)
			{
				return new ExeDataContainer(ExeDataContainerReason.ExceptionOccured);
			}
		}

		private void AddToWaitQueue(int pid, ulong processTime, int count, int ParentID)
		{
			if (count > 5)
				return;

			lock (inOutLock)
			{
				WaitStruct ws = new WaitStruct();

				ws.Count = count;
				ws.PID = pid;
				ws.StartTime = processTime;
				ws.ParentID = ParentID;

				waitedApps.Add(ws);
			}
		}

		private void ScanForApplication(int PID, string name, string path,
			FileVersionInfo fileVersion, ulong processTime, int ParentID)
		{
			lock (inOutLock)
			{
				List<AppRulePair> qualified = new List<AppRulePair>();
				Dictionary<string, string> md5Cache = new Dictionary<string, string>();
				RulePriority definedPriority = RulePriority.None;

				foreach (var it in definedApps)
				{
					if (it.Rules == null)
						continue;

					foreach (var jt in it.Rules)
					{
						int rules_match = 0;

						if (definedPriority > jt.Priority)
							continue;

						foreach (var kt in jt.Rules)
						{
							if (Utils.IsStringMatch(kt.MatchString,
								Utils.ExtractString(kt.MatchTo, PID, name, path,
									fileVersion, ref md5Cache),
								kt.MatchAlgorithm))
								rules_match++;
							else
							{
								if (jt.Kind == RuleSet.All)
									break;
							}
						}

						if (jt.Kind == RuleSet.All &&
							rules_match == jt.Rules.Length)
						{
							qualified.Add(new AppRulePair(it.UniqueID,
								jt.UniqueId, jt.Priority));

							if (definedPriority > jt.Priority)
								definedPriority = jt.Priority;
						}
						else if (jt.Kind == RuleSet.Any && rules_match > 0)
						{
							qualified.Add(new AppRulePair(it.UniqueID,
								jt.UniqueId, jt.Priority));

							if (definedPriority > jt.Priority)
								definedPriority = jt.Priority;
						}
					}
				}

				if (qualified.Count == 0)
					return;

				qualified = qualified.OrderByDescending(x => x.Priority).ToList();
				App chosen_app = GetAppByRule(qualified[0]);

				StartApp(PID, chosen_app, processTime, chosen_app.Time,
					qualified[0], ParentID);
			}
		}

		public App AddNewDefinition(string applicationName,
			string applicationUniqueID, AppRuleSet[] applicationRules,
			bool allowOnlyOne, bool merge)
		{
			string application_unique_id = applicationUniqueID;
			int counter = 2;

			while (definedApps.Contains(c => c.UniqueID == application_unique_id))
				application_unique_id = string.Format("{0}_{1}", applicationUniqueID, counter++);

			applicationUniqueID = application_unique_id;

			App app = new App();

			app.Name = applicationName;
			app.UniqueID = applicationUniqueID;
			app.Rules = applicationRules;
			app.IsShell = false;

			app.StartCounter = 0;
			app.Time = 0;

			app.AllowOnlyOne = allowOnlyOne;
			app.MergeSpawned = merge;

			definedApps.Add(app);

			AppendBinary(new AddNewDefinitionType(applicationUniqueID, app.Name));

			return app;
		}

		public void RemoveApp(string text)
		{
			foreach (var it in definedApps)
			{
				if (it.UniqueID == text)
				{
					App c = it;
					c.IsShell = true;
					c.Rules = null;

					AppendBinary(new RemoveDefinition(c.UniqueID));

					definedApps.Remove(it);
					definedApps.Add(c);
					return;
				}
			}
		}

		public void UpdateApp(string uniqueID, App idata)
		{
			definedApps.Replace(p => p.UniqueID == uniqueID, idata);
		}

		public App GetAppById(string AppID)
		{
			return (from t in definedApps where t.UniqueID == AppID select t).First();
		}

		public List<App> GetDefiniedApps()
		{
			return (from t in definedApps where t.IsShell == false select t).ToList();
		}

		private App GetAppByRule(AppRulePair appRulePair)
		{
			return GetAppById(appRulePair.UniqueID);
		}

		private void StartApp(int PID, App chosen_app,
			ulong processTime, ulong fullTime, AppRulePair ruleselected,
			int ParentID)
		{
			DateTime x = DateTime.Now;

			var n = new App(chosen_app);
			n.StartCounter++;

			definedApps.Replace(p => p.UniqueID == chosen_app.UniqueID, n);

			CurrentApps ca = new CurrentApps();

			ca.PID = PID;
			ca.UniqueId = chosen_app.UniqueID;
			ca.StartTime = processTime;
			ca.AllTime = fullTime;
			ca.RuleTriggered = ruleselected;
			ca.StartCount = n.StartCounter;
			ca.Merged = CheckMergedParent(ParentID);

			currentApps.Add(ca);

			AppendBinary(new BeginEventType(PID, ruleselected.UniqueID,
				ruleselected.RuleSetID, ParentID), x);

			// rejestrujemy nazwe głównego okna
			try
			{
				Process p = Process.GetProcessById(PID);

				AppendBinary(new NamechangeToken((uint)PID, p.MainWindowTitle), x, true);
			} catch (Exception)
			{
				AppendBinary(new NamechangeToken((uint)PID, ""), x, true);
			}
		}

		private bool CheckMergedParent(int parentID)
		{
			App? parent = GetAppByPID(parentID);

			if (parent.HasValue)
			{
				return parent.Value.MergeSpawned;
			} else
			{
				return false;
			}
		}

		private App? GetAppByPID(int parentID)
		{
			var app = (from ca in currentApps where ca.PID == parentID select ca).ToList();

			if (app.Count == 0)
				return null;
			else
				return app[0].App;
		}

		private void ProcessDestoryed(int pid)
		{
			lock (inOutLock)
			{
				// sprawdzamy czy podany proces istnieje w głównym oknie
				var idx = currentApps.FindIndex(p => p.PID == pid);

				if (idx >= 0)
				{
					// główne okno
					StopApp(pid, currentApps[idx]);
					currentApps.RemoveAt(idx);
				}
			}
		}

		private void StopApp(int pid, CurrentApps current)
		{
			ulong ct = GetTickCount64() - current.StartTime;
			DateTime x = DateTime.Now;

			var idx = definedApps.FindIndex(p => p.UniqueID == current.RuleTriggered.UniqueID);

			Debug.Assert(idx >= 0);

			App c = definedApps[idx];
			c.Time += ct;
			definedApps[idx] = c;

			// sprawdzamy czy nie dodajemy promocji
			AppendBinary(new EndEventType(pid, current.RuleTriggered.UniqueID), x, true);
		}

		public void FinishTracking()
		{
			tracker.Dispose();
			tracker = null;

			ProcessBeginWatcher.Stop();
			ProcessEndWatcher.Stop();

			ProcessBeginWatcher.Dispose();
			ProcessEndWatcher.Dispose();
		}

		private void FinishProcess()
		{
			foreach (var it in currentApps)
				StopApp(it.PID, it);
		}

		internal List<CurrentApps> GetRunningAps()
		{
			List<CurrentApps> ret = new List<CurrentApps>(currentApps.Count);

			for (var it = 0; it < currentApps.Count; ++it)
			{
				CurrentApps copy = currentApps[it];
				copy.App = (from da in definedApps
							where da.UniqueID == copy.UniqueId
							select da).First();
				ret.Add(copy);
			}

			return ret;
		}

		internal void GrabAll()
		{
			foreach (var it in Win32_Process.Fetch())
			{
				int parentId = 0;

				if (it.ParentProcessId.HasValue)
					parentId = (int)it.ParentProcessId.Value;

				if (it.ProcessIdWasQueried)
					NewProcessArrived((int)it.ProcessId.Value, parentId);
			}
		}

		internal void SaveDatabase()
		{
			lock (inOutLock)
			{
				try
				{
					definedApps = definedApps.OrderBy(o => o.UniqueID).ToList();

					XmlWriterSettings xws = new XmlWriterSettings();
					xws.CloseOutput = true;
					xws.Encoding = Encoding.UTF8;
					xws.Indent = true;
					xws.IndentChars = "\t";

					using (XmlWriter xw = XmlWriter.Create(ApplicationDefinitions, xws))
					{
						XmlSerializer xs = new XmlSerializer(definedApps.GetType());
						xs.Serialize(xw, definedApps);
					}
				}
				catch (Exception)
				{
				}
			}
		}
	}
}
