﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KinectToolkit;
using CsPotrace;
using System.Diagnostics;
using System.Threading;
using System.Drawing;

namespace LapinCretinsFormes
{
    /// <summary>s
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IUserControlContainer
    {

        private UserControl loadedContent;

        public MainWindow()
        {
            InitializeComponent();
            LoadContent(new MainMenuUserControl(this, new GameManager()));
            //LoadContent(new EmailInputUserControl(this, null));
        }

        public void LoadContent(UserControl content)
        {
            this.loadedContent = content;
            ContentOnWindow.Content = content;
        }
    }
}
