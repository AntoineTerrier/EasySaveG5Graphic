using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualBasic.FileIO;

public class DiffSaveStrategy : ISaveStrategy
{
    private string fullSaveDir;

    public DiffSaveStrategy(string fullSaveDir)
    {
        this.fullSaveDir = fullSaveDir;
    }
    public void myProcess_Exited(object sender, System.EventArgs e)
    {
        Console.WriteLine(
            $"Durée du cryptage : {((Process)sender).ExitTime - ((Process)sender).StartTime}");
    }
    public string Save(string sourceDir, string targetDir, Semaphore MaxSizeSemaphore)
    {

        // Compare 2 directories
        System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(sourceDir);
        System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(fullSaveDir);

        IEnumerable<System.IO.FileInfo> list1 = dir1.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
        IEnumerable<System.IO.FileInfo> list2 = dir2.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

        // Create a list with the files to save
        FileCompare myFileCompare = new FileCompare();
        var queryList1Only = (from file in list1 select file).Except(list2, myFileCompare);

        // Create the folder name
        string dateDay = DateTime.Now.ToString("dd");
        string dateMonth = DateTime.Now.ToString("MM");
        string dateYear = DateTime.Now.ToString("yyyy");
        string dateHour = DateTime.Now.ToString("HH");
        string dateMin = DateTime.Now.ToString("mm");
        string dateSec = DateTime.Now.ToString("ss");
        string folderName = "\\" + dateYear + "-" + dateMonth + "-" + dateDay + "_" + dateHour + "h" + dateMin + "min" + dateSec + "-FullSave";
        string[] extensions = File.ReadAllLines("ExtensionFile.txt");
        TimeSpan cryptingTime;
        cryptingTime = TimeSpan.Zero;
        StateFile statefile = new StateFile();
        Save save = new Save();


        FileSort fileSort = new FileSort();
        queryList1Only = fileSort.PriorizeList(queryList1Only);

        // If there are files to save 
        if (queryList1Only != null)
        {
            // Create the save directory
            Directory.CreateDirectory(targetDir + folderName);

            DateTime D1 = DateTime.Now;
            // Create all the needed directories
            foreach (string dirPath in Directory.GetDirectories(sourceDir, "*", System.IO.SearchOption.AllDirectories))
            { Directory.CreateDirectory(dirPath.Replace(sourceDir, targetDir + folderName)); }


            foreach (var file in queryList1Only)
            {
                Console.WriteLine(file.FullName);

                string fileName = file.Name;
                string vPath = file.FullName.Substring(sourceDir.Length + 1);
                bool FileLargerParameter = false;

                //Substring(sourceDir.Length + 1);


                if (File.Exists("FileSizeLimit.txt"))
                {
                    if (FileSystem.GetFileInfo($"{sourceDir}/{vPath}").Length > Convert.ToInt64(File.ReadAllText($"{Environment.CurrentDirectory}/FileSizeLimit.txt")))
                    {

                        MaxSizeSemaphore.WaitOne();
                        FileLargerParameter = true;

                    }
                }


                try
                {
                    bool file2Crypt = false;
                    foreach (var ext in extensions)
                    {
                        if (ext == file.Extension)
                        {
                            file2Crypt = true;
                        }
                    }

                    if (file2Crypt == true)
                    {
                        while (Controller.EnterpriseSoftwareRunning() == true) { };
                        Process processus = new Process();
                        processus.StartInfo.FileName = Directory.GetCurrentDirectory() + "\\Cryptosoft.exe";
                        processus.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        Console.WriteLine("Transfert du fichier " + fileName + "...");
                        processus.StartInfo.Arguments = "-c " + sourceDir + "\\" + vPath + " fe3a2d57c378d7dc946589e9aa8cee011cae8013 " + targetDir + folderName + "\\" + vPath;
                        processus.EnableRaisingEvents = true;
                        processus.Exited += new EventHandler(myProcess_Exited);
                        processus.Start();
                        Console.WriteLine("Cryptage du fichier" + fileName + " en cours...");
                        processus.WaitForExit();
                        cryptingTime += ((Process)processus).ExitTime - ((Process)processus).StartTime;
                    }
                    else
                    {
                        file.CopyTo(targetDir + "//" + folderName + "//" + vPath);
                    }
                    save.SaveUpdate(folderName, sourceDir, targetDir, fileName);
                    statefile.UpdateStateFile(save);
                }

                // Catch exception if the file was already copied.
                catch (IOException copyError)
                {
                    Console.WriteLine(copyError.Message);
                    return "error";
                }
                if (FileLargerParameter)
                {

                    MaxSizeSemaphore.Release();


                }
            }
            // Calculate the transfer time
            DateTime D2 = DateTime.Now;
            TimeSpan transferTime = D2 - D1;
            Console.WriteLine("Le temps de transfert total des fichiers a été de " + transferTime);
            Console.WriteLine("Le temps de cryptage total des fichiers a été de " + cryptingTime);
            // Create and update the logs file
            LogSave logSave = new LogSave();
            logSave.CreateLog(folderName, sourceDir, targetDir, transferTime, cryptingTime);
            logSave.UpdateLogFile(logSave);
        }

        return folderName;

    }
    
}
