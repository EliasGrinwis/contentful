using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Models.Management;
using File = Contentful.Core.Models.File;

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
            },
            new Field()
            {
                Name = "ProfilePicture",
                Id = "profilePicture",
                Type = "Link",
                LinkType = "Asset"
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

            //String new_asset_id = "asset" + counter;
            //var asset = await client.GetAsset(new_asset_id);

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
            },
                profilePicture = new Dictionary<string, object>()
            {
                { "en-US", new Asset() { SystemProperties = new SystemProperties() { LinkType = "Asset", Id = "asset" + counter, Type = "Link" } } }
            }
            };


            var newEntry = await client.CreateOrUpdateEntry(entry, contentTypeId: contentTypeId);
            await client.PublishEntry(new_id, 1);
            counter++;
        }
    }

    public async void create_assets()
    {
        var httpClient = new HttpClient();
        var client = new ContentfulManagementClient(httpClient, "CFPAT-YXYuAEil2xQAg_cE3ngWeVupEuLue2aSI5HCxE-iUaU", "jlvdks7mf5c1");

        TwitterData twitterData = new TwitterData();
        var profilePicturesList = twitterData.twitter_profilePictures_list_function();

        var counter = 1;
        var asset = new ManagementAsset();

        foreach (var imageUrl in profilePicturesList)
        {

            String new_asset_id = "asset" + counter;

            asset.SystemProperties = new SystemProperties();
            asset.SystemProperties.Id = new_asset_id;
            asset.SystemProperties.Version = 3;

            asset.Title = new Dictionary<string, string> {
            { "en-US", new_asset_id }
            };

            asset.Files = new Dictionary<string, File>
            {
                { "en-US", new File()
                    {
                    ContentType = "TwitterFollowers",
                    FileName = "image.png",
                    UploadUrl = imageUrl
                    }
                }
            };

            var create_asset = await client.CreateOrUpdateAsset(asset);
            await client.ProcessAsset(new_asset_id, 2, "en-US");
            Thread.Sleep(250); // wait 250ms to make sure the asset is processed
            await client.PublishAsset(new_asset_id, 2);

            counter++;
        }
    }

    public async void link_asset_with_entry()
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
                profilePicture = new Dictionary<string, object>()
            {
                { "en-US", new Asset() { SystemProperties = new SystemProperties() { LinkType = "Asset", Id = "asset" + counter, Type = "Link" } } }
            }
            };

            var newEntry = await client.CreateOrUpdateEntry(entry, contentTypeId: contentTypeId);
            await client.PublishEntry(new_id, 1);
            counter++;
        }
    }
}