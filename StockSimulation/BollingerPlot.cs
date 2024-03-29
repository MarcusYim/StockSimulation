﻿using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Forms;

namespace StockSimulation
{
    public partial class BollingerPlot : Form
    {
        public BollingerPlot()
        {
            InitializeComponent();
        }

        private void BollingerPlot_Load(object sender, EventArgs e)
        {
            ColumnSeries col = new ColumnSeries() { DataLabels = true, Values = new ChartValues<int>(), LabelPoint = point => point.Y.ToString() };
            Axis ax = new Axis() { Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
            ax.Labels = new List<String>();

        }
    }
}
