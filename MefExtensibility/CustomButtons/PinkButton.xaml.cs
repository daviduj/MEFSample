﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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

namespace MefExtensibility
{
    /// <summary>
    /// Interaction logic for GreyButton.xaml
    /// </summary>
    /// 
    [Export(typeof(ICustomButton))]
    public partial class PinkButton : UserControl, ICustomButton
    {        

        public PinkButton()
        {
            InitializeComponent();
            
        }

        public Action ClickAction { get; set; }

        public Brush GetColor()
        {
            return PinkButton1.Background;
        }

        private void PinkButton1_Click(object sender, RoutedEventArgs e)
        {
            ClickAction.Invoke();
        }
    }
}
