using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timetracker
{
	public partial class ApplicationDefinition : Form
	{
		public ApplicationDefinition()
		{
			InitializeComponent();
		}

		public List<TrackSystem.Structs.App> AppDefinitions
		{
			set
			{
				foreach (var it in value)
				{
					listView1.Items.Add(new ListViewItem(new string[]
					{
						it.UniqueID,
						it.Name,
						TrackSystem.Utils.GetTime(it.Time),
						it.StartCounter.ToString()
					}));
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			AddDefinition ad = new AddDefinition();

			ad.ShowDialog();

			if (ad.IsValid)
			{
				var appinfo = TrackSystem.TrackingSystemState.AddNewDefinition(ad.ApplicationName,
					ad.ApplicationUniqueID, ad.ApplicationRules);

				listView1.Items.Add(new ListViewItem(new string[]
				{
					appinfo.UniqueID,
					appinfo.Name,
					TrackSystem.Utils.GetTime(appinfo.Time),
					appinfo.StartCounter.ToString()
				}));
			}
		}
	}
}
