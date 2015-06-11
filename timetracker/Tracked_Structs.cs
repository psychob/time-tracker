using System;
using System.Xml.Serialization;

namespace timetracker
{
 public struct application_definition
 {
  [XmlAttribute("app-name")]
  public string name;
  [XmlAttribute("app-guid")]
  public Guid   guid;

  [XmlElement("app-ruleset")]
  public application_ruleset[] ruleset;
 }

 public struct application_ruleset
 {
  [XmlAttribute("kind")]
  public application_ruleset_kind kind;
  [XmlAttribute("name")]
  public string rules_name;

  [XmlElement("ruleset")]
  public application_rule[] rules;
 }

 public enum application_ruleset_kind
 {
  all,
  any,
 }

 public struct application_rule
 {
  [XmlAttribute("what")]
  public application_rule_match_to match_to;
  [XmlAttribute("to")]
  public string math_what;
  [XmlAttribute("with")]
  public Utils.MatchAlgorithm algorithm;
 }

 public enum application_rule_match_to
 {
  file_name,
  file_name_path,
  file_path,
  file_version_name,
  file_version_desc,
  file_version_company,
  file_version_product_version,
  file_version_file_version,
  file_md5,
 }

 public struct application_tracked
 {
  [XmlAttribute("app-guid")]
  public Guid guid;
  [XmlAttribute("app-time")]
  public ulong time;
 }

 public struct application_current_tracked
 {
  public Guid guid;
  public int pid;
  public ulong start_time;
  public ulong all_time;
 }

 public struct all_application_definitions
 {
  [XmlElement("apps")]
  public application_definition[] applications;
 }

 public struct all_application_tracked
 {
  [XmlElement("apps")]
  public application_tracked[] applications;
 }

 public struct application_queue
 {
  public int pid;
  public ulong start_time;

  public application_queue( int pid_, ulong st )
  {
   pid = pid_;
   start_time = st;
  }
 }

 public struct application_tracked_detailed
 {
  public string name;
  public ulong time;
 }
}
