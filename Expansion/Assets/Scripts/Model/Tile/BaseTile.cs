using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BaseTile : INotifyPropertyChanged
{
    public TerrainInfo TerrainData  { get; set; }
    public TileResourceInfo TileResourceData { get; set; }

    public BaseTile Left { get; set; }
    public BaseTile Right { get; set; }
    public BaseTile Top { get; set; }
    public BaseTile Bottom { get; set; }

    public TileCache Cache { get; set; }

    public bool Explored { get; set; }

    public EventHandler TileDataChanged;

    public event PropertyChangedEventHandler PropertyChanged;

    public World World { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public BaseTile(World world, int x, int y)
    {
        World = world;
        X = x;
        Y = y;
        TerrainData = new TerrainInfo();
        TileResourceData = new TileResourceInfo();
        TerrainData.Rivers = new List<River>();
        Cache = new TileCache();
    }

    public void ExploreTile(object entity = null)
    {
        var human = entity as HumanEntity;
        if (human != null)
        {
            AddInventoryToCache(TileExplorer.ExploreTile(human, this));
        }
        this.Explored = true;
        Debug.Log("Explored tile.");
    }

    public void AddInventoryToCache(Inventory inventory)
    {

    }

    protected void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
            handler(this, e);
    }

    protected void OnPropertyChanged(string propertyName)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    public void UpdateBiomeBitmask()
    {
        int count = 0;

        if (TerrainData.Collidable && Top != null && Top.TerrainData.BiomeType == TerrainData.BiomeType)
            count += 1;
        if (TerrainData.Collidable && Bottom != null && Bottom.TerrainData.BiomeType == TerrainData.BiomeType)
            count += 4;
        if (TerrainData.Collidable && Left != null && Left.TerrainData.BiomeType == TerrainData.BiomeType)
            count += 8;
        if (TerrainData.Collidable && Right != null && Right.TerrainData.BiomeType == TerrainData.BiomeType)
            count += 2;

        TerrainData.BiomeBitmask = count;
    }

    public void UpdateBitmask()
    {
        int count = 0;

        if (TerrainData.Collidable && Top != null && Top.TerrainData.HeightType == TerrainData.HeightType)
            count += 1;
        if (TerrainData.Collidable && Right != null && Right.TerrainData.HeightType == TerrainData.HeightType)
            count += 2;
        if (TerrainData.Collidable && Bottom != null && Bottom.TerrainData.HeightType == TerrainData.HeightType)
            count += 4;
        if (TerrainData.Collidable && Left != null && Left.TerrainData.HeightType == TerrainData.HeightType)
            count += 8;

        TerrainData.Bitmask = count;
    }

    public int GetRiverNeighborCount(River river)
    {
        int count = 0;
        if (Left != null && Left.TerrainData.Rivers.Count > 0 && Left.TerrainData.Rivers.Contains(river))
            count++;
        if (Right != null && Right.TerrainData.Rivers.Count > 0 && Right.TerrainData.Rivers.Contains(river))
            count++;
        if (Top != null && Top.TerrainData.Rivers.Count > 0 && Top.TerrainData.Rivers.Contains(river))
            count++;
        if (Bottom != null && Bottom.TerrainData.Rivers.Count > 0 && Bottom.TerrainData.Rivers.Contains(river))
            count++;
        return count;
    }    

    public void SetRiverPath(River river)
    {
        if (!TerrainData.Collidable)
            return;

        if (!TerrainData.Rivers.Contains(river))
        {
            TerrainData.Rivers.Add(river);
        }
    }

    private void SetRiverTile(River river)
    {
        SetRiverPath(river);
        TerrainData.HeightType = HeightType.River;
        TerrainData.HeightValue = 0;
        TerrainData.Collidable = false;
    }

    // This function got messy.  Sorry.
    public void DigRiver(River river, int size)
    {
        SetRiverTile(river);
        TerrainData.RiverSize = size;

        if (size == 1)
        {
            if (Bottom != null)
            {
                Bottom.SetRiverTile(river);
                if (Bottom.Right != null) Bottom.Right.SetRiverTile(river);
            }
            if (Right != null) Right.SetRiverTile(river);
        }

        if (size == 2)
        {
            if (Bottom != null)
            {
                Bottom.SetRiverTile(river);
                if (Bottom.Right != null) Bottom.Right.SetRiverTile(river);
            }
            if (Right != null)
            {
                Right.SetRiverTile(river);
            }
            if (Top != null)
            {
                Top.SetRiverTile(river);
                if (Top.Left != null) Top.Left.SetRiverTile(river);
                if (Top.Right != null) Top.Right.SetRiverTile(river);
            }
            if (Left != null)
            {
                Left.SetRiverTile(river);
                if (Left.Bottom != null) Left.Bottom.SetRiverTile(river);
            }
        }

        if (size == 3)
        {
            if (Bottom != null)
            {
                Bottom.SetRiverTile(river);
                if (Bottom.Right != null) Bottom.Right.SetRiverTile(river);
                if (Bottom.Bottom != null)
                {
                    Bottom.Bottom.SetRiverTile(river);
                    if (Bottom.Bottom.Right != null) Bottom.Bottom.Right.SetRiverTile(river);
                }
            }
            if (Right != null)
            {
                Right.SetRiverTile(river);
                if (Right.Right != null)
                {
                    Right.Right.SetRiverTile(river);
                    if (Right.Right.Bottom != null) Right.Right.Bottom.SetRiverTile(river);
                }
            }
            if (Top != null)
            {
                Top.SetRiverTile(river);
                if (Top.Left != null) Top.Left.SetRiverTile(river);
                if (Top.Right != null) Top.Right.SetRiverTile(river);
            }
            if (Left != null)
            {
                Left.SetRiverTile(river);
                if (Left.Bottom != null) Left.Bottom.SetRiverTile(river);
            }
        }

        if (size == 4)
        {

            if (Bottom != null)
            {
                Bottom.SetRiverTile(river);
                if (Bottom.Right != null) Bottom.Right.SetRiverTile(river);
                if (Bottom.Bottom != null)
                {
                    Bottom.Bottom.SetRiverTile(river);
                    if (Bottom.Bottom.Right != null) Bottom.Bottom.Right.SetRiverTile(river);
                }
            }
            if (Right != null)
            {
                Right.SetRiverTile(river);
                if (Right.Right != null)
                {
                    Right.Right.SetRiverTile(river);
                    if (Right.Right.Bottom != null) Right.Right.Bottom.SetRiverTile(river);
                }
            }
            if (Top != null)
            {
                Top.SetRiverTile(river);
                if (Top.Right != null)
                {
                    Top.Right.SetRiverTile(river);
                    if (Top.Right.Right != null) Top.Right.Right.SetRiverTile(river);
                }
                if (Top.Top != null)
                {
                    Top.Top.SetRiverTile(river);
                    if (Top.Top.Right != null) Top.Top.Right.SetRiverTile(river);
                }
            }
            if (Left != null)
            {
                Left.SetRiverTile(river);
                if (Left.Bottom != null)
                {
                    Left.Bottom.SetRiverTile(river);
                    if (Left.Bottom.Bottom != null) Left.Bottom.Bottom.SetRiverTile(river);
                }

                if (Left.Left != null)
                {
                    Left.Left.SetRiverTile(river);
                    if (Left.Left.Bottom != null) Left.Left.Bottom.SetRiverTile(river);
                    if (Left.Left.Top != null) Left.Left.Top.SetRiverTile(river);
                }

                if (Left.Top != null)
                {
                    Left.Top.SetRiverTile(river);
                    if (Left.Top.Top != null) Left.Top.Top.SetRiverTile(river);
                }
            }
        }
    } 
}