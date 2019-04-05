using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ninject;
using SQLite;
using timetracker4.Entity;
using timetracker4.Services;
using Application = timetracker4.Entity.Application;
using Rule = timetracker4.Entity.Rule;

namespace timetracker4.Forms
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            var db = Program.DependencyInjection.Get<IDatabase>();
        }
    }
}
