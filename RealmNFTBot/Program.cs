using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealmNFTBot
{
    class Program
    {
        public static List<Playerbuilding> builds = new List<Playerbuilding>();
        public static List<Playertransport> transportUpgrades = new List<Playertransport>();
        public static List<Playerresearch> researches = new List<Playerresearch>();

        public static Task transportCollecting;
        public static Task missioning;

        public static string strategyPath = "strategy.txt";
        static void Main(string[] args)
        {
            while (true)
            {
                string command = Console.ReadLine();

                if(command.Contains("/run"))
                {
                    string[] data = command.Split(' ');
                    List<string> strategy = ParseStrategy(strategyPath);
                    Client client = new Client("13565", data[1]);
                    Task.Run(() => RunStrategy(client, strategy));
                }
            }
        }

        public static void RunStrategy(Client client, List<string> strategy)
        {
            client.GetConfigData();
            client.ParsePlayerData();

            var g = client.playerData.PlayerBuildings.FindAll(x => x.IsUpgrading);
            builds.AddRange(client.playerData.PlayerBuildings.FindAll(x => x.IsUpgrading));
            builds = builds.Select(x => { x.finishTime = ConvertFromLDAP(x.FinishTicks); return x; }).ToList();
            transportUpgrades.AddRange(client.playerData.PlayerTransports.FindAll(x => x.IsUpgrading));
            transportUpgrades = transportUpgrades.Select(x => { x.finishTime = ConvertFromLDAP(x.FinishTicks); return x; }).ToList();
            researches.AddRange(client.playerData.PlayerResearches.FindAll(x => x.IsResearching));
            researches = researches.Select(x => { x.finishTime = ConvertFromLDAP(x.FinishTicks); return x; }).ToList();

            transportCollecting = new Task(() => CollectingTransport(client));
            transportCollecting.Start();
            
            missioning = new Task(() => Missioning(client));
            missioning.Start();


            for (int i = 0; i < strategy.Count; i++)
            {
                if(strategy[i].EndsWith('+'))
                {
                    continue;
                }

                client.ParsePlayerData();

                string[] data = strategy[i].Split('|');

                switch(data[0])
                {
                    case "assign":
                        client.AssignLeader(int.Parse(data[1]), int.Parse(data[2]));
                        break;
                    case "build":
                        if (builds.Count > 0)
                        {
                            while (DateTime.UtcNow < builds[0].finishTime)
                            {
                                Task.Delay(5000).Wait();
                            }
                            builds.RemoveAt(0);
                        }

                        client.Build(int.Parse(data[1]), int.Parse(data[2]));
                        Task.Delay(5000).Wait();
                        break;
                    case "upgrade":
                        switch(data[1])
                        {
                            case "building":
                                if(builds.Count > 0)
                                {
                                    while (DateTime.UtcNow < builds[0].finishTime)
                                    {
                                        Task.Delay(60*1000).Wait();
                                    }
                                    builds.RemoveAt(0);
                                }

                                BuildingUpgradeInfo buildingUpgradeInfo = client.GetBuildingUpgradeInfo(int.Parse(data[2]));

                                while (client.playerData.PlayerState.Resources < buildingUpgradeInfo.ResourceCost
                                    || client.playerData.PlayerState.Tokens < buildingUpgradeInfo.TokenCost
                                    || client.playerData.PlayerState.Titanium < buildingUpgradeInfo.TitaniumCost)
                                {
                                    Task.Delay(5 * 60 * 1000).Wait();
                                    client.ParsePlayerData();
                                }

                                UpgradeBuildingResponse upgradeBuildingResponse = client.UpgradeBuilding(int.Parse(data[2]));
                                upgradeBuildingResponse.PlayerBuilding.finishTime = ConvertFromLDAP(upgradeBuildingResponse.PlayerBuilding.FinishTicks);
                                builds.Add(upgradeBuildingResponse.PlayerBuilding);

                                break;
                            case "transport":
                                if (transportUpgrades.Count > 0)
                                {
                                    while (DateTime.UtcNow < transportUpgrades[0].finishTime)
                                    {
                                        Task.Delay(5000).Wait();
                                    }

                                    transportUpgrades.RemoveAt(0);
                                }

                                TransportUpgradeInfo transportUpgradeInfo = client.GetTransportUpgradeInfo(int.Parse(data[2]));

                                while (client.playerData.PlayerState.Resources < transportUpgradeInfo.ResourceCost
                                    || client.playerData.PlayerState.Tokens < transportUpgradeInfo.TokenCost)
                                {
                                    Task.Delay(5 * 60 * 1000).Wait();
                                    client.ParsePlayerData();
                                }

                                UpgradeTransportResponse upgradeTransportResponse = client.UpgradeTransport(int.Parse(data[2]));
                                Playertransport transport = upgradeTransportResponse.FindTransportById(int.Parse(data[2]));
                                transport.finishTime = ConvertFromLDAP(transport.FinishTicks);
                                transportUpgrades.Add(transport);

                                break;
                            default:
                                Log.WriteToLog($"Unexpected upgrade type: {data[1]}", Log.LogType.Warning);
                                break;
                        }
                        break;
                    case "research":
                        if (researches.Count > 0)
                        {
                            while (DateTime.UtcNow < researches[0].finishTime)
                            {
                                Task.Delay(5000).Wait();
                            }

                            researches.RemoveAt(0);
                        }

                        ResearchUpgradeInfo researchUpgradeInfo = client.GetResearchUpgradeInfo(int.Parse(data[2]));

                        while (client.playerData.PlayerState.Resources < researchUpgradeInfo.ResourceCost
                            || client.playerData.PlayerState.Tokens < researchUpgradeInfo.TokenCost)
                        {
                            Task.Delay(5 * 60 * 1000).Wait();
                            client.ParsePlayerData();
                        }

                        StartResearchResponse startResearchResponse = client.StartResearch(int.Parse(data[1]), int.Parse(data[2]));
                        Playerresearch research = startResearchResponse.PlayerResearch;
                        research.finishTime = ConvertFromLDAP(research.FinishTicks).AddMinutes(2);
                        researches.Add(research);

                        break;
                    default:
                        Log.WriteToLog($"Unexpected command: {data[0]}", Log.LogType.Warning);
                        break;
                }

                strategy[i] += "+";
                SaveStrategy(strategyPath, strategy);
            }

            Log.WriteToLog("Strategy was fully completed", Log.LogType.Success);
        }

        public static void CollectingTransport(Client client)
        {
            while (true)
            {
                for (int i = 0; i < client.playerData.PlayerTransports.Count; i++)
                {
                    client.CollectTransport(client.playerData.PlayerTransports[i].TransportID, true);
                }

                Task.Delay(5 * 60 * 1000).Wait();
            }
        }

        public static void Missioning(Client client)
        {
            while (true)
            {
                //Task.Delay(30 * 60 * 1000).Wait();

                Playermission mission = client.playerData.PlayerMissions.Find(x => (x.ExpiryTicks == 0) && (x.HasAttempted ? client.playerData.PlayerMissionAttempts.Find(y => y.PlayerMissionID == x.PlayerMissionID) == null : true));
                List<LeaderFullInfo> leaders = client.ParseLeaders();
                LeaderFullInfo leader = leaders.Find(x => x.AssignedRegionID == 0 && !x.IsOnMission);
                Playerhelper workers = client.playerData.PlayerHelpers.Find(x => x.HelperID == 10);
                int availableHelpersCount = workers == null ? 0 : workers.Count;
                int neededHelpers = (int)(mission.ContributionRequired / client.config.Helpers.Find(x => x.HelperID == 10).ContributionValue);

                while ((neededHelpers > client.playerData.PlayerState.HelperLimit && availableHelpersCount < leader.LeaderInfo.TeamSize) || (neededHelpers <= client.playerData.PlayerState.HelperLimit && neededHelpers <= leader.LeaderInfo.TeamSize && availableHelpersCount < neededHelpers))
                {
                    Task.Delay(5 * 60 * 1000).Wait();
                    client.ParsePlayerData();
                }
                if(availableHelpersCount >= neededHelpers)
                {
                    availableHelpersCount = neededHelpers;
                }
                if (availableHelpersCount > leader.LeaderInfo.TeamSize)
                {
                    availableHelpersCount = leader.LeaderInfo.TeamSize;
                }

                StartMissionResponse response = client.StartMission(mission.PlayerMissionID, leader.LeaderAssetID, availableHelpersCount);
                response.PlayerMissionAttempt.finishTime = ConvertFromLDAP(response.PlayerMissionAttempt.FinishTicks);

                while (DateTime.UtcNow < response.PlayerMissionAttempt.finishTime)
                {
                    Task.Delay(4 * 60 * 1000).Wait();
                }

                MissionResult result =  client.GetMisssionResult(response.PlayerMissionAttempt.PlayerMissionAttemptID);
                if(result.PlayerMissionAttempt.IsSuccessful)
                {
                    client.ClaimMissionReward(response.PlayerMissionAttempt.PlayerMissionAttemptID);
                }

                Task.Delay(25 * 60 * 1000).Wait();

                client.ParsePlayerData();
            }
        }

        public static List<string> ParseStrategy(string path)
        {
            return File.ReadAllLines(path).ToList();
        }
        public static void SaveStrategy(string path, IEnumerable<string> strategy)
        {
            File.WriteAllLines(path, strategy);
        }
        public static DateTime ConvertFromLDAP(long ticks)
        {
            DateTime dateTime = new DateTime(1601, 01, 01, 0, 0, 0, DateTimeKind.Utc).AddTicks(ticks);
            if(dateTime.Year > 3000)
            {
                dateTime = dateTime.AddYears(-1600);
            }
            return dateTime;
        }
    }
}
