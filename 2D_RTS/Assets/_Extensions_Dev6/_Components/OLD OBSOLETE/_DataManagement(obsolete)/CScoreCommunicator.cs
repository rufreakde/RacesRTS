using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/*
OBSOLETE!
OBSOLETE!
OBSOLETE!
*/

public class CScoreCommunicator : MonoBehaviour
{
	#region EVENTS

	public delegate void ScoreDownloadHandler(CScoreEntry[] _Entries);

	#endregion


	#region VARIABLES

	public string GetScoresURL = "http://www.dev-six.com/games/almka/scores.php";
	public string UploadScoresURL = "http://www.dev-six.com/games/almka/addscore.php";

	#endregion



	#region MONOBEHAVIOUR

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	#endregion



	#region PUBLIC ACCESS

	/// <summary>
	/// Gets the current online scores.
	/// </summary>
	/// <param name="_Multiplayer">Single or multiplayer scores.</param>
	/// <param name="_Mode">The selected mode.</param>
	/// <param name="_Amount">The maximum amount of scores to get.</param>
	public void GetScores(bool _Multiplayer, string _Mode, int _Amount, ScoreDownloadHandler _Callback)
	{
		// Get scores from server
		StartCoroutine(RequestScores(_Multiplayer, _Mode, _Amount, _Callback));
	}

	/// <summary>
	/// Uploads the scores to the database.
	/// </summary>
	/// <param name="_Multiplayer">Single or multiplayer scores.</param>
	/// <param name="_Mode">The selected mode.</param>
	/// <param name="_Entries">The entries to upload</param>
	public void UploadScores(bool _Multiplayer, string _Mode, CScoreEntry[] _Entries)
	{
		StartCoroutine(SendScores(_Multiplayer, _Mode, _Entries));
	}

	#endregion



	#region PRIVATE ACCESS

	private IEnumerator RequestScores(bool _Multiplayer, string _Mode, int _Amount, ScoreDownloadHandler _Callback)
	{
		// Create JsonTable with necessary data
		JsonTable info = new JsonTable();
		info.Add("multiplayer", _Multiplayer);
		info.Add("mode", _Mode);
		info.Add("amount", _Amount);

		//Debug.Log("Json: " + info.ToJson());

		// Attach info to url
		string json = GetScoresURL + "?json=" + info.ToJson();

		//Debug.Log("URL: " + json);

		// Load url
		WWW get = new WWW(json);

		// Await response
		yield return get;

		if(get.error != null)
		{
			Debug.LogError("CScoreCommunicator::RequestScores - Error getting scores from '" + GetScoresURL + "' with data '" + info.ToJson() + "'. Error: " + get.error);
			if(_Callback != null)
				_Callback(null);
			yield break;
		}

		json = get.text;
		//Debug.Log("CScoreCommunicator::RequestScores - Recieved scores: " + text);

		List<CScoreEntry> result = new List<CScoreEntry>(_Amount);
		if(json.Length > 2)
		{
			JsonObject obj = Json.Decode(json);
			info = obj.ToTable();
			JsonTable entry;
			for(int i = 0; i < _Amount; i++)
			{
				entry = info.GetSubtable("entry" + i);
				if(entry != null)
				{
					result.Add(new CScoreEntry(entry.ToString("name"), 
					                           (float)entry.ToDouble("score"), 
					                           (float)entry.ToDouble("time"),
					                           _Multiplayer,
					                           _Mode,
					                           i));
					//Debug.Log("Entry " + i + ": " + entry["name"] + ", " + entry["score"] + ", " + entry["time"]);
				}
				else
				{
					result.Add(new CScoreEntry(_Multiplayer, _Mode[0].ToString().ToUpper(), i, false));
					//Debug.Log("Entry " + i + ": No entry available.");
				}
			}
		}
		else
		{
			for(int i = 0; i < _Amount; i++)
			{
				result.Add(new CScoreEntry(_Multiplayer, _Mode[0].ToString().ToUpper(), i, false));
				//Debug.Log("Entry " + i + ": No entry available.");
			}
		}


		if(_Callback != null)
			_Callback(result.ToArray());
	}

	private IEnumerator SendScores(bool _Multiplayer, string _Mode, CScoreEntry[] _Entries)
	{
		// Generate json table
		JsonTable info = new JsonTable();
		info.Add("multiplayer", _Multiplayer);
		info.Add("mode", _Mode);
		info.Add("count", _Entries.Length);
		
		JsonTable entry;
		for(int i = 0; i < _Entries.Length; i++)
		{
			entry = new JsonTable();
			entry.Add("name", _Entries[i].Name.Replace(" ", ""));
			entry.Add("score", _Entries[i].Score);
			entry.Add("time", Mathf.Round(_Entries[i].TotalTime * 100.0f) / 100.0f);
			info.Add("entry" + i, entry);
		}

		//Debug.Log("Json: " + info.ToJson());

		// Get complete url
		string json = UploadScoresURL + "?json=" + info.ToJson();

		Debug.Log("URL: " + json);
		
		// Send string to server
		WWW post = new WWW(json);

		//Debug.Log("URL: " + post.url);

		// Await response
		yield return post;

		if(post.error != null)
			Debug.LogError("CScoreCommunicator::SendScores - Error uploading scores to '" + UploadScoresURL + "' with data '" + info.ToJson() + "'. Error: " + post.error);
		else
			Debug.Log("CScoreCommunicator::SendScores - Uploaded scores with result: " + post.text);
	}

	#endregion
}
