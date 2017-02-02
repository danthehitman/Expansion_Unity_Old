using AccidentalNoise;
using UnityEngine;

public class WrappingWorldGenerator : Generator  {
        
    protected ImplicitFractal HeightMap;
    protected ImplicitCombiner HeatMap;
    protected ImplicitFractal MoistureMap;

    public WrappingWorldGenerator(World world, int height, int width) : base(world, height, width)
    {
    }

    protected override void Initialize()
    {
        // HeightMap
        HeightMap = new ImplicitFractal (FractalType.MULTI, 
                                         BasisType.SIMPLEX, 
                                         InterpolationType.QUINTIC, 
                                         TerrainOctaves, 
                                         TerrainFrequency, 
                                         Seed);
                
        // Heat Map
        ImplicitGradient gradient  = new ImplicitGradient (1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1);
        ImplicitFractal heatFractal = new ImplicitFractal(FractalType.MULTI, 
                                                          BasisType.SIMPLEX, 
                                                          InterpolationType.QUINTIC, 
                                                          HeatOctaves, 
                                                          HeatFrequency, 
                                                          Seed);
        
        HeatMap = new ImplicitCombiner (CombinerType.MULTIPLY);
        HeatMap.AddSource (gradient);
        HeatMap.AddSource (heatFractal);
        
        // Moisture Map
        MoistureMap = new ImplicitFractal (FractalType.MULTI, 
                                           BasisType.SIMPLEX, 
                                           InterpolationType.QUINTIC, 
                                           MoistureOctaves, 
                                           MoistureFrequency, 
                                           Seed);	
    }

    protected override void GetData()
    {
        HeightData = new MapData (width, height);
        HeatData = new MapData (width, height);
        MoistureData = new MapData (width, height);
        
        // loop through each x,y point - get height value
        for (var x = 0; x < width; x++) {
            for (var y = 0; y < height; y++) {
                
                // WRAP ON BOTH AXIS
                // Noise range
                float x1 = 0, x2 = 2;
                float y1 = 0, y2 = 2;				
                float dx = x2 - x1;
                float dy = y2 - y1;
                
                // Sample noise at smaller intervals
                float s = x / (float)width;
                float t = y / (float)height;
                
                // Calculate our 4D coordinates
                float nx = x1 + Mathf.Cos (s*2*Mathf.PI) * dx/(2*Mathf.PI);
                float ny = y1 + Mathf.Cos (t*2*Mathf.PI) * dy/(2*Mathf.PI);
                float nz = x1 + Mathf.Sin (s*2*Mathf.PI) * dx/(2*Mathf.PI);
                float nw = y1 + Mathf.Sin (t*2*Mathf.PI) * dy/(2*Mathf.PI);				

                float heightValue = (float)HeightMap.Get (nx, ny, nz, nw);
                float heatValue = (float)HeatMap.Get (nx, ny, nz, nw);
                float moistureValue = (float)MoistureMap.Get (nx, ny, nz, nw);
                
                // keep track of the max and min values found
                if (heightValue > HeightData.Max) HeightData.Max = heightValue;
                if (heightValue < HeightData.Min) HeightData.Min = heightValue;
                
                if (heatValue > HeatData.Max) HeatData.Max = heatValue;
                if (heatValue < HeatData.Min) HeatData.Min = heatValue;
                
                if (moistureValue > MoistureData.Max) MoistureData.Max = moistureValue;
                if (moistureValue < MoistureData.Min) MoistureData.Min = moistureValue;
                
                HeightData.Data[x,y] = heightValue;
                HeatData.Data[x,y] = heatValue;
                MoistureData.Data[x,y] = moistureValue;		
            }
        }			
    }
    
    protected override BaseTile GetTop(BaseTile t)
    {
        return Tiles [t.X, MathHelper.Mod (t.Y - 1, height)];
    }
    protected override BaseTile GetBottom(BaseTile t)
    {
        return Tiles [t.X, MathHelper.Mod (t.Y + 1, height)];
    }
    protected override BaseTile GetLeft(BaseTile t)
    {
        return Tiles [MathHelper.Mod(t.X - 1, height), t.Y];
    }
    protected override BaseTile GetRight(BaseTile t)
    {
        return Tiles [MathHelper.Mod (t.X + 1, height), t.Y];
    }
}
