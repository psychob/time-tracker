using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.WinAPI.WMI
{
	internal abstract class CIM_StatisticalInformation : BaseClass
	{
		public string Caption { get; private set; }

		public string Description { get; private set; }

		public string Name { get; private set; }

		public CIM_StatisticalInformation(ManagementBaseObject mbo)
		{
			Caption = GetValueString(mbo, nameof(Caption));
			Description = GetValueString(mbo, nameof(Description));
			Name = GetValueString(mbo, nameof(Name));
		}
	}
}
