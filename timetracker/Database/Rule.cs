using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace timetracker.Database
{
	[XmlRoot(ElementName = "rule")]
	public struct Rule
	{
		[XmlAttribute(AttributeName = "match-to")]
		public RuleMatchTo MatchTo;

		[XmlAttribute(AttributeName = "match-algorithm")]
		public RuleMatchAlgorithm MatchAlgorithm;

		[XmlText]
		public string MatchPattern;
	}

	public enum RuleMatchTo
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

	public enum RuleMatchAlgorithm
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
}
