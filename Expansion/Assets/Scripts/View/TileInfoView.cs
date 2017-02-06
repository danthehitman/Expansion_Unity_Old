using UnityEngine;
using UnityEngine.UI;

public class TileInfoView : MonoBehaviour
{
    public Text Coords;
    public Text Height;
    public Text Heat;
    public Text Moisture;
    public Text Biome;
    public Text Collidable;

    private string coordText = "Coords: ";
    private string coordValue = "";
    private string heightText = "Height: ";
    private string heightValue = "";
    private string heatText = "Heat: ";
    private string heatValue = "";
    private string moistureText = "Moisture: ";
    private string moistureValue = "";
    private string biomeText = "Biome: ";
    private string biomeValue = "";
    private string collidableText = "Collidable: ";
    private string collidableValue = "";

    public void UpdateInfo(BaseTile tile)
    {
        if (tile != null)
        {
            coordValue = tile.X + ", " + tile.Y;
            heightValue = tile.HeightType.ToString() + " (" + tile.HeightValue +")";
            heatValue = tile.HeatType.ToString() + " (" + tile.HeatValue + ")";
            moistureValue = tile.MoistureType.ToString() + "(" + tile.MoistureValue + ")";
            biomeValue = tile.BiomeType.ToString();
            collidableValue = tile.Collidable.ToString();
        }
        else
        {
            coordValue = heightValue = heatValue = moistureValue = biomeValue = collidableValue = null;
        }
        Coords.text = coordText + coordValue;
        Height.text = heightText + heightValue;
        Heat.text = heatText + heatValue;
        Moisture.text = moistureText + moistureValue;
        Biome.text = biomeText + biomeValue;
        Collidable.text = collidableText + collidableValue;
    }
}
