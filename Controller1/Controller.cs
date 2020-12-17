using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

public sealed class Controller
{
    public static Semaphore MaxSizeSemaphore;

    private static Controller instance = null;
    public static string Lang { get; set; }
    public static int cmb { get; set; }
    public static Barrier Barrier { get; set; }
    private static readonly object padlock = new object();
    public Controller()
    {

        MaxSizeSemaphore = new Semaphore(1, 1);

    }
    public static Controller Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Controller();
                }
                return instance;
            }
        }
    }

    //public bool MainMenu(Controller controller)
    //{
    //    bool menu = true;
    //    while(menu == true)
    //    {
    //        view.ShowMenu(controller);
    //    }
    //    return true;
    //}

    public void doFullSave(string source, string target)
    {
        FullSaveStrategy fullSaveStrategy = new FullSaveStrategy();
        fullSaveStrategy.Save(source, target, MaxSizeSemaphore);
        Barrier.AddParticipant();
    }
    public void doDiffSave(string fullSaveDir, string source, string target)
    {
        DiffSaveStrategy diffSaveStrategy = new DiffSaveStrategy(fullSaveDir);
        diffSaveStrategy.Save(source, target, MaxSizeSemaphore);
        Barrier.AddParticipant();

    }
    public static bool EnterpriseSoftwareRunning()
    {
        if (Process.GetProcessesByName("Calculator").Length > 0)
        {
            string message = "You have to close your enterprise software if you want to continue the backup.\n Do you want to close it ?";
            string caption = "EasySave";
            var result = System.Windows.MessageBox.Show(message, caption,
                System.Windows.MessageBoxButton.YesNo);
            switch (result)
            {
                case System.Windows.MessageBoxResult.Yes:
                    Process[] proc = Process.GetProcessesByName("Calculator");
                    if (proc.Length == 0)
                    {
                        System.Windows.MessageBox.Show("The software has been closed");
                    }
                    else
                    {
                        proc[0].Kill();
                        System.Windows.MessageBox.Show("The software has been closed");
                    }
                    break;
                case System.Windows.MessageBoxResult.No:
                    EnterpriseSoftwareRunning();
                    break;
            }
            return true;

        }

        return false;
    }


}
