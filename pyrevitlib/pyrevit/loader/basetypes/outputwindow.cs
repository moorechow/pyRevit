using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;


namespace PyRevitBaseClasses
{
    public partial class ScriptOutput : Window, IComponentConnector, IDisposable
    {

        private bool _contentLoaded;

        System.Windows.Forms.Integration.WindowsFormsHost host;
        public System.Windows.Forms.WebBrowser renderer;

        public System.Windows.Forms.WebBrowserNavigatingEventHandler _navigateHandler;
        public delegate void CustomProtocolHandler(String url);
        public CustomProtocolHandler UrlHandler;

        public string OutputId;


        public ScriptOutput()
        {
            InitializeComponent();
        }


        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;

            this.Loaded += Window_Loaded;
            this.Closing += Window_Closing;

            host = new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the WebBrowser control.
            renderer = new System.Windows.Forms.WebBrowser();

            _navigateHandler = new System.Windows.Forms.WebBrowserNavigatingEventHandler(renderer_Navigating);
            renderer.Navigating += _navigateHandler;

            //renderer.DocumentCompleted +=
                // new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(renderer_DocumentCompleted);

            renderer.DocumentText = String.Format("{0}<html><body></body></html>", ExternalConfig.doctype);
            while (renderer.Document.Body == null)
                System.Windows.Forms.Application.DoEvents();

            // Setup body style
            renderer.Document.Body.Style = ExternalConfig.htmlstyle;

            // Assign the WebBrowser control as the host control's child.
            host.Child = renderer;

            // Add the interop host control to the Grid
            // control's collection of child controls.
            Grid baseGrid = new Grid();
            baseGrid.Children.Add(host);
            this.Content = baseGrid;

            // Setup window styles
            this.Background = Brushes.White;
            this.Width = 900;
            this.Height = 600;
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Title = "pyRevit";
        }


        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            this._contentLoaded = true;
        }


        public void AppendToOutputList()
        {
            var outputList = (List<object>)AppDomain.CurrentDomain.GetData(EnvDictionaryKeys.outputWindows);
            if (outputList == null)
            {
                var newOutputList = new List<object>();
                newOutputList.Add(this);

                AppDomain.CurrentDomain.SetData(EnvDictionaryKeys.outputWindows, newOutputList);
            }
            else
            {
                outputList.Add(this);
            }
        }


        public void RemoveFromOutputList()
        {
            var outputList = (List<object>)AppDomain.CurrentDomain.GetData(EnvDictionaryKeys.outputWindows);
            if (outputList == null)
            {
                return;
            }
            else
            {
                if (outputList.Contains(this))
                {
                    outputList.Remove(this);
                }
            }
        }


        public void WaitReadyBrowser()
        {
            System.Windows.Forms.Application.DoEvents();
        }


        public void LockSize()
        {
            this.ResizeMode = ResizeMode.NoResize;
        }


        public void UnlockSize()
        {
            this.ResizeMode = ResizeMode.CanResizeWithGrip;
        }


        public void ScrollToBottom()
        {
            if (renderer.Document != null)
            {
                renderer.Document.Window.ScrollTo(0, renderer.Document.Body.ScrollRectangle.Height);
            }
        }


        public void FocusOutput()
        {
            renderer.Focus();
        }


        public void AppendText(String OutputText, String HtmlElementType)
        {
            WaitReadyBrowser();
            var div = renderer.Document.CreateElement(HtmlElementType);
            div.InnerHtml = OutputText;
            renderer.Document.Body.AppendChild(div);
            ScrollToBottom();
        }


        //private void renderer_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        //{
        //}


        private void renderer_Navigating(object sender, System.Windows.Forms.WebBrowserNavigatingEventArgs e)
        {
            if (!(e.Url.ToString().Equals("about:blank", StringComparison.InvariantCultureIgnoreCase)))
            {
                var commandStr = e.Url.ToString();
                if (commandStr.StartsWith("http"))
                {
                    System.Diagnostics.Process.Start(e.Url.ToString());
                }
                else
                {
                    UrlHandler(e.Url.OriginalString);
                }

                e.Cancel = true;
            }
        }


        public void ShowProgressBar()
        {
            WaitReadyBrowser();
            if (renderer.Document != null)
            {
                var pbar = renderer.Document.CreateElement(ExternalConfig.progressbar);
                var pbargraph = renderer.Document.CreateElement("div");
                pbargraph.Id = ExternalConfig.progressbargraphid;
                pbargraph.Style = String.Format(ExternalConfig.progressbargraphstyle, 10);
                pbar.AppendChild(pbargraph);
                renderer.Document.Body.AppendChild(pbar);
            }
        }


        public void UpdateProgressBar(float curValue, float maxValue)
        {
            WaitReadyBrowser();
            if (renderer.Document != null)
            {
                var pbargraph = renderer.Document.GetElementById(ExternalConfig.progressbargraphid);
                if (pbargraph == null)
                {
                    ShowProgressBar();
                    pbargraph = renderer.Document.GetElementById(ExternalConfig.progressbargraphid);
                }
                pbargraph.Style = String.Format(ExternalConfig.progressbargraphstyle, (curValue / maxValue) * 100);
            }
        }


        public void SelfDestructTimer(int miliseconds)
        {
            // Create a 30 min timer
            var timer = new System.Timers.Timer(miliseconds);
            // Hook up the Elapsed event for the timer.
            timer.Elapsed += (sender, e) => SelfDestructTimerEvent(sender, e, this);
            timer.Enabled = true;
        }


        private static void SelfDestructTimerEvent(object source, System.Timers.ElapsedEventArgs e, ScriptOutput output_window)
        {
            output_window.Close();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AppendToOutputList();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RemoveFromOutputList();

            var grid = (Grid)this.Content;
            grid.Children.Remove(host);

            renderer.Navigating -= _navigateHandler;
            renderer.Dispose();
        }

        public void Dispose()
        {
            _navigateHandler = null;
            UrlHandler = null;
            renderer = null;
            this.Content = null;
        }
    }
}
