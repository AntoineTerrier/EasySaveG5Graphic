using System;
using System.IO;

public class Save
{
    public string Name { get; set; }
    public string SourceDir { get; set; }
    public string TargetDir { get; set; }
    public string Progression { get; set; }
    public string State { get; set; }
    public int FileNumber { get; set; }
    public int RemainingFiles { get; set; }
    public string SizeRemainingFiles { get; set; }
    public string FolderSize { get; set; }
    public DateTime Date { get; set; }
    public string NameFile { get; set; }
    //public int TransferTime { get; set; }

    public Save()
    {
    }
    public void SaveUpdate(string name, string sourceDir, string targetDir, string fileName)
    {
        string[] sourceFilesList = Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories);
        string[] targetFilesList = Directory.GetFiles(targetDir+name, "*.*", SearchOption.AllDirectories);
        long sourceFolderSize = 0;
        long targetFolderSize = 0;
        foreach (var item in sourceFilesList)
        {
            sourceFolderSize += item.Length;
        }
        foreach (var item in targetFilesList)
        {
            targetFolderSize += item.Length;

        }


        this.Name = name;
        this.Date = DateTime.Now;
        this.SourceDir = sourceDir;
        this.TargetDir = targetDir;
        this.FileNumber = sourceFilesList.Length;
        long sizeRemaining = (sourceFolderSize -targetFolderSize) / (1000000);
        this.RemainingFiles = FileNumber - targetFilesList.Length;
        this.SizeRemainingFiles = sizeRemaining + "Mo";

        this.NameFile = fileName;
        if (sourceFolderSize == 0)
        {
            this.Progression = "100%";
        }
        else
        {
            this.Progression = (targetFolderSize * 100) / (sourceFolderSize) + "%";
        }
        long folderSize = (sourceFolderSize) / (1000000);
        this.FolderSize = folderSize + "Mo";
        if (sizeRemaining == 0)
        {
            this.State = "finish";
        }
        else
        {
            this.State = "running";
        }
    }
}
