using System.Xml.Serialization;

namespace timetracker.Entities
{
	public struct App
	{
		public string UniqueID { get; set; }

		public string Name { get; set; }

		public ulong Time { get; set; }

		public ulong StartCounter { get; set; }

		public bool IsShell { get; set; }

		public AppRuleSet[] Rules { get; set; }

		public bool AllowOnlyOne
		{
			get
			{
				if (_AllowOnlyOne.HasValue)
					return _AllowOnlyOne.Value;
				else
					return false;
			}

			set
			{
				_AllowOnlyOne = value;
			}
		}

		public bool AllowOnlyOneSpecified
		{
			get
			{
				return _AllowOnlyOne.HasValue;
			}
		}

		private bool? _AllowOnlyOne;

		[XmlAttribute(AttributeName = "merge-spawned")]
		public bool MergeSpawned { get; set; }

		public App(App copy)
		{
			Name = copy.Name;
			UniqueID = copy.UniqueID;
			Time = copy.Time;
			StartCounter = copy.StartCounter;
			IsShell = copy.IsShell;
			Rules = copy.Rules;
			_AllowOnlyOne = copy._AllowOnlyOne;
			MergeSpawned = copy.MergeSpawned;
		}
	}
}
