using System;
using System.Collections.Generic;
using System.Text;

namespace RealmNFTBot
{
    public class UpgradeBuildingResponse
    {
        public Playerbuilding PlayerBuilding { get; set; }
        public Playerstate PlayerState { get; set; }
    }

    public class AssignResponse
    {
        public Playerregion[] PlayerRegions { get; set; }
        public Playerstate PlayerState { get; set; }
        public Playermodifier[] PlayerModifiers { get; set; }
        public object[] PlayerTransports { get; set; }
    }


    public class StartMissionResponse
    {
        public Playerhelper[] PlayerHelpers { get; set; }
        public Playermissionattempt PlayerMissionAttempt { get; set; }
        public Playerstate PlayerState { get; set; }
        public object[] PlayerTrainings { get; set; }
    }

    public class Playermissionattempt
    {
        public int PlayerMissionAttemptID { get; set; }
        public int PlayerMissionID { get; set; }
        public int AssignedLeaderAssetID { get; set; }
        public int MissionLeaderAssetID { get; set; }
        public long StartTicks { get; set; }
        public long FinishTicks { get; set; }
        public float SuccessChance { get; set; }
        public float MissionSpeedPercent { get; set; }
        public float MissionRewardPercent { get; set; }
        public float MissionRepPercent { get; set; }
        public float MissionExpPercent { get; set; }
        public int RequirementsReduction { get; set; }
        public bool IsSuccessful { get; set; }
        public bool IsClaimed { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsPoweredUp { get; set; }
        public object AssignedLeaderAsset { get; set; }
        public Missionleaderasset MissionLeaderAsset { get; set; }
        public DateTime finishTime { get; set; }
    }

    public class Missionleaderasset
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


    public class ClaimMissionReward
    {
        public Playermission PlayerMission { get; set; }
        public Playermissionattempt PlayerMissionAttempt { get; set; }
        public Assignedleaderinfo AssignedLeaderInfo { get; set; }
        public Missionleaderinfo MissionLeaderInfo { get; set; }
        public object RealmLevelInfo { get; set; }
        public Playerstate PlayerState { get; set; }
        public bool AssignedLeaderLevelUp { get; set; }
        public bool MissionLeaderLevelUp { get; set; }
        public object MysteryResult { get; set; }
    }

    public class Assignedleaderasset
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

    public class Assignedleaderinfo
    {
        public int LeaderAssetID { get; set; }
        public long AssetID { get; set; }
        public string MintNumber { get; set; }
        public int Experience { get; set; }
        public int AssignedRegionID { get; set; }
        public bool IsOnMission { get; set; }
        public Leaderinfo LeaderInfo { get; set; }
        public Equipmentslotinfo[] EquipmentSlotInfos { get; set; }
        public int CurrentLevel { get; set; }
        public int ValueForCurrentLevel { get; set; }
        public int ValueForNextLevel { get; set; }
    }

    public class Leaderinfo
    {
        public int LeaderID { get; set; }
        public string LeaderName { get; set; }
        public int StarCount { get; set; }
        public int TeamSize { get; set; }
        public float Salary { get; set; }
        public int Owned { get; set; }
        public Leaderbonus[] LeaderBonuses { get; set; }
    }

    public class Leaderbonus
    {
        public string BonusName { get; set; }
        public string BonusDescription { get; set; }
        public float BonusValue { get; set; }
    }

    public class Equipmentslotinfo
    {
        public int SlotNumber { get; set; }
        public bool IsUnlocked { get; set; }
        public int UnlockCost { get; set; }
        public int EquipmentID { get; set; }
    }

    public class Missionleaderinfo
    {
        public int LeaderAssetID { get; set; }
        public long AssetID { get; set; }
        public string MintNumber { get; set; }
        public int Experience { get; set; }
        public int AssignedRegionID { get; set; }
        public bool IsOnMission { get; set; }
        public Leaderinfo LeaderInfo { get; set; }
        public Equipmentslotinfo[] EquipmentSlotInfos { get; set; }
        public int CurrentLevel { get; set; }
        public int ValueForCurrentLevel { get; set; }
        public int ValueForNextLevel { get; set; }
    }


    public class CollectTransportResponse
    {
        public Playertransport[] PlayerTransports { get; set; }
        public Transportcollection[] TransportCollections { get; set; }
        public Playerstate PlayerState { get; set; }
    }

    public class Transportcollection
    {
        public int TransportCollectionType { get; set; }
        public int Amount { get; set; }
        public bool MaxStorageHit { get; set; }
        public int ChancePercentage { get; set; }
        public int ResultPercentage { get; set; }
        public object HelperID { get; set; }
        public object BoostID { get; set; }
        public object EquipmentID { get; set; }
        public object RealmLevelInfo { get; set; }
    }

