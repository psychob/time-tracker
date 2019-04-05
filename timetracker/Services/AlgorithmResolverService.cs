using System;
using System.Text.RegularExpressions;
using timetracker.Entity;

namespace timetracker.Services
{
    class AlgorithmResolverService
    {
        public bool IsMatching(RuleAlgorithm algo, string pattern, string value)
        {
            switch (algo)
            {
                case RuleAlgorithm.EndWith:
                    return value.EndsWith(pattern);

                case RuleAlgorithm.EndWithInvariant:
                    return value.EndsWith(pattern, StringComparison.InvariantCultureIgnoreCase);

                case RuleAlgorithm.Exact:
                    return value == pattern;

                case RuleAlgorithm.ExactInvariant:
                    return value.Equals(pattern, StringComparison.InvariantCultureIgnoreCase);

                case RuleAlgorithm.StartsWith:
                    return value.StartsWith(pattern);

                case RuleAlgorithm.StartsWithInvariant:
                    return value.StartsWith(pattern, StringComparison.InvariantCultureIgnoreCase);

                case RuleAlgorithm.Near:
                    return new Regex(GenerateNearCharacters(pattern)).IsMatch(value);

                case RuleAlgorithm.NearInvariant:
                    return new Regex(GenerateNearCharacters(pattern), RegexOptions.IgnoreCase).IsMatch(value);

                case RuleAlgorithm.Regex:
                    try
                    {
                        return new Regex(pattern).IsMatch(value);
                    }
                    catch (Exception)
                    {
                        return false;
                    }

                case RuleAlgorithm.RegexInvariant:
                    try
                    {
                        return new Regex(pattern, RegexOptions.IgnoreCase).IsMatch(value);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
            }

            throw new NotImplementedException("Algorithm is not implemented");
        }

        private string GenerateNearCharacters(string pattern)
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

        private string EscapeCharacters(string str)
        {
            foreach (var it in new string[] { "\\", "[", "]", "(", ")", ".", "*", "?", "^", "$" })
                str = str.Replace(it, "\\" + it);

            return str;
        }
    }
}
