﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;

namespace timetracker
{
 public class Tracked_Apps : IDisposable
 {
  // variable definitions
  List<application_definition> app_defs = new List<application_definition>();
  List<application_tracked> app_tracks = new List<application_tracked>();
  List<application_current_tracked> app_current = new List<application_current_tracked>();
  List<application_queue> app_queue = new List<application_queue>();

  string all_aps_cfg;
  string all_tracks_cfg;
  object lock_object = new object();
  private Tracker t;

  private void load_config_app_defs( )
  {
   string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, all_aps_cfg);

   if (!File.Exists(path))
    return;
   else
   {
    try
    {
     all_application_definitions all_app_def = new all_application_definitions();

     XmlReaderSettings xrs = new XmlReaderSettings();
     using( XmlReader xr = XmlReader.Create(path, xrs))
     {
      XmlSerializer xs = new XmlSerializer(all_app_def.GetType());
      all_app_def = (all_application_definitions)xs.Deserialize(xr);
     }

     if (all_app_def.applications != null)
      app_defs = all_app_def.applications.ToList();

    } catch ( Exception )
    { }
   }
  }

  private void load_config_app_track()
  {
   string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, all_tracks_cfg);

   if (!File.Exists(path))
    return;
   else
   {
    try
    {
     all_application_tracked all_app_def = new all_application_tracked();

     XmlReaderSettings xrs = new XmlReaderSettings();
     using (XmlReader xr = XmlReader.Create(path, xrs))
     {
      XmlSerializer xs = new XmlSerializer(all_app_def.GetType());
      all_app_def = (all_application_tracked)xs.Deserialize(xr);
     }

     if (all_app_def.applications != null)
      app_tracks = all_app_def.applications.ToList();

    }
    catch (Exception)
    { }
   }
  }

  public void load_configs(string app_def, string app_track)
  {
   all_aps_cfg = app_def;
   all_tracks_cfg = app_track;

   load_config_app_defs();
   load_config_app_track();
  }

  public void save_config_app_defs()
  {
   app_defs = app_defs.OrderBy(o => o.name).ToList();

   try
   {
    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, all_aps_cfg);

    XmlWriterSettings xws = new XmlWriterSettings();
    xws.Indent = true;
    xws.IndentChars = "\t";
    xws.Encoding = Encoding.UTF8;

    all_application_definitions aad = new all_application_definitions();
    aad.applications = app_defs.ToArray();

    using (XmlWriter xw = XmlWriter.Create(path, xws))
    {
     XmlSerializer xs = new XmlSerializer(aad.GetType());
     xs.Serialize(xw, aad);
    }
   } catch (Exception)
   { }
  }

  public void save_config_app_track()
  {
   app_tracks = app_tracks.OrderBy(o => o.time).ToList();

   try
   {
    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, all_tracks_cfg);

    XmlWriterSettings xws = new XmlWriterSettings();
    xws.Indent = true;
    xws.IndentChars = "\t";
    xws.Encoding = Encoding.UTF8;

    all_application_tracked aad = new all_application_tracked();
    aad.applications = app_tracks.ToArray();

    using (XmlWriter xw = XmlWriter.Create(path, xws))
    {
     XmlSerializer xs = new XmlSerializer(aad.GetType());
     xs.Serialize(xw, aad);
    }
   }
   catch (Exception)
   { }
  }

  private void save_config()
  {
   save_config_app_defs();
   save_config_app_track();
  }

  public void app_arived( int pid, bool created )
  {
   if (created)
    start_process(pid, WinAPI.GetTickCount64());
   else
    finish_process(pid, WinAPI.GetTickCount64());
  }

  private void start_process(int pid, ulong p)
  {
   lock ( lock_object )
   {
    int process_id = pid;
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

     FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(process_path);

     process_fileversion_name = fvi.ProductName;
     process_fileversion_company = fvi.CompanyName;
     process_fileversion_product_version = fvi.ProductVersion;
     process_fileversion_file_version = fvi.FileVersion;
     process_fileversion_description = fvi.FileDescription;
    } catch (Win32Exception w32)
    {
     if (w32.NativeErrorCode == 299)
      app_queue.Add(new application_queue(pid, p));
     return;
    }
    catch (Exception)
    {
     return;
    }

    add_process_if_valid(process_id, process_filename, process_path,
                         process_fileversion_name, process_fileversion_company,
                         process_fileversion_product_version,
                         process_fileversion_file_version,
                         process_fileversion_description, p);
   }
  }

  private void add_process_if_valid(int process_id, string filename, string path,
                                    string fileversion_name, string fileversion_company,
                                    string fileversion_product_version,
                                    string fileversion_file_version,
                                    string fileversion_description, ulong time)
  {
   lock ( lock_object )
   {
    string md5_str = "";

    foreach ( var it  in app_defs )
    {
     foreach ( var jt in it.ruleset )
     {
      int ktcount = 0;

      foreach ( var kt in jt.rules )
      {
       switch ( kt.match_to )
       {
        case application_rule_match_to.file_md5:
         if ( md5_str == string.Empty )
         {
          // obliczamy sume kontrolną
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
          } catch (Exception)
          {
           md5_str = "";
          }
         }

         ktcount += (Utils.compareStrings(kt.math_what, md5_str, kt.algorithm) ? 1 : 0);
         break;

        case application_rule_match_to.file_name:
         ktcount += (Utils.compareStrings(kt.math_what, filename, kt.algorithm) ? 1 : 0);
         break;

        case application_rule_match_to.file_name_path:
         ktcount += (Utils.compareStrings(kt.math_what, Path.GetDirectoryName(path), kt.algorithm) ? 1 : 0);
         break;

        case application_rule_match_to.file_path:
         ktcount += (Utils.compareStrings(kt.math_what, path, kt.algorithm) ? 1 : 0);
         break;

        case application_rule_match_to.file_version_company:
         ktcount += (Utils.compareStrings(kt.math_what, fileversion_company, kt.algorithm) ? 1 : 0);
         break;

        case application_rule_match_to.file_version_desc:
         ktcount += (Utils.compareStrings(kt.math_what, fileversion_description, kt.algorithm) ? 1 : 0);
         break;

        case application_rule_match_to.file_version_file_version:
         ktcount += (Utils.compareStrings(kt.math_what, fileversion_file_version, kt.algorithm) ? 1 : 0);
         break;

        case application_rule_match_to.file_version_name:
         ktcount += (Utils.compareStrings(kt.math_what, fileversion_name, kt.algorithm) ? 1 : 0);
         break;

        case application_rule_match_to.file_version_product_version:
         ktcount += (Utils.compareStrings(kt.math_what, fileversion_product_version, kt.algorithm) ? 1 : 0);
         break;

        default:
         throw new Exception("...");
       }
      }

      if ( ktcount > 0 )
      {
       if ( jt.kind == application_ruleset_kind.any )
       {
        add_valid_process(process_id, it.guid, time);
        return;
       } else if ( ktcount == jt.rules.Length )
       {
        add_valid_process(process_id, it.guid, time);
        return;
       }
      }
     }
    }
   }
  }

  private void add_valid_process(int pid, Guid guid, ulong time)
  {
   application_current_tracked act = new application_current_tracked();
   act.pid = pid;
   act.guid = guid;
   act.start_time = time;
   act.all_time = 0;

   app_current.Add(act);
  }

  private void finish_process(int pid, ulong p)
  {
   lock ( lock_object )
   {
    foreach (var it in app_current)
     if ( it.pid == pid )
     {
      update_tracked_record(it.guid, p - it.start_time);
      app_current.Remove(it);
      break;
     }
   }
  }

  private void update_tracked_record(Guid guid, ulong p)
  {
   bool added = false;

   for (int it = 0; it < app_tracks.Count; ++it  )
    if ( app_tracks[it].guid == guid )
    {
     application_tracked new_element = new application_tracked();
     new_element.guid = guid;
     new_element.time = app_tracks[it].time + p;

     app_tracks.RemoveAt(it);
     app_tracks.Add(new_element);

     added = true;
     break;
    }

   if (!added)
   {
    application_tracked ne = new application_tracked();
    ne.guid = guid;
    ne.time = p;

    app_tracks.Add(ne);
   }
  }

  public void Dispose()
  {
   finish_all_process();
   save_config();
  }

  public List<application_tracked_detailed> get_tracked_apps()
  {
   lock (lock_object)
   {
    List<application_tracked_detailed> latd = new List<application_tracked_detailed>();

    foreach (var it in app_tracks)
    {
     application_tracked_detailed atd = new application_tracked_detailed();

     atd.name = get_app_by_guid(it.guid).name;
     atd.time = it.time;

     latd.Add(atd);
    }

    return latd;
   }
  }

  private application_definition get_app_by_guid(Guid guid)
  {
   foreach (var it in app_defs)
    if (it.guid == guid)
     return it;

   throw new Exception("not found");
  }

  private void finish_all_process()
  {
   while (app_current.Count() > 0)
    finish_process(app_current[0].pid, WinAPI.GetTickCount64());
  }

  public List<application_definition> get_defined_apps()
  {
   return app_defs;
  }

  public void set_defined_apps(List<application_definition> list)
  {
   app_defs = list;
  }

  public void set_tracker(Tracker global_tracker)
  {
   t = global_tracker;
   t.set_callback(this.app_arived);
   t.report_all();
  }
 }
}
