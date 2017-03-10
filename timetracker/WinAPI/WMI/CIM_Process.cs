using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.WinAPI.WMI
{
	internal abstract class CIM_Process : CIM_LogicalElement
	{
		public enum ExecutionStateType : UInt16
		{
			Unknown = 0,
			Other = 1,
			Ready = 2,
			Running =3,
			Blocked = 4,
			SuspendedBlocked = 5,
			SuspendedReady = 6,
			Terminated = 7,
			Stopped = 8,
			Growing = 9,

			_NotImplemented = UInt16.MaxValue,
		}

		/// <summary>
		/// Name of the class or subclass used in the creation of an instance.
		/// When used with other key properties of the class, this property
		/// allows all instances of the class and its subclasses to be uniquely
		/// identified.
		/// </summary>
		public string CreationClassName { get; private set; }

		/// <summary>
		/// Time that the process began executing.
		/// </summary>
		public DateTime? CreationDate { get; private set; }

		/// <summary>
		/// Scoping computer system's creation class name.
		/// </summary>
		public string CSCreationClassName { get; private set; }

		/// <summary>
		/// Scoping computer system's name.
		/// </summary>
		public string CSName { get; private set; }

		/// <summary>
		/// Current operating condition of the process.
		/// </summary>
		public ExecutionStateType? ExecutionState { get; private set; }

		/// <summary>
		/// Identifies the process. A process identifier is a kind of process
		/// handle.
		/// </summary>
		public string Handle { get; private set; }


		/// <summary>
		/// Time in kernel mode, in 100 nanosecond units. If this information
		/// is not available, a value of 0 (zero) should be used.
		/// </summary>
		public UInt64? KernelModeTime { get; private set; }

		/// <summary>
		/// Scoping operating system's creation class name.
		/// </summary>
		public string OSCreationClassName { get; private set; }

		/// <summary>
		/// Scoping operating system's name.
		/// </summary>
		public string OSName { get; private set; }

		/// <summary>
		/// Urgency or importance for process execution. If a priority is not
		/// defined for a process, a value of 0 (zero) should be used.
		/// </summary>
		public UInt32? Priority { get; private set; }

		/// <summary>
		/// Time that the process was stopped or terminated.
		/// </summary>
		public DateTime? TerminationDate { get; private set; }

		/// <summary>
		/// Time in user mode, in 100 nanosecond units. If this information is
		/// not available, a value of 0 (zero) should be used.
		/// </summary>
		public UInt64? UserModeTime { get; private set; }

		/// <summary>
		/// Amount of memory, in bytes, that a process needs to execute
		/// efficiently for an operating system that uses page-based memory
		/// management. If the system does not have enough memory (less than
		/// the working set size), thrashing occurs. If the size of the working
		/// set is not known, use NULL or 0 (zero). If working set data is
		/// provided, you can monitor the information to understand the
		/// changing memory requirements of a process.
		/// </summary>
		public UInt64? WorkingSetSize { get; private set; }

		public CIM_Process(ManagementBaseObject mbo)
			: base(mbo)
		{
			CreationClassName = GetValueString(mbo, nameof(CreationClassName));
			CreationDate = GetValueDateTime(mbo, nameof(CreationDate));
			CSCreationClassName = GetValueString(mbo, nameof(CSCreationClassName));
			CSName = GetValueString(mbo, nameof(CSName));
			ExecutionState = GetValueEnum<UInt16, ExecutionStateType>(mbo,
				nameof(ExecutionState),	p => ConvertExecutionState(p));
			Handle = GetValueString(mbo, nameof(Handle));
			KernelModeTime = GetValue<UInt64>(mbo, nameof(KernelModeTime));
			OSCreationClassName = GetValueString(mbo, nameof(OSCreationClassName));
			OSName = GetValueString(mbo, nameof(OSName));
			Priority = GetValue<UInt32>(mbo, nameof(Priority));
			TerminationDate = GetValueDateTime(mbo, nameof(TerminationDate));
			UserModeTime = GetValue<UInt64>(mbo, nameof(UserModeTime));
			WorkingSetSize = GetValue<UInt64>(mbo, nameof(WorkingSetSize));
		}

		public static ExecutionStateType ConvertExecutionState(UInt16 i)
		{
			if (i > 9)
				return ExecutionStateType._NotImplemented;

			return (ExecutionStateType)i;
		}
	}
}
