using System.Diagnostics;

namespace timetracker.Entities
{
	public struct ExeData
	{
		public int PID { get; set; }

		public string Name { get; set; }

		public string Path { get; set; }

		public FileVersionInfo FileVersion { get; set; }
	}

	public struct ExeDataContainer
	{
		public ExeDataContainerReason Reason { get; set; }
		private ExeData? data;

		public ExeDataContainer(ExeData ed)
		{
			Reason = ExeDataContainerReason.Valid;
			data = ed;
		}

		public ExeDataContainer(ExeDataContainerReason reason)
		{
			Reason = reason;
			data = null;
		}

		public ExeData Value
		{
			get
			{
				return data.Value;
			}

			set
			{
				Reason = ExeDataContainerReason.Valid;
				data = value;
			}
		}
	}

	public enum ExeDataContainerReason
	{
		Valid,
		IncompleteInformation,
		ExceptionOccured,
	}
}
