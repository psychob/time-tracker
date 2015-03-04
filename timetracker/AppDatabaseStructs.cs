using System.Xml.Serialization;

namespace timetracker
{
 public struct AppDB_Entry
 {
  [XmlAttribute]
  public string nameOfApp;
  [XmlAttribute]
  public string internalNameOfApp;
  public AppDB_Entry_Rules[] rules;
 }

 public struct AppDB_Entry_Rules
 {
  [XmlAttribute]
  public Utils.MatchAlgorithm matchAlgorithm;
  [XmlAttribute]
  public AppDB_Entry_Properties matchToWhat;
  [XmlAttribute]
  public string matchString;
 }

 public enum AppDB_Entry_Properties
 {
  FileName,
  FilePath,
 }

 public struct AppDB_Configuration_File
 {
  public AppDB_Entry[] appDBConfigurationFile;
 }

 public struct AppTrack_Entry
 {
  [XmlAttribute]
  public string internalName;
  [XmlAttribute]
  public ulong allTime;
 }

 public struct AppTrack_Configuration_File
 {
  public AppTrack_Entry[] appDBConfigurationFile;
 }

 public struct AppTrack_Current
 {
  public string internalAppName;
  public ulong startTime;
  public ulong allTime;
  public uint id;
 }
}
