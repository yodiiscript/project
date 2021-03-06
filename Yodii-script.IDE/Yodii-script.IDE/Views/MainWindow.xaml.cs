﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using Yodii_script.IDE.Views;
using GUI;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using Yodii.Script;
using Yodii.Script.Debugger;
using Yodii_script.IDE.View_Models;
using System.Windows.Media.Imaging;

namespace Yodii_script.IDE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ScriptContext _scriptCon = new ScriptContext();
        List<bool?> _breakpoints = new List<bool?>();
        Watch _watches;
        Repl _console;


        public MainWindow()
        {
            InitializeComponent();
            LoadYodiiSyntax();
            LoadEditorConfig();
            Uri iconUri = new Uri( "../../../yodii_2.ico", UriKind.RelativeOrAbsolute );

            this.Icon = BitmapFrame.Create( iconUri );
        }
        private void LoadYodiiSyntax()
        {
            // Load a different syntax config file
            System.IO.StreamReader s = new System.IO.StreamReader( @"../../../ys.xshd" );
            {
                using( XmlTextReader reader = new XmlTextReader( s ) )
                {
                    this.ScriptEditor.SyntaxHighlighting = HighlightingLoader.Load( reader, HighlightingManager.Instance );
                }
            }
        }
        private void LoadEditorConfig()
        {
            this.ScriptEditor.ShowLineNumbers = true;
            this.ScriptEditor.LineNumbersForeground = new SolidColorBrush( Colors.Yellow );
            this.ScriptEditor.WordWrap = true;
            this.ScriptEditor.Background = new SolidColorBrush( Colors.Black );
            this.ScriptEditor.Foreground = new SolidColorBrush( Colors.White );
            BreakPointsMargin.Height = ScriptEditor.Height;
            ScriptEditor.TextArea.TextView.ScrollOffsetChanged +=TextView_ScrollOffsetChanged;
        }

        private void TextView_ScrollOffsetChanged( object sender, EventArgs e )
        {           
            sv1.ScrollToVerticalOffset(ScriptEditor.TextArea.TextView.ScrollOffset.Length);
        }

        /// <summary>
        /// Synchronizes the number of lines and breakpoints
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SyncMarginWithLines( object sender, EventArgs e )
        {
            BreakPointsMargin.Items.Clear();
            if (!String.IsNullOrEmpty(ScriptEditor.Text) )
            {
                Expr exp = ExprAnalyser.AnalyseString( ScriptEditor.Text );
                BreakableVisitor bkv = new BreakableVisitor();
                bkv.VisitExpr( exp );               
                foreach( var item in bkv.BreakableExprs )
                {
                    CheckBox cb = new CheckBox();
                    cb.Margin = new Thickness( 1 );
                    cb.IsChecked = false;
                    if( item == null )
                    {
                        _breakpoints.Add( null );
                        cb.IsEnabled = false;
                    }
                    else
                    {
                        _breakpoints.Add( false );
                        cb.IsEnabled = true;
                    }
                    BreakPointsMargin.Items.Add( cb );
                }
            }
        }

        internal void Debug_Click( object sender, RoutedEventArgs e )
        {
            if( _watches == null )
            {
                _watches = new Watch( this );
                DebugPanel.Children.Add( _watches );
                entry_Debug.Content = "Stop";
            }
            else if( _watches != null)
            {
                DebugPanel.Children.Remove( _watches );
                _watches = null;
                entry_Debug.Content = "Run";
            }
        }

        internal void StartConsole_Click( object sender, RoutedEventArgs e )
        {
            if( _console == null )
            {
                _console = new Repl( this );
                DebugPanel.Children.Add( _console );
            }
            else if( _console != null )
            {
                ((Panel)_console.Parent).Children.Remove( _console );
                _console = null;
            }
        }

        private void CheckBox_Checked( object sender, RoutedEventArgs e )
        {
            
        }

        private void ScriptCol_MouseDoubleClick( object sender, System.Windows.Input.MouseButtonEventArgs e )
        {
            MainWindowVM DataCont = (MainWindowVM)base.DataContext;
            var CommandToExec = DataCont.EditScriptCommand;
            CommandToExec.Execute( ScriptCol.SelectedItem );
        }
    }
}
