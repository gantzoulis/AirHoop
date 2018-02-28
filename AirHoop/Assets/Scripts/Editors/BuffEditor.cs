using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Buff))]
public class BuffEditor: Editor
{
	public  enum EditorType
	{
		BuffAsset, AllBuffsAsset
	}

	public EditorType editorType;

	private Buff buff;

	private const float buffButtonWidth = 30f;

	private SerializedProperty nameProperty;
	private SerializedProperty modelProperty;
	private SerializedProperty typeProperty;
	private SerializedProperty amountProperty;
	private SerializedProperty hashProperty;

	private const string  buffPropName = "buffName";
	private const string  buffPropModel = "model";
	private const string  buffPropType = "type";
	private const string  buffPropAmount = "amount";
	private const string  buffPropHash = "hash";

	private void OnEnable()
	{
		buff = (Buff)target;

		if (target == null)
		{
			DestroyImmediate(this);
			return;
		}

		nameProperty = serializedObject.FindProperty(buffPropName);
		modelProperty = serializedObject.FindProperty(buffPropModel);
		typeProperty = serializedObject.FindProperty(buffPropType);
		amountProperty = serializedObject.FindProperty(buffPropAmount);
		hashProperty = serializedObject.FindProperty(buffPropHash);
	}

	public override void OnInspectorGUI()
	{
		switch (editorType)
		{
		case EditorType.BuffAsset:
			BuffAssetGUI();
			break;
		case EditorType.AllBuffsAsset:
			AllBuffsAssetGUI();
			break;
		default:
			throw new UnityException("Unknown BuffEditor.EditorType.");
		}
	}

	private void BuffAssetGUI()
	{
		EditorGUILayout.BeginVertical(GUI.skin.box);

		EditorGUILayout.LabelField(buff.buffName);

		EditorGUILayout.EndVertical();
	}

	private void AllBuffsAssetGUI()
	{
		EditorGUILayout.BeginVertical(GUI.skin.box);

		EditorGUILayout.BeginHorizontal();
		EditorGUI.indentLevel++;

		nameProperty.isExpanded = EditorGUILayout.Foldout(nameProperty.isExpanded, nameProperty.stringValue);

		if(GUILayout.Button("-", GUILayout.Width(buffButtonWidth)))
		{
			AllBuffsEditor.RemoveBuff(buff);
		}

		EditorGUI.indentLevel--;
		EditorGUILayout.EndHorizontal();

		EditorGUI.BeginChangeCheck();
		if (nameProperty.isExpanded)
		{
			ExpandedGUI();
		}

		EditorGUILayout.EndVertical();

		if(buff)
		{
			if(EditorGUI.EndChangeCheck())
			{
				serializedObject.ApplyModifiedProperties();
			}
		}
	}

	private void ExpandedGUI()
	{
		EditorGUI.indentLevel++;
		EditorGUILayout.Space();
		EditorGUILayout.PropertyField(modelProperty);
		EditorGUILayout.PropertyField(typeProperty);
		EditorGUILayout.PropertyField(amountProperty);
		//EditorGUILayout.LabelField(aircraft.hash.ToString());
		EditorGUI.indentLevel--;
	}

	public static Buff CreateAircraft(string buffName)
	{
		Buff newAircraft = CreateInstance<Buff>();
		newAircraft.buffName = buffName;
		SetHash(newAircraft);
		return newAircraft;
	}

	private static void SetHash(Buff buff)
	{
		buff.hash = Animator.StringToHash(buff.buffName);
	}
}