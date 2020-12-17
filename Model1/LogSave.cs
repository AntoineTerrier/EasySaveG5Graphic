using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


public class LogSave
{
    public string Name { get; set; }
    public string SourceDir { get; set; }
    public string TargetDir { get; set; }
    public string TransferTime { get; set; }
    public DateTime Date { get; set; }
    public string FolderSize { get; set; }
    public string CryptingTime { get; set; }
    public LogSave()
    {
    }
    public void CreateLog(string name, string sourceDir, string targetDir, TimeSpan transferTime, TimeSpan cryptingTime)
    {
        var dirSource = new DirectoryInfo(sourceDir);
        var sourceSize = dirSource.GetFiles();
        long sourceFolderSize = 0;
        foreach (var item in sourceSize)
        {
            sourceFolderSize += item.Length;
        }

        this.Name = name;
        this.SourceDir = sourceDir;
        this.TargetDir = targetDir;
        this.Date = DateTime.Now;
        this.FolderSize = (sourceFolderSize) / (1000000) + "Mo";
        this.TransferTime = transferTime.Minutes + " minutes " + transferTime.Seconds + "." + transferTime.Milliseconds + " sec";
        this.CryptingTime = cryptingTime.Minutes + " minutes " + cryptingTime.Seconds + "." + cryptingTime.Milliseconds + " sec";
    }
    public void UpdateLogFile(LogSave logSave)
    {
        string dateDay = DateTime.Now.ToString("dd");
        string dateMonth = DateTime.Now.ToString("MM");
        string dateYear = DateTime.Now.ToString("yyyy");
        string dateHour = DateTime.Now.ToString("HH");
        string dateMin = DateTime.Now.ToString("mm");
        if (File.Exists(Environment.CurrentDirectory + "\\logfile_" + dateYear + dateMonth + dateDay + ".json"))
        {
            string json = JsonConvert.SerializeObject(logSave, Formatting.Indented);
            File.AppendAllText(Environment.CurrentDirectory + "\\logfile_" + dateYear + dateMonth + dateDay + ".json", json);
        }
        else
        {
            var file = File.Create(Environment.CurrentDirectory + "\\logfile_" + dateYear + dateMonth + dateDay + ".json");
            file.Close();
            File.AppendAllText(file.Name, JsonConvert.SerializeObject(logSave, Formatting.Indented));


        }
    }
}