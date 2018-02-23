using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using SystemProgramming_WPF_01.Model_DataBaseForBrowser;
using Microsoft.CSharp;
using Microsoft.Win32;
using Process = System.Diagnostics.Process;
using ProcessStartInfo = System.Diagnostics.ProcessStartInfo;

namespace SystemProgramming_WPF_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Process> proc = Process.GetProcesses().OrderBy(o => o.Threads.Count).ToList();
        private static ProcessesDb DB = new ProcessesDb();
        CancellationTokenSource cts = new CancellationTokenSource();
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            /*
            // Для отлавливания события при закрытии хрома и записи в базу данных информации
            */
            InitializeComponent();
            //process
            //processinfo
            //processred
            //processModule
            ProccessList.ItemsSource = proc;
            Process.GetProcesses().OrderBy(o => o.ProcessName);
            timer.Interval = TimeSpan.FromMilliseconds(750);
            timer.Tick += OnTimerTick;
            timer.Start();
            //GetProc(true, 500, null);
        }
        private async void GetProc(bool flag, int count, string orderBy)
        {
            for (int i = 1; i > 0; i++)
            {
                if (flag == false)
                {
                    cts.Cancel();
                    break;
                }
                await GetProc_(count, orderBy);
            }

            // Thread.Sleep(count);
        }
        private void Sas(object sender, EventArgs e)
        {
            List<Process> pp = Process.GetProcesses().ToList();
            var a = proc.Where(w => w.ProcessName == "browser" || w.ProcessName == "chrome").Select(s => new
            {
                s.ProcessName,
                s.Id,
                s.WorkingSet64
            }).ToList();
            if (a.Count != 0)
            {

                try
                {
                    List<Model_DataBaseForBrowser.Process> databaseprocesses = new List<Model_DataBaseForBrowser.Process>();
                    foreach (var proc in a)
                    {
                        databaseprocesses.Add(new Model_DataBaseForBrowser.Process() { ProcessId = proc.Id, ProcessName = proc.ProcessName, ProcessMemorySize = proc.WorkingSet64 / 1024 / 1024 });
                    }
                    MessageBox.Show("Записываем данныем в базу");
                    DB.Processes.AddRange(databaseprocesses);
                    DB.SaveChanges();
                    MessageBox.Show("Данные в базу были добавлены успешно! ");
                }
                catch (Exception)
                {
                    //ErrorOrSuccesTex.Text += ex;
                }
            }
        }
        private Task GetProc_(int count, string orderBy)
        {

            ProccessList.ItemsSource = null;
            switch (orderBy)
            {
                case null:
                    {

                        SetData(Process.GetProcesses().ToList());
                        List<Process> proclist = Process.GetProcesses().Where(w => w.ProcessName == "chrome" || w.ProcessName == "browser").ToList();
                        if (proclist.Count > 0)
                        {
                            if (DB.DataScienceTables.Any())
                            {
                                var dbproclist = DB.DataScienceTables.Max(m => m.MemorySize);
                                if (proclist.Any(w => w.WorkingSet64 / 1024 / 1024 > dbproclist * 2))
                                {
                                    Process a = proclist.FirstOrDefault(w =>
                                        w.WorkingSet64 / 1024 / 1024 > dbproclist * 2);
                                    if (a != null)
                                    {
                                        a.Kill();
                                        MessageBox.Show(
                                            "Внимание! Один из процессов хрома сильно нагружал систему , в целях предотвращения сбоев нам пришлось убить этот процесс!! ");
                                    }
                                }
                            }
                        }


                        return Task.Run(() =>
                        {
                            //cts.Cancel();

                            //Thread.Sleep(count);
                        });
                    }
                case "Name":
                    {
                        SetData(Process.GetProcesses().OrderBy(o => o.ProcessName).ToList());
                        return Task.Run(() =>
                        {

                        });
                    }
                case "Memory":
                    {
                        SetData(Process.GetProcesses().OrderBy(o => o.WorkingSet64).ToList());
                        return Task.Run(() =>
                        {

                        });
                    }
                case "ThreadsCount":
                    {
                        SetData(Process.GetProcesses().OrderBy(o => o.Threads.Count).ToList());
                        return Task.Run(() =>
                        {

                        });
                    }
                default:
                    SetData(Process.GetProcesses().ToList());
                    return Task.Run(() =>
                    {

                    });

            }

        }
        private void SetData(List<Process> p)
        {

            ProccessList.ItemsSource = p;
        }
        private async void OnTimerTick(object sender, object e)
        {
            //List<Process> pp = Process.GetProcesses().OrderBy(o => o.ProcessName).ToList();
            //ProccessList.ItemsSource = pp;
            //Process p = new Process();
            await GetProc_(1200, null);

        }
        private async void OrderByName(object sender, object e)
        {
            await GetProc_(1200, "Name");
        }
        private void ProccessList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Process b = (Process)ProccessList.SelectedItem;
                ProcessThreadCollection c = b.Threads;
                ThreadsList.ItemsSource = c;
            }
            catch (Exception ex)
            {

                ErrorOrSuccesTex.Text += ex;
            }

        }
        private void ProccessList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Process a = (Process)ProccessList.SelectedItem;
                a.Kill();
                ProccessList.ItemsSource = Process.GetProcesses().OrderBy(o => o.ProcessName);
            }
            catch (Exception ex)
            {
                ErrorOrSuccesTex.Text += ex;
            }
        }
        private void CheckForavaliable_OnClick(object sender, RoutedEventArgs e)
        {
            Process chrome = Process.GetProcesses().FirstOrDefault(f => f.ProcessName == "chrome");
            if (chrome != null)
            {
                chrome.EnableRaisingEvents = true;
                proc = Process.GetProcesses().OrderBy(o => o.ProcessName).ToList();
                ErrorOrSuccesTex.Text = "Процесс найден!";
                chrome.Exited += Sas;
            }
            else
            {
                ErrorOrSuccesTex.Text = "Процесс не найден!";
            }
        }
        private void FindProcessButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(FindProcessTextBox.Text))
                {
                    // GetProc(false, 500, null);
                    timer.Tick -= null;
                    timer.Tick += GetProcessesByName;
                }
            }
            catch (Exception ex)
            {
                ErrorOrSuccesTex.Text += ex;
            }

        }
        private void GetProcessesByName(object sender, object e)
        {
            ProccessList.ItemsSource = Process.GetProcesses().Where(w => w.ProcessName == FindProcessTextBox.Text || w.ProcessName.Contains(FindProcessTextBox.Text)).ToList();
        }
        private void OrderByNamesButton_Checked(object sender, RoutedEventArgs e)
        {
            timer.Tick -= null;
            timer.Tick += OrderByName;
            timer.Start();
        }
        private void OrderByMemoryButton_Checked(object sender, RoutedEventArgs e)
        {
            timer.Tick -= null;
            timer.Tick += OrderByMemory;

        }
        private async void OrderByMemory(object sender, object e)
        {
            await GetProc_(1200, "Memory");
        }

        private void OrderByThreadsCount_Checked(object sender, RoutedEventArgs e)
        {
            timer.Tick -= null;
            timer.Tick += OrderByThreadsCount_;
            timer.Start();
        }
        private async void OrderByThreadsCount_(object sender, object e)
        {
            await GetProc_(1200, "ThreadsCount");
        }

        private void LearnDataBAse_Click(object sender, RoutedEventArgs e)
        {
            int k = 10;
            while (k > 0)
            {
                List<Process> browser = Process.GetProcesses().Where(f => f.ProcessName == "chrome" || f.ProcessName == "browser").ToList();
                if (browser.Count > 0)
                {
                    ErrorOrSuccesTex.Text += "Процессы найдены";
                    var antype = browser.Where(w => w.WorkingSet64 / 1024 / 1024 > 90).OrderBy(o => o.WorkingSet64).Select(s => new
                    {
                        s.ProcessName,
                        s.WorkingSet64,
                        s.Id
                    });
                    List<Model_DataBaseForBrowser.Process> l = new List<Model_DataBaseForBrowser.Process>();
                    foreach (var item in antype)
                    {
                        l.Add(new Model_DataBaseForBrowser.Process() { ProcessId = item.Id, ProcessName = item.ProcessName, ProcessMemorySize = item.WorkingSet64 / 1024 / 1024 });
                    }
                    try
                    {
                        DB.Processes.AddRange(l);
                        DB.SaveChanges();
                        ErrorOrSuccesTex.Text += ("Система была обучена!");
                        ErrorOrSuccesTex.Text = null;
                        k--;
                    }
                    catch (Exception ex)
                    {
                        ErrorOrSuccesTex.Text += ex;
                        break;
                    }


                }
                else
                {
                    ErrorOrSuccesTex.Text = "Ни одного процесса не было найдено)))";
                    break;
                }
            }
            MessageBox.Show("Система была обучена если в строке exceptiona пусто!)");
        }

        private void OpenFileDialogButton_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.DefaultExt = ".exe";
            file.Filter = "Only exe files (.exe)|*.exe";
            bool? resultDialog = file.ShowDialog();
            if (resultDialog == true)
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = file.FileName;
                try
                {
                    Process.Start(info);
                    MessageBox.Show("Процесс был запущен успешно!");
                }
                catch (Exception ex)
                {
                    ErrorOrSuccesTex.Text += ex;
                }

            }


        }

        private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
        {
            Task2ErrorOrSuccessTextBlock.Text = CodeTextBox.ActualHeight.ToString(CultureInfo.InvariantCulture);
            if (CodeTextBox.ActualHeight > MainWindowForResize.ActualHeight - 190)
            {
                MainWindowForResize.Height = MainWindowForResize.Height + 30;
            }

            else if (CodeTextBox.ActualHeight < MainWindowForResize.ActualHeight - 230)
            {
                MainWindowForResize.Height = MainWindowForResize.Height - 30;
            }else if (e.Key == Key.F5)
            {
                var csc = new CSharpCodeProvider(new Dictionary<string, string>() { });
                var parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll" }, "test.exe", true);
                parameters.GenerateExecutable = true;
                string code = CodeTextBox.Text;
                CompilerResults results = csc.CompileAssemblyFromSource(parameters, code);

                results.Errors.Cast<CompilerError>().ToList().ForEach(error => Task2ErrorOrSuccessTextBlock.Text += (error.ErrorText));
                CompilerParameters compilerParams = new CompilerParameters();
                string outputDirectory = Directory.GetCurrentDirectory();

                compilerParams.GenerateInMemory = true;
                compilerParams.TreatWarningsAsErrors = false;
                compilerParams.GenerateExecutable = true;
                compilerParams.CompilerOptions = "/optimize";
                compilerParams.OutputAssembly = "test.exe";

                string[] references = { "System.dll" };
                compilerParams.ReferencedAssemblies.AddRange(references);

                CSharpCodeProvider provider = new CSharpCodeProvider();
                CompilerResults compile = provider.CompileAssemblyFromSource(compilerParams, code);
                Process.Start(outputDirectory + @"\test.exe");
            }
        }

        private void StartCodeButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {

               var csc = new CSharpCodeProvider(new Dictionary<string, string>() { });
                var parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll" }, "test.exe", true);
                parameters.GenerateExecutable = true;
                string code = CodeTextBox.Text;
                CompilerResults results = csc.CompileAssemblyFromSource(parameters, code);

                results.Errors.Cast<CompilerError>().ToList().ForEach(error => Task2ErrorOrSuccessTextBlock.Text+=(error.ErrorText));
                CompilerParameters compilerParams = new CompilerParameters();
                string outputDirectory = Directory.GetCurrentDirectory();

                compilerParams.GenerateInMemory = true;
                compilerParams.TreatWarningsAsErrors = false;
                compilerParams.GenerateExecutable = true;
                compilerParams.CompilerOptions = "/optimize";
                compilerParams.OutputAssembly = "test.exe";

                string[] references = { "System.dll" };
                compilerParams.ReferencedAssemblies.AddRange(references);

                CSharpCodeProvider provider = new CSharpCodeProvider();
                CompilerResults compile = provider.CompileAssemblyFromSource(compilerParams, code);
                Process.Start(outputDirectory+@"\test.exe");
            }
            catch (Exception ex)
            {
                Task2ErrorOrSuccessTextBlock.Text += ex;

            }
        }
    }
}