using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAPI.WMI
{
	public class Win32_NetworkAdapter
	{
		public enum NetConnectionStatus : UInt16
		{
			Disconnected = (0),
			Connecting = (1),
			Connected = (2),
			Disconnecting = (3),
			HardwareNotPresent = (4),
			HardwareDisabled = (5),
			HardwareMalfunction = (6),
			MediaDisconnected = (7),
			Authenticating = (8),
			AuthenticationSucceeded = (9),
			AuthenticationFailed = (10),
			InvalidAddress = (11),
			CredentialsRequired = (12),
			Other,
		}

		public static NetConnectionStatus Convert(UInt16 status)
		{
			switch (status)
			{
				case 0:
					return NetConnectionStatus.Disconnected;

				case 1:
					return NetConnectionStatus.Connecting;

				case 2:
					return NetConnectionStatus.Connected;

				case 3:
					return NetConnectionStatus.Disconnecting;

				case 4:
					return NetConnectionStatus.HardwareNotPresent;

				case 5:
					return NetConnectionStatus.HardwareDisabled;

				case 6:
					return NetConnectionStatus.HardwareMalfunction;

				case 7:
					return NetConnectionStatus.MediaDisconnected;

				case 8:
					return NetConnectionStatus.Authenticating;

				case 9:
					return NetConnectionStatus.AuthenticationSucceeded;

				case 10:
					return NetConnectionStatus.AuthenticationFailed;

				case 11:
					return NetConnectionStatus.InvalidAddress;

				case 12:
					return NetConnectionStatus.CredentialsRequired;

				default:
					return NetConnectionStatus.Other;
			}
		}
	}
}
