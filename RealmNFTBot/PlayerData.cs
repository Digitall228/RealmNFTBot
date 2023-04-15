using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RealmNFTBot
{
    public class Player
    {
        public Playerstate PlayerState { get; set; }
        public List<Playermodifier> PlayerModifiers { get; set; }
        public List<Playerregion> PlayerRegions { get; set; }
        public List<Playerbuilding> PlayerBuildings { get; set; }
        public List<Playerresearch> PlayerResearches { get; set; }
        public List<Playertraining> PlayerTrainings { get; set; }
        public List<Playertransport> PlayerTransports { get; set; }
        public List<Playertechnology> PlayerTechnologies { get; set; }
        public List<Playerhelper> PlayerHelpers { get; set; }
        public List<Playermission> PlayerMissions { get; set; }
        public List<Playermissionattempt> PlayerMissionAttempts { get; set; }
        public object[] PlayerBoosts { get; set; }

        public Playerbuilding FindBuildingById(int buildingId)
        {
            return PlayerBuildings.Find(x => x.BuildingID == buildingId);
        }
        public Playerbuilding FindBuildingByPad(int buildingPadID)
        {
            return PlayerBuildings.Find(x => x.BuildingPadID == buildingPadID);
        }
        public Playertransport FindTransportById(int transportId)
        {
            return PlayerTransports.Find(x => x.TransportID == transportId);
        }
    }

    public class Playerstate
    {
        public int PlayerStateID { get; set; }
        public int Reputation { get; set; }
        public int Resources { get; set; }
        public float ResourcesPerMinute { get; set; }
        public float Tokens { get; set; }
        public float TokensPerHour { get; set; }
        public int Titanium { get; set; }
        public long ValuesLastUpdated { get; set; }
        public int ResourceLimit { get; set; }
        public int HelperLimit { get; set; }
        public int LogNotificationCount { get; set; }
        public int DepositNotificationCount { get; set; }
        public int RealmLevelNotificationCount { get; set; }
        public bool SalarySuspended { get; set; }
        public int League { get; set; }
        public int MessagesLastChecked { get; set; }
        public int CurrentLevel { get; set; }
        public int ValueForCurrentLevel { get; set; }
        public int ValueForNextLevel { get; set; }
    }

    public class Playertechnology
    {
        public int PlayerTechnologyID { get; set; }
        public int TechnologyID { get; set; }
        public int Level { get; set; }
        public bool IsPoweredUp { get; set; }
        public int LockLevel { get; set; }
    }
    public class Playermodifier
    {
        public int PlayerModifierID { get; set; }
        public int ModifierID { get; set; }
        public int ModifierSource { get; set; }
        public int EntityID { get; set; }
        public string EntityName { get; set; }
        public float Value { get; set; }
        public long FinishTicks { get; set; }
    }

    public class Playerregion
    {
        public int PlayerRegionID { get; set; }
        public int RegionID { get; set; }
        public bool IsUnlocked { get; set; }
        public int LeaderAssetID { get; set; }
        public Leaderasset LeaderAsset { get; set; }
    }

    public class Leaderasset
    {
        public int LeaderAssetID { get; set; }
        public int LeaderID { get; set; }
        public long AssetID { get; set; }
        public string MintNumber { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public bool IsEquipmentSlot1Unlocked { get; set; }
        public bool IsEquipmentSlot2Unlocked { get; set; }
        public Leader Leader { get; set; }
    }

    public class Playerbuilding
    {
        public int PlayerBuildingID { get; set; }
        public int BuildingPadID { get; set; }
        public int BuildingID { get; set; }
        public int Level { get; set; }
        public bool IsConstructing { get; set; }
        public bool IsUpgrading { get; set; }
        public bool IsDemolishing { get; set; }
        public long StartTicks { get; set; }
        public long FinishTicks { get; set; }
        public bool IsPoweredUp { get; set; }
        public DateTime finishTime { get; set; }
    }

    public class Playerresearch
    {
        public int PlayerResearchID { get; set; }
        public int ResearchID { get; set; }
        public int Level { get; set; }
        public bool IsResearching { get; set; }
        public long StartTicks { get; set; }
        public long FinishTicks { get; set; }
        public object PlayerBuildingID { get; set; }
        public bool IsPoweredUp { get; set; }
        public DateTime finishTime { get; set; }
    }

    public class Playertraining
    {
        public int PlayerTrainingID { get; set; }
        public int PlayerBuildingID { get; set; }
        public int TrainingSlot { get; set; }
        public int? HelperAssetID { get; set; }
        public float TokenCost { get; set; }
        public int ResourceCost { get; set; }
        public int TitaniumCost { get; set; }
        public int TimeCost { get; set; }
        public int Count { get; set; }
        public long StartTicks { get; set; }
        public long FinishTicks { get; set; }
        public int Status { get; set; }
        public bool IsAuto { get; set; }
        public Helperasset HelperAsset { get; set; }
    }

    public class Helperasset
    {
        public int HelperAssetID { get; set; }
        public int HelperID { get; set; }
        public int AssetID { get; set; }
        public object MintNumber { get; set; }
        public int Level { get; set; }
        public object Helper { get; set; }
    }

    public class Playertransport
    {
        public int PlayerTransportID { get; set; }
        public int PlayerBuildingID { get; set; }
        public int TransportID { get; set; }
        public int Level { get; set; }
        public bool IsUpgrading { get; set; }
        public long StartTicks { get; set; }
        public long FinishTicks { get; set; }
        public float CollectionRate { get; set; }
        public float CollectionCapacity { get; set; }
        public float Accumulated { get; set; }
        public long LastUpdate { get; set; }
        public bool IsPoweredUp { get; set; }
        public DateTime finishTime { get; set; }
    }

    public class Playerhelper
    {
        public int PlayerHelperID { get; set; }
        public int HelperID { get; set; }
        public int Count { get; set; }
    }

    public class Playermission
    {
        public int PlayerMissionID { get; set; }
        public int MissionID { get; set; }
        public int MissionPadID { get; set; }
        public int MissionLevel { get; set; }
        public int TimeRequired { get; set; }
        public int ContributionRequired { get; set; }
        public int ResourceReward { get; set; }
        public int ReputationReward { get; set; }
        public int LeaderExperienceReward { get; set; }
        public float TokenReward { get; set; }
        public int TitaniumReward { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsPoweredUp { get; set; }
        public bool IsSkipped { get; set; }
        public long ExpiryTicks { get; set; }
        public object EventMissionID { get; set; }
        public bool HasAttempted { get; set; }
        public Alteredcontribution[] AlteredContributions { get; set; }
        public Dailycontribution[] DailyContributions { get; set; }
        public int DailyAttemptCost { get; set; }
    }

    public class Alteredcontribution
    {
        public int HelperID { get; set; }
        public float Value { get; set; }
    }

    public class Dailycontribution
    {
        public int HelperID { get; set; }
        public float Value { get; set; }
    }

}
