using System;
using System.Text.RegularExpressions;

namespace timetracker
{
 public class Utils
 {
  public enum MatchAlgorithm
  {
   ExactSensitive,
   ExactInsensitive,
   NearSensitive,
   NearInsensitive,
   RegularExpression,
  }

  public static string calculateTime(ulong time)
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

  public static bool compareStrings(string str, string str2, MatchAlgorithm applicationMatchType)
  {
   switch ( applicationMatchType )
   {
    case MatchAlgorithm.NearSensitive:
     return new Regex(substitute_chars(str)).IsMatch(str2);

    case MatchAlgorithm.NearInsensitive:
     return new Regex(substitute_chars(str), RegexOptions.IgnoreCase).IsMatch(str2);

    case MatchAlgorithm.ExactSensitive:
     return str == str2;

    case MatchAlgorithm.ExactInsensitive:
     return str.Equals(str2, StringComparison.InvariantCultureIgnoreCase);

    case MatchAlgorithm.RegularExpression:
     {
      try
      {
       return new Regex(str).IsMatch(str2);
      } catch ( Exception )
      {
       return false;
      }
     }
   }

   return false;
  }

  private static string substitute_chars(string str)
  {
   // ? -> .{1}
   // * -> .*

   string out_str = "";
   int it = 0;

   while (it < str.Length)
   {
    int asterix = str.IndexOf("*", it);
    int exclam = str.IndexOf("?", it);

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
     out_str += escape_chars(str.Substring(it));
     it = str.Length;
    }
    else
    {
     if ( char_ != it )
     {
      out_str += escape_chars(str.Substring(it, char_ - it));
      it = char_;
     }

     switch ( str[it])
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

  private static string escape_chars(string p)
  {
   foreach (var it in new string[] { "\\", "[", "]", "(", ")", ".", "*", "?", "^", "$" })
    p = p.Replace(it, "\\" + it);

   return p;
  }
 }
}
