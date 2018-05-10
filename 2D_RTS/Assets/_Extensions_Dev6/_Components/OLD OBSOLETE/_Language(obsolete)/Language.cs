using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class handling the game's language.
/// </summary>
static public class Language
{
	public delegate void ChangeCallback();



	/// <summary>
	/// The language files' path.
	/// </summary>
	public const string Path = "/Language/";
	/// <summary>
	/// The default game language.
	/// </summary>
	public const SystemLanguage Default = SystemLanguage.English;

	/// <summary>
	/// The current game language.
	/// </summary>
	static public SystemLanguage Current = SystemLanguage.English;
	/// <summary>
	/// The current language's data.
	/// </summary>
	static private LanguageData CurrentData;

	/// <summary>
	/// Flag indicating the class's availability.
	/// </summary>
	static public bool Ready = true;
	/// <summary>
	/// The callback for language requests.
	/// </summary>
	static private ChangeCallback ReadyCallback;



	/// <summary>
	/// Initializes the default language.
	/// </summary>
	static public void Initialize()
	{
		CurrentData = new LanguageData();
		Set(Current, null);
	}

	/// <summary>
	/// Set the current language, downloading missing language files.
	/// </summary>
	/// <param name="_Language">The new language.</param>
	static public void Set(SystemLanguage _Language, ChangeCallback _Callback)
	{
		if(!Ready)
			return;

		Current = _Language;

		if(!CurrentData.Load(Current))
		{
			Debug.Log("Language: Missing language file, requesting.");

			Ready = false;
			ReadyCallback = _Callback;

			GameObject go = new GameObject("Download");
			GetTextFromInternet dl = go.AddComponent<GetTextFromInternet>();
			dl.Request("?language=" + _Language.ToString(), RequestCallback);
		}

		if(Ready && _Callback != null)
			_Callback();
	}

	/// <summary>
	/// Set the current language to the default.
	/// </summary>
	static public bool SetDefault()
	{
		Current = Default;
		
		if(!CurrentData.Load(Current))
		{
			Debug.LogError("Language: Missing default language data (" + Default.ToString() + ")!");
			return false;
		}
		
		return true;
	}

	/// <summary>
	/// Gets the text corresponding to the specified key.
	/// </summary>
	/// <returns>The text.</returns>
	/// <param name="_Key">The key to search for.</param>
	static public string GetText(string _Key)
	{
		if(CurrentData == null)
		{
			Debug.LogError("Language: Unable to get string for key \"" + _Key + "\": missing valid language data!");
			return string.Empty;
		}

		return CurrentData.GetText(_Key);
	}

	/// <summary>
	/// Determines if the specified language is available.
	/// </summary>
	/// <returns><c>true</c> if the specified language is available, otherwise <c>false</c>.</returns>
	/// <param name="_Language">The language to check.</param>
	static public bool IsAvailable(SystemLanguage _Language)
	{
		LanguageData data = new LanguageData();
		return data.Load(_Language);
	}
	
	/// <summary>
	/// Called after a language request.
	/// </summary>
	/// <param name="_Success">The success of the request.</param>
	/// <param name="_Text">The text returned by the request.</param>
	static private void RequestCallback(bool _Success, string _Text)
	{
		Debug.Log("Language: Language request finished.");

		if(!_Success)
		{
			Debug.LogError("Language: Unable to load language \"" + Current + "\", reverting to default. Error: " + _Text);
			SetDefault();
		}
		else
		{
			// Load data
			CurrentData.Load(_Text);
			
			// Save data to disc
			CurrentData.Save(Current);
		}

		Ready = true;
		if(ReadyCallback != null)
		{
			ReadyCallback();
			ReadyCallback = null;
		}
	}
}