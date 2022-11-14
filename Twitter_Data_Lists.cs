using System.Diagnostics;
using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Models.Management;
using Newtonsoft.Json;
using Project.Models;

public class Twitter_Data_Lists
{
    public List<String> twitter_id_list_function()
    {
        ManageData manageData = new ManageData();
        var output = manageData.test_powershell();

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
        ManageData manageData = new ManageData();
        var output = manageData.test_powershell();

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
        ManageData manageData = new ManageData();
        var output = manageData.test_powershell();

        var twitterUsers = JsonConvert.DeserializeObject<List<TwitterUser>>(output); // list

        List<string> twitter_username_list = new List<string>();


        foreach (TwitterUser item in twitterUsers!) // Twitter username
        { // Loop through List with foreach
            twitter_username_list.Add(item.username!);
        }

        return twitter_username_list;
    }
}



