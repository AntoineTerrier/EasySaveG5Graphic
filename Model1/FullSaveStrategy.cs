using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualBasic.FileIO;
using System.Linq;

public class FullSaveStrategy : ISaveStrategy
{
    public void myProcess_Exited(object sender, System.EventArgs e)
    {
        Console.WriteLine(
            $"Durée du cryptage : {((Process)sender).ExitTime - ((Process)sender).StartTime}");
    }
    public string Save(string sourceDir, string targetDir, Semaphore MaxSizeSemaphore)
    {
        try
        {
            //Folder naming
            DirectoryInfo dir1 = new DirectoryInfo(sourceDir);
            IEnumerable<FileInfo> list1 = dir1.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
            string dateDay = DateTime.Now.ToString("dd");
            string dateMonth = DateTime.Now.ToString("MM");
            string dateYear = DateTime.Now.ToString("yyyy");
            string dateHour = DateTime.Now.ToString("HH");
            string dateMin = DateTime.Now.ToString("mm");
            string dateSec = DateTime.Now.ToString("ss");
            string folderName = "\\" + dateYear + "-" + dateMonth + "-" + dateDay + "_" + dateHour + "h" + dateMin + "min"+ dateSec + "-FullSave";

            string[] filesListSource = Directory.GetFiles(sourceDir, "*.*", System.IO.SearchOption.AllDirectories);
            string[] extensions = File.ReadAllLines("ExtensionFile.txt");

            FileSort fileSort = new FileSort();
            List<FileInfo> prioList = fileSort.PriorizeList(list1);

            TimeSpan cryptingTime;
            cryptingTime = TimeSpan.Zero;

            StateFile statefile = new StateFile();
            Save save = new Save();


            //If there are files to transfer
            if (filesListSource.Length != 0)
            {
                // Create the save directory
                Directory.CreateDirectory(targetDir + folderName);

                DateTime D1 = DateTime.Now;
                // Create all the Directories needed
                foreach (string dirPath in Directory.GetDirectories(sourceDir, "*", System.IO.SearchOption.AllDirectories))
                { Directory.CreateDirectory(dirPath.Replace(sourceDir, targetDir + folderName)); }


                // Copy all the files in the associated directory
                foreach (var file in prioList)
                {
                    
                    // Remove path from the file name.
                    string fileName = file.Name;
                    string vPath = file.FullName.Substring(sourceDir.Length + 1);
                    bool FileLargerParameter = false;

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
                            if(file.Extension != File.ReadAllText("PriorityFiles.txt"))
                            {
                                Controller.Barrier.SignalAndWait();
                            }
                            Process processus = new Process();
                            processus.StartInfo.FileName = Directory.GetCurrentDirectory() + "\\Cryptosoft.exe";
                            processus.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            Console.WriteLine("\nTransfert du fichier " + fileName + "...");
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
            else
            {
                Console.WriteLine("No files to copy");
            }
            return folderName;
        }
        catch (DirectoryNotFoundException dirNotFound)
        {
            Console.WriteLine(dirNotFound.Message);
            return "error";
        }
    }
}
