using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Management;
using System.Windows.Forms;
using System.Diagnostics;

namespace timetracker
{
 public class ApplicationDatabase
 {
  // definicje typów
  public struct DatabaseEntry
  {
   [XmlAttribute] public string internalId;
   [XmlAttribute] public string nameOfApplication;
   public DatabaseEntryRule[] rules;
  }

  public struct DatabaseEntryRule
  {
   [XmlAttribute] public DatabaseEntryRuleCompareTo what;
   [XmlAttribute] public Utils.MatchAlgorithm how;
   [XmlAttribute] public string str;
   [XmlAttribute] public bool required;
  }

  public enum DatabaseEntryRuleCompareTo
  {
   FileName,
   FilePath,
   FileVersionName,
   FileVersionDescription,
   FileVersionCompany,
   FileVersionProductVersion,
   FileVersionFileVersion,
  }

  public struct DatabaseTrack
  {
   [XmlAttribute] public string internalId;
   [XmlAttribute] public ulong countedTime;
  }

  public struct DatabaseCurrent
  {
   public string internalId;
   public int processId;
   public ulong startTime;
  }

  public struct DatabaseCurrentView
  {
   public string name;
   public int processid;
   public ulong currentTime;
   public ulong allTime;
  }

  [XmlRoot("db")]
  public struct CfgDatabase
  {
   public DatabaseEntry[] allentries;
  }

  [XmlRoot("db")]
  public struct CfgTrack
  {
   public DatabaseTrack[] allentries;
  }

  // pola
  MainWindow linkToForm;
  List<DatabaseEntry> dbAllEntries;
  List<DatabaseTrack> dbTrackEntries;
  List<DatabaseCurrent> dbCurrentEntries = new List<DatabaseCurrent>();
  ManagementEventWatcher mewCreatingProcess,
                         mewDeletingProcess;

  public ApplicationDatabase( MainWindow mw )
  {
   linkToForm = mw;
  }

  public void StartApp()
  {
   _LoadDatabaseData();
   _LoadTrackData();

   _RegisterAllCurrentProcess();

   _StartTracking();
  }

  public void CloseApp()
  {
   _CloseTracking();

   _UnregisterAllCurrentProcess();

   _SaveTrackData();
   _SaveDatabaseData();
  }

  private void _LoadDatabaseData()
  {
   string xml_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "application-database.xml");

   if (!File.Exists(xml_path))
   {
    dbAllEntries = new List<DatabaseEntry>();
    return;
   }

   CfgDatabase cfg = new CfgDatabase();

   try
   {
    XmlReaderSettings xrs = new XmlReaderSettings();
    using (XmlReader xr = XmlReader.Create(xml_path, xrs))
    {
     XmlSerializer xs = new XmlSerializer(cfg.GetType());

     cfg = (CfgDatabase)xs.Deserialize(xr);
    }

    if (cfg.allentries == null)
     throw new Exception();

    dbAllEntries = cfg.allentries.ToList();
   }
   catch (Exception)
   {
    dbAllEntries = new List<DatabaseEntry>();
   }
  }

  private void _LoadTrackData()
  {
   string xml_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "track-database.xml");

   if (!File.Exists(xml_path))
   {
    dbTrackEntries = new List<DatabaseTrack>();
    return;
   }

   CfgTrack cfg = new CfgTrack();

