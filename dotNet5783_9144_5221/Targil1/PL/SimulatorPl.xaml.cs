using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;
using SimulatorProject;
using System.Net.NetworkInformation;
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
        BackgroundWorker timerworker;
        BackgroundWorker Worker;
        Tuple<BO.Order, int, eOrderStatus> dcT;
        int seconds = 3;
        // Duration duration;
        // DoubleAnimation doubleanimation;
        // ProgressBar ProgressBar;
        public SimulatorPl()
        {
            InitializeComponent();
            Loaded += ToolWindow_Loaded;
            //simulator thred 
            Worker = new BackgroundWorker();
            Worker.DoWork += Worker_DoWork;
            Worker.ProgressChanged += Worker_ProgressChanged;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            Worker.WorkerSupportsCancellation = true;
            Worker.WorkerReportsProgress = true;
            Worker.RunWorkerAsync();
            //timer thred
            stopWatch = new Stopwatch();
            timerworker = new BackgroundWorker();
            timerworker.DoWork += Timer_DoWork;
            timerworker.ProgressChanged += Timer_ProgressChanged;
            timerworker.WorkerReportsProgress = true;
            stopWatch.Restart();
            isTimerRun = true;
            timerworker.RunWorkerAsync();

        }



        //void ProgressBarStart(int sec)
        //{
        //    if (ProgressBar != null)
        //    {
        //        SBar.Items.Remove(ProgressBar);
        //    }
        //    ProgressBar = new ProgressBar();
        //    ProgressBar.IsIndeterminate = false;
        //    ProgressBar.Orientation = Orientation.Horizontal;
        //    ProgressBar.Width = 500;
        //    ProgressBar.Height = 30;
        //    duration = new Duration(TimeSpan.FromSeconds(sec * 2));
        //    doubleanimation = new DoubleAnimation(200.0, duration);
        //    ProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
        //    SBar.Items.Add(ProgressBar);
        //}
        ////////////////////////
        private void stopTimerButton_Click(object sender, RoutedEventArgs e)
        {
            if (isTimerRun)
            {
                stopWatch.Stop();
                isTimerRun = false;
            }
        }

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;



        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {

            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void Timer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            this.timerTextBlock.Text = timerText;
        }
        private void Timer_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                timerworker.ReportProgress(1);
                Thread.Sleep(1000);

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SimulatorProject.Simulator.stopSimulator();
            stopTimerButton_Click(null, null);
            this.Close();
        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            SimulatorProject.Simulator.ProgressChange += changeOrder;
            try
            {
                SimulatorProject.Simulator.startSimulator();
            }
            catch (BlNoOrderToUpdateException ex)
            {
                MessageBox.Show(ex.Message);
            }

            while (Worker.WorkerSupportsCancellation)
            {
                System.Threading.Thread.Sleep(1000);
                Worker.ReportProgress(1);
            }
        }

        private void changeOrder(object sender, EventArgs e)
        {
            if (!(e is Details))
                return;
            Details? details = e as Details;
            this.seconds = details.seconds;
            //details.seconds = details.seconds;

            eOrderStatus nextStatus = details.order.OrderStatus == eOrderStatus.Ordered ? eOrderStatus.Shipped : eOrderStatus.Delivered;

            dcT = new Tuple<BO.Order, int, eOrderStatus>(details.order, details.seconds, nextStatus);
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(changeOrder, sender, e);
            }
            else
            {
                DataContext = dcT;
            }
        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;

            worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int a = 0;
            while (true)
            {
                (sender as BackgroundWorker).ReportProgress(a);
                Thread.Sleep(1000);
                a++;

            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value = e.ProgressPercentage;
        }


        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {

        }

        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void pbStatus_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }

}
