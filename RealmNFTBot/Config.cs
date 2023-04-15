using System;
using System.Collections.Generic;
using System.Text;

namespace RealmNFTBot
{
    public class Config
    {
        public List<Building> Buildings { get; set; }
        public List<Buildingpad> BuildingPads { get; set; }
        public List<Region> Regions { get; set; }
        public List<Leader> Leaders { get; set; }
        public List<Helper> Helpers { get; set; }
        public List<Mission> Missions { get; set; }
        public List<Missionpad> MissionPads { get; set; }
        public List<Modifier> Modifiers { get; set; }
        public List<Research> Researches { get; set; }
        public List<Transport> Transports { get; set; }
        public List<Technology> Technologies { get; set; }
        public List<Boost> Boosts { get; set; }
        public List<Equipment> Equipments { get; set; }
    }

    public class Building
    {
        public int BuildingID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxLevel { get; set; }
        public int UnlockLevel { get; set; }
        public int ConstructionTime { get; set; }
        public int DemolishTime { get; set; }
        public bool IsBaseOnly { get; set; }
        public bool IsUnique { get; set; }
        public bool IsUniquePerRegion { get; set; }
        public int Order { get; set; }
    }

    public class Buildingpad
    {
        public int BuildingPadID { get; set; }
        public int RegionID { get; set; }
        public int PadNumber { get; set; }
        public int UnlockLevel { get; set; }
    }

    public class Region
    {
        public int RegionID { get; set; }
        public string Name { get; set; }
        public int RequiredLevel { get; set; }
        public int UnlockCost { get; set; }
        public float BuildingMultiplier { get; set; }
        public float TrainingMultiplier { get; set; }
    }

    public class Leader
    {
        public int LeaderID { get; set; }
        public int TemplateID { get; set; }
        public string Name { get; set; }
        public int StarCount { get; set; }
        public int TeamSize { get; set; }
        public float Salary { get; set; }
        public Leadermodifier[] LeaderModifiers { get; set; }
    }

    public class Leadermodifier
    {
        public int LeaderModifierID { get; set; }
        public int LeaderID { get; set; }
        public int ModifierID { get; set; }
        public float Value { get; set; }
    }

    public class Helper
    {
        public int HelperID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TemplateID { get; set; }
        public int FacilityLevel { get; set; }
        public float TokenCost { get; set; }
        public int ResourceCost { get; set; }
        public int TitaniumCost { get; set; }
        public int TimeCost { get; set; }
        public float ContributionValue { get; set; }
        public int Order { get; set; }
    }

    public class Mission
    {
        public int MissionID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int PreferredLeaderID { get; set; }
        public bool IsDaily { get; set; }
        public int? EventType { get; set; }
        public Missioncontribution[] MissionContributions { get; set; }
    }

    public class Missioncontribution
    {
        public int MissionContributionID { get; set; }
        public int MissionID { get; set; }
        public int HelperID { get; set; }
        public float Value { get; set; }
    }

    public class Missionpad
    {
        public int MissionPadID { get; set; }
        public int RegionID { get; set; }
        public int PadNumber { get; set; }
    }

    public class Modifier
    {
        public int ModifierID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsNeededOnClient { get; set; }
    }

    public class Research
    {
        public int ResearchID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int UnlockLevel { get; set; }
        public int MaxLevel { get; set; }
    }

    public class Transport
    {
        public int TransportID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UnlockLevel { get; set; }
        public int MaxLevel { get; set; }
    }

    public class Technology
    {
        public int TechnologyID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public float UnlockCost { get; set; }
        public Technologymodifier[] TechnologyModifiers { get; set; }
    }

    public class Technologymodifier
    {
        public int TechnologyModifierID { get; set; }
        public int TechnologyID { get; set; }
        public int ModifierID { get; set; }
        public float Value { get; set; }
    }

    public class Boost
    {
        public int BoostID { get; set; }
        public int TemplateID { get; set; }
        public string Type { get; set; }
        public string Rarity { get; set; }
        public int Duration { get; set; }
        public int Order { get; set; }
        public string DisplayName { get; set; }
        public Boostmodifier[] BoostModifiers { get; set; }
    }

    public class Boostmodifier
    {
        public int BoostModifierID { get; set; }
        public int BoostID { get; set; }
        public int ModifierID { get; set; }
        public float Value { get; set; }
    }

    public class Equipment
    {
        public int EquipmentID { get; set; }
        public int TemplateID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Rarity { get; set; }
        public int Order { get; set; }
        public Equipmentmodifier[] EquipmentModifiers { get; set; }
    }

    public class Equipmentmodifier
    {
        public int EquipmentModifierID { get; set; }
        public int EquipmentID { get; set; }
        public int ModifierID { get; set; }
        public float Value { get; set; }
        public bool IsMissionModifier { get; set; }
    }

}