   try
   {
    XmlReaderSettings xrs = new XmlReaderSettings();
    using (XmlReader xr = XmlReader.Create(xml_path, xrs))
    {
     XmlSerializer xs = new XmlSerializer(cfg.GetType());

     cfg = (CfgTrack)xs.Deserialize(xr);
    }

    if (cfg.allentries == null)
     throw new Exception();

    dbTrackEntries = cfg.allentries.ToList();
   }
   catch (Exception)
   {
    dbTrackEntries = new List<DatabaseTrack>();
   }
  }

  private void _SaveDatabaseData()
  {
   string xml_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "application-database.xml");

   XmlWriterSettings xws = new XmlWriterSettings();
   xws.Indent = true;
   xws.IndentChars = " ";
   xws.Encoding = Encoding.UTF8;

   CfgDatabase db = new CfgDatabase();
   db.allentries = dbAllEntries.ToArray();

   using ( XmlWriter xw = XmlWriter.Create(xml_path, xws))
   {
    XmlSerializer xs = new XmlSerializer(db.GetType());
    xs.Serialize(xw, db);
   }
  }

  private void _SaveTrackData()
  {
   string xml_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "track-database.xml");

   XmlWriterSettings xws = new XmlWriterSettings();
   xws.Indent = true;
   xws.IndentChars = " ";
   xws.Encoding = Encoding.UTF8;

   CfgTrack db = new CfgTrack();
   db.allentries = dbTrackEntries.ToArray();

   using (XmlWriter xw = XmlWriter.Create(xml_path, xws))
   {
    XmlSerializer xs = new XmlSerializer(db.GetType());
    xs.Serialize(xw, db);
   }
  }

  private void _StartTracking()
  {
   string ns = @"\\.\root\CIMV2";
   string qc = @"SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process'";
   string qd = @"SELECT * FROM __InstanceDeletionEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process'";

   mewCreatingProcess = new ManagementEventWatcher(ns, qc);
   mewCreatingProcess.EventArrived += _NewProcessCreated;

   mewDeletingProcess = new ManagementEventWatcher(ns, qd);
   mewDeletingProcess.EventArrived += _NewProcessDestroyed;

   mewCreatingProcess.Start();
   mewDeletingProcess.Start();
  }

  private void _CloseTracking()
  {
   mewCreatingProcess.Stop();
   mewDeletingProcess.Stop();

   mewCreatingProcess.Dispose();
   mewDeletingProcess.Dispose();
  }

  private void _NewProcessDestroyed(object sender, EventArrivedEventArgs e)
  {
   ManagementBaseObject mbo = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;

   _processWasDestroyed((int)(UInt32)mbo.Properties["ProcessId"].Value);
  }

  private void _NewProcessCreated(object sender, EventArrivedEventArgs e)
  {
   ManagementBaseObject mbo = e.NewEvent.Properties["TargetInstance"].Value as ManagementBaseObject;

   _newProcessArrived((int)(UInt32)mbo.Properties["ProcessId"].Value);
  }

  private void _RegisterAllCurrentProcess()
  {
   string ns = @"root\CIMV2";
   string q = @"SELECT * FROM Win32_Process";

   using (ManagementObjectSearcher mos = new ManagementObjectSearcher(ns, q))
   {
    foreach (ManagementObject mo in mos.Get())
    {
     if (mo["ProcessId"] != null)
     {
      int id = (int)(UInt32)mo["ProcessId"];

      if (id != 0)
       _newProcessArrived(id);
     }
    }
   }
  }

  private void _UnregisterAllCurrentProcess()
  {
   while (dbCurrentEntries.Count > 0)
    _unregisterNewProcess(dbCurrentEntries[0].processId);
  }

  private void _newProcessArrived(int p)
  {
   this.linkToForm.Invoke((MethodInvoker)delegate
   {
    int process_id = p;
    string process_filename,
           process_path,
           process_fileversion_name,
           process_fileversion_company,
           process_fileversion_product_version,
           process_fileversion_file_version,
           process_fileversion_description;
    try
    {
     Process proc = Process.GetProcessById(process_id);
     process_filename = Path.GetFileName(proc.MainModule.FileName);
     process_path = proc.MainModule.FileName;
     process_fileversion_name = proc.MainModule.FileVersionInfo.ProductName;
     process_fileversion_company = proc.MainModule.FileVersionInfo.CompanyName;
     process_fileversion_product_version = proc.MainModule.FileVersionInfo.ProductVersion;
     process_fileversion_file_version = proc.MainModule.FileVersionInfo.FileVersion;
     process_fileversion_description = proc.MainModule.FileVersionInfo.FileDescription;
    } catch (Exception)
    {
     return;
    }

    _registerNewProcess(process_id, process_filename, process_path,
                        process_fileversion_name, process_fileversion_company,
                        process_fileversion_product_version,
                        process_fileversion_file_version, process_fileversion_description);
   });
  }

  private void _processWasDestroyed(int p)
  {
   this.linkToForm.Invoke((MethodInvoker)delegate
   {
    foreach (ApplicationDatabase.DatabaseCurrent it in dbCurrentEntries)
     if ( it.processId == p )
     {
      _unregisterNewProcess(p);
      break;
     }
   });
  }

  private void _registerNewProcess(int process_id, string process_filename,
                                   string process_path, string process_fileversion_name,
                                   string process_fileversion_company, string process_fileversion_product_version,
                                   string process_fileversion_file_version,
                                   string process_fileversion_description)
  {
   // sprawdzamy czy process nie został już zarejestrowany
   foreach (DatabaseCurrent it in dbCurrentEntries)
    if (process_id == it.processId)
     return;

   // teraz szukamy odpowiedniego wpisu w bazie danych
   DatabaseEntry? dbEntry_ = searchForDatabaseEntry(process_filename, process_path,
                                                    process_fileversion_name,
                                                    process_fileversion_company,
                                                    process_fileversion_product_version,
                                                    process_fileversion_file_version,
                                                    process_fileversion_description);

   if (!dbEntry_.HasValue)
    return;

   DatabaseCurrent db = new DatabaseCurrent();

   db.processId = process_id;
   db.startTime = WinAPI.GetTickCount64();
   db.internalId = dbEntry_.Value.internalId;

   dbCurrentEntries.Add(db);
  }

  private void _unregisterNewProcess(int p)
  {
   DatabaseCurrent? process_ = searchForCurrentProcessAndRemove(p);

   if (!process_.HasValue)
    return;

   DatabaseCurrent process = process_.Value;

   DatabaseTrack? dbTrack_ = searchForTrackEntry(process.internalId);

   if ( !dbTrack_.HasValue )
    createTrackEntry(process.internalId, WinAPI.GetTickCount64() - process.startTime);
   else
    updateTrackEntry(process.internalId, WinAPI.GetTickCount64() - process.startTime);
  }

  private DatabaseEntry? searchForDatabaseEntry(string filename, string path,
                                                string fv_name, string fv_company,
                                                string fv_product_version,
                                                string fv_file_version,
                                                string fv_file_desc)
  {
   foreach (DatabaseEntry it in dbAllEntries)
   {
    // testujemy zasady
    bool req_test = true;
    bool ret_test = false;
    int req_count = 0;
    int ret_count = 0;

    foreach (DatabaseEntryRule jt in it.rules)
    {
     switch ( jt.what )
     {
      case DatabaseEntryRuleCompareTo.FileName:
       {
        bool test = Utils.compareStrings(jt.str, filename, jt.how);

        if (jt.required)
        {
         req_test &= test;
         req_count++;
        }
        else
        {
         ret_test |= test;
         ret_count++;
        }
       }
       break;

      case DatabaseEntryRuleCompareTo.FilePath:
       {
        bool test = Utils.compareStrings(jt.str, path, jt.how);

        if (jt.required)
        {
         req_test &= test;
         req_count++;
        }
        else
        {
         ret_test |= test;
         ret_count++;
        }
       }
       break;

      case DatabaseEntryRuleCompareTo.FileVersionCompany:
       {
        bool test = Utils.compareStrings(jt.str, fv_company, jt.how);

        if (jt.required)
        {
         req_test &= test;
         req_count++;
        }
        else
        {
         ret_test |= test;
         ret_count++;
        }
       }
       break;

      case DatabaseEntryRuleCompareTo.FileVersionDescription:
       {
        bool test = Utils.compareStrings(jt.str, fv_file_desc, jt.how);

        if (jt.required)
        {
         req_test &= test;
         req_count++;
        }
        else
        {
         ret_test |= test;
         ret_count++;
        }
       }
       break;

      case DatabaseEntryRuleCompareTo.FileVersionFileVersion:
       {
        bool test = Utils.compareStrings(jt.str, fv_file_version, jt.how);

        if (jt.required)
        {
         req_test &= test;
         req_count++;
        }
        else
        {
         ret_test |= test;
         ret_count++;
        }
       }
       break;

      case DatabaseEntryRuleCompareTo.FileVersionName:
       {
        bool test = Utils.compareStrings(jt.str, fv_name, jt.how);

        if (jt.required)
        {
         req_test &= test;
         req_count++;
        }
        else
        {
         ret_test |= test;
         ret_count++;
        }
       }
       break;

      case DatabaseEntryRuleCompareTo.FileVersionProductVersion:
       {
        bool test = Utils.compareStrings(jt.str, fv_product_version, jt.how);

        if (jt.required)
        {
         req_test &= test;
         req_count++;
        }
        else
        {
         ret_test |= test;
         ret_count++;
        }
       }
       break;
     }

     if ( req_count > 0 && ret_count > 0)
     {
      if (req_test == true && ret_test == true)
       return it;
     } else if ( req_count > 0 && ret_count == 0 )
     {
      if (req_test == true)
       return it;
     } else if ( req_count == 0 && ret_count > 0)
     {
      if (ret_test == true)
       return it;
     }
    }
   }
   return null;
  }

  private DatabaseEntry? searchForDatabaseEntry(string p)
  {
   foreach (DatabaseEntry it in dbAllEntries)
    if (it.internalId == p)
     return it;

   return null;
  }

  private DatabaseTrack? searchForTrackEntry(string p)
  {
   foreach (DatabaseTrack it in dbTrackEntries)
    if (it.internalId == p)
     return it;

   return null;
  }

  private DatabaseCurrent? searchForCurrentProcessAndRemove(int p)
  {
   for (int it = 0; it < dbCurrentEntries.Count; it++)
    if ( dbCurrentEntries[it].processId == p )
    {
     DatabaseCurrent tmp = dbCurrentEntries[it];
     dbCurrentEntries.RemoveAt(it);
     return tmp;
    }

   return null;
  }

  private void createTrackEntry(string internalId, ulong time)
  {
   DatabaseTrack newEntry = new DatabaseTrack();
   newEntry.internalId = internalId;
   newEntry.countedTime = time;
   dbTrackEntries.Add(newEntry);
  }

  private void updateTrackEntry(string internalId, ulong time)
  {
   for (int it = 0; it < dbTrackEntries.Count; it++)
    if ( dbTrackEntries[it].internalId == internalId )
    {
     ulong alltime = dbTrackEntries[it].countedTime + time;
     dbTrackEntries.RemoveAt(it);

     createTrackEntry(internalId, alltime);
     break;
    }
  }

  public List<DatabaseCurrentView> PollActiveApps()
  {
   List<DatabaseCurrentView> ret = new List<DatabaseCurrentView>();

   foreach (DatabaseCurrent it in dbCurrentEntries)
   {
    DatabaseCurrentView dcv = new DatabaseCurrentView();
    dcv.currentTime = WinAPI.GetTickCount64() - it.startTime;
    dcv.processid = it.processId;

    DatabaseEntry? dbe_ = searchForDatabaseEntry(it.internalId);
    if (!dbe_.HasValue)
     continue;

    dcv.name = dbe_.Value.nameOfApplication;

    DatabaseTrack? dbt_ = searchForTrackEntry(it.internalId);
    if (dbt_.HasValue)
     dcv.allTime = dbt_.Value.countedTime;

    ret.Add(dcv);
   }

   return ret;
  }

  public List<DatabaseEntry> PollAllEntries()
  {
   return dbAllEntries;
  }

  public void SynchronizeEntries(List<DatabaseEntry> list)
  {
   dbAllEntries = list;

   _RegisterAllCurrentProcess();
  }
 }
}
