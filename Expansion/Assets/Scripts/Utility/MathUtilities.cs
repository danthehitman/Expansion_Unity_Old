public class MathUtilities
{
    public static float GetFloatAtPercentBetween(float min, float max, float percentage)
    {
        return (max - min) * percentage;
    }

    public static int GetIntAtPercentBetween(int min, int max, int percentage)
    {
        return (max - min) * percentage;
    }
}
