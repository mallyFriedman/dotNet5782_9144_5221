using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using BlApi;
using SimulatorProject;
using BO;
using System.Windows.Media.Animation;

namespace PL
{

    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class SimulatorPl : Window
    {
        private Stopwatch stopWatch;
        private bool isTimerRun;
        BackgroundWorker Worker;
        Tuple<BO.Order, int, eOrderStatus> dcT;
        int seconds = 3;
        Duration duration;
        DoubleAnimation doubleanimation;
        ProgressBar ProgressBar;
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        public SimulatorPl()
        {
            InitializeComponent();
            Loaded += ToolWindow_Loaded;
            Worker = new BackgroundWorker();
            Worker.DoWork += Worker_DoWork;
            Worker.ProgressChanged += Worker_ProgressChanged;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            Worker.WorkerSupportsCancellation = true;
            Worker.WorkerReportsProgress = true;
            Worker.RunWorkerAsync();
            stopWatch = new Stopwatch();
            stopWatch.Restart();
            isTimerRun = true;

        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
           
            SimulatorProject.Simulator.ProgressChange += changeOrder;
            Simulator.StopSimulator += Stop;
            try
            {
                SimulatorProject.Simulator.Run();
                while (Worker.WorkerSupportsCancellation)
                {
                    Worker.ReportProgress(1);
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch (BlNoOrderToUpdateException ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            this.timerTextBlock.Text = timerText;
        }

        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (isTimerRun)
            {
                isTimerRun = false;
                SimulatorProject.Simulator.ProgressChange -= changeOrder;
                Simulator.StopSimulator -= Stop;
                Simulator.stopSimulator();
                stopWatch.Stop();
            }
        }

        private void changeOrder(object sender, EventArgs e)
        {
            if (!(e is Details))
                return;
            Details? details = e as Details;
            this.seconds = details.seconds;
            eOrderStatus nextStatus = details.order.OrderStatus == eOrderStatus.Ordered ? eOrderStatus.Shipped : eOrderStatus.Delivered;

            dcT = new Tuple<BO.Order, int, eOrderStatus>(details.order, details.seconds, nextStatus);
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(changeOrder, sender, e);
            }
            else
            {
                ProgressBarStart(seconds);
                DataContext = dcT;
            }
        }

        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {

            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }


        void ProgressBarStart(int sec)
        {
                if (ProgressBar != null)
                {
                    progressBar.Items.Remove(ProgressBar);
                }
                ProgressBar = new ProgressBar();
                ProgressBar.IsIndeterminate = false;
                ProgressBar.Orientation = Orientation.Horizontal;
                ProgressBar.Width = 500;
                ProgressBar.Height = 30;
                duration = new Duration(TimeSpan.FromSeconds(sec * 2));
                doubleanimation = new DoubleAnimation(200.0, duration);
                ProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
                progressBar.Items.Add(ProgressBar);
        }

        private void Stop(object sender, EventArgs e)
        {
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(Stop, sender, e);
            }
            else
            {
                if (Worker.WorkerSupportsCancellation == true)
                    Worker.CancelAsync();
                 MessageBox.Show("updating has completed");
                stopWatch.Stop();
                 this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isTimerRun)
            {
                isTimerRun = false;
                SimulatorProject.Simulator.stopSimulator();
                stopWatch.Stop();
            }
        }
    }
}
