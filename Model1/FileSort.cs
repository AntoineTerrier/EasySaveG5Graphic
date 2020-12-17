using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

public class FileSort 
{
    public FileSort() { }

    public List<FileInfo> PriorizeList(IEnumerable<FileInfo> files) {


        if (!File.Exists("PriorityFiles.txt"))
        {
            return files.ToList();
        }


        else
        {
            string[] priorities = File.ReadAllLines("PriorityFiles.txt");
            List<FileInfo> sortedList = new List<FileInfo>();


            foreach (var file in files)
            {
                foreach (var extension in priorities)
                {
                    if (extension == file.Extension)
                    {
                        sortedList.Add(file);
                    }
                }
            }
            return sortedList.Concat(files.Except(sortedList)).ToList();
        }
    }
}
