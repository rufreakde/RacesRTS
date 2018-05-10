using System;
using System.Collections;
using System.Globalization;
using System.Text;


public class JsonObject
{
	/// <summary>
	/// Enthält alle gültigen JsonObject Datentypen
	/// </summary>
	public enum Types
	{
	    Hashtable, ArrayList, String, Double, Boolean
	}
	private object data;

	/// <summary>
	/// Überprüft ob das übergebene Objekt gültig ist und speichert es im Erfolgsfall
	/// </summary>
	/// <exception cref="NullReferenceException">Wenn das übergebene Objekt NULL ist</exception>
	/// <exception cref="InvalidCastException">Wenn das übergebene Objekt ungültig ist</exception>
	/// <param name="input">Das im Konstruktor übergebene Objekt</param>
	private void GetInput(object input)
	{
	    int Valid = 0;
	    if (input == null) throw new NullReferenceException();

	    if (input is JsonObject)
	    {
	        this.GetInput(((JsonObject)input).ToObject());
	        return;
	    }

	    Valid += Convert.ToInt32(input is JsonTable);
	    Valid += Convert.ToInt32(input is ArrayList);
	    Valid += Convert.ToInt32(input is String);
	    Valid += Convert.ToInt32(input is Double);
	    Valid += Convert.ToInt32(input is Boolean);

	    if (Valid <= 0) throw new InvalidCastException();

	    this.data = input;
	}

	/// <summary>
	/// Repräsentiert ein Objekt, welches sich in einen JSON-String umwandeln lässt (Wrapper Objekt)
	/// </summary>
	/// <param name="JsonData">Hashtable, ArrayList, Double, String oder Boolean (nicht Null!)</param>
	public JsonObject(object JsonData) { GetInput(JsonData); }

	/// <summary>
	/// Liest das Original Objekt aus
	/// </summary>
	/// <returns>Das Original Objekt</returns>
	public object ToObject() { return this.data; }

	/// <summary>
	/// Liest das Original Objekt als Double aus
	/// </summary>
	/// <returns>Eine Zahl</returns>
	public double ToDouble() { return Convert.ToDouble(this.data); }

	/// <summary>
	/// Liest das Original Objekt als Boolean aus
	/// </summary>
	/// <returns>Ein boolscher Wert</returns>
	public bool ToBoolean() { return Convert.ToBoolean(this.data); }

	/// <summary>
	/// Liest das Original Objekt als Hashtable aus
	/// </summary>
	/// <returns>Eine Hashtable</returns>
	public JsonTable ToTable() { return (JsonTable)this.data; }

	/// <summary>
	/// Liest das Original Objekt als JsonArrayList aus
	/// </summary>
	/// <returns>Eine JsonArrayList</returns>
	public JsonArray ToArray() { return (JsonArray)this.data; }

	/// <summary>
	/// Liest das Original Objekt als String aus
	/// </summary>
	/// <returns>Ein String</returns>
	public override string ToString() { return this.data.ToString(); }


	/// <summary>
	/// Liest den Datentyp des Original Objekts aus
	/// </summary>
	/// <returns>Eine Zahl</returns>
	public new Type GetType() { return this.data.GetType(); }

	/// <summary>
	/// Bestimmt den Datentyp des Objekts und liefert ihn als Enumeration zurück
	/// </summary>
	/// <returns>Der Original Datentyp als Enumeration</returns>
	public Types GetRealType()
	{
	    if (data is Hashtable) return Types.Hashtable;
	    if (data is ArrayList) return Types.ArrayList;
	    if (data is Double) return Types.Double;
	    if (data is Boolean) return Types.Boolean;
	    return Types.String;
	}
}


public class JsonTable : Hashtable
{
	public JsonTable GetSubtable(string identifier)
	{
	    return (JsonTable)this[identifier];
	}

	public string ToJson(string identifier)
	{
	    return Json.Encode(new JsonObject(this[identifier]));
	}

	public string ToJson()
	{
	    return Json.Encode(new JsonObject(this));
	}

	public int ToInt()
	{
	    return Convert.ToInt32(this);
	}

	public int ToInt(string identifier)
	{
	    return Convert.ToInt32(this[identifier]);
	}

