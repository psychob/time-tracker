using System;
using System.Diagnostics;

namespace timetracker.Entities
{
	public struct ExeData
	{
		public int PID;
		public string Name;
		public string Path;
		public FileVersionInfo FileVersion;
	}

	public struct ExeDataContainer
	{
		public ExeDataContainerReason Reason;
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
				if (data.HasValue)
					return data.Value;
				else
					throw new InvalidOperationException("We don't have value!");
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
