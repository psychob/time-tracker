using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace WinAPI.WMI
{
	/// <summary>
	/// The Win32_Process WMI class represents a process on an operating
	/// system.
	/// </summary>
	public class Win32_Process : CIM_Process
	{
		/// <summary>
		/// Command line used to start a specific process, if applicable.
		/// </summary>
		public string CommandLine { get; private set; }

		/// <summary>
		/// Was CommandLine Queried
		/// </summary>
		public bool CommandLineWasQueried { get; private set; }

		/// <summary>
		/// Path to the executable file of the process.
		/// </summary>
		public string ExecutablePath { get; private set; }

		/// <summary>
		/// Was ExecutablePath Queried
		/// </summary>
		public bool ExecutablePathWasQueried { get; private set; }

		/// <summary>
		/// Total number of open handles owned by the process. HandleCount is
		/// the sum of the handles currently open by each thread in this
		/// process. A handle is used to examine or modify the system
		/// resources. Each handle has an entry in a table that is maintained
		/// internally. Entries contain the addresses of the resources and data
		/// to identify the resource type.
		/// </summary>
		public UInt32? HandleCount { get; private set; }

		/// <summary>
		/// Was HandleCount Queried
		/// </summary>
		public bool HandleCountWasQueried { get; private set; }

		/// <summary>
		/// Maximum working set size of the process. The working set of a
		/// process is the set of memory pages visible to the process in
		/// physical RAM. These pages are resident, and available for an
		/// application to use without triggering a page fault.
		/// </summary>
		public UInt32? MaximumWorkingSetSize { get; private set; }

		/// <summary>
		/// Was MaximumWorkingSetSize Queried
		/// </summary>
		public bool MaximumWorkingSetSizeWasQueried { get; private set; }

		/// <summary>
		/// Minimum working set size of the process. The working set of a
		/// process is the set of memory pages visible to the process in
		/// physical RAM. These pages are resident and available for an
		/// application to use without triggering a page fault.
		/// </summary>
		public UInt32? MinimumWorkingSetSize { get; private set; }

		/// <summary>
		/// Was MinimumWorkingSetSize Queried
		/// </summary>
		public bool MinimumWorkingSetSizeWasQueried { get; private set; }

		/// <summary>
		/// Number of I/O operations performed that are not read or write
		/// operations.
		/// </summary>
		public UInt64? OtherOperationCount { get; private set; }

		/// <summary>
		/// Was OtherOperationCount Queried
		/// </summary>
		public bool OtherOperationCountWasQueried { get; private set; }

		/// <summary>
		/// Amount of data transferred during operations that are not read or
		/// write operations.
		/// </summary>
		public UInt64? OtherTransferCount { get; private set; }

		/// <summary>
		/// Was OtherTransferCount Queried
		/// </summary>
		public bool OtherTransferCountWasQueried { get; private set; }

		/// <summary>
		/// Number of page faults that a process generates.
		/// </summary>
		public UInt32? PageFaults { get; private set; }

		/// <summary>
		/// Was PageFaults Queried
		/// </summary>
		public bool PageFaultsWasQueried { get; private set; }

		/// <summary>
		/// Amount of page file space that a process is using currently. This
		/// value is consistent with the VMSize value in TaskMgr.exe.
		/// </summary>
		public UInt32? PageFileUsage { get; private set; }

		/// <summary>
		/// Was PageFileUsage Queried
		/// </summary>
		public bool PageFileUsageWasQueried { get; private set; }

		/// <summary>
		/// Unique identifier of the process that creates a process. Process
		/// identifier numbers are reused, so they only identify a process for
		/// the lifetime of that process. It is possible that the process
		/// identified by ParentProcessId is terminated, so ParentProcessId
		/// may not refer to a running process. It is also possible that
		/// ParentProcessId incorrectly refers to a process that reuses a
		/// process identifier. You can use the CreationDate property to
		/// determine whether the specified parent was created after the
		/// process represented by this Win32_Process instance was created.
		/// </summary>
		public UInt32? ParentProcessId { get; private set; }

		/// <summary>
		/// Was ParentProcessId Queried
		/// </summary>
		public bool ParentProcessIdWasQueried { get; private set; }

		/// <summary>
		/// Maximum amount of page file space used during the life of a
		/// process.
		/// </summary>
		public UInt32? PeakPageFileUsage { get; private set; }

		/// <summary>
		/// Was PeakPageFileUsage Queried
		/// </summary>
		public bool PeakPageFileUsageWasQueried { get; private set; }

		/// <summary>
		/// Maximum virtual address space a process uses at any one time.
		/// Using virtual address space does not necessarily imply
		/// corresponding use of either disk or main memory pages. However,
		/// virtual space is finite, and by using too much the process might
		/// not be able to load libraries.
		/// </summary>
		public UInt64? PeakVirtualSize { get; private set; }

		/// <summary>
		/// Was PeakVirtualSize Queried
		/// </summary>
		public bool PeakVirtualSizeWasQueried { get; private set; }

		/// <summary>
		/// Peak working set size of a process.
		/// </summary>
		public UInt32? PeakWorkingSetSize { get; private set; }

		/// <summary>
		/// Was PeakWorkingSetSize Queried
		/// </summary>
		public bool PeakWorkingSetSizeWasQueried { get; private set; }

		/// <summary>
		/// Current number of pages allocated that are only accessible to the
		/// process represented by this Win32_Process instance.
		/// </summary>
		public UInt64? PrivatePageCount { get; private set; }

		/// <summary>
		/// Was PrivatePageCount Queried
		/// </summary>
		public bool PrivatePageCountWasQueried { get; private set; }

		/// <summary>
		/// Numeric identifier used to distinguish one process from another.
		/// ProcessIDs are valid from process creation time to process
		/// termination. Upon termination, that same numeric identifier can be
		/// applied to a new process.
		///
		/// This means that you cannot use ProcessID alone to monitor a
		/// particular process.For example, an application could have a
		/// ProcessID of 7, and then fail. When a new process is started,
		/// the new process could be assigned ProcessID 7. A script that
		/// checked only for a specified ProcessID could thus be "fooled" into
		/// thinking that the original application was still running.
		/// </summary>
		public UInt32? ProcessId { get; private set; }

		/// <summary>
		/// Was ProcessIdWasQueried Queried
		/// </summary>
		public bool ProcessIdWasQueried { get; private set; }

		/// <summary>
		/// Quota amount of nonpaged pool usage for a process.
		/// </summary>
		public UInt32? QuotaNonPagedPoolUsage { get; private set; }

		/// <summary>
		/// Was QuotaNonPagedPoolUsage Queried
		/// </summary>
		public bool QuotaNonPagedPoolUsageWasQueried { get; private set; }

		/// <summary>
		/// Quota amount of paged pool usage for a process.
		/// </summary>
		public UInt32? QuotaPagedPoolUsage { get; private set; }

		/// <summary>
		/// Was QuotaPagedPoolUsage Queried
		/// </summary>
		public bool QuotaPagedPoolUsageWasQueried { get; private set; }

		/// <summary>
		/// Peak quota amount of nonpaged pool usage for a process.
		/// </summary>
		public UInt32? QuotaPeakNonPagedPoolUsage { get; private set; }

		/// <summary>
		/// Was QuotaPeakNonPagedPoolUsage Queried
		/// </summary>
		public bool QuotaPeakNonPagedPoolUsageWasQueried { get; private set; }

		/// <summary>
		/// Peak quota amount of paged pool usage for a process.
		/// </summary>
		public UInt32? QuotaPeakPagedPoolUsage { get; private set; }

		/// <summary>
		/// Was QuotaPeakPagedPoolUsage Queried
		/// </summary>
		public bool QuotaPeakPagedPoolUsageWasQueried { get; private set; }

		/// <summary>
		/// Number of read operations performed.
		/// </summary>
		public UInt64? ReadOperationCount { get; private set; }

		/// <summary>
		/// Was ReadOperationCount Queried
		/// </summary>
		public bool ReadOperationCountWasQueried { get; private set; }

		/// <summary>
		/// Amount of data read.
		/// </summary>
		public UInt64? ReadTransferCount { get; private set; }

		/// <summary>
		/// Was ReadTransferCount Queried
		/// </summary>
		public bool ReadTransferCountWasQueried { get; private set; }

		/// <summary>
		/// Unique identifier that an operating system generates when a
		/// session is created. A session spans a period of time from logon
		/// until logoff from a specific system.
		/// </summary>
		public UInt32? SessionId { get; private set; }

		/// <summary>
		/// Was SessionId Queried
		/// </summary>
		public bool SessionIdWasQueried { get; private set; }

		/// <summary>
		/// Number of active threads in a process. An instruction is the basic
		/// unit of execution in a processor, and a thread is the object that
		/// executes an instruction. Each running process has at least one
		/// thread.
		/// </summary>
		public UInt32? ThreadCount { get; private set; }

		/// <summary>
		/// Was ThreadCount Queried
		/// </summary>
		public bool ThreadCountWasQueried { get; private set; }

		/// <summary>
		/// Current size of the virtual address space that a process is using,
		/// not the physical or virtual memory actually used by the process.
		/// Using virtual address space does not necessarily imply
		/// corresponding use of either disk or main memory pages. Virtual
		/// space is finite, and by using too much, the process might not be
		/// able to load libraries. This value is consistent with what you see
		/// in Perfmon.exe.
		/// </summary>
		public UInt64? VirtualSize { get; private set; }

		/// <summary>
		/// Was VirtualSize Queried
		/// </summary>
		public bool VirtualSizeWasQueried { get; private set; }

		/// <summary>
		/// Version of Windows in which the process is running.
		/// </summary>
		public string WindowsVersion { get; private set; }

		/// <summary>
		/// Was WindowsVersion Queried
		/// </summary>
		public bool WindowsVersionWasQueried { get; private set; }

		/// <summary>
		/// Number of write operations performed.
		/// </summary>
		public UInt64? WriteOperationCount { get; private set; }

		/// <summary>
		/// Was WriteOperationCount Queried
		/// </summary>
		public bool WriteOperationCountWasQueried { get; private set; }

		/// <summary>
		/// Amount of data written.
		/// </summary>
		public UInt64? WriteTransferCount { get; private set; }

		/// <summary>
		/// Was WriteTransferCount Queried
		/// </summary>
		public bool WriteTransferCountWasQueried { get; private set; }

		/// <summary>
		/// Class name
		/// </summary>
		public const string ClassName = "Win32_Process";

		/// <summary>
		/// Namespace of class
		/// </summary>
		public const string Namespace = @"\\.\root\CIMV2";

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="mbo"></param>
		public Win32_Process(ManagementBaseObject mbo)
			: base(mbo)
		{
			CommandLine = GetValueString(mbo, nameof(CommandLine),
				s => CommandLineWasQueried = s);

			ExecutablePath = GetValueString(mbo, nameof(ExecutablePath),
				s => ExecutablePathWasQueried = s);

			HandleCount = GetValueUInt32(mbo, nameof(HandleCount),
				s => HandleCountWasQueried = s);

			MaximumWorkingSetSize = GetValueUInt32(mbo, nameof(MaximumWorkingSetSize),
				s => MaximumWorkingSetSizeWasQueried = s);

			MinimumWorkingSetSize = GetValueUInt32(mbo, nameof(MinimumWorkingSetSize),
				s => MinimumWorkingSetSizeWasQueried = s);

			OtherOperationCount = GetValueUInt64(mbo, nameof(OtherOperationCount),
				s => OtherOperationCountWasQueried = s);

			OtherTransferCount = GetValueUInt64(mbo, nameof(OtherTransferCount),
				s => OtherTransferCountWasQueried = s);

			PageFaults = GetValueUInt32(mbo, nameof(PageFaults),
				s => PageFaultsWasQueried = s);

			PageFileUsage = GetValueUInt32(mbo, nameof(PageFileUsage),
				s => PageFileUsageWasQueried = s);

			ParentProcessId = GetValueUInt32(mbo, nameof(ParentProcessId),
				s => ParentProcessIdWasQueried = s);

			PeakPageFileUsage = GetValueUInt32(mbo, nameof(PeakPageFileUsage),
				s => PeakPageFileUsageWasQueried = s);

			PeakVirtualSize = GetValueUInt64(mbo, nameof(PeakVirtualSize),
				s => PeakVirtualSizeWasQueried = s);

			PeakWorkingSetSize = GetValueUInt32(mbo, nameof(PeakWorkingSetSize),
				s => PeakWorkingSetSizeWasQueried = s);

			PrivatePageCount = GetValueUInt64(mbo, nameof(PrivatePageCount),
				s => PrivatePageCountWasQueried = s);

			ProcessId = GetValueUInt32(mbo, nameof(ProcessId),
				s => ProcessIdWasQueried = s);

			QuotaNonPagedPoolUsage = GetValueUInt32(mbo, nameof(QuotaNonPagedPoolUsage),
				s => QuotaNonPagedPoolUsageWasQueried = s);

			QuotaPagedPoolUsage = GetValueUInt32(mbo, nameof(QuotaPagedPoolUsage),
				s => QuotaPagedPoolUsageWasQueried = s);

			QuotaPeakNonPagedPoolUsage = GetValueUInt32(mbo, nameof(QuotaPeakNonPagedPoolUsage),
				s => QuotaPeakNonPagedPoolUsageWasQueried = s);

			QuotaPeakPagedPoolUsage = GetValueUInt32(mbo, nameof(QuotaPeakPagedPoolUsage),
				s => QuotaPeakPagedPoolUsageWasQueried = s);

			ReadOperationCount = GetValueUInt64(mbo, nameof(ReadOperationCount),
				s => ReadOperationCountWasQueried = s);

			ReadTransferCount = GetValueUInt64(mbo, nameof(ReadTransferCount),
				s => ReadTransferCountWasQueried = s);

			SessionId = GetValueUInt32(mbo, nameof(SessionId),
				s => SessionIdWasQueried = s);

			ThreadCount = GetValueUInt32(mbo, nameof(ThreadCount),
				s => ThreadCountWasQueried = s);

			VirtualSize = GetValueUInt64(mbo, nameof(VirtualSize),
				s => VirtualSizeWasQueried = s);

			WindowsVersion = GetValueString(mbo, nameof(WindowsVersion),
				s => WindowsVersionWasQueried = s);

			WriteOperationCount = GetValueUInt64(mbo, nameof(WriteOperationCount),
				s => WriteOperationCountWasQueried = s);

			WriteTransferCount = GetValueUInt64(mbo, nameof(WriteTransferCount),
				s => WriteTransferCountWasQueried = s);
		}

		/// <summary>
		/// Fetch list of all current processes.
		/// </summary>
		/// <returns></returns>
		public static List<Win32_Process> Fetch()
		{
			return FetchImpl(Namespace, ClassName, p => new Win32_Process(p));
		}

		/// <summary>
		/// Watch all the creation/deletion/modification of this object
		/// </summary>
		/// <param name="Interval">Interval</param>
		/// <param name="Type">Type of watch</param>
		/// <param name="onEvent">Action you want to be performed on event</param>
		/// <returns>Handle to watcher</returns>
		public static ManagementEventWatcher Watch(int Interval,
			WatchType Type, Action<Win32_Process> onEvent)
		{
			return WatchImpl(Interval, Namespace, ClassName, Type, onEvent,
				p => new Win32_Process(p));
		}
	}
}
