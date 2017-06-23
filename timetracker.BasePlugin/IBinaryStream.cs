using System;

namespace timetracker.BasePlugin
{
	/// <summary>
	/// This interface is representing BinaryStream
	/// </summary>
	public interface IBinaryStream
	{
		/// <summary>
		/// Sends token to binary stream
		/// </summary>
		/// <param name="token">Token data</param>
		/// <param name="dt">What date should be assigned to this token</param>
		/// <param name="flush">Should stream be flushed afterwards</param>
		void Send(IToken token, DateTime? dt = null, bool flush = false);

		/// <summary>
		/// Flush the stream
		/// </summary>
		void Flush();
	}
}