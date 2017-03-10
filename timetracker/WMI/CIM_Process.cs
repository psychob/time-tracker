using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.WMI
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

		public bool CreationClassNameWasQueried { get; private set; }

		/// <summary>
		/// Time that the process began executing.
		/// </summary>
		public DateTime? CreationDate { get; private set; }

		public bool CreationDateWasQueried { get; private set; }

		/// <summary>
		/// Scoping computer system's creation class name.
		/// </summary>
		public string CSCreationClassName { get; private set; }

		public bool CSCreationClassNameWasQueried { get; private set; }

		/// <summary>
		/// Scoping computer system's name.
		/// </summary>
		public string CSName { get; private set; }

		public bool CSNameWasQueried { get; private set; }

		/// <summary>
		/// Current operating condition of the process.
		/// </summary>
		public ExecutionStateType? ExecutionState { get; private set; }

		public bool ExecutionStateWasQueried { get; private set; }

		/// <summary>
		/// Identifies the process. A process identifier is a kind of process
		/// handle.
		/// </summary>
		public string Handle { get; private set; }

		public bool HandleWasQueried { get; private set; }


		/// <summary>
		/// Time in kernel mode, in 100 nanosecond units. If this information
		/// is not available, a value of 0 (zero) should be used.
		/// </summary>
		public UInt64? KernelModeTime { get; private set; }

		public bool KernelModeTimeWasQueried { get; private set; }

		/// <summary>
		/// Scoping operating system's creation class name.
		/// </summary>
		public string OSCreationClassName { get; private set; }

		public bool OSCreationClassNameWasQueried { get; private set; }

		/// <summary>
		/// Scoping operating system's name.
		/// </summary>
		public string OSName { get; private set; }

		public bool OSNameWasQueried { get; private set; }

		/// <summary>
		/// Urgency or importance for process execution. If a priority is not
		/// defined for a process, a value of 0 (zero) should be used.
		/// </summary>
		public UInt32? Priority { get; private set; }

		public bool PriorityWasQueried { get; private set; }

		/// <summary>
		/// Time that the process was stopped or terminated.
		/// </summary>
		public DateTime? TerminationDate { get; private set; }

		public bool TerminationDateWasQueried { get; private set; }

		/// <summary>
		/// Time in user mode, in 100 nanosecond units. If this information is
		/// not available, a value of 0 (zero) should be used.
		/// </summary>
		public UInt64? UserModeTime { get; private set; }

		public bool UserModeTimeWasQueried { get; private set; }

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

		public bool WorkingSetSizeWasQueried { get; private set; }

		public CIM_Process(ManagementBaseObject mbo)
			: base(mbo)
		{
			CreationClassName = GetValueString(mbo, nameof(CreationClassName),
				s => CreationClassNameWasQueried = s);

			CreationDate = GetValueDateTime(mbo, nameof(CreationDate),
				s => CreationDateWasQueried = s);

			CSCreationClassName = GetValueString(mbo, nameof(CSCreationClassName),
				s => CSCreationClassNameWasQueried = s);

			CSName = GetValueString(mbo, nameof(CSName),
				s => CSNameWasQueried = s);

			ExecutionState = GetValueEnum<UInt16, ExecutionStateType>(mbo,
				nameof(ExecutionState), s => ExecutionStateWasQueried = s,
				p => ConvertExecutionState(p));

			Handle = GetValueString(mbo, nameof(Handle), s => HandleWasQueried = s);

			KernelModeTime = GetValueUInt64(mbo, nameof(KernelModeTime),
				s => KernelModeTimeWasQueried = s);

			OSCreationClassName = GetValueString(mbo, nameof(OSCreationClassName),
				s => OSCreationClassNameWasQueried = s);

			OSName = GetValueString(mbo, nameof(OSName),
				s => OSNameWasQueried = s);

			Priority = GetValueUInt32(mbo, nameof(Priority),
				s => PriorityWasQueried = s);

			TerminationDate = GetValueDateTime(mbo, nameof(TerminationDate),
				s => TerminationDateWasQueried = s);

			UserModeTime = GetValueUInt64(mbo, nameof(UserModeTime),
				s => UserModeTimeWasQueried = s);

			WorkingSetSize = GetValueUInt64(mbo, nameof(WorkingSetSize),
				s => WorkingSetSizeWasQueried = s);
		}

		public static ExecutionStateType ConvertExecutionState(UInt16 i)
		{
			if (i > 9)
				return ExecutionStateType._NotImplemented;

			return (ExecutionStateType)i;
		}
	}
}
