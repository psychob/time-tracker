using log4net;

namespace timetracker.BasePlugin
{
	public interface ISpy
	{
		void Initialize();

		void Deinitialize();

		void Start();

		void Stop();

		void SetObjects(IValueStorage storage,
			IBinaryStream stream, ILog logger,
			IConfigurationStorage conf);
	}
}
