﻿using Scream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Scream
{
    /// <summary>
    /// PreferencesPage.xaml 的交互逻辑
    /// </summary>
    public partial class Preferences : Page
    {
        Settings grid = new Settings();
        public MainWindow mainWindow;
        public Preferences()
        {
            InitializeComponent();
            Grid.DataContext = grid;
            foreach (string lv in Utilities.LOG_LEVEL_LIST)
            {
                ComboBoxLogLevel.Items.Add(lv);
            }
        }

        public void InitializeData()
        {
            grid.HTTP = mainWindow.httpPort;
            grid.SOCKS5 = mainWindow.localPort;
            grid.LogLevelIndex = Utilities.LOG_LEVEL_LIST.FindIndex(x => x == mainWindow.logLevel);
            grid.DNS = mainWindow.dnsString;
            grid.Bypass = mainWindow.bypass;
            grid.Subscription = String.Join("\n", mainWindow.subscriptionUrl);
            TextBlockCoreVersion.Text = Utilities.corePath;
            TextBlockMode.Text = Utilities.ProxyMode_LIST[(int)mainWindow.proxyMode];

            TextBlockRoute.Text = mainWindow.routingRuleSets[mainWindow.selectedRoutingSet]["name"].ToString();
            TextBlockLogLevel.Text = mainWindow.logLevel;
            List<string> tag = new List<string>();
            if (mainWindow.usePartServer)
            {
                foreach (int id in mainWindow.selectedPartServerIndex)
                {
                    tag.Add(mainWindow.profiles[id]["tag"].ToString());
                }
            }
            else if (mainWindow.useMultipleServer)
            {
                foreach (Dictionary<string, object> tg in mainWindow.profiles)
                {
                    tag.Add(tg["tag"].ToString());
                }
            }
            else
            {
                tag.Add(mainWindow.profiles[mainWindow.selectedServerIndex]["tag"].ToString());
            }
            TextBlockOutBounds.Text = String.Join(",", tag);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            mainWindow.httpPort = grid.HTTP;
            mainWindow.localPort = grid.SOCKS5;
            mainWindow.logLevel = Utilities.LOG_LEVEL_LIST[grid.LogLevelIndex];
            mainWindow.dnsString = grid.DNS;
            mainWindow.bypass = grid.Bypass;
            mainWindow.subscriptionUrl = grid.Subscription.Split(new[] { '\r', '\n' }).Select(line => line.Trim()).Where(line => line.Length > 0).ToList();
        }
    }
}
