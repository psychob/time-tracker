namespace timetracker.BasePlugin
{
	public interface IConfigurationStorage
	{
		IConfigurationStorage GetKey(string key);

		string GetValue(string key);
	}
}