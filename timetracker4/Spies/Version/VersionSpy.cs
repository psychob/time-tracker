using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using timetracker4.Services;

namespace timetracker4.Spies.Version
{
    internal class VersionSpy: ISpy
    {
        private IDatabase database;

        public string GetName()
        {
            return "Application Version";
        }

        public void Start()
        {
            var currentVersion = "";
            {
                var ass = Assembly.GetExecutingAssembly();
                var fvi = FileVersionInfo.GetVersionInfo(ass.Location);
                var str = fvi.ProductVersion.Split('.');
                var it = str.Length - 1;

                while (it >= 0 && str[it] == "0")
                    it--;

                for (var kt = 0; kt <= it; ++kt)
                    currentVersion += str[kt] + '.';

                currentVersion = currentVersion.Trim('.');
            }

            database.NewEvent(new VersionEvent(currentVersion));
        }

        public void Stop()
        {
            // noop
        }
    }
}
