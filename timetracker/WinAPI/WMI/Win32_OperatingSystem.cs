using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.WinAPI.WMI
{
	class Win32_OperatingSystem : CIM_OperatingSystem
	{
		public string BootDevice { get; private set; }

		public string BuildNumber { get; private set; }

		public string BuildType { get; private set; }

		public string CodeSet { get; private set; }

		public string CountryCode { get; private set; }

		public string CSDVersion { get; private set; }

		public Boolean? DataExecutionPrevention_Available { get; private set; }

		public Boolean? DataExecutionPrevention_32BitApplications { get; private set; }

		public Boolean? DataExecutionPrevention_Drivers { get; private set; }

		public Byte? DataExecutionPrevention_SupportPolicy { get; private set; }

		public Boolean? Debug { get; private set; }

		public UInt32? EncryptionLevel { get; private set; }

		public Byte? ForegroundApplicationBoost { get; private set; }

		public UInt32? LargeSystemCache { get; private set; }

		public string Locale { get; private set; }

		public string Manufacturer { get; private set; }

		public string[] MUILanguages { get; private set; }

		public UInt32? OperatingSystemSKU { get; private set; }

		public string Organization { get; private set; }

		public string OSArchitecture { get; private set; }

		public UInt32? OSLanguage { get; private set; }

		public UInt32? OSProductSuite { get; private set; }

		public Boolean? PAEEnabled { get; private set; }

		public string PlusProductID { get; private set; }

		public string PlusVersionNumber { get; private set; }

		public Boolean? PortableOperatingSystem { get; private set; }

		public Boolean? Primary { get; private set; }

		public UInt32? ProductType { get; private set; }

		public string RegisteredUser { get; private set; }

		public string SerialNumber { get; private set; }

		public UInt16? ServicePackMajorVersion { get; private set; }

		public UInt16? ServicePackMinorVersion { get; private set; }

		public UInt32? SuiteMask { get; private set; }

		public string SystemDevice { get; private set; }

		public string SystemDirectory { get; private set; }

		public string SystemDrive { get; private set; }

		public string WindowsDirectory { get; private set; }

		public Byte? QuantumLength { get; private set; }

		public Byte? QuantumType { get; private set; }

		public const string ClassName = "Win32_OperatingSystem";

		public const string Namespace = @"\\.\root\CIMV2";

		public Win32_OperatingSystem(ManagementBaseObject mbo) : base(mbo)
		{
			BootDevice = GetValueString(mbo, nameof(BootDevice));
			BuildNumber = GetValueString(mbo, nameof(BuildNumber));
			BuildType = GetValueString(mbo, nameof(BuildType));
			CodeSet = GetValueString(mbo, nameof(CodeSet));
			CountryCode = GetValueString(mbo, nameof(CountryCode));
			CSDVersion = GetValueString(mbo, nameof(CSDVersion));
			DataExecutionPrevention_Available = GetValue<bool>(mbo, nameof(DataExecutionPrevention_Available));
			DataExecutionPrevention_32BitApplications = GetValue<bool>(mbo, nameof(DataExecutionPrevention_32BitApplications));
			DataExecutionPrevention_Drivers = GetValue<bool>(mbo, nameof(DataExecutionPrevention_Drivers));
			DataExecutionPrevention_SupportPolicy = GetValue<Byte>(mbo, nameof(DataExecutionPrevention_SupportPolicy));
			Debug = GetValue<bool>(mbo, nameof(Debug));
			EncryptionLevel = GetValue<UInt32>(mbo, nameof(EncryptionLevel));
			ForegroundApplicationBoost = GetValue<Byte>(mbo, nameof(ForegroundApplicationBoost));
			LargeSystemCache = GetValue<UInt32>(mbo, nameof(LargeSystemCache));
			Locale = GetValueString(mbo, nameof(Locale));
			Manufacturer = GetValueString(mbo, nameof(Manufacturer));
			MUILanguages = GetValueArrayString(mbo, nameof(MUILanguages));
			OperatingSystemSKU = GetValue<UInt32>(mbo, nameof(OperatingSystemSKU));
			Organization = GetValueString(mbo, nameof(Organization));
			OSArchitecture = GetValueString(mbo, nameof(OSArchitecture));
			OSLanguage = GetValue<UInt32>(mbo, nameof(OSLanguage));
			OSProductSuite = GetValue<UInt32>(mbo, nameof(OSProductSuite));
			PAEEnabled = GetValue<Boolean>(mbo, nameof(PAEEnabled));
			PlusProductID = GetValueString(mbo, nameof(PlusProductID));
			PlusVersionNumber = GetValueString(mbo, nameof(PlusVersionNumber));
			PortableOperatingSystem = GetValue<bool>(mbo, nameof(PortableOperatingSystem));
			Primary = GetValue<bool>(mbo, nameof(Primary));
			ProductType = GetValue<UInt32>(mbo, nameof(ProductType));
			RegisteredUser = GetValueString(mbo, nameof(RegisteredUser));
			SerialNumber = GetValueString(mbo, nameof(SerialNumber));
			ServicePackMajorVersion = GetValue<UInt16>(mbo, nameof(ServicePackMajorVersion));
			ServicePackMinorVersion = GetValue<UInt16>(mbo, nameof(ServicePackMinorVersion));
			SuiteMask = GetValue<UInt32>(mbo, nameof(SuiteMask));
			SystemDevice = GetValueString(mbo, nameof(SystemDevice));
			SystemDirectory = GetValueString(mbo, nameof(SystemDirectory));
			SystemDrive = GetValueString(mbo, nameof(SystemDrive));
			WindowsDirectory = GetValueString(mbo, nameof(WindowsDirectory));
			QuantumLength = GetValue<Byte>(mbo, nameof(QuantumLength));
			QuantumType = GetValue<Byte>(mbo, nameof(QuantumType));
		}

		public static List<Win32_OperatingSystem> Fetch()
		{
			return FetchImpl(Namespace, ClassName, p => new Win32_OperatingSystem(p));
		}

		public static ManagementEventWatcher Watch(int Interval,
			WatchType Type, Action<Win32_OperatingSystem> onEvent)
		{
			return WatchImpl(Interval, Namespace, ClassName, Type, onEvent,
				p => new Win32_OperatingSystem(p));
		}
	}
}
