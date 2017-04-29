using System.Collections.Generic;
using System.Xml.Serialization;

namespace timetracker.Database
{
	[XmlRoot(ElementName = "application")]
	struct App
	{
		[XmlAttribute(AttributeName = "id")]
		public string Id;

		[XmlAttribute(AttributeName = "name-of-app")]
		public string Name;

		[XmlAttribute(AttributeName = "time-running")]
		public ulong Time;

		[XmlAttribute(AttributeName = "started-count")]
		public ulong StartCount;

		[XmlAttribute(AttributeName = "is-valid")]
		public bool IsValid;

		[XmlAttribute(AttributeName = "only-one")]
		public bool AllowOnlyOne;

		[XmlAttribute(AttributeName = "merge-spawned")]
		public bool MergeSpawned;

		[XmlElement(ElementName = "list-of-rules")]
		public List<RuleSet> Rules;
	}
}
