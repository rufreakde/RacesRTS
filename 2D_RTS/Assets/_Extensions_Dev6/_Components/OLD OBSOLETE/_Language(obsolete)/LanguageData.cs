using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

/// <summary>
/// Class holding the current game language's data.
/// </summary>
public class LanguageData
{
	#region VARIABLES

	/// <summary>
	/// The dictionary of keywords and corresponding texts.
	/// </summary>
	private Dictionary<string, string> texts;

	#endregion



	#region INITIALIZATION

	/// <summary>
	/// Initializes a new instance of the <see cref="LanguageData"/> class.
	/// </summary>
	public LanguageData()
	{
		texts = new Dictionary<string, string>();
	}

	#endregion



	#region LOADING

	/// <summary>
	/// Load the specified language from a xml file.
	/// </summary>
	/// <param name="_Language">The language to load.</param>
	public bool Load(SystemLanguage _Language)
	{
		// Clear old data
		texts.Clear();
		
		FileStream file = null;
		try
		{
			// Open the file
			file = new FileStream(Application.streamingAssetsPath + Language.Path + _Language + ".xml", FileMode.Open);
		}
		catch(IOException exception)
		{
			// Return if there was an error
			Debug.LogException(exception);
			return false;
		}
		
		// Load xml document
		XmlDocument xml = new XmlDocument();
		xml.Load(file);
		
		// Get inner nodes
		XmlNodeList nodes = xml.DocumentElement.ChildNodes;
		for(int i = 0; i < nodes.Count; i++)
			texts.Add(nodes[i].Name, nodes[i].InnerText);
		
		// Close the file stream
		file.Close();
		
		return true;
	}

	/// <summary>
	/// Parse the language out of a JSON string.
	/// </summary>
	/// <param name="_JSON">The JSON string to load.</param>
	public bool Load(string _JSON)
	{
		if(_JSON.Length <= 2)
			return false;
		
		JsonObject obj = Json.Decode(_JSON);
		JsonTable tbl = obj.ToTable();
		
		foreach(string key in tbl.Keys)
			texts.Add(key, (string)tbl[key]);

		return true;
	}

	#endregion



	#region SAVING

	/// <summary>
	/// Save the current texts in a xml file.
	/// </summary>
	/// <param name="_Language">The language to save as.</param>
	public bool Save(SystemLanguage _Language)
	{
		XmlDocument xml = new XmlDocument();
		XmlNode root = xml.CreateElement(_Language.ToString());

		XmlNode node;
		foreach(KeyValuePair<string, string> pair in texts)
		{
			node = xml.CreateElement(pair.Key);
			node.InnerText = pair.Value;
			root.AppendChild(node);
		}

		xml.AppendChild(root);
		xml.Save(Application.streamingAssetsPath + Language.Path + _Language.ToString() + ".xml");

		return true;
	}

	#endregion



	#region ACCESS

	/// <summary>
	/// Returns the text for the specified key.
	/// </summary>
	/// <returns>The text.</returns>
	/// <param name="_Key">The key to look for.</param>
	public string GetText(string _Key)
	{
		if(texts.ContainsKey(_Key))
			return texts[_Key];

		return string.Empty;
	}

	#endregion
}
