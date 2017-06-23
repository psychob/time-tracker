namespace timetracker.BasePlugin
{
	public interface IConfigurationStorage
	{
		IConfigurationStorage GetKey(string key);

		string GetValue(string key, string def);
	}

	public static class ConfigurationStorageExtensions
	{
		public static int GetInteger(this IConfigurationStorage store,
			string key, int def)
		{
			string value = store.GetValue(key, null);

			if (value == null)
			{
				return def;
			}

			int result = 0;

			if (int.TryParse(value, out result))
			{
				return result;
			} else
			{
				return def;
			}
		}
	}
}