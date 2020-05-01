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
                listView1.Items.Clear();

                foreach (var it in value)
                {
                    var lvi = new ListViewItem(new string[]
                    {
                        it.UniqueID,
                        it.Name,
                        TrackSystem.Utils.GetTime(it.Time),
                        it.StartCounter.ToString()
                    });

                    if (it.AllowOnlyOne)
                        lvi.BackColor = Color.PaleGreen;

                    listView1.Items.Add(lvi);
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
                    ad.ApplicationUniqueID, ad.ApplicationRules, ad.ApplicationAllowOnlyOne, ad.ApplicationCountOnlyParent);

                var lvi = new ListViewItem(new string[]
                {
                    appinfo.UniqueID,
                    appinfo.Name,
                    TrackSystem.Utils.GetTime(appinfo.Time),
                    appinfo.StartCounter.ToString()
                });

                if (ad.ApplicationAllowOnlyOne)
                    lvi.BackColor = Color.PaleGreen;

                listView1.Items.Add(lvi);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ret = MainWindow.OpenFileTemplate();

            if (ret.HasValue)
            {
                var appinfo = ret.Value;
                var lvi = new ListViewItem(new string[]
                {
                    appinfo.UniqueID,
                    appinfo.Name,
                    TrackSystem.Utils.GetTime(appinfo.Time),
                    appinfo.StartCounter.ToString()
                });

                if (appinfo.AllowOnlyOne)
                    lvi.BackColor = Color.PaleGreen;

                listView1.Items.Add(lvi);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
                return;

            var sic = listView1.SelectedItems[0];

            var idata = TrackSystem.TrackingSystemState.GetAppById(sic.SubItems[0].Text);

            AddDefinition adef = new AddDefinition();
            adef.ApplicationName = idata.Name;
            adef.ApplicationUniqueID = idata.UniqueID;
            adef.ApplicationRules = idata.Rules;
            adef.ApplicationEditing = true;
            adef.ApplicationAllowOnlyOne = idata.AllowOnlyOne;
            adef.ApplicationCountOnlyParent = idata.CountOnlyParent;

            adef.ShowDialog();

            idata.Name = adef.ApplicationName;
            idata.Rules = adef.ApplicationRules;
            idata.AllowOnlyOne = adef.ApplicationAllowOnlyOne;
            idata.CountOnlyParent = adef.ApplicationCountOnlyParent;

            if (adef.ApplicationAllowOnlyOne)
                sic.BackColor = Color.PaleGreen;
            else
            {
                if (adef.ApplicationCountOnlyParent)
                    sic.BackColor = Color.AliceBlue;
                else
                    sic.BackColor = Color.White;
            }

            TrackSystem.TrackingSystemState.UpdateApp(idata.UniqueID, idata);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
                return;

            var sic = listView1.SelectedItems[0];

            TrackSystem.TrackingSystemState.RemoveApp(sic.SubItems[0].Text);

            listView1.Items.Remove(sic);
        }
    }
}
