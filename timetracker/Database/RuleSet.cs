using System.Collections.Generic;
using System.Xml.Serialization;

namespace timetracker.Database
{
	[XmlRoot(ElementName = "rule-set")]
	public struct RuleSet
	{
		[XmlAttribute(AttributeName = "kind")]
		public RuleSetKind Kind;

		[XmlAttribute(AttributeName = "priority")]
		public RuleSetPriority Priority;

		[XmlAttribute(AttributeName = "id")]
		public string Id;

		public List<Rule> Rules;
	}

	public enum RuleSetKind
	{
		RequireAny,
		RequireAll,
	}

	public enum RuleSetPriority
	{
		Low,
		Medium,
		High,
	}
}