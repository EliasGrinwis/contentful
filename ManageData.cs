using System.Diagnostics;
using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Models.Management;
using Newtonsoft.Json;
using Project.Models;

public class ManageData
{

    String contentTypeId = "TwitterFollowers";
    int contentTypeVersion = 7;

    public async void Make_Content_Type()
    {

        var httpClient = new HttpClient();
        var client = new ContentfulManagementClient(httpClient, "CFPAT-YXYuAEil2xQAg_cE3ngWeVupEuLue2aSI5HCxE-iUaU", "jlvdks7mf5c1");


        var contentType = new ContentType();
        contentType.SystemProperties = new SystemProperties();
        contentType.SystemProperties.Id = contentTypeId; //random id
        contentType.SystemProperties.Version = contentTypeVersion;

        contentType.Name = "TwitterFollowers"; // name of content type
        contentType.Description = "";

        contentType.Fields = new List<Field>() // Array fields in my content type

        {
            new Field()
            {
                Name = "Id",
                Id = "id",
                Type = "Symbol",
            },
            new Field()
            {
                Name = "Name",
                Id = "name",
                Type = "Symbol",
            },
            new Field()
            {
                Name = "Username",
                Id = "username",
                Type = "Symbol",
            }
        };

        contentType.DisplayField = "name";

        var result_contentType = await client.CreateOrUpdateContentType(contentType);
        await client.ActivateContentType(contentTypeId, version: result_contentType.SystemProperties.Version!.Value);

        // Loop 13 times to make 13 entries

        Twitter_Data_Lists twitter_Data_Lists = new Twitter_Data_Lists();
        var twitter_id_list = twitter_Data_Lists.twitter_id_list_function();
        var twitter_name_list = twitter_Data_Lists.twitter_name_list_function();
        var twitter_username_list = twitter_Data_Lists.twitter_username_list_function();

        var counter = 1;
        var entry = new Entry<dynamic>();

        foreach (var result in twitter_id_list.Zip(twitter_name_list, (first, second) => new { object1 = first, object2 = second })
                          .Zip(twitter_username_list, (first, second) => new { object1 = first.object1, object2 = first.object2, object3 = second }))
        {

            String new_id = "user" + counter;

            entry.SystemProperties = new SystemProperties();
            entry.SystemProperties.Id = new_id;
            entry.SystemProperties.Version = 1;

            entry.Fields = new
            {
                id = new Dictionary<string, string>()
            {
                { "en-US", result.object1 }
            },

                name = new Dictionary<string, string>()
            {
                { "en-US", result.object2 }
            },

                username = new Dictionary<string, string>()
            {
                { "en-US", result.object3 }
            }
            };

            var newEntry = await client.CreateOrUpdateEntry(entry, contentTypeId: contentTypeId);

            await client.PublishEntry(new_id, 1);
            counter++;

        }
    }


    public String test_powershell()
    {
        Process p = new Process();
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "powershell.exe";
        startInfo.Arguments = "-file C:\\Users\\Elias\\OneDrive\\Documenten\\Script.ps1";
        p.StartInfo = startInfo;
        p.StartInfo.RedirectStandardOutput = true;
        p.Start();
        string output = p.StandardOutput.ReadToEnd();

        return output;
    }
}



