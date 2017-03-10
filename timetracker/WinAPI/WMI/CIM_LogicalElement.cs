using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.WinAPI.WMI
{
	/// <summary>
	/// The CIM_LogicalElement class is the base class for all system
	/// components that represent abstract system components, such as profiles,
	/// processes, or system capabilities, in the form of logical devices.
	///
	/// Important The DMTF(Distributed Management Task Force)
	/// CIM(Common Information Model) classes are the parent classes upon which
	/// WMI classes are built. WMI currently supports only the CIM 2.x version
	/// schemas.
	/// </summary>
	internal abstract class CIM_LogicalElement : CIM_ManagedSystemElement
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="mbo">
		/// </param>
		public CIM_LogicalElement(ManagementBaseObject mbo)
			: base(mbo)
		{
		}
	}
}
