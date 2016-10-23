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
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace timetracker
{
	public class TrackSystem
	{
		const string ApplicationDefinitions = "appdefinition3.xml";
		const string TrackedTimesCatalogue = "tracks3";

		public static class Structs
		{
			public struct App
			{
				public string Name, UniqueID;
				public ulong Time, StartCounter;
				public bool IsShell;

				public AppRuleSet[] Rules;

				public bool AllowOnlyOne
				{
					get
					{
						if (_AllowOnlyOne.HasValue)
							return _AllowOnlyOne.Value;
						else
							return false;
					}

					set
					{
						_AllowOnlyOne = value;
					}
				}

				public bool AllowOnlyOneSpecified
				{
					get
					{
						return _AllowOnlyOne.HasValue;
					}
				}

				private bool? _AllowOnlyOne;

				public App(App copy)
				{
					Name = copy.Name;
					UniqueID = copy.UniqueID;
					Time = copy.Time;
					StartCounter = copy.StartCounter;
					IsShell = copy.IsShell;
					Rules = copy.Rules;
					_AllowOnlyOne = copy._AllowOnlyOne;
				}
			}

			public struct AppRuleSet
			{
				public RuleSet Kind;
				public string UniqueId;
				public RulePriority Priority;

				public AppRule[] Rules;
			}

			public enum RulePriority
			{
				None,
				Low,
				Medium,
				High,
			}

			public enum RuleSet
			{
				Any,
				All
			}

			public struct AppRule
			{
				public AppRuleMatchTo MatchTo;
				public string MatchString;
				public AppRuleAlgorithm MatchAlgorithm;

				public AppRule(AppRuleMatchTo mt, string str, AppRuleAlgorithm a)
				{
					MatchTo = mt;
					MatchString = str;
					MatchAlgorithm = a;
				}
			}

			public enum AppRuleMatchTo
			{
				FileName,
				FileNamePath,
				FilePath,
				FileVersionName,
				FileVersionDesc,
				FileVersionCompany,
				FileVersionProductVersion,
				FileVersionFileVersion,
				FileMD5,
			}

			public enum AppRuleAlgorithm
			{
				Exact,
				ExactInvariant,
				Near,
				NearInvariant,
				Regex,
				RegexInvariant,
				StartsWith,
				StartsWithInvariant,
				EndWith,
				EndWithInvariant,
			}

			public struct AppTrack
			{
				public ulong EventID;
				public DateTime BeginTime, EndTime;
				public string UniqueID;
				public ulong Time;
			}

			internal struct CurrentApps
			{
				public int PID;
				public string UniqueId;
				public AppRulePair RuleTriggered;
				public ulong StartTime;
				public ulong AllTime;
				public ulong StartCount;
				public App App;
			}

			internal struct AppRulePair
			{
				public string UniqueID;
				public string RuleSetID;
				public RulePriority Priority;

				public AppRulePair(string id, string gid, RulePriority p)
				{
					UniqueID = id;
					RuleSetID = gid;
					Priority = p;
				}
			}

			internal struct ExeData
			{
				public int PID;
				public string Name;
				public string Path;
				public FileVersionInfo FileVersion;
			}

			internal struct ExeDataContainer
			{
				public ExeDataContainerReason Reason;
				private ExeData? data;

				public ExeDataContainer(ExeData ed)
				{
					Reason = ExeDataContainerReason.Valid;
					data = ed;
				}

				public ExeDataContainer(ExeDataContainerReason reason)
				{
					Reason = reason;
					data = null;
				}

				public ExeData Value
				{
					get
					{
						if (data.HasValue)
							return data.Value;
						else
							throw new InvalidOperationException("We don't have value!");
					}

					set
					{
						Reason = ExeDataContainerReason.Valid;
						data = value;
					}
				}
			}

			internal enum ExeDataContainerReason
			{
				Valid,
				IncompleteInformation,
				ExceptionOccured,
			}

			internal struct WaitStruct
			{
				public int PID;
				public ulong StartTime;
				public int Count;
			}
		}

		internal static class Utils
		{
			public static bool IsStringMatch(string pattern, string subject,
				Structs.AppRuleAlgorithm algo)
			{
				switch (algo)
				{
					case Structs.AppRuleAlgorithm.Exact:
						return pattern == subject;

					case Structs.AppRuleAlgorithm.ExactInvariant:
						return pattern.Equals(subject, StringComparison.CurrentCultureIgnoreCase);

					case Structs.AppRuleAlgorithm.Near:
						return new Regex(GenerateNearCharacters(pattern)).IsMatch(subject);

					case Structs.AppRuleAlgorithm.NearInvariant:
						return new Regex(GenerateNearCharacters(pattern), RegexOptions.IgnoreCase).IsMatch(subject);

					case Structs.AppRuleAlgorithm.Regex:
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

					case Structs.AppRuleAlgorithm.RegexInvariant:
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

					case Structs.AppRuleAlgorithm.StartsWith:
						return subject.StartsWith(pattern);

					case Structs.AppRuleAlgorithm.StartsWithInvariant:
						return subject.StartsWith(pattern, StringComparison.CurrentCultureIgnoreCase);

					case Structs.AppRuleAlgorithm.EndWith:
						return subject.EndsWith(pattern);

					case Structs.AppRuleAlgorithm.EndWithInvariant:
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

			internal static string ExtractString(Structs.AppRuleMatchTo matchTo, int pID,
				string name, string path, FileVersionInfo fileVersion,
				ref Dictionary<string, string> md5Cache)
			{
				switch (matchTo)
				{
					case Structs.AppRuleMatchTo.FileName:
						return name;

					case Structs.AppRuleMatchTo.FileNamePath:
						return path;

					case Structs.AppRuleMatchTo.FilePath:
						return Path.GetDirectoryName(path);

					case Structs.AppRuleMatchTo.FileVersionCompany:
						return fileVersion.CompanyName;

					case Structs.AppRuleMatchTo.FileVersionDesc:
						return fileVersion.FileDescription;

					case Structs.AppRuleMatchTo.FileVersionFileVersion:
						return fileVersion.FileVersion;

					case Structs.AppRuleMatchTo.FileVersionName:
						return fileVersion.ProductName;

					case Structs.AppRuleMatchTo.FileVersionProductVersion:
						return fileVersion.ProductVersion;

					case Structs.AppRuleMatchTo.FileMD5:
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

		public static class WinAPI
		{
			public const int WM_QUERYENDSESSION = 0x0011;
			public const int WM_ENDSESSION = 0x0016;

			[System.Runtime.InteropServices.DllImport("kernel32.dll")]
			public static extern ulong GetTickCount64();

			public enum NetConnectionStatus : UInt16
			{
				Disconnected = (0),
				Connecting = (1),
				Connected = (2),
				Disconnecting = (3),
				HardwareNotPresent = (4),
				HardwareDisabled = (5),
				HardwareMalfunction = (6),
				MediaDisconnected = (7),
				Authenticating = (8),
				AuthenticationSucceeded = (9),
				AuthenticationFailed = (10),
				InvalidAddress = (11),
				CredentialsRequired = (12),
				Other,
			}
		}

		internal class Tracker : IDisposable
		{
			internal delegate void ProcesSpawnedType(int pid);
			internal delegate void ProcesEndedType(int pid);
			internal delegate void InternetChangeStateType(Guid guid, string Name, WinAPI.NetConnectionStatus state);
			internal delegate void OSChangeType(ulong free, ulong all, ulong virtualFree, ulong virtualAll);

			internal ProcesSpawnedType OnCreate;
			internal ProcesEndedType OnDelete;
			internal InternetChangeStateType OnInternetEvent;
			internal OSChangeType OnOsEvent;

			ManagementEventWatcher eventCreated;
			ManagementEventWatcher eventDestroyed;
			ManagementEventWatcher eventInternet;
			ManagementEventWatcher eventOS;

			public void Start()
			{
				GrabAll();

				InitializeTracking();
			}

			private void InitializeTracking()
			{
				const string NameSpace = @"\\.\root\CIMV2";
				const string CreateSql = @"SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process'";
				const string DeleteSql = @"SELECT * FROM __InstanceDeletionEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process'";
				const string NetChange = @"SELECT * FROM __InstanceModificationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_NetworkAdapter' AND TargetInstance.PhysicalAdapter = True";
				const string OperatingSystem = @"SELECT * FROM __InstanceModificationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_OperatingSystem'";

				eventCreated = new ManagementEventWatcher(NameSpace, CreateSql);
				eventCreated.EventArrived += OnCreateProcessEvent;

				eventDestroyed = new ManagementEventWatcher(NameSpace, DeleteSql);
				eventDestroyed.EventArrived += OnDeleteProcessEvent;

				eventInternet = new ManagementEventWatcher(NameSpace, NetChange);
				eventInternet.EventArrived += OnModificationInternetEvent;

				eventOS = new ManagementEventWatcher(NameSpace, OperatingSystem);
				eventOS.EventArrived += OnOSEvent;

				PullAllInternet();

				eventCreated.Start();
				eventDestroyed.Start();
				eventInternet.Start();
				eventOS.Start();
			}

			private void OnCreateProcessEvent(object sender, EventArrivedEventArgs e)
			{
				ManagementBaseObject mbo = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;

				int pid = (int)(UInt32)mbo.Properties["ProcessId"].Value;

				OnCreate(pid);
			}

			private void OnDeleteProcessEvent(object sender, EventArrivedEventArgs e)
			{
				ManagementBaseObject mbo = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;

				int pid = (int)(UInt32)mbo.Properties["ProcessId"].Value;

				OnDelete(pid);
			}

			private void OnModificationInternetEvent(object sender, EventArrivedEventArgs e)
			{
				ManagementBaseObject mbo = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;

				string name = (string)mbo.Properties["Name"].Value;
				int status = (int)(UInt16)mbo.Properties["NetConnectionStatus"].Value;
				Guid g = Guid.Parse((string)mbo.Properties["GUID"].Value);
				WinAPI.NetConnectionStatus n = status.ToNet();

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

			public void GrabAll()
			{
				const string NameSpace = @"root\CIMV2";
				const string Query = @"SELECT * FROM Win32_Process";

				using (ManagementObjectSearcher mos = new ManagementObjectSearcher(NameSpace, Query))
				{
					foreach (ManagementObject mo in mos.Get())
					{
						if (mo["ProcessId"] != null)
						{
							int id = (int)(UInt32)mo["ProcessId"];

							if (id != 0)
								OnCreate(id);
						}
					}
				}
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
						int status = (int)(UInt16)mbo.Properties["NetConnectionStatus"].Value;
						Guid g = Guid.Parse((string)mbo.Properties["GUID"].Value);
						WinAPI.NetConnectionStatus n = status.ToNet();

						OnInternetEvent(g, name, n);
					}
				}
			}

			public void Finish()
			{
				eventCreated.Stop();
				eventDestroyed.Stop();
				eventInternet.Stop();
				eventOS.Stop();

				eventCreated.Dispose();
				eventDestroyed.Dispose();
				eventInternet.Dispose();
				eventOS.Dispose();

				eventInternet = eventCreated = eventDestroyed = null;
			}

			public void Dispose()
			{
				Finish();
			}
		}

		internal static TrackSystem TrackingSystemState = null;

		Tracker tracker = null;
		List<Structs.CurrentApps> currentApps { get; set; } = new List<Structs.CurrentApps>();
		List<Structs.App> definedApps = new List<Structs.App>();
		List<Structs.WaitStruct> waitedApps = new List<Structs.WaitStruct>();
		XmlWriter xmlTracker = null;
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
			lock (inOutLock)
			{
				xmlTracker.WriteNode("ping", new Dictionary<string, string>
				{
					{ "pong", DateTime.Now.Ticks.ToString() },
				});
			}

			tracker.PullAllInternet();
		}

		private bool PidNotRunning(int PID)
		{
			return GetProcessData(PID).Reason != Structs.ExeDataContainerReason.Valid;
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
					NewProcessArrived(it.PID, it.Count, it.StartTime);

				waited_timer_running = false;
			}
		}

		private void LoadState()
		{
			try
			{
				XmlReader xr = XmlReader.Create(ApplicationDefinitions);

				XmlSerializer xs = new XmlSerializer(definedApps.GetType());
				definedApps = (List<Structs.App>)xs.Deserialize(xr);
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

			string file = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss.fff") + ".xml";

			xmlTracker = XmlWriter.Create(Path.Combine(TrackedTimesCatalogue, file), xws);
			xmlTracker.WriteStartDocument(true);
			xmlTracker.WriteStartElement("root");
			xmlTracker.WriteAttributeString("start-date", DateTime.Now.ToSensibleFormat());
		}

		private void SaveState()
		{
			xmlTracker.WriteEndElement();
			xmlTracker.WriteEndDocument();
			xmlTracker.Close();

			SaveDatabase();
		}

		public void BeginTracking()
		{
			tracker = new Tracker();

			tracker.OnCreate = NewProcessArrived;
			tracker.OnDelete = ProcessDestoryed;
			tracker.OnInternetEvent = InternetEvent;
			tracker.OnOsEvent = MemoryEvent;

			tracker.Start();
		}

		private string lastround = "";

		private void MemoryEvent(ulong free, ulong all, ulong vfree, ulong vall)
		{
			lock (inOutLock)
			{
				DateTime x = DateTime.Now;

				// physicial
				double ptotal = all;
				double pfree = free;
				string pround = (Math.Round(((ptotal - pfree) / ptotal * 100), 2)).ToString();

				// virtual
				double vtotal = vall;
				double vfrre = vfree;
				string vround = (Math.Round(((vtotal - vfrre) / vtotal * 100), 2)).ToString();

				// all
				double atotal = ptotal + vtotal;
				double afree = pfree + vfree;
				string round = (Math.Round(((atotal - afree) / atotal * 100), 2)).ToString();

				ulong pvtotal = all + vall;
				ulong pvfree = free + vfree;

				if (lastround != round)
				{
					xmlTracker.WriteNode("memory-info", new Dictionary<string, string>
					{
						{ "p-free", free.ToString() },
						{ "p-total", all.ToString() },
						{ "p-proc", pround.ToString() },

						{ "v-free", vfree.ToString() },
						{ "v-total", vall.ToString() },
						{ "v-proc", vround.ToString() },

						{ "a-free", pvfree.ToString() },
						{ "a-total", pvtotal.ToString() },
						{ "a-proc", round.ToString() },

						{ "time", x.ToSensibleFormat() },
						{ "precise-time", x.Ticks.ToString() },
					}, false);

					lastround = round;
				}
			}
		}

		private void InternetEvent(Guid g, string Name, WinAPI.NetConnectionStatus state)
		{
			lock (inOutLock)
			{
				DateTime x = DateTime.Now;

				xmlTracker.WriteNode("event-internet-change", new Dictionary<string, string>
				{
					{ "name", Name },
					{ "device", g.ToString() },
					{ "status", state.ToString() },
					{ "time", x.ToSensibleFormat() },
					{ "precise-time", x.Ticks.ToString() },
				});
			}
		}

		private void NewProcessArrived(int pid)
		{
			NewProcessArrived(pid, 0, WinAPI.GetTickCount64());
		}

		private void NewProcessArrived(int pid, int retire, ulong processTime)
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
					case Structs.ExeDataContainerReason.IncompleteInformation:
						AddToWaitQueue(pid, processTime, retire);
						return;

					case Structs.ExeDataContainerReason.ExceptionOccured:
						return;
				}

				var ipd = pd.Value;

				ScanForApplication(ipd.PID, ipd.Name, ipd.Path, ipd.FileVersion,
					processTime);
			}
		}

		private Structs.ExeDataContainer GetProcessData(int pid)
		{
			try
			{
				Structs.ExeData ed;

				var pinfo = Process.GetProcessById(pid);

				ed.PID = pid;
				ed.Path = pinfo.MainModule.FileName;
				ed.Name = Path.GetFileName(ed.Path);
				ed.FileVersion = FileVersionInfo.GetVersionInfo(ed.Path);

				return new Structs.ExeDataContainer(ed);
			}
			catch (Win32Exception w32) when (w32.NativeErrorCode == 299)
			{
				return new Structs.ExeDataContainer(Structs.ExeDataContainerReason.IncompleteInformation);
			}
			catch (Exception)
			{
				return new Structs.ExeDataContainer(Structs.ExeDataContainerReason.ExceptionOccured);
			}
		}

		private void AddToWaitQueue(int pid, ulong processTime, int count)
		{
			if (count > 5)
				return;

			lock (inOutLock)
			{
				Structs.WaitStruct ws = new Structs.WaitStruct();

				ws.Count = count;
				ws.PID = pid;
				ws.StartTime = processTime;

				waitedApps.Add(ws);
			}
		}

		private void ScanForApplication(int PID, string name, string path,
			FileVersionInfo fileVersion, ulong processTime)
		{
			lock (inOutLock)
			{
				List<Structs.AppRulePair> qualified = new List<Structs.AppRulePair>();
				Dictionary<string, string> md5Cache = new Dictionary<string, string>();
				Structs.RulePriority definedPriority = Structs.RulePriority.None;

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
								if (jt.Kind == Structs.RuleSet.All)
									break;
							}
						}

						if (jt.Kind == Structs.RuleSet.All &&
							rules_match == jt.Rules.Length)
						{
							qualified.Add(new Structs.AppRulePair(it.UniqueID,
								jt.UniqueId, jt.Priority));

							if (definedPriority > jt.Priority)
								definedPriority = jt.Priority;
						}
						else if (jt.Kind == Structs.RuleSet.Any && rules_match > 0)
						{
							qualified.Add(new Structs.AppRulePair(it.UniqueID,
								jt.UniqueId, jt.Priority));

							if (definedPriority > jt.Priority)
								definedPriority = jt.Priority;
						}
					}
				}

				if (qualified.Count == 0)
					return;

				qualified = qualified.OrderByDescending(x => x.Priority).ToList();
				Structs.App chosen_app = GetAppByRule(qualified[0]);

				StartApp(PID, chosen_app, processTime, chosen_app.Time, qualified[0]);
			}
		}

		public Structs.App AddNewDefinition(string applicationName,
			string applicationUniqueID, Structs.AppRuleSet[] applicationRules,
			bool allowOnlyOne)
		{
			string application_unique_id = applicationUniqueID;
			int counter = 2;

			while (definedApps.Contains(c => c.UniqueID == application_unique_id))
				application_unique_id = string.Format("{0}_{1}", applicationUniqueID, counter++);

			applicationUniqueID = application_unique_id;

			Structs.App app = new Structs.App();

			app.Name = applicationName;
			app.UniqueID = applicationUniqueID;
			app.Rules = applicationRules;
			app.IsShell = false;

			app.StartCounter = 0;
			app.Time = 0;

			app.AllowOnlyOne = allowOnlyOne;

			definedApps.Add(app);

			lock (inOutLock)
			{
				DateTime x = DateTime.Now;

				xmlTracker.WriteNode("event-definition-added", new Dictionary<string, string>
				{
					{ "app", applicationUniqueID },
					{ "time", x.ToSensibleFormat() },
					{ "precise-time", x.Ticks.ToString() },
				});
			}

			return app;
		}

		public void RemoveApp(string text)
		{
			foreach (var it in definedApps)
			{
				if (it.UniqueID == text)
				{
					Structs.App c = it;
					c.IsShell = true;
					c.Rules = null;

					lock (inOutLock)
					{
						DateTime x = DateTime.Now;

						xmlTracker.WriteNode("event-definition-removed", new Dictionary<string, string>
						{
							{ "app", c.UniqueID },
							{ "time", x.ToSensibleFormat() },
							{ "precise-time", x.Ticks.ToString() },
						});
					}

					definedApps.Remove(it);
					definedApps.Add(c);
					return;
				}
			}
		}

		public void UpdateApp(string uniqueID, Structs.App idata)
		{
			definedApps.Replace(p => p.UniqueID == uniqueID, idata);
		}

		public Structs.App GetAppById(string AppID)
		{
			return (from t in definedApps where t.UniqueID == AppID select t).First();
		}

		public List<Structs.App> GetDefiniedApps()
		{
			return (from t in definedApps where t.IsShell == false select t).ToList();
		}

		private Structs.App GetAppByRule(Structs.AppRulePair appRulePair)
		{
			return GetAppById(appRulePair.UniqueID);
		}

		private void StartApp(int PID, Structs.App chosen_app,
			ulong processTime, ulong fullTime, Structs.AppRulePair ruleselected)
		{
			DateTime x = DateTime.Now;

			var n = new Structs.App(chosen_app);
			n.StartCounter++;

			definedApps.Replace(p => p.UniqueID == chosen_app.UniqueID, n);

			Structs.CurrentApps ca = new Structs.CurrentApps();

			ca.PID = PID;
			ca.UniqueId = chosen_app.UniqueID;
			ca.StartTime = processTime;
			ca.AllTime = fullTime;
			ca.RuleTriggered = ruleselected;
			ca.StartCount = n.StartCounter;

			currentApps.Add(ca);

			lock (inOutLock)
			{
				xmlTracker.WriteNode("begin", new Dictionary<string, string>
				{
					{ "app", ruleselected.UniqueID },
					{ "pid", PID.ToString() },
					{ "time", x.ToSensibleFormat() },
					{ "precise-time", x.Ticks.ToString() },
					{ "rule-set", ruleselected.RuleSetID },
				});
			}
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

		private void StopApp(int pid, Structs.CurrentApps current)
		{
			ulong ct = WinAPI.GetTickCount64() - current.StartTime;
			DateTime x = DateTime.Now;

			var idx = definedApps.FindIndex(p => p.UniqueID == current.RuleTriggered.UniqueID);

			Debug.Assert(idx >= 0);

			Structs.App c = definedApps[idx];
			c.Time += ct;
			definedApps[idx] = c;

			// sprawdzamy czy nie dodajemy promocji

			lock (inOutLock)
			{
				xmlTracker.WriteNode("end", new Dictionary<string, string>
				{
					{ "app", current.RuleTriggered.UniqueID },
					{ "pid", pid.ToString() },
					{ "time", x.ToSensibleFormat() },
					{ "elapsed", ct.ToString() },
					{ "precise-time", x.Ticks.ToString() }
				});
			}
		}

		public void FinishTracking()
		{
			tracker.Dispose();
			tracker = null;
		}

		private void FinishProcess()
		{
			foreach (var it in currentApps)
				StopApp(it.PID, it);
		}

		internal List<Structs.CurrentApps> GetRunningAps()
		{
			List<Structs.CurrentApps> ret = new List<Structs.CurrentApps>(currentApps.Count);

			for (var it = 0; it < currentApps.Count; ++it)
			{
				Structs.CurrentApps copy = currentApps[it];
				copy.App = (from da in definedApps
							where da.UniqueID == copy.UniqueId
							select da).First();
				ret.Add(copy);
			}

			return ret;
		}

		internal void GrabAll()
		{
			tracker.GrabAll();
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
