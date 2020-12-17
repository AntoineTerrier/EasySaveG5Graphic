using System;
using System.Collections.Generic;
using System.Text;

public class View
{
    public View()
    {

    }

    public bool ShowMenu(Controller controller)
    {
        string source;
        string target;
        string fullsave;
        
        Console.WriteLine(" _________________________________________________");
        Console.WriteLine("|  ______                   _____                 |");
        Console.WriteLine("| |  ____|                 / ____|                |");
        Console.WriteLine("| | |__   __ _ ___ _   _  | (___   __ ___   _____ |");
        Console.WriteLine("| |  __| / _` / __| | | |  \\___ \\ / _` \\ \\ / / _ \\|");
        Console.WriteLine("| | |___| (_| \\__ \\ |_| |  ____) | (_| |\\ V /  __/|");
        Console.WriteLine("| |______\\__,_|___/\\__, | |_____/ \\__,_| \\_/ \\___||");
        Console.WriteLine("|                   __/ |                         |");
        Console.WriteLine("|                  |___/                          |");
        Console.WriteLine("|_________________________________________________|\n");
        Console.WriteLine("Choose an option");
        Console.WriteLine("1.Full Backup");
        Console.WriteLine("2.Differential Backup");
        Console.WriteLine("3.Exit");
        Console.WriteLine("4.Display an ASCII Unicorn");
        Console.Write("\r\nSelect an option: ");
        switch (Console.ReadLine()) //Menu
        {
            case "1":
                Console.WriteLine("Source Directory:");
                source = Console.ReadLine();
                Console.WriteLine("Target Directory:");
                target = Console.ReadLine();
                controller.doFullSave(source, target);
                return true;

            case "2":
                Console.WriteLine("Last full save Directory :");
                fullsave = Console.ReadLine();
                Console.WriteLine("Source Directory:");
                source = Console.ReadLine();
                Console.WriteLine("Target Directory:");
                target = Console.ReadLine();
                controller.doDiffSave(fullsave, source, target);
                return true;
            case "3":
                return false;
            case "4":
                Console.WriteLine(" _______\\)%%%%%%%%._              ");
                Console.WriteLine("`''''-'-;   % % % % %'-._         ");
                Console.WriteLine("        :b) \\            '-.      ");
                Console.WriteLine("        : :__)'    .'    .'       ");
                Console.WriteLine("        :.::/  '.'   .'           ");
                Console.WriteLine("        o_i/   :    ;             ");
                Console.WriteLine("               :   .'             ");
                Console.WriteLine("                ''`");
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                return true;
            default:
                return true;
        }
    }
}