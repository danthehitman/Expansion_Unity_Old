using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoView : MonoBehaviour {

    public Text Ag;
    public Text Arc;
    public Text Research;
    public Text Exploration;
    public Text Foraging;
    public Text Hunting;
    public Text Building;
    public Text Fishing;
    public Text Fighting;
    public Text Mining;
    public Text Husbandry;
    public Text Survival;
    public Text Camo;


    private string agText = "Ag: ";
    private string agValue = "";
    private string arcText = "Arc: ";
    private string arcValue = "";
    private string researchText = "Research: ";
    private string researchValue = "";
    private string explorationText = "Exploration: ";
    private string explorationValue = "";
    private string foragingText = "Foraging: ";
    private string foragingValue = "";
    private string huntingText = "Hunting: ";
    private string huntingValue = "";
    private string buildingText = "Building: ";
    private string buildingValue = "";
    private string fishingText = "Fishing: ";
    private string fishingValue = "";
    private string fightingText = "Fighting: ";
    private string fightingValue = "";
    private string miningText = "Mining: ";
    private string miningValue = "";
    private string husbandryText = "Husbandry: ";
    private string husbandryValue = "";
    private string survivalText = "Survival: ";
    private string survivalValue = "";
    private string camoText = "Camo: ";
    private string camoValue = "";

    public void UpdateInfo(HumanEntity entity)
    {
        if (entity != null)
        {
            agValue = entity.AdjustedAgricultureSkill.ToString();
            arcValue = entity.AdjustedArchiologicalSkill.ToString();
            researchValue = entity.AdjustedResearchSkill.ToString();
            explorationValue = entity.AdjustedExplorationSkill.ToString();
            foragingValue = entity.AdjustedForagingSkill.ToString();
            huntingValue = entity.AdjustedHuntingSkill.ToString();
            buildingValue = entity.AdjustedBuildingSkill.ToString();
            fishingValue = entity.AdjustedFishingSkill.ToString();
            fightingValue = entity.AdjustedFightingSkill.ToString();
            miningValue = entity.AdjustedMiningSkill.ToString();
            husbandryValue = entity.AdjustedHusbandrySkill.ToString();
            survivalValue = entity.AdjustedSurvivalSkill.ToString();
            camoValue = entity.AdjustedCamoSkill.ToString();
        }
        else
        {
            agValue = arcValue = researchValue = explorationValue = foragingValue = huntingValue = null;
            buildingValue = fishingValue = fightingValue = miningValue = husbandryValue = survivalValue = null;
            camoValue = null;
        }

        Ag.text = agText + agValue;
        Arc.text = arcText + arcValue;
        Research.text = researchText + researchValue;
        Exploration.text = explorationText + explorationValue;
        Foraging.text = foragingText + foragingValue;
        Hunting.text = huntingText + huntingValue;
        Building.text = buildingText + buildingValue;
        Fishing.text = fishingText + fishingValue;
        Fighting.text = fightingText + fightingValue;
        Mining.text = miningText + miningValue;
        Husbandry.text = husbandryText + husbandryValue;
        Survival.text = survivalText + survivalValue;
        Camo.text = camoText + camoValue;
    }
}
