namespace timetracker.BasePlugin
{
	public interface IValueStorage
	{
		void SetString(string key, string value);

		void SetInteger(string key, long value);

		void SetUnsigned(string key, ulong value);

		void Define(string key, bool history, int historyCount);
	}
}