using System.Diagnostics;
using Newtonsoft.Json;
using Project.Models;

public class TwitterData
{

    public String execute_powershell_script()
    {
        Process p = new Process();
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "powershell.exe";
        startInfo.Arguments = "-file Script.ps1";
        p.StartInfo = startInfo;
        p.StartInfo.RedirectStandardOutput = true;
        p.Start();
        string output = p.StandardOutput.ReadToEnd();

        return output;
    }


    public List<String> twitter_id_list_function()
    {
        //ManageData manageData = new ManageData();
        //var output = manageData.test_powershell();
        var output = execute_powershell_script();

        var twitterUsers = JsonConvert.DeserializeObject<List<TwitterUser>>(output); // list

        List<string> twitter_id_list = new List<string>();


        foreach (TwitterUser item in twitterUsers!) // Twitter user_id
        { // Loop through List with foreach
            twitter_id_list.Add(item.id!);
        }

        return twitter_id_list;

    }

    public List<String> twitter_name_list_function()
    {
        //ManageData manageData = new ManageData();
        //var output = manageData.test_powershell();
        var output = execute_powershell_script();

        var twitterUsers = JsonConvert.DeserializeObject<List<TwitterUser>>(output); // list

        List<string> twitter_name_list = new List<string>();


        foreach (TwitterUser item in twitterUsers!) // Twitter name
        { // Loop through List with foreach
            twitter_name_list.Add(item.name!);
        }

        return twitter_name_list;

    }

    public List<String> twitter_username_list_function()
    {
        //ManageData manageData = new ManageData();
        //var output = manageData.test_powershell();
        var output = execute_powershell_script();

        var twitterUsers = JsonConvert.DeserializeObject<List<TwitterUser>>(output); // list

        List<string> twitter_username_list = new List<string>();


        foreach (TwitterUser item in twitterUsers!) // Twitter username
        { // Loop through List with foreach
            twitter_username_list.Add(item.username!);
        }

        return twitter_username_list;
    }
}



