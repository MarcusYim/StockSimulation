using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Forms;
using LiveCharts.Wpf;
using LiveCharts;
using System.Collections.Generic;
using System;

namespace StockSimulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ColumnSeries col = new ColumnSeries() { DataLabels = true, Values = new ChartValues<int>(), LabelPoint = point => point.Y.ToString() };
            Axis ax = new Axis() { Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
            ax.Labels = new List<String>();
        }
    }
}
