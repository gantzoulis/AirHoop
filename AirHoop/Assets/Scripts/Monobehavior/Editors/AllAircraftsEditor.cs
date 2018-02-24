using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AllAircrafts))]
public class AllAircraftsEditor : Editor 
{
	public static string[] AllAircraftsNames
	{
		get
		{
			if(allAircraftNames == null)
			{
				SetAllAircraftNames();
			}
			return allAircraftNames;
		}
		private set 
		{
			allAircraftNames = value;
		}
	}

	private static string[] allAircraftNames;

	private AircraftEditor[] aircraftEditors;
	private AllAircrafts allAircrafts;
	private string newAircraftName = "New Aircraft";

	private const string creationPath = "Assets/Resources/Aircrafts/AllAircrafts.asset";
	private const float buttonWidth = 30f;
	private const float refreshButtonWidth = 90f;

	private void OnEnable()
	{
		allAircrafts = (AllAircrafts)target;

		if(allAircrafts.aircrafts == null)
		{
			allAircrafts.aircrafts = new Aircraft[0];
		}

		if(aircraftEditors == null)
		{
			CreateEditors();
		}
	}

	private void OnDisable()
	{
		for (int i =0; i <aircraftEditors.Length; i++)
		{
			DestroyImmediate(aircraftEditors[i]);
		}

		aircraftEditors = null;
	}

	public override void OnInspectorGUI()
	{
		if (aircraftEditors.Length != TryGetAircraftsLength())
		{
			for (int i = 0; i < aircraftEditors.Length; i++)
			{
				DestroyImmediate(aircraftEditors[i]);
			}

			CreateEditors();
		}

		for (int i = 0; i < aircraftEditors.Length; i++)
		{
			aircraftEditors[i].OnInspectorGUI();
		}

		if (TryGetAircraftsLength() > 0)
		{
			EditorGUILayout.Space();
			EditorGUILayout.Space();
		}

		EditorGUILayout.BeginHorizontal();
		newAircraftName = EditorGUILayout.TextField(GUIContent.none, newAircraftName);
		if (GUILayout.Button("+", GUILayout.Width(buttonWidth)) && newAircraftName != "New Aircraft")
		{
			AddAircraft(newAircraftName);
			newAircraftName = "New Aircraft";
		}
		EditorGUILayout.EndHorizontal();
	}

	private void CreateEditors()
	{
		aircraftEditors = new AircraftEditor[allAircrafts.aircrafts.Length];

		for (int i = 0; i < aircraftEditors.Length; i++)
		{
			aircraftEditors[i] = CreateEditor(TryGetAircraftAt(i)) as AircraftEditor;
			aircraftEditors[i].editorType = AircraftEditor.EditorType.AllAircraftsAsset;
		}
	}

	[MenuItem("Assets/Create/AllAircrafts")]
	private static void CreateAllAircraftsAsset()
	{
		if(AllAircrafts.Instance)
		{
			return;
		}

		AllAircrafts instance = CreateInstance<AllAircrafts>();
		AssetDatabase.CreateAsset(instance, creationPath);

		AllAircrafts.Instance = instance;

		instance.aircrafts = new Aircraft[0];
	}

	private void AddAircraft(string aircraftName)
	{
		if (!AllAircrafts.Instance)
		{
			Debug.LogError("AllAircrafts has not been created yet.");
			return;
		}

		Aircraft newAircraft = AircraftEditor.CreateAircraft(aircraftName);
		newAircraft.name = aircraftName;

		Undo.RecordObject(newAircraft, "Created new Aircraft");

		AssetDatabase.AddObjectToAsset(newAircraft, AllAircrafts.Instance);
		AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newAircraft));

		ArrayUtility.Add(ref AllAircrafts.Instance.aircrafts, newAircraft);

		EditorUtility.SetDirty(AllAircrafts.Instance);

		SetAllAircraftNames();
	}

	public static void RemoveAircraft(Aircraft aircraft)
	{
		if(!AllAircrafts.Instance)
		{
			Debug.LogError("AllAircrafts has not been created yet.");
			return;
		}

		Undo.RecordObject(AllAircrafts.Instance, "Removing Aircraft");

		ArrayUtility.Remove(ref AllAircrafts.Instance.aircrafts, aircraft);

		DestroyImmediate(aircraft, true);
		AssetDatabase.SaveAssets();

		EditorUtility.SetDirty(AllAircrafts.Instance);


	}

	private static void SetAllAircraftNames()
	{
		AllAircraftsNames = new string[TryGetAircraftsLength()];

		for (int i = 0; i < AllAircraftsNames.Length; i++)
		{
			AllAircraftsNames[i] = TryGetAircraftAt(i).aircraftName;
		}
	}

	private static Aircraft TryGetAircraftAt (int index)
	{
		Aircraft[] allAircrafts = AllAircrafts.Instance.aircrafts;

		if(allAircrafts == null || allAircrafts[0] == null)
		{
			return null;
		}

		if (index >= allAircrafts.Length)
		{
			return allAircrafts[0];
		}

		return allAircrafts[index];
	}

	private static int TryGetAircraftsLength()
	{
		if(AllAircrafts.Instance.aircrafts == null)
		{
			return 0;
		}
		else
		{
			return AllAircrafts.Instance.aircrafts.Length;
		}
	}
}