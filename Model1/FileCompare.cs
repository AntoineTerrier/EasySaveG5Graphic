using System;
using System.Collections.Generic;
using System.Text;

public class FileCompare : System.Collections.Generic.IEqualityComparer<System.IO.FileInfo>
{
    public FileCompare() { }

    public bool Equals(System.IO.FileInfo f1, System.IO.FileInfo f2)
    {
        // Checks if the 2 files have the same LastWriteTime and Length
        return (f1.Name == f2.Name &&
                f1.Length == f2.Length);
    }

    public int GetHashCode(System.IO.FileInfo fi)
    {
        string s = $"{fi.Name}{fi.Length}";
        return s.GetHashCode();
    }
}