	public long ToLong()
	{
	    return Convert.ToInt64(this);
	}

	public long ToLong(string identifier)
	{
	    return Convert.ToInt64(this[identifier]);
	}

	public long ToLong2(string identifier)
	{
	    return (long)this[identifier];
	}

	public double ToDouble(string identifier)
	{
	    return Convert.ToDouble(this[identifier]);
	}

	public double ToDouble()
	{
	    return Convert.ToDouble(this);
	}


	public bool ToBoolean(string identifier)
	{
	    return Convert.ToBoolean(this[identifier]);
	}

	public bool ToBoolean()
	{
	    return Convert.ToBoolean(this);
	}

	public string ToString(string identifier)
	{
	    return Convert.ToString(this[identifier]);
	}

	public JsonArray ToArray(string identifier)
	{
	    return (JsonArray)(this[identifier]);
	}

	public bool IsArray(string identifier)
	{
	    if (this[identifier] is JsonArray)
	    {
	        return true;
	    }
	    else
	    {
	        return false;
	    }
	}

	public bool IsTable(string identifier)
	{
	    if (this[identifier] is JsonTable)
	    {
	        return true;
	    }
	    else
	    {
	        return false;
	    }
	}
}


public class JsonArray : ArrayList
{
	public JsonArray GetSubarray(int identifier)
	{
	    if (!(this[identifier] is ArrayList)) throw new InvalidCastException();
	    return (JsonArray)this[identifier];
	}

	public string ToJson(int identifier)
	{
	    return Json.Encode(new JsonObject(this[identifier]));
	}

	public string ToJson()
	{
	    return Json.Encode(new JsonObject(this));
	}

	public int ToInt(int identifier)
	{
	    return Convert.ToInt32(this[identifier]);
	}

	public int ToInt()
	{
	    return Convert.ToInt32(this);
	}

	public double ToDouble(int identifier)
	{
	    return Convert.ToDouble(this[identifier]);
	}

	public double ToDouble()
	{
	    return Convert.ToDouble(this);
	}

	public bool ToBoolean(int identifier)
	{
	    return Convert.ToBoolean(this[identifier]);
	}

	public bool ToBoolean()
	{
	    return Convert.ToBoolean(this);
	}

	public string ToString(int identifier)
	{
	    return Convert.ToString(this[identifier]);
	}

	public JsonTable ToTable(int identifier)
	{
	    return (JsonTable)this[identifier];
	}

	public bool IsTable(int identifier)
	{
	    if (this[identifier] is JsonTable)
	    {
	        return true;
	    }
	    else
	    {
	        return false;
	    }
	}

	public bool IsArray(int identifier)
	{
	    if (this[identifier] is JsonArray)
	    {
	        return true;
	    }
	    else
	    {
	        return false;
	    }
	}
}


/// <summary>
/// This class encodes and decodes JSON strings.
/// Spec. details, see http://www.json.org/
/// 
/// JSON uses Arrays and Objects. These correspond here to the datatypes ArrayList and Hashtable.
/// All numbers are parsed to doubles.
/// </summary>
public class Json
{
	private static int TOKEN_NONE = 0;
	private static int TOKEN_CURLY_OPEN = 1;
	private static int TOKEN_CURLY_CLOSE = 2;
	private static int TOKEN_SQUARED_OPEN = 3;
	private static int TOKEN_SQUARED_CLOSE = 4;
	private static int TOKEN_COLON = 5;
	private static int TOKEN_COMMA = 6;
	private static int TOKEN_STRING = 7;
	private static int TOKEN_NUMBER = 8;
	private static int TOKEN_TRUE = 9;
	private static int TOKEN_FALSE = 10;
	private static int TOKEN_NULL = 11;
	
	/// <summary>
	/// Liest einen JSON-String und erzeugt aus ihm ein JsonObject
	/// </summary>
	/// <exception cref="JsonDecodeException">Wird geworfen wenn beim dekodieren ein Fehler aufgetreten ist</exception>
	/// <param name="json">Ein JSON-String</param>
	/// <returns>Ein JsonObject</returns>
	public static JsonObject Decode(string json)
	{
		object result = null;
		bool success = false;
		
		result = Decode(json, out success);
		
		return (result == null) ? null : new JsonObject(result);
	}
	
	/// <summary>
	/// Liest einen JSON-String und erzeugt aus ihm ein JsonObject.
	/// </summary>
	/// <param name="json">Ein JSON-String</param>
	/// <param name="success">TRUE wenn alles funktioniert hat</param>
	/// <returns>Ein JsonObject ODER null</returns>
	public static JsonObject Decode(string json, out bool success)
	{
		success = false;
		object result = null;
		result = JsonDecode(json, ref success);
		return (result == null) ? null : new JsonObject(result);
	}
	
	private static object JsonDecode(string json, ref bool success)
	{
		success = true;
		if (json != null)
		{
			char[] charArray = json.ToCharArray();
			int index = 0;
			object value = ParseValue(charArray, ref index, ref success);
			return value;
		}
		else
		{
			return null;
		}
	}
	
	/// <summary>
	/// Erzeugt aus einem Json Objekt einen Json String
	/// </summary>
	/// <exception cref="JsonNotSerializableException">Wenn das übergeben Objekt nicht serialisierbar ist</exception>
	/// <param name="data">Ein Json Objekt</param>
	/// <returns>Einen JSON-String</returns>
	public static string Encode(JsonObject data)
	{
		string Result = null;
		bool success = false;
		
		Result = JsonEncode(data.ToObject(), out success);
		
		return Result;
	}
	
	/// <summary>
	/// Erzeugt aus einem Json Objekt einen Json String
	/// </summary>
	/// <param name="data">Ein Json Objekt</param>
	/// <param name="success">TRUE wenn alles funktioniert hat</param>
	/// <returns>Ein JSON-String ODER null</returns>
	public static string Encode(JsonObject data, out bool success)
	{
		success = false;
		string Result = JsonEncode(data.ToObject(), out success);
		success = (success == true && Result != null);
		return Result;
	}
	
	private static string JsonEncode(object json, out bool success)
	{
		StringBuilder builder = new StringBuilder();
		success = SerializeValue(json, builder);
		return (success ? builder.ToString() : null);
	}
	
	private static JsonTable ParseObject(char[] json, ref int index, ref bool success)
	{
		JsonTable table = new JsonTable();
		int token;
		
		// {
		NextToken(json, ref index);
		
		bool done = false;
		while (!done)
		{
			token = LookAhead(json, index);
			if (token == Json.TOKEN_NONE)
			{
				success = false;
				return null;
			}
			else if (token == Json.TOKEN_COMMA)
			{
				NextToken(json, ref index);
			}
			else if (token == Json.TOKEN_CURLY_CLOSE)
			{
				NextToken(json, ref index);
				return table;
			}
			else
			{
				
				// name
				string name = ParseString(json, ref index, ref success);
				if (!success)
				{
					success = false;
					return null;
				}
				
				// :
				token = NextToken(json, ref index);
				if (token != Json.TOKEN_COLON)
				{
					success = false;
					return null;
				}
				
				// value
				object oValue = ParseValue(json, ref index, ref success);
				if (!success)
				{
					success = false;
					return null;
				}
				
				table[name] = oValue;
			}
		}
		
		return table;
	}
	
	private static JsonArray ParseArray(char[] json, ref int index, ref bool success)
	{
		JsonArray array = new JsonArray();
		
		// [
		NextToken(json, ref index);
		
		bool done = false;
		while (!done)
		{
			int token = LookAhead(json, index);
			if (token == Json.TOKEN_NONE)
			{
				success = false;
				return null;
			}
			else if (token == Json.TOKEN_COMMA)
			{
				NextToken(json, ref index);
			}
			else if (token == Json.TOKEN_SQUARED_CLOSE)
			{
				NextToken(json, ref index);
				break;
			}
			else
			{
				object oValue = ParseValue(json, ref index, ref success);
				if (!success) return null;
				
				array.Add(oValue);
			}
		}
		
		return array;
	}
	
	private static object ParseValue(char[] json, ref int index, ref bool success)
	{
		int tmpValue = LookAhead(json, index);
		if (tmpValue == Json.TOKEN_STRING)
		{
			return ParseString(json, ref index, ref success);
		}
		else if (tmpValue == Json.TOKEN_NUMBER)
		{
			return ParseNumber(json, ref index);
		}
		else if (tmpValue == Json.TOKEN_CURLY_OPEN)
		{
			return ParseObject(json, ref index, ref success);
			
		}
		else if (tmpValue == Json.TOKEN_SQUARED_OPEN)
		{
			return ParseArray(json, ref index, ref success);
		}
		else if (tmpValue == Json.TOKEN_TRUE)
		{
			NextToken(json, ref index); return true;
		}
		else if (tmpValue == Json.TOKEN_FALSE)
		{
			NextToken(json, ref index); return false;
		}
		else if (tmpValue == Json.TOKEN_NULL)
		{
			NextToken(json, ref index); return null;
		}
		else if (tmpValue == Json.TOKEN_NONE)
		{
			
		}
		
		success = false;
		return null;
	}
	
	private static string ParseString(char[] json, ref int index, ref bool success)
	{
		StringBuilder s = new StringBuilder();
		char c;
		
		EatWhitespace(json, ref index);
		
		// "
		c = json[index++];
		
		bool complete = false, breakLoop = false;
		while (!complete && !breakLoop)
		{
			if (index == json.Length) break;
			
			c = json[index++];
			if (c == '"')
			{
				complete = true;
				break;
			}
			else if (c == '\\')
			{
				if (index == json.Length) break;
				c = json[index++];
				
				switch (c)
				{
				case '"': s.Append('"'); break;
				case '\\': s.Append('\\'); break;
				case '/': s.Append('/'); break;
				case 'b': s.Append('\b'); break;
				case 'f': s.Append('\f'); break;
				case 'n': s.Append('\n'); break;
				case 'r': s.Append('\r'); break;
				case 't': s.Append('\t'); break;
					
				case 'u':
					
					int remainingLength = json.Length - index;
					if (remainingLength >= 4)
					{
						// fetch the next 4 chars
						char[] unicodeCharArray = new char[4];
						Array.Copy(json, index, unicodeCharArray, 0, 4);
						// parse the 32 bit hex into an integer codepoint
						uint codePoint = Convert.ToUInt32(new string(unicodeCharArray), 16);
						// convert the integer codepoint to a unicode char and add to string
						s.Append(Char.ConvertFromUtf32((int)codePoint));
						// skip 4 chars
						index += 4;
					}
					else
					{
						breakLoop = true; // While Break
					}
					
					break;
				}
			}
			else
			{
				s.Append(c);
			}
		}
		
		if (!complete)
		{
			success = false;
			return null;
		}
		
		return s.ToString();
	}
	
	protected static double ParseNumber(char[] json, ref int index)
	{
		EatWhitespace(json, ref index);
		
		int lastIndex = GetLastIndexOfNumber(json, index);
		int charLength = (lastIndex - index) + 1;
		char[] numberCharArray = new char[charLength];
		
		Array.Copy(json, index, numberCharArray, 0, charLength);
		index = lastIndex + 1;
		return Double.Parse(new string(numberCharArray));
	}
	
	private static int GetLastIndexOfNumber(char[] json, int index)
	{
		int lastIndex;
		for (lastIndex = index; lastIndex < json.Length; lastIndex++)
		{
			if ("0123456789+-.eE".IndexOf(json[lastIndex]) == -1) break;
		}
		return lastIndex - 1;
	}
	
	private static void EatWhitespace(char[] json, ref int index)
	{
		for (; index < json.Length; index++)
		{
			if (" \t\n\r".IndexOf(json[index]) == -1) break;
		}
	}
	
	private static int LookAhead(char[] json, int index)
	{
		int saveIndex = index;
		return NextToken(json, ref saveIndex);
	}
	
	private static bool CheckKeyWord(string Keyword, char[] Json, ref int index)
	{
		int i = index;
		char[] Compare = Keyword.ToCharArray();
		
		for (i = 0; i < Compare.Length; i++)
		{
			if (Compare[i] != Json[i + index]) return false;
		}
		index += i;
		return true;
	}
	
	private static int NextToken(char[] json, ref int index)
	{
		EatWhitespace(json, ref index);
		if (index == json.Length) return Json.TOKEN_NONE;
		
		switch (json[index++])
		{
		case '{':
			return Json.TOKEN_CURLY_OPEN;
		case '}':
			return Json.TOKEN_CURLY_CLOSE;
		case '[':
			return Json.TOKEN_SQUARED_OPEN;
		case ']':
			return Json.TOKEN_SQUARED_CLOSE;
		case ',':
			return Json.TOKEN_COMMA;
		case '"':
			return Json.TOKEN_STRING;
		case '0':
		case '1':
		case '2':
		case '3':
		case '4':
		case '5':
		case '6':
		case '7':
		case '8':
		case '9':
		case '-':
			return Json.TOKEN_NUMBER;
		case ':':
			return Json.TOKEN_COLON;
		}
		index--;
		
		int remainingLength = json.Length - index;
		
		if (remainingLength >= 5 && CheckKeyWord("false", json, ref index)) return Json.TOKEN_FALSE;
		if (remainingLength >= 4 && CheckKeyWord("true", json, ref index)) return Json.TOKEN_TRUE;
		if (remainingLength >= 4 && CheckKeyWord("null", json, ref index)) return Json.TOKEN_NULL;
		
		return Json.TOKEN_NONE;
	}
	
	private static bool SerializeValue(object value, StringBuilder builder)
	{
		bool success = true;
		
		if (value is string) success = SerializeString((string)value, builder);
		else if (value is JsonTable) success = SerializeObject((JsonTable)value, builder);
		else if (value is ArrayList) success = SerializeArray((ArrayList)value, builder);
		else if (IsNumeric(value)) success = SerializeNumber(Convert.ToDouble(value), builder);
		else if ((value is Boolean))
		{
			if ((Boolean)value == true) builder.Append("true");
			else builder.Append("false");
		}
		else if (value == null) builder.Append("null");
		else success = false;
		
		return success;
	}
	
	private static bool SerializeObject(JsonTable anObject, StringBuilder builder)
	{
		builder.Append("{");
		
		IDictionaryEnumerator e = anObject.GetEnumerator();
		bool first = true;
		while (e.MoveNext())
		{
			string key = e.Key.ToString();
			object value = e.Value;
			
			if (!first) builder.Append(",");
			
			SerializeString(key, builder);
			builder.Append(":");
			if (!SerializeValue(value, builder)) return false;
			
			first = false;
		}
		
		builder.Append("}");
		return true;
	}
	
	private static bool SerializeArray(ArrayList anArray, StringBuilder builder)
	{
		builder.Append("[");
		
		bool first = true;
		for (int i = 0; i < anArray.Count; i++)
		{
			if (!first) builder.Append(",");
			if (!SerializeValue(anArray[i], builder)) return false;
			first = false;
		}
		
		builder.Append("]");
		return true;
	}
	
	private static bool SerializeString(string aString, StringBuilder builder)
	{
		builder.Append("\"");
		
		char[] charArray = aString.ToCharArray();
		for (int i = 0; i < charArray.Length; i++)
		{
			char c = charArray[i];
			
			switch (c)
			{
			case '"': builder.Append("\\\""); break;
			case '\\': builder.Append("\\\\"); break;
			case '\b': builder.Append("\\b"); break;
			case '\f': builder.Append("\\f"); break;
			case '\r': builder.Append("\\r"); break;
			case '\t': builder.Append("\\t"); break;
				
			default:
				int codepoint = Convert.ToInt32(c);
				if ((codepoint >= 32) && (codepoint <= 126)) builder.Append(c);
				else builder.Append("\\u" + Convert.ToString(codepoint, 16).PadLeft(4, '0'));
				break;
			}
		}
		
		builder.Append("\"");
		return true;
	}
	
	private static bool SerializeNumber(double number, StringBuilder builder)
	{
		builder.Append(number.ToString());
		return true;
	}
	
	private static bool IsNumeric(object o)
	{
		double result;
		return (o == null) ? false : Double.TryParse(o.ToString(), out result);
	}
}
