using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace timetracker
{
 public partial class MainWindow
 {
  List<AppDB_Entry> dbBaseApplications;
  List<AppTrack_Entry> dbTrackApplication;
  List<AppTrack_Current> dbTrackCurrent = new List<AppTrack_Current>();

  private void registerTrackApp(uint procid, string name, string commandline)
  {
   this.Invoke((MethodInvoker)delegate
   {
    // szukamy czy możemy taką aplikacje sprawdzić
    AppDB_Entry? isValidApp = checkIfAppExistInDB(name, commandline);

    if (!isValidApp.HasValue)
     return;

    // sprawdzamy czy aplikacja była już śledzona
    AppTrack_Entry? wasAppTracked = checkIfAppWasTracked(isValidApp.Value.internalNameOfApp);

    // sprawdzamy też czy nie mamy tego procesu już w śledzonych rzeczach
    foreach (AppTrack_Current it in dbTrackCurrent)
     if (it.id == procid)
      return; // nie dodajemy

    ulong oldtime = 0;
    if (wasAppTracked.HasValue)
     oldtime = wasAppTracked.Value.allTime;

    AppTrack_Current atc = new AppTrack_Current();
    atc.allTime = oldtime;
    atc.id = procid;
    atc.internalAppName = isValidApp.Value.internalNameOfApp;
    atc.startTime = WinAPI.GetTickCount64();

    dbTrackCurrent.Add(atc);

    ListViewItem lvi = new ListViewItem(procid.ToString());
    lvi.SubItems.Add(isValidApp.Value.nameOfApp);
    lvi.SubItems.Add("0s");
    lvi.SubItems.Add(Utils.calculateTime(oldtime));

    lvTrackApp.Items.Add(lvi);
   });
  }

  private AppTrack_Entry? checkIfAppWasTracked(string internalName)
  {
   foreach ( AppTrack_Entry it in dbTrackApplication )
   {
    if (it.internalName == internalName)
     return it;
   }

   return null;
  }

  private AppDB_Entry? checkIfAppExistInDB(string name, string commandline)
  {
   foreach (AppDB_Entry it in dbBaseApplications)
   {
    bool isValid = false;

    foreach ( AppDB_Entry_Rules jt in it.rules )
    {
     switch ( jt.matchToWhat )
     {
      case AppDB_Entry_Properties.FileName:
       isValid |= Utils.compareStrings(name, jt.matchString, jt.matchAlgorithm);
       break;

      case AppDB_Entry_Properties.FilePath:
       isValid |= Utils.compareStrings(commandline, jt.matchString, jt.matchAlgorithm);
       break;
     }

     if (isValid)
      return it;
    }
   }

   return null;
  }

  private void unregisterTrackApp(uint procid)
  {
   this.Invoke((MethodInvoker)delegate
   {
    ulong ctime = WinAPI.GetTickCount64();

    foreach (AppTrack_Current it in dbTrackCurrent)
     if ( it.id == procid )
     {
      // ładujemy czy kiedyś już śledziliśmy tą aplikacje
      AppTrack_Entry? ate = checkIfAppWasTracked(it.internalAppName);

      if ( !ate.HasValue)
      {
       // dodajemy nową rzecz
       AppTrack_Entry nate = new AppTrack_Entry();
       nate.internalName = it.internalAppName;
       nate.allTime = ctime - it.startTime;
       dbTrackApplication.Add(nate);
      } else
      {
       AppTrack_Entry oate = ate.Value;
       oate.allTime += ctime - it.startTime;
       updateAppTrack(oate);
      }
      dbTrackCurrent.Remove(it);
      break;
     }
   });
  }

  private void updateAppTrack(AppTrack_Entry oate)
  {
   foreach (AppTrack_Entry it in dbTrackApplication)
    if ( it.internalName == oate.internalName )
    {
     dbTrackApplication.Remove(it);
     break;
    }

   dbTrackApplication.Add(oate);
  }

  private void refreshTrackApplication()
  {
   this.Invoke((MethodInvoker)delegate
   {
    lvTrackApp.SuspendLayout();

    lvTrackApp.Items.Clear();
    ulong ctime = WinAPI.GetTickCount64();

    foreach (AppTrack_Current atc in dbTrackCurrent)
    {
     ListViewItem lvi = new ListViewItem(atc.id.ToString());

     AppDB_Entry? dbEntry = checkIfAppExistInDBInternal(atc.internalAppName);

     if (!dbEntry.HasValue)
      continue;

     lvi.SubItems.Add(dbEntry.Value.nameOfApp);
     lvi.SubItems.Add(Utils.calculateTime(ctime - atc.startTime));
     lvi.SubItems.Add(Utils.calculateTime(atc.allTime));

     lvTrackApp.Items.Add(lvi);
    }

    lvTrackApp.ResumeLayout(true);
   });
  }

  private AppDB_Entry? checkIfAppExistInDBInternal(string p)
  {
   foreach (AppDB_Entry it in dbBaseApplications)
    if (it.internalNameOfApp == p)
     return it;

   return null;
  }

  private void FinishAllTrackedProcess()
  {
   this.Invoke((MethodInvoker)delegate
   {
    while (dbTrackCurrent.Count != 0)
     unregisterTrackApp(dbTrackCurrent[0].id);
   });
  }

  private void removeZombieProcess()
  {
   for (int it = 0; it < dbTrackCurrent.Count; ++it )
   {
    AppTrack_Current cap = dbTrackCurrent[it];

    try
    {
     Process np = Process.GetProcessById((int)cap.id);

     if (!compareIfProcessAreCool(np.ProcessName + ".exe", "", cap.internalAppName))
      throw new Exception(); // sorry :(
    }
    catch (Exception)
    {
     dbTrackCurrent.RemoveAt(it);
     it--;
    }
   }
  }

  private bool compareIfProcessAreCool(string name, string commandline, string internalName)
  {
   AppDB_Entry? ate = checkIfAppExistInDBInternal(internalName);

   if (!ate.HasValue)
    return false;

   AppDB_Entry ae = ate.Value;

   bool isValid = false;

   foreach (AppDB_Entry_Rules jt in ae.rules)
   {
    switch (jt.matchToWhat)
    {
     case AppDB_Entry_Properties.FileName:
      isValid |= Utils.compareStrings(name, jt.matchString, jt.matchAlgorithm);
      break;

     case AppDB_Entry_Properties.FilePath:
      isValid |= Utils.compareStrings(commandline, jt.matchString, jt.matchAlgorithm);
      break;
    }

    if (isValid)
     return true;
   }

   return false;
  }

  private void LoadTrackData()
  {
   if (!File.Exists("./db-track.xml"))
   {
    dbTrackApplication = new List<AppTrack_Entry>();
    return;
   }

   AppTrack_Configuration_File adb_cf = new AppTrack_Configuration_File();

   XmlReaderSettings xrs = new XmlReaderSettings();
   using (XmlReader xr = XmlReader.Create("./db-track.xml", xrs))
   {
    XmlSerializer xs = new XmlSerializer(adb_cf.GetType());
    adb_cf = (AppTrack_Configuration_File)xs.Deserialize(xr);
   }

   if (adb_cf.appDBConfigurationFile != null)
    dbTrackApplication = adb_cf.appDBConfigurationFile.ToList();
   else
    dbTrackApplication = new List<AppTrack_Entry>();
  }

  private void SaveTrackData()
  {
   AppTrack_Configuration_File adb_cf = new AppTrack_Configuration_File();

   if (dbTrackApplication.Count != 0)
    adb_cf.appDBConfigurationFile = dbTrackApplication.ToArray();

   XmlWriterSettings xws = new XmlWriterSettings();
   xws.Indent = true;
   xws.IndentChars = " ";
   xws.Encoding = Encoding.UTF8;

   using (XmlWriter xw = XmlWriter.Create("./db-track.xml", xws))
   {
    XmlSerializer xs = new XmlSerializer(adb_cf.GetType());
    xs.Serialize(xw, adb_cf);
   }
  }

  private void LoadBaseData()
  {
   if (!File.Exists("./db-data.xml"))
   {
    dbBaseApplications = new List<AppDB_Entry>();
    return;
   }

   AppDB_Configuration_File adb_cf = new AppDB_Configuration_File();

   XmlReaderSettings xrs = new XmlReaderSettings();
   using (XmlReader xr = XmlReader.Create("./db-data.xml", xrs))
   {
    XmlSerializer xs = new XmlSerializer(adb_cf.GetType());
    adb_cf = (AppDB_Configuration_File)xs.Deserialize(xr);
   }

   if (adb_cf.appDBConfigurationFile != null)
    dbBaseApplications = adb_cf.appDBConfigurationFile.ToList();
   else
    dbBaseApplications = new List<AppDB_Entry>();
  }

  private void SaveBaseData()
  {
   AppDB_Configuration_File adb_cf = new AppDB_Configuration_File();

   if ( dbBaseApplications.Count != 0 )
    adb_cf.appDBConfigurationFile = dbBaseApplications.OrderBy(o => o.nameOfApp).ToArray();

   XmlWriterSettings xws = new XmlWriterSettings();
   xws.Indent = true;
   xws.IndentChars = " ";
   xws.Encoding = Encoding.UTF8;

   using (XmlWriter xw = XmlWriter.Create("./db-data.xml", xws))
   {
    XmlSerializer xs = new XmlSerializer(adb_cf.GetType());
    xs.Serialize(xw, adb_cf);
   }
  }
 }
}
