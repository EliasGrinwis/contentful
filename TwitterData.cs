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

    public List<String> twitter_profilePictures_list_function()
    {
        var profilePictures = new List<string> {
        "https://pbs.twimg.com/profile_images/1574797189951397889/tSjb4iZZ_400x400.jpg",
        "https://pbs.twimg.com/profile_images/1243918575112531968/0efRkgVU_400x400.png",
        "https://pbs.twimg.com/profile_images/1574811963024195585/zAqk41JB_400x400.jpg",
        "https://pbs.twimg.com/profile_images/1574491879047151631/Ih4ISTbT_400x400.jpg",
        "https://abs.twimg.com/sticky/default_profile_images/default_profile_400x400.png",
        "https://pbs.twimg.com/profile_images/1575027994824941574/hH8OviHJ_400x400.jpg",
        "https://pbs.twimg.com/profile_images/1354358986439593984/r20mt5lK_400x400.jpg",
        "https://pbs.twimg.com/profile_images/1576229345626046467/9mJp5QhR_400x400.jpg",
        "https://pbs.twimg.com/profile_images/1576507818441596928/xMKDEDe4_400x400.jpg",
        "https://pbs.twimg.com/profile_images/1576993821606514695/o-Zh6w3z_400x400.jpg",
        "https://pbs.twimg.com/profile_images/1575187416859787270/oQQUSomK_400x400.jpg"
        };

        return profilePictures;

    }
}



