using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.WinAPI.WMI
{
	abstract class CIM_OperatingSystem : CIM_LogicalElement
	{
		public string CreationClassName { get; private set; }

		public string CSCreationClassName { get; private set; }

		public string CSName { get; private set; }

		public Int16? CurrentTimeZone { get; private set; }

		public bool? Distributed { get; private set; }

		public UInt64? FreePhysicalMemory { get; private set; }

		public UInt64? FreeSpaceInPagingFiles { get; private set; }

		public UInt64? FreeVirtualMemory { get; private set; }

		public DateTime? LastBootUpTime { get; private set; }

		public DateTime? LocalDateTime { get; private set; }

		public UInt32? MaxNumberOfProcesses { get; private set; }

		public UInt64? MaxProcessMemorySize { get; private set; }

		public UInt32? NumberOfLicensedUsers { get; private set; }

		public UInt32? NumberOfProcesses { get; private set; }

		public UInt32? NumberOfUsers { get; private set; }

		public UInt16? OSType { get; private set; }

		public string OtherTypeDescription { get; private set; }

		public UInt64? SizeStoredInPagingFiles { get; private set; }

		public UInt64? TotalSwapSpaceSize { get; private set; }

		public UInt64? TotalVirtualMemorySize { get; private set; }

		public UInt64? TotalVisibleMemorySize { get; private set; }

		public string Version { get; private set; }

		public CIM_OperatingSystem(ManagementBaseObject mbo) : base(mbo)
		{
			CreationClassName = GetValueString(mbo, nameof(CreationClassName));
			CSCreationClassName = GetValueString(mbo, nameof(CSCreationClassName));
			CSName = GetValueString(mbo, nameof(CSName));
			CurrentTimeZone = GetValue<Int16>(mbo, nameof(CurrentTimeZone));
			Distributed = GetValue<Boolean>(mbo, nameof(Distributed));
			FreePhysicalMemory = GetValue<UInt64>(mbo, nameof(FreePhysicalMemory));
			FreeSpaceInPagingFiles = GetValue<UInt64>(mbo, nameof(FreeSpaceInPagingFiles));
			FreeVirtualMemory = GetValue<UInt64>(mbo, nameof(FreeVirtualMemory));
			LastBootUpTime = GetValueDateTime(mbo, nameof(LastBootUpTime));
			LocalDateTime = GetValueDateTime(mbo, nameof(LocalDateTime));
			MaxNumberOfProcesses = GetValue<UInt32>(mbo, nameof(MaxNumberOfProcesses));
			MaxProcessMemorySize = GetValue<UInt32>(mbo, nameof(MaxProcessMemorySize));
			NumberOfLicensedUsers = GetValue<UInt32>(mbo, nameof(NumberOfLicensedUsers));
			NumberOfProcesses = GetValue<UInt32>(mbo, nameof(NumberOfProcesses));
			NumberOfUsers = GetValue<UInt32>(mbo, nameof(NumberOfUsers));
			OSType = GetValue<UInt16>(mbo, nameof(OSType));
			OtherTypeDescription = GetValueString(mbo, nameof(OtherTypeDescription));
			SizeStoredInPagingFiles = GetValue<UInt64>(mbo, nameof(SizeStoredInPagingFiles));
			TotalSwapSpaceSize = GetValue<UInt64>(mbo, nameof(TotalSwapSpaceSize));
			TotalVirtualMemorySize = GetValue<UInt64>(mbo, nameof(TotalVirtualMemorySize));
			TotalVisibleMemorySize = GetValue<UInt64>(mbo, nameof(TotalVisibleMemorySize));
			Version = GetValueString(mbo, nameof(Version));
		}
	}
}
