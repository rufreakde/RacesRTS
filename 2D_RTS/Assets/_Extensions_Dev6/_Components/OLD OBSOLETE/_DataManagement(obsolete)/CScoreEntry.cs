using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class CScoreEntry
{
/*
OBSOLETE!
OBSOLETE!
OBSOLETE!
*/

    #region STATIC ACCESS

    static public int CompareByScore(CScoreEntry _X, CScoreEntry _Y)
	{
		if(_X == null)
		{
			if(_Y == null)
				return 0;
			else
				return -1;
		}
		else
		{
			if(_Y == null || _X.Score > _Y.Score)
				return 1;
			else if(_X.Score < _Y.Score)
				return -1;
			else
				return 0;
		}
	}
	
	static public int CompareByTotalTime(CScoreEntry _X, CScoreEntry _Y)
	{
		if(_X == null)
		{
			if(_Y == null)
				return 0;
			else
				return -1;
		}
		else
		{
			if(_Y == null || _X.TotalTime > _Y.TotalTime)
				return 1;
			else if(_X.TotalTime < _Y.TotalTime)
				return -1;
			else
				return 0;
		}
	}
	
	static public int CompareByScoreAndTime(CScoreEntry _X, CScoreEntry _Y)
	{
		int result = -CompareByScore(_X, _Y);
		
		if(result == 0)
			result = CompareByTotalTime(_X, _Y);
		
		return result;
	}
	
	static public int CompareByTimeAndScore(CScoreEntry _X, CScoreEntry _Y)
	{
		int result = CompareByTotalTime(_X, _Y);
		
		if(result == 0)
			result = -CompareByScore(_X, _Y);
		
		return result;
	}
	
	#endregion



	#region PUBLIC VARIABLES

	public string Name;
	public float Score;
	public float TotalTime;

	public bool Multiplayer;
	public string Mode;
	public int Position;

	#endregion



	#region CONSTRUCTORS

	/// <summary>
	/// Initializes a new instance of the <see cref="CScoreEntry"/> class with default values.
	/// </summary>
	public CScoreEntry()
	{
		Name = "Unknown";
		Score = 0;
		TotalTime = 0.0f;

		Multiplayer = false;
		Mode = "S";
		Position = 0;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CScoreEntry"/> class with specified values.
	/// </summary>
	/// <param name="_Name">The player name.</param>
	/// <param name="_Score">The achieved score.</param>
	/// <param name="_TotalTime">The total time.</param>
	/// <param name="_Mode">The chosen mode.</param>
	/// <param name="_Position">The score's position.</param>
	public CScoreEntry(string _Name, float _Score, float _TotalTime, bool _Multiplayer, string _Mode, int _Position)
	{
		Name = _Name;
		Score = _Score;
		TotalTime = _TotalTime;

		Multiplayer = _Multiplayer;
		Mode = _Mode;
		Position = _Position;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CScoreEntry"/> class from the player preferences.
	/// </summary>
	/// <param name="_Mode">The chosen mode.</param>
	/// <param name="_Position">The score's position.</param>
	public CScoreEntry(bool _Multiplayer, string _Mode, int _Position, bool _Load = true)
	{
		Name = "Unknown";
		Score = 0;
		TotalTime = 0.0f;

		Multiplayer = _Multiplayer;
		Mode = _Mode;
		Position = _Position;

		if(_Load)
			FromPrefs();

		if(_Mode == "T" && TotalTime == 0.0f)
			TotalTime = 1000000;
	}

	#endregion



	#region PUBLIC ACCESS

	/// <summary>
	/// Saves the entry to the player preferences. Requires a valid mode and position.
	/// </summary>
	public void ToPrefs()
	{
		//Debug.Log("CScoreEntry: Saving self to preferences with key: Score_" + (Multiplayer ? "M" : "S") + "_" + Mode + "_" + Position);
		//Debug.Log("CScoreEntry: Name=" + Name + ", Score=" + Score + ", TotalTime=" + TotalTime);

		PlayerPrefs.SetString("Score_" + (Multiplayer ? "M" : "S") + "_" + Mode + "_" + Position + "_Name", Name);
		PlayerPrefs.SetFloat("Score_" + (Multiplayer ? "M" : "S") + "_" + Mode + "_" + Position + "_Score", Score);
		PlayerPrefs.SetFloat("Score_" + (Multiplayer ? "M" : "S") + "_" + Mode + "_" + Position + "_TotalTime", TotalTime);
	}

	/// <summary>
	/// Loads the entry from the player preferences. Requires a valid mode and position.
	/// </summary>
	public void FromPrefs()
	{
		//Debug.Log("CScoreEntry: Loading self from preferences with key: Score_" + (Multiplayer ? "M" : "S") + "_" + Mode + "_" + Position);

		if(PlayerPrefs.HasKey("Score_" + (Multiplayer ? "M" : "S") + "_" + Mode + "_" + Position + "_Name"))
			Name = PlayerPrefs.GetString("Score_" + (Multiplayer ? "M" : "S") + "_" + Mode + "_" + Position + "_Name");
		//else
			//Debug.LogWarning("CScoreEntry: Unable to locate Name key.");

		if(PlayerPrefs.HasKey("Score_" + (Multiplayer ? "M" : "S") + "_" + Mode + "_" + Position + "_Score"))
			Score = PlayerPrefs.GetFloat("Score_" + (Multiplayer ? "M" : "S") + "_" + Mode + "_" + Position + "_Score");
		//else
			//Debug.LogWarning("CScoreEntry: Unable to locate Score key.");

		if(PlayerPrefs.HasKey("Score_" + (Multiplayer ? "M" : "S") + "_" + Mode + "_" + Position + "_TotalTime"))
			TotalTime = PlayerPrefs.GetFloat("Score_" + (Multiplayer ? "M" : "S") + "_" + Mode + "_" + Position + "_TotalTime");
		//else
			//Debug.LogWarning("CScoreEntry: Unable to locate TotalTime key.");

		//Debug.Log("CScoreEntry: Name=" + Name + ", Score=" + Score + ", TotalTime=" + TotalTime);
	}

	#endregion
}
