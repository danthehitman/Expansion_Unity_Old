﻿using System.Collections.Generic;

public enum TileGroupType
{
	Water, 
	Land
}

public class TileGroup  {
	
	public TileGroupType Type;
	public List<BaseTile> Tiles;

	public TileGroup()
	{
		Tiles = new List<BaseTile> ();
	}
}
