using System;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace dashing.net.jobs
{
    public class TeamCityMock : ITeamCity
    {
        public TeamCityMock(string server)
        {
            
        }

        public BuildData GetBuildData(string buildId)
        {
            var rnd = new Random().Next(4);

            var buildList = GetDynamicResponse(@"C:\LocalSD\dashing.net\src\dashing.net.jobs\buildlist.xml");

            dynamic latest;
            switch (rnd)
            {
                case 0:
                    latest = GetDynamicResponse(@"C:\LocalSD\dashing.net\src\dashing.net.jobs\successrunning.xml");
                    break;
                case 1:
                    latest = GetDynamicResponse(@"C:\LocalSD\dashing.net\src\dashing.net.jobs\successfinished.xml");
                    break;
                case 2:
                    latest = GetDynamicResponse(@"C:\LocalSD\dashing.net\src\dashing.net.jobs\failingfinished.xml");
                    break;
                default:
                    latest = GetDynamicResponse(@"C:\LocalSD\dashing.net\src\dashing.net.jobs\failingrunning.xml");
                    break;
                    
            }
            
            return new BuildData(buildList, latest);
        }

        private static dynamic GetDynamicResponse(string xml)
        {
            var doc = XDocument.Load(xml);

            var json = JsonConvert.SerializeXNode(doc);
            var x = JsonConvert.DeserializeObject<dynamic>(json);
            return x;
        }
    }
}