    public class BuildingUpgradeInfo
    {
        public int ResourceCost { get; set; }
        public float TokenCost { get; set; }
        public int TitaniumCost { get; set; }
        public int TimeCost { get; set; }
        public Currentbonus[] CurrentBonuses { get; set; }
        public Upgradebonus[] UpgradeBonuses { get; set; }
    }

    public class Currentbonus
    {
        public string BonusName { get; set; }
        public string BonusDescription { get; set; }
        public float BonusValue { get; set; }
    }

    public class Upgradebonus
    {
        public string BonusName { get; set; }
        public string BonusDescription { get; set; }
        public float BonusValue { get; set; }
    }


    public class TransportUpgradeInfo
    {
        public int ResourceCost { get; set; }
        public float TokenCost { get; set; }
        public int TimeCost { get; set; }
        public Currentbonus[] CurrentBonuses { get; set; }
        public Upgradebonus[] UpgradeBonuses { get; set; }
    }


    public class UpgradeTransportResponse
    {
        public List<Playertransport> PlayerTransports { get; set; }
        public object TransportCollections { get; set; }
        public Playerstate PlayerState { get; set; }

        public Playertransport FindTransportById(int transportId)
        {
            return PlayerTransports.Find(x => x.TransportID == transportId);
        }
    }


    public class LeaderFullInfo
    {
        public int LeaderAssetID { get; set; }
        public long AssetID { get; set; }
        public string MintNumber { get; set; }
        public int Experience { get; set; }
        public int AssignedRegionID { get; set; }
        public bool IsOnMission { get; set; }
        public Leaderinfo LeaderInfo { get; set; }
        public Equipmentslotinfo[] EquipmentSlotInfos { get; set; }
        public int CurrentLevel { get; set; }
        public int ValueForCurrentLevel { get; set; }
        public int ValueForNextLevel { get; set; }
    }


    public class StartResearchResponse
    {
        public Playerresearch PlayerResearch { get; set; }
        public Playerstate PlayerState { get; set; }
    }


    public class ResearchUpgradeInfo
    {
        public int ResourceCost { get; set; }
        public float TokenCost { get; set; }
        public int TimeCost { get; set; }
        public object[] CurrentBonuses { get; set; }
        public Upgradebonus[] UpgradeBonuses { get; set; }
    }


    public class MissionLog
    {
        public Missionloginfo[] MissionLogInfos { get; set; }
        public Playerstate PlayerState { get; set; }
        public int MissionsActive { get; set; }
        public int MissionsSuccessful { get; set; }
        public int MissionsFailed { get; set; }
    }

    public class Missionloginfo
    {
        public int PlayerMissionAttemptID { get; set; }
        public int RegionID { get; set; }
        public string MissionName { get; set; }
        public int MissionLevel { get; set; }
        public long StartTicks { get; set; }
        public long FinishTicks { get; set; }
        public float SuccessChance { get; set; }
        public float MissionSpeedPercent { get; set; }
        public float MissionRewardPercent { get; set; }
        public float MissionRepPercent { get; set; }
        public float MissionExpPercent { get; set; }
        public int ResourceReward { get; set; }
        public int ReputationReward { get; set; }
        public int LeaderExperienceReward { get; set; }
        public float TokenReward { get; set; }
        public int TitaniumReward { get; set; }
        public int Attempt { get; set; }
        public int RequirementsReduction { get; set; }
        public bool IsSuccessful { get; set; }
        public bool IsClaimed { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsDaily { get; set; }
        public object EventMissionID { get; set; }
        public int ContributionSent { get; set; }
        public object Team { get; set; }
        public Missionleaderasset MissionLeaderAsset { get; set; }
    }


    public class ClaimMissionRewardsResponse
    {
        public Playermission PlayerMission { get; set; }
        public Playermissionattempt PlayerMissionAttempt { get; set; }
        public Assignedleaderinfo AssignedLeaderInfo { get; set; }
        public Missionleaderinfo MissionLeaderInfo { get; set; }
        public object RealmLevelInfo { get; set; }
        public Playerstate PlayerState { get; set; }
        public bool AssignedLeaderLevelUp { get; set; }
        public bool MissionLeaderLevelUp { get; set; }
        public object MysteryResult { get; set; }
    }


    public class MissionResult
    {
        public Playermission PlayerMission { get; set; }
        public Playermissionattempt PlayerMissionAttempt { get; set; }
        public Playerstate PlayerState { get; set; }
    }


}
