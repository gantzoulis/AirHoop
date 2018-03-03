using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AllBuffs))]
public class AllBuffsEditor : Editor 
{
	public static string[] AllBuffsNames
	{
		get
		{
			if(allBuffNames == null)
			{
				SetAllBuffNames();
			}
			return allBuffNames;
		}
		private set 
		{
			allBuffNames = value;
		}
	}

	private static string[] allBuffNames;

	private BuffEditor[] buffEditors;
	private AllBuffs allBuffs;
	private string newBuffName = "New Buff";

	private const string creationPath = "Assets/Resources/ScriptableCollections/AllBuffs.asset";
	private const float buttonWidth = 30f;
	private const float refreshButtonWidth = 90f;

	private void OnEnable()
	{
		allBuffs = (AllBuffs)target;

		if(allBuffs.buffs == null)
		{
			allBuffs.buffs = new Buff[0];
		}

		if(buffEditors == null)
		{
			CreateEditors();
		}
	}

	private void OnDisable()
	{
		for (int i =0; i <buffEditors.Length; i++)
		{
			DestroyImmediate(buffEditors[i]);
		}

		buffEditors = null;
	}

	public override void OnInspectorGUI()
	{
		if (buffEditors.Length != TryGetBuffsLength())
		{
			for (int i = 0; i < buffEditors.Length; i++)
			{
				DestroyImmediate(buffEditors[i]);
			}

			CreateEditors();
		}

		for (int i = 0; i < buffEditors.Length; i++)
		{
			buffEditors[i].OnInspectorGUI();
		}

		if (TryGetBuffsLength() > 0)
		{
			EditorGUILayout.Space();
			EditorGUILayout.Space();
		}

		EditorGUILayout.BeginHorizontal();
		newBuffName = EditorGUILayout.TextField(GUIContent.none, newBuffName);
		if (GUILayout.Button("+", GUILayout.Width(buttonWidth)) && newBuffName != "New Buff")
		{
			AddBuff(newBuffName);
			newBuffName = "New Buff";
		}
		EditorGUILayout.EndHorizontal();
	}

	private void CreateEditors()
	{
		buffEditors = new BuffEditor[allBuffs.buffs.Length];

		for (int i = 0; i < buffEditors.Length; i++)
		{
			buffEditors[i] = CreateEditor(TryGetBuffAt(i)) as BuffEditor;
			buffEditors[i].editorType = BuffEditor.EditorType.AllBuffsAsset;
		}
	}

	[MenuItem("Assets/Create/AllBuffs")]
	private static void CreateAllBuffsAsset()
	{
		if(AllBuffs.Instance)
		{
			return;
		}

		AllBuffs instance = CreateInstance<AllBuffs>();
		AssetDatabase.CreateAsset(instance, creationPath);

		AllBuffs.Instance = instance;

		instance.buffs = new Buff[0];
	}

	private void AddBuff(string buffName)
	{
		if (!AllBuffs.Instance)
		{
			Debug.LogError("AllBuffs has not been created yet.");
			return;
		}

		Buff newBuff = BuffEditor.CreateAircraft(buffName);
		newBuff.name = buffName;

		Undo.RecordObject(newBuff, "Created new Buff");

		AssetDatabase.AddObjectToAsset(newBuff, AllBuffs.Instance);
		AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newBuff));

		ArrayUtility.Add(ref AllBuffs.Instance.buffs, newBuff);

		EditorUtility.SetDirty(AllBuffs.Instance);

		SetAllBuffNames();
	}

	public static void RemoveBuff(Buff buff)
	{
		if(!AllBuffs.Instance)
		{
			Debug.LogError("AllBuffs has not been created yet.");
			return;
		}

		Undo.RecordObject(AllBuffs.Instance, "Removing Buff");

		ArrayUtility.Remove(ref AllBuffs.Instance.buffs, buff);

		DestroyImmediate(buff, true);
		AssetDatabase.SaveAssets();

		EditorUtility.SetDirty(AllBuffs.Instance);


	}

	private static void SetAllBuffNames()
	{
		AllBuffsNames = new string[TryGetBuffsLength()];

		for (int i = 0; i < AllBuffsNames.Length; i++)
		{
			AllBuffsNames[i] = TryGetBuffAt(i).buffName;
		}
	}

	private static Buff TryGetBuffAt (int index)
	{
		Buff[] allBuffs = AllBuffs.Instance.buffs;

		if(allBuffs == null || allBuffs[0] == null)
		{
			return null;
		}

		if (index >= allBuffs.Length)
		{
			return allBuffs[0];
		}

		return allBuffs[index];
	}

	private static int TryGetBuffsLength()
	{
		if(AllBuffs.Instance.buffs == null)
		{
			return 0;
		}
		else
		{
			return AllBuffs.Instance.buffs.Length;
		}
	}
}