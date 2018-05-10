using UnityEngine;
using System.Collections;

public class GetTextFromInternet : MonoBehaviour 
{
	public delegate void CallbackHandler(bool _Success, string _Text);

	/// <summary>
	/// The url to use for requests.
	/// </summary>
	public string URL = "http://www.dev-six.com/games/almka/language.php";

	/// <summary>
	/// Sends a www request with the specified parameters and calls the callback function afterwards.
	/// </summary>
	/// <param name="_Parameters">The parameters.</param>
	/// <param name="_Callback">The callback function.</param>
	public void Request(string _Parameters, CallbackHandler _Callback)
	{
		StartCoroutine(SendRequest(_Parameters, _Callback));
	}

	/// <summary>
	/// Sends a www request with the specified parameters and calls the callback function with a success flag and the received text.
	/// </summary>
	/// <param name="_Parameters">The parameters.</param>
	/// <param name="_Callback">The callback function.</param>
	private IEnumerator SendRequest(string _Parameters, CallbackHandler _Callback)
	{
		bool success = false;
		string text = "";
		
		// Create url request
		string url = URL + _Parameters;
		//Debug.Log(this.name + ": URL=" + url);
		WWW request = new WWW(url);
		
		// Wait for response
		yield return request;
		
		// Handle error
		if(request.error != null)
		{
			text = request.error;
		}
		else
		{
			success = true;
			text = request.text;
		}

		//Debug.Log(this.name + ": Returning \"" + text + "\" with success: " + success);
		
		if(_Callback != null)
			_Callback(success, text);

		Destroy(gameObject);
	}
}
