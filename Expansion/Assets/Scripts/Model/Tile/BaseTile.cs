using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BaseTile : INotifyPropertyChanged, IHasContext
{
    #region Terrain Information
    public HeightType HeightType { get; set; }
    public HeatType HeatType { get; set; }
    public MoistureType MoistureType { get; set; }
    public BiomeType BiomeType { get; set; }

    public float HeightValue { get; set; }
    public float HeatValue { get; set; }
    public float MoistureValue { get; set; }

    public int Bitmask { get; set; }
    public int BiomeBitmask { get; set; }

    public BaseTile Left { get; set; }
    public BaseTile Right { get; set; }
    public BaseTile Top { get; set; }
    public BaseTile Bottom { get; set; }

    public bool Collidable { get; set; }
    public bool FloodFilled { get; set; }

    public List<River> Rivers { get; set; }

    public int RiverSize { get; set; }
    #endregion

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
        Rivers = new List<River>();

        actions = new List<ContextAction>()
        {
            new ContextAction("Explore", ExploreTile)
        };
    }

    public void ExploreTile()
    {
        Debug.Log("Explored tile.");
        Explored = true;
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

        if (Collidable && Top != null && Top.BiomeType == BiomeType)
            count += 1;
        if (Collidable && Bottom != null && Bottom.BiomeType == BiomeType)
            count += 4;
        if (Collidable && Left != null && Left.BiomeType == BiomeType)
            count += 8;
        if (Collidable && Right != null && Right.BiomeType == BiomeType)
            count += 2;

        BiomeBitmask = count;
    }

    public void UpdateBitmask()
    {
        int count = 0;

        if (Collidable && Top != null && Top.HeightType == HeightType)
            count += 1;
        if (Collidable && Right != null && Right.HeightType == HeightType)
            count += 2;
        if (Collidable && Bottom != null && Bottom.HeightType == HeightType)
            count += 4;
        if (Collidable && Left != null && Left.HeightType == HeightType)
            count += 8;

        Bitmask = count;
    }

    public int GetRiverNeighborCount(River river)
    {
        int count = 0;
        if (Left != null && Left.Rivers.Count > 0 && Left.Rivers.Contains(river))
            count++;
        if (Right != null && Right.Rivers.Count > 0 && Right.Rivers.Contains(river))
            count++;
        if (Top != null && Top.Rivers.Count > 0 && Top.Rivers.Contains(river))
            count++;
        if (Bottom != null && Bottom.Rivers.Count > 0 && Bottom.Rivers.Contains(river))
            count++;
        return count;
    }

    public Direction GetLowestNeighbor(Generator generator)
    {
        float left = generator.GetHeightValue(Left);
        float right = generator.GetHeightValue(Right);
        float bottom = generator.GetHeightValue(Bottom);
        float top = generator.GetHeightValue(Top);

        if (left < right && left < top && left < bottom)
            return Direction.Left;
        else if (right < left && right < top && right < bottom)
            return Direction.Right;
        else if (top < left && top < right && top < bottom)
            return Direction.Top;
        else if (bottom < top && bottom < right && bottom < left)
            return Direction.Bottom;
        else
            return Direction.Bottom;
    }

    public void SetRiverPath(River river)
    {
        if (!Collidable)
            return;

        if (!Rivers.Contains(river))
        {
            Rivers.Add(river);
        }
    }

    private void SetRiverTile(River river)
    {
        SetRiverPath(river);
        HeightType = HeightType.River;
        HeightValue = 0;
        Collidable = false;
    }

    // This function got messy.  Sorry.
    public void DigRiver(River river, int size)
    {
        SetRiverTile(river);
        RiverSize = size;

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

    private IEnumerable<ContextAction> actions;
    public IEnumerable<ContextAction> GetActions()
    {
        return actions;
    }
}