namespace timetracker.BasePlugin
{
	/// <summary>
	/// This interface represents message that is put down to IBinaryStream
	/// </summary>
	public interface IToken
	{
		/// <summary>
		/// Get Type of Token
		/// </summary>
		/// <returns>
		/// Three letter string that indicate type of token
		/// </returns>
		string GetType();

		/// <summary>
		/// Get data that need to be written to IBinaryStream
		/// </summary>
		byte[] ToBinaryStream();
	}
}