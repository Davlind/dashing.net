using System.Diagnostics;
using System.Net;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace dashing.net.jobs
{
    public class TeamCity : ITeamCity
    {
        private string Server { get; set; }
        private const string RequestTemplateBuildList = "{0}/guestAuth/app/rest/builds?locator=running:any,count:5,buildType(id:{1})";

        public TeamCity(string server)
        {
            Server = server;
        }

        private static dynamic GetDynamicResponse(string requestUrl)
        {
            var request = WebRequest.Create(requestUrl) as HttpWebRequest;
            var response = request.GetResponse() as HttpWebResponse;

            Debug.Assert(response != null, "response != null");
            var doc = XDocument.Load(response.GetResponseStream());

            var json = JsonConvert.SerializeXNode(doc);
            var x = JsonConvert.DeserializeObject<dynamic>(json);
            return x;
        }

        public BuildData GetBuildData(string buildId)
        {
            var request = string.Format(RequestTemplateBuildList, Server, buildId);

            var buildList = GetDynamicResponse(request);
            var latestBuild = GetDynamicResponse(string.Format("{0}{1}", Server, buildList.builds.build[0]["@href"]));
            
            return new BuildData(buildList,latestBuild);
        }
    }
}