using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class StateFile
{
    public StateFile()
    {
        string date = DateTime.Now.ToString("F");
    }
    public void UpdateStateSave(Save save)
    {
        UpdateStateFile(save);
    }
    public void UpdateStateFile(Save save)
    {
        if(Directory.Exists(Environment.CurrentDirectory + "\\state.json"))
        {
            File.WriteAllText(Environment.CurrentDirectory + "\\state.json", JsonConvert.SerializeObject(save, Formatting.Indented));
        }
        else
        {
            var file = File.Create(Environment.CurrentDirectory + "\\state.json");
            file.Close();
            File.WriteAllText(file.Name, JsonConvert.SerializeObject(save, Formatting.Indented));

        }
    }
    //see about target and source repository

}
