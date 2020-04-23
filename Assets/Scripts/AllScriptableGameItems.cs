using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//https://bitbucket.org/richardfine/scriptableobjectdemo/src/default/Assets/ScriptableObject/GameSession/GameSettings.cs
[CreateAssetMenu]
public class AllScriptableGameItems : ScriptableObject
{
	#region code
	private static AllScriptableGameItems _instance;
	public static AllScriptableGameItems instance
	{
		get
		{
			if (!_instance)
				_instance = Resources.FindObjectsOfTypeAll<AllScriptableGameItems>().FirstOrDefault();
#if UNITY_EDITOR
			if (!_instance)
				InitializeFromDefault(UnityEditor.AssetDatabase.LoadAssetAtPath<AllScriptableGameItems>("Assets/All Scriptable Game Items.asset"));
#endif
			return _instance;
		}
	}

	public static void LoadFromJSON(string path)
	{
		if (!_instance) DestroyImmediate(_instance);
		_instance = ScriptableObject.CreateInstance<AllScriptableGameItems>();
		JsonUtility.FromJsonOverwrite(System.IO.File.ReadAllText(path), _instance);
		_instance.hideFlags = HideFlags.HideAndDontSave;
	}

	public void SaveToJSON(string path)
	{
		Debug.LogFormat("Saving game settings to {0}", path);
		System.IO.File.WriteAllText(path, JsonUtility.ToJson(this, true));
	}

	public static void InitializeFromDefault(AllScriptableGameItems settings)
	{
		if (_instance) DestroyImmediate(_instance);
		_instance = Instantiate(settings);
		_instance.hideFlags = HideFlags.HideAndDontSave;
	}

#if UNITY_EDITOR
	[UnityEditor.MenuItem("Window/Game Items")]
	public static void ShowGameSettings()
	{
		UnityEditor.Selection.activeObject = instance;
	}
#endif
	#endregion code

	#region collections

	private List<CharacterData> _characterDatas;
	public List<CharacterData> characterDatas
	{
		set
		{
			_characterDatas = value;
		}
		get
		{
			if (_characterDatas == null)
			{
				_characterDatas = new List<CharacterData>();
			}
			if (_characterDatas.Count == 0)
			{
#if UNITY_EDITOR
				// When working in the Editor and launching the game directly from the play scenes, rather than the
				// main menu, the brains may not be loaded and so Resources.FindObjectsOfTypeAll will not find them.
				// Instead, use the AssetDatabase to find them. At runtime, all available brains get loaded by the
				// MainMenuController so it's not a problem outside the editor.
				_characterDatas = UnityEditor.AssetDatabase.FindAssets("t:CharacterData")
								.Select(guid => UnityEditor.AssetDatabase.GUIDToAssetPath(guid))
								.Select(path => UnityEditor.AssetDatabase.LoadAssetAtPath<CharacterData>(path))
								.Where(b => b).ToList();
#else
				_characterDatas = Resources.FindObjectsOfTypeAll<CharacterData>();
#endif
			}
			return _characterDatas;
		}
	}


	#endregion collections
}
