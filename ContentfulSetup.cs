using System.Diagnostics;
using Contentful.Core;
using Contentful.Core.Models;

public class ContentfulSetup
{

    String contentTypeId = "TwitterFollowers";
    int contentTypeVersion = 7;

    public async void create_content_type()
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

    }

    public async void create_entries()
    {

        var httpClient = new HttpClient();
        var client = new ContentfulManagementClient(httpClient, "CFPAT-YXYuAEil2xQAg_cE3ngWeVupEuLue2aSI5HCxE-iUaU", "jlvdks7mf5c1");

        TwitterData twitterData = new TwitterData();
        var twitter_id_list = twitterData.twitter_id_list_function();
        var twitter_name_list = twitterData.twitter_name_list_function();
        var twitter_username_list = twitterData.twitter_username_list_function();

        var counter = 1;
        var entry = new Entry<dynamic>();

        // Loop 13 times to make 13 entries
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
}



