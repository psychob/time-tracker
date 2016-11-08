using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.Spys
{
	internal interface ISpy
	{
		/// <summary>
		/// This method initializes a spy
		/// </summary>
		/// <returns>
		/// Was registering a spy successful
		/// </returns>
		bool RegisterSpy();

		/// <summary>
		/// This method deinitializes a spy
		/// </summary>
		void UnregisterSpy();

		/// <summary>
		/// Can this spy be disabled?
		/// </summary>
		bool CanDisableSpy();

		/// <summary>
		/// Is this spy visible in option menu
		/// </summary>
		/// <remarks>
		/// By default, if you don't return any options, and candisable is
		/// retrurning false, then spy is always not-visible
		/// </remarks>
		bool IsVisibleInOptions();

		/// <summary>
		/// Get name of this spy
		/// </summary>
		string GetName();

		/// <summary>
		/// Get author of this spy
		/// </summary>
		string GetAuthor();

		/// <summary>
		/// Get version of this spy
		/// </summary>
		string GetVersion();

		/// <summary>
		/// All counters available in this spy
		/// </summary>
		ICounter[] GetCounters();

		/// <summary>
		/// All options available
		/// </summary>
		IOption[] GetOptions();
	}
}
