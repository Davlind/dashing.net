using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using dashing.net.common;
using dashing.net.streaming;
using Newtonsoft.Json.Linq;
using TeamCitySharp;
using TeamCitySharp.Locators;
using System.Xml;

namespace dashing.net.jobs
{
    [Export(typeof(IJob))]
    public class TcBuildStatus : IJob
    {
        private readonly Random _rand;

        public Lazy<Timer> Timer { get; private set; }

        private TeamCityClient _client;

        public TcBuildStatus()
        {
            _rand = new Random();



            Timer = new Lazy<Timer>(() => new Timer(SendMessage, null, TimeSpan.Zero, TimeSpan.FromSeconds(5)));
        }

        protected void SendMessage(object message)
        {
            try
            {
                ITeamCity teamCity = new TeamCityMock("http://teamcity.jetbrains.com");
                
                var buildData = teamCity.GetBuildData("Kotlin_KAnnotator_VerifyAndPubli");

                var percentage = buildData.LatestBuild.build["running-info"] != null
                    ? Convert.ToInt32(buildData.LatestBuild.build["running-info"]["@percentageComplete"])
                    : 0;

                var result =
                    new
                    {
                        state = buildData.LatestBuild.build["@state"],
                        percentage,
                        progressText = buildData.LatestBuild.build.statusText,
                        status = buildData.LatestBuild.build["@status"],
                        historicStatus = buildData.HistoricBuildStatus,
                        id = "buildid"
                    };

                Dashing.SendMessage(result);
            }
            catch (Exception ex)
            {
                var e = ex;
            }

        }
    }

    public class BuildData
    {
        public BuildData(dynamic historicBuildStatus, dynamic latestBuild)
        {
            HistoricBuildStatus = new List<string>();
            for (int i = 1; i < 5; i++)
            {
                HistoricBuildStatus.Add(historicBuildStatus.builds.build[i]["@status"].ToString());
            }
            LatestBuild = latestBuild;
        }

        public BuildData(List<string> historicBuildStatus, dynamic latestBuild)
        {
            HistoricBuildStatus = historicBuildStatus;
            LatestBuild = latestBuild;
        }

        public List<string> HistoricBuildStatus { get; set; }
        public dynamic LatestBuild { get; set; }
    }
}
