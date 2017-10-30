﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using WinForms = System.Windows.Forms;

namespace BordersFilters.View
{
    /// <summary>
    /// Логика взаимодействия для ChoseAlgorithmView.xaml
    /// </summary>
    public partial class ChoseAlgorithmView : UserControl
    {

        public ChoseAlgorithmView()
        {
            InitializeComponent();
        }
        private readonly Regex _regex = new Regex("[^0-9]");

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (_regex.IsMatch(e.Text)) e.Handled = true;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new WinForms.FolderBrowserDialog
            {
                RootFolder = System.Environment.SpecialFolder.DesktopDirectory,
                ShowNewFolderButton = true
            };
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OutTextBox.Text = openFileDialog.SelectedPath;
            }
        }
    }
}
