using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace timetracker
{
 public class TrackedLegacy
 {
  public struct DatabaseEntry
  {
   [XmlAttribute]
   public string internalId;
   [XmlAttribute]
   public string nameOfApplication;
   public DatabaseEntryRule[] rules;
  }

  public struct DatabaseEntryRule
  {
   [XmlAttribute]
   public DatabaseEntryRuleCompareTo what;
   [XmlAttribute]
   public Utils.MatchAlgorithm how;
   [XmlAttribute]
   public string str;
   [XmlAttribute]
   public bool required;
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
   [XmlAttribute]
   public string internalId;
   [XmlAttribute]
   public ulong countedTime;
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

  public static void ImportData(List<application_definition> app_defs,
                                List<application_tracked> app_tracks)
  {
   string app_data_old = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "application-database.xml");
   string app_track_old = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "track-database.xml");

   List<DatabaseEntry> dbAllEntries = new List<DatabaseEntry>();
   List<DatabaseTrack> dbTrackEntries = new List<DatabaseTrack>();

   {
    CfgDatabase cfg = new CfgDatabase();

    try
    {
     XmlReaderSettings xrs = new XmlReaderSettings();
     using (XmlReader xr = XmlReader.Create(app_data_old, xrs))
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

   {
    CfgTrack cfg = new CfgTrack();

    try
    {
     XmlReaderSettings xrs = new XmlReaderSettings();
     using (XmlReader xr = XmlReader.Create(app_track_old, xrs))
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

   Dictionary<string, Guid> internal_map = new Dictionary<string, Guid>();

   foreach ( var it in dbAllEntries )
   {
    application_definition appdef = new application_definition();
    appdef.guid = Guid.NewGuid();
    appdef.name = it.nameOfApplication;

    internal_map[it.internalId] = appdef.guid;

    application_ruleset app_ruleset = new application_ruleset();
    app_ruleset.rules_name = "Root";
    app_ruleset.kind = application_ruleset_kind.any;

    appdef.ruleset = new application_ruleset[] { app_ruleset };

    List<application_rule> rules_imported = new List<application_rule>();

    foreach ( var rt in it.rules )
    {
     application_rule ar = new application_rule();

     ar.algorithm = rt.how;
     ar.math_what = rt.str;
     switch ( rt.what)
     {
      case DatabaseEntryRuleCompareTo.FileName:
       ar.match_to = application_rule_match_to.file_name;
       break;

      case DatabaseEntryRuleCompareTo.FilePath:
       ar.match_to = application_rule_match_to.file_path;
       break;

      case DatabaseEntryRuleCompareTo.FileVersionCompany:
       ar.match_to = application_rule_match_to.file_version_company;
       break;

      case DatabaseEntryRuleCompareTo.FileVersionDescription:
       ar.match_to = application_rule_match_to.file_version_desc;
       break;

      case DatabaseEntryRuleCompareTo.FileVersionFileVersion:
       ar.match_to = application_rule_match_to.file_version_file_version;
       break;

      case DatabaseEntryRuleCompareTo.FileVersionName:
       ar.match_to = application_rule_match_to.file_version_name;
       break;

      case DatabaseEntryRuleCompareTo.FileVersionProductVersion:
       ar.match_to = application_rule_match_to.file_version_product_version;
       break;
     }

     rules_imported.Add(ar);
    }

    appdef.ruleset[0].rules = rules_imported.ToArray();

    app_defs.Add(appdef);
   }

   foreach ( var it in dbTrackEntries )
   {
    application_tracked at = new application_tracked();

    if ( internal_map.ContainsKey(it.internalId))
    {
     at.guid = internal_map[it.internalId];
     at.time = it.countedTime;

     app_tracks.Add(at);
    }
   }
  }
 }
}
