using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.WinAPI.WMI
{
	/// <summary>
	/// The Win32_Process WMI class represents a process on an operating
	/// system.
	/// </summary>
	internal class Win32_Process : CIM_Process
	{
		/// <summary>
		/// Command line used to start a specific process, if applicable.
		/// </summary>
		public string CommandLine { get; private set; }

		/// <summary>
		/// Path to the executable file of the process.
		/// </summary>
		public string ExecutablePath { get; private set; }

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
		/// Maximum working set size of the process. The working set of a
		/// process is the set of memory pages visible to the process in
		/// physical RAM. These pages are resident, and available for an
		/// application to use without triggering a page fault.
		/// </summary>
		public UInt32? MaximumWorkingSetSize { get; private set; }

		/// <summary>
		/// Minimum working set size of the process. The working set of a
		/// process is the set of memory pages visible to the process in
		/// physical RAM. These pages are resident and available for an
		/// application to use without triggering a page fault.
		/// </summary>
		public UInt32? MinimumWorkingSetSize { get; private set; }

		/// <summary>
		/// Number of I/O operations performed that are not read or write
		/// operations.
		/// </summary>
		public UInt64? OtherOperationCount { get; private set; }

		/// <summary>
		/// Amount of data transferred during operations that are not read or
		/// write operations.
		/// </summary>
		public UInt64? OtherTransferCount { get; private set; }

		/// <summary>
		/// Number of page faults that a process generates.
		/// </summary>
		public UInt32? PageFaults { get; private set; }

		/// <summary>
		/// Amount of page file space that a process is using currently. This
		/// value is consistent with the VMSize value in TaskMgr.exe.
		/// </summary>
		public UInt32? PageFileUsage { get; private set; }

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
		/// Maximum amount of page file space used during the life of a
		/// process.
		/// </summary>
		public UInt32? PeakPageFileUsage { get; private set; }

		/// <summary>
		/// Maximum virtual address space a process uses at any one time.
		/// Using virtual address space does not necessarily imply
		/// corresponding use of either disk or main memory pages. However,
		/// virtual space is finite, and by using too much the process might
		/// not be able to load libraries.
		/// </summary>
		public UInt64? PeakVirtualSize { get; private set; }

		/// <summary>
		/// Peak working set size of a process.
		/// </summary>
		public UInt32? PeakWorkingSetSize { get; private set; }

		/// <summary>
		/// Current number of pages allocated that are only accessible to the
		/// process represented by this Win32_Process instance.
		/// </summary>
		public UInt64? PrivatePageCount { get; private set; }

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
		/// Quota amount of nonpaged pool usage for a process.
		/// </summary>
		public UInt32? QuotaNonPagedPoolUsage { get; private set; }

		/// <summary>
		/// Quota amount of paged pool usage for a process.
		/// </summary>
		public UInt32? QuotaPagedPoolUsage { get; private set; }

		/// <summary>
		/// Peak quota amount of nonpaged pool usage for a process.
		/// </summary>
		public UInt32? QuotaPeakNonPagedPoolUsage { get; private set; }

		/// <summary>
		/// Peak quota amount of paged pool usage for a process.
		/// </summary>
		public UInt32? QuotaPeakPagedPoolUsage { get; private set; }

		/// <summary>
		/// Number of read operations performed.
		/// </summary>
		public UInt64? ReadOperationCount { get; private set; }

		/// <summary>
		/// Amount of data read.
		/// </summary>
		public UInt64? ReadTransferCount { get; private set; }

		/// <summary>
		/// Unique identifier that an operating system generates when a
		/// session is created. A session spans a period of time from logon
		/// until logoff from a specific system.
		/// </summary>
		public UInt32? SessionId { get; private set; }

		/// <summary>
		/// Number of active threads in a process. An instruction is the basic
		/// unit of execution in a processor, and a thread is the object that
		/// executes an instruction. Each running process has at least one
		/// thread.
		/// </summary>
		public UInt32? ThreadCount { get; private set; }

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
		/// Version of Windows in which the process is running.
		/// </summary>
		public string WindowsVersion { get; private set; }

		/// <summary>
		/// Number of write operations performed.
		/// </summary>
		public UInt64? WriteOperationCount { get; private set; }

		/// <summary>
		/// Amount of data written.
		/// </summary>
		public UInt64? WriteTransferCount { get; private set; }

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
			CommandLine = GetValueString(mbo, nameof(CommandLine));
			ExecutablePath = GetValueString(mbo, nameof(ExecutablePath));
			HandleCount = GetValue<UInt32>(mbo, nameof(HandleCount));
			MaximumWorkingSetSize = GetValue<UInt32>(mbo, nameof(MaximumWorkingSetSize));
			MinimumWorkingSetSize = GetValue<UInt32>(mbo, nameof(MinimumWorkingSetSize));
			OtherOperationCount = GetValue<UInt64>(mbo, nameof(OtherOperationCount));
			OtherTransferCount = GetValue<UInt64>(mbo, nameof(OtherTransferCount));
			PageFaults = GetValue<UInt32>(mbo, nameof(PageFaults));
			PageFileUsage = GetValue<UInt32>(mbo, nameof(PageFileUsage));
			ParentProcessId = GetValue<UInt32>(mbo, nameof(ParentProcessId));
			PeakPageFileUsage = GetValue<UInt32>(mbo, nameof(PeakPageFileUsage));
			PeakVirtualSize = GetValue<UInt64>(mbo, nameof(PeakVirtualSize));
			PeakWorkingSetSize = GetValue<UInt32>(mbo, nameof(PeakWorkingSetSize));
			PrivatePageCount = GetValue<UInt64>(mbo, nameof(PrivatePageCount));
			ProcessId = GetValue<UInt32>(mbo, nameof(ProcessId));
			QuotaNonPagedPoolUsage = GetValue<UInt32>(mbo, nameof(QuotaNonPagedPoolUsage));
			QuotaPagedPoolUsage = GetValue<UInt32>(mbo, nameof(QuotaPagedPoolUsage));
			QuotaPeakNonPagedPoolUsage = GetValue<UInt32>(mbo, nameof(QuotaPeakNonPagedPoolUsage));
			QuotaPeakPagedPoolUsage = GetValue<UInt32>(mbo, nameof(QuotaPeakPagedPoolUsage));
			ReadOperationCount = GetValue<UInt64>(mbo, nameof(ReadOperationCount));
			ReadTransferCount = GetValue<UInt64>(mbo, nameof(ReadTransferCount));
			SessionId = GetValue<UInt32>(mbo, nameof(SessionId));
			ThreadCount = GetValue<UInt32>(mbo, nameof(ThreadCount));
			VirtualSize = GetValue<UInt64>(mbo, nameof(VirtualSize));
			WindowsVersion = GetValueString(mbo, nameof(WindowsVersion));
			WriteOperationCount = GetValue<UInt64>(mbo, nameof(WriteOperationCount));
			WriteTransferCount = GetValue<UInt64>(mbo, nameof(WriteTransferCount));
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
