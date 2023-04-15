using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leaf.xNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RealmNFTBot
{
    public class Client
    {
        public HttpRequest request { get; set; }
        public string playerId { get; set; }
        public string playerToken { get; set; }
        public string server { get; set; }
        public Player playerData { get; set; }
        public Config config { get; set; }

        public object requestLocker { get; private set; } = new object();

        public Client(string PlayerId, string PlayerToken, string Server = "https://realmserver.azurewebsites.net/api")
        {
            playerId = PlayerId;
            playerToken = PlayerToken;
            server = Server;

            request = new HttpRequest();
            request.KeepAlive = true;
            request.UserAgentRandomize();
            request["Accept"] = "*/*";
            request["Accept-Encoding"] = "gzip, deflate, br";
            request["Accept-Language"] = "en-US,en;q=0.9,uk-UA;q=0.8,uk;q=0.7,ru;q=0.6";
            request["ClientVersion"] = "4.1.0";
            request["Content-Type"] = "application/json";
            request["Host"] = "realmserver.azurewebsites.net";
            request["Origin"] = "https://play.realmnft.io";
            request["PlayerID"] = playerId;
            request["PlayerToken"] = playerToken;
            request["Referer"] = "https://play.realmnft.io/";
        }


        public T SendGetRequest<T>(string url, int maxTries = 3)
        {
            lock (requestLocker)
            {
                for (int i = 0; i < maxTries; i++)
                {
                    try
                    {
                        HttpResponse response = request.Get(url);
                        return JsonConvert.DeserializeObject<T>(response.ToString());
                    }
                    catch (Exception ex)
                    {
                        Log.WriteToLog($"ERROR: {request.Response}", Log.LogType.Error);
                        Task.Delay(5000).Wait();
                    }
                }

                return default(T);
            }
        }

        public void ParsePlayerData()
        {
            playerData = SendGetRequest<Player>($"{server}/Login/PlayerData");
            Log.WriteToLog("PlayerData parsed successfully", Log.LogType.Success);
        }
        public void GetConfigData()
        {
            config = SendGetRequest<Config>($"{server}/Login/ConfigData");
            Log.WriteToLog("Config parsed successfully", Log.LogType.Success);
        }

        public Playerbuilding Build(int buildingPadID, int buildingID)
        {//
            Playerbuilding buildResponse = SendGetRequest<Playerbuilding>($"{server}/Building/Construct?buildingPadID={buildingPadID}&buildingID={buildingID}");
            Log.WriteToLog("Building started successfully", Log.LogType.Success);

            return buildResponse;
        }
        public BuildingUpgradeInfo GetBuildingUpgradeInfo(int buildingPad)
        {//
            BuildingUpgradeInfo buildingUpgradeInfo = SendGetRequest<BuildingUpgradeInfo>($"{server}/Building/GetBuildingUpgrade?playerBuildingID={playerData.FindBuildingByPad(buildingPad).PlayerBuildingID}");
            Log.WriteToLog("BuildingUpgradeInfo parsed successfully", Log.LogType.Success);

            return buildingUpgradeInfo;
        }
        public UpgradeBuildingResponse UpgradeBuilding(int buildingPad)
        {//
            UpgradeBuildingResponse upgradeBuildingResponse = SendGetRequest<UpgradeBuildingResponse>($"{server}/Building/Upgrade?playerBuildingID={playerData.FindBuildingByPad(buildingPad).PlayerBuildingID}");
            Log.WriteToLog("Building upgrade started successfully", Log.LogType.Success);

            return upgradeBuildingResponse;
        }
        public TransportUpgradeInfo GetTransportUpgradeInfo(int transportID)
        {
            TransportUpgradeInfo transportUpgradeInfo = SendGetRequest<TransportUpgradeInfo>($"{server}/Transport/GetTransportUpgrade?playerTransportID={playerData.FindTransportById(transportID).PlayerTransportID}");
            Log.WriteToLog("Transport upgrade info was parsed successfully", Log.LogType.Success);

            return transportUpgradeInfo;
        }
        public UpgradeTransportResponse UpgradeTransport(int transportID)
        {
            UpgradeTransportResponse upgradeTransportResponse = SendGetRequest<UpgradeTransportResponse>($"{server}/Transport/Upgrade?playerTransportID={playerData.FindTransportById(transportID).PlayerTransportID}");
            Log.WriteToLog("Transport upgrade started successfully", Log.LogType.Success);

            return upgradeTransportResponse;
        }
        public CollectTransportResponse CollectTransport(int transportID, bool isCollectAll)
        {
            CollectTransportResponse collectTransportResponse = SendGetRequest<CollectTransportResponse>($"{server}/Transport/Collect?playerTransportID={playerData.FindTransportById(transportID).PlayerTransportID}&isCollectAll={isCollectAll}");
            Log.WriteToLog("Transport collected successfully", Log.LogType.Success);

            return collectTransportResponse;
        }
        public List<LeaderFullInfo> ParseLeaders(bool includeCitizen = true)
        {
            List<LeaderFullInfo> response = SendGetRequest<List<LeaderFullInfo>>($"{server}/Leader/GetLeaders?includeCitizen={includeCitizen}");

            for (int i = 0; i < response.Count; i++)
            {
                LeaderFullInfo leader = response[i];

                float technologyK = 0;
                if (playerData.PlayerTechnologies.Find(x => x.TechnologyID == 4) != null)
                {
                    technologyK = (config.Technologies.Find(x => x.TechnologyID == 4).TechnologyModifiers.First().Value * playerData.PlayerTechnologies.Find(x => x.TechnologyID == 4).Level);
                }

                float modifierK = 0;
                if(playerData.PlayerModifiers.Find(x => x.EntityName == $"Leadership_ {leader.LeaderInfo.LeaderName.Split(' ').Last()}") != null)
                {
                    modifierK = (playerData.PlayerModifiers.Find(x => x.EntityName == $"Leadership_ {leader.LeaderInfo.LeaderName.Split(' ').Last()}").Value);
                }
                leader.LeaderInfo.TeamSize = (int)Math.Round(leader.LeaderInfo.TeamSize * leader.CurrentLevel * (1 + technologyK + modifierK));
            }

            Log.WriteToLog("Leaders parsed successfully", Log.LogType.Success);

            return response;
        }
        public StartMissionResponse StartMission(int playerMissionID, int leaderAssetID, int helpersCount, bool instantFinish = false)
        {
            string helpers = $"%7B%22MHs%22:[%7B%22H%22:10,%22C%22:{helpersCount}%7D]%7D";
            StartMissionResponse response = SendGetRequest<StartMissionResponse>($"{server}/Mission/Start?playerMissionID={playerMissionID}&leaderAssetID={leaderAssetID}&helpers={helpers}&instantFinish={instantFinish}");

            Log.WriteToLog("Mission started successfully", Log.LogType.Success);

            return response;
        }
        public MissionResult GetMisssionResult(int playerMissionAttemptID)
        {//TODO
            MissionResult response = SendGetRequest<MissionResult>($"{server}/Mission/Finalize?playerMissionAttemptID={playerMissionAttemptID}");

            Log.WriteToLog("Mission results parsed successfully", Log.LogType.Success);

            return response;
        }
        public MissionLog CheckMissionLog(bool logType = true)
        {//TODO
            MissionLog response = SendGetRequest<MissionLog>($"{server}/Mission/MissionLog?logType={logType}");

            Log.WriteToLog("Mission log parsed successfully", Log.LogType.Success);

            return response;
        }
        public ClaimMissionReward ClaimMissionReward(int playerMissionAttemptID)
        {
            ClaimMissionReward response = SendGetRequest<ClaimMissionReward>($"{server}/Mission/ClaimReward?playerMissionAttemptID={playerMissionAttemptID}");

            Log.WriteToLog("Mission rewards claimed successfully", Log.LogType.Success);

            return response;
        }
        public AssignResponse AssignLeader(int leaderAssetID, int regionID)
        {
            AssignResponse response = SendGetRequest<AssignResponse>($"{server}/Leader/AssignToRegion?leaderAssetID={leaderAssetID}&regionID={regionID}");

            Log.WriteToLog("Leader assigned successfully", Log.LogType.Success);

            return response;
        }
        public ResearchUpgradeInfo GetResearchUpgradeInfo(int researchID)
        {//TODO
            ResearchUpgradeInfo response = SendGetRequest<ResearchUpgradeInfo>($"{server}/Research/GetResearchUpgrade?researchID={researchID}");

            Log.WriteToLog("Research upgrade info parsed successfully", Log.LogType.Success);

            return response;
        }
        public StartResearchResponse StartResearch(int buildingPad, int researchID)
        {//TODO
            StartResearchResponse response = SendGetRequest<StartResearchResponse>($"{server}/Research/Research?playerBuildingID={playerData.FindBuildingByPad(buildingPad).PlayerBuildingID}&researchID={researchID}");

            Log.WriteToLog("Research started successfully", Log.LogType.Success);

            return response;
        }
    }
}
