using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Aircraft))]
public class AircraftEditor: Editor
{
	public  enum EditorType
	{
		AircraftAsset, AllAircraftsAsset
	}

	public EditorType editorType;

	private Aircraft aircraft;

	private const float aircraftButtonWidth = 30f;

	private SerializedProperty nameProperty;
	private SerializedProperty modelProperty;
	private SerializedProperty fuelProperty;
	private SerializedProperty speedProperty;
	private SerializedProperty maneuverProperty;
	private SerializedProperty hashProperty;

	private const string  aircraftPropName = "aircraftName";
	private const string  aircraftPropModel = "model";
	private const string  aircraftPropFuel = "fuel";
	private const string  aircraftPropSpeed = "speed";
	private const string  aircraftPropManeuver = "maneuver";
	private const string  aircraftPropHash = "hash";

	private void OnEnable()
	{
		aircraft = (Aircraft)target;

		if (target == null)
		{
			DestroyImmediate(this);
			return;
		}

		nameProperty = serializedObject.FindProperty(aircraftPropName);
		modelProperty = serializedObject.FindProperty(aircraftPropModel);
		fuelProperty = serializedObject.FindProperty(aircraftPropFuel);
		speedProperty = serializedObject.FindProperty(aircraftPropSpeed);
		maneuverProperty = serializedObject.FindProperty(aircraftPropManeuver);
		hashProperty = serializedObject.FindProperty(aircraftPropHash);
	}

	public override void OnInspectorGUI()
	{
		switch (editorType)
		{
		case EditorType.AircraftAsset:
			AircraftAssetGUI();
			break;
		case EditorType.AllAircraftsAsset:
			AllAircraftsAssetGUI();
			break;
		default:
			throw new UnityException("Unknown AircraftEditor.EditorType.");
		}
	}

	private void AircraftAssetGUI()
	{
		EditorGUILayout.BeginVertical(GUI.skin.box);

		EditorGUILayout.LabelField(aircraft.aircraftName);

		EditorGUILayout.EndVertical();
	}

	private void AllAircraftsAssetGUI()
	{
		EditorGUILayout.BeginVertical(GUI.skin.box);

		EditorGUILayout.BeginHorizontal();
		EditorGUI.indentLevel++;

		nameProperty.isExpanded = EditorGUILayout.Foldout(nameProperty.isExpanded, nameProperty.stringValue);

		if(GUILayout.Button("-", GUILayout.Width(aircraftButtonWidth)))
		{
			AllAircraftsEditor.RemoveAircraft(aircraft);
		}

		EditorGUI.indentLevel--;
		EditorGUILayout.EndHorizontal();

		EditorGUI.BeginChangeCheck();
		if (nameProperty.isExpanded)
		{
			ExpandedGUI();
		}

		EditorGUILayout.EndVertical();

		if(aircraft)
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
		EditorGUILayout.PropertyField(modelProperty, true);
		EditorGUILayout.PropertyField(fuelProperty);
		EditorGUILayout.PropertyField(speedProperty);
		EditorGUILayout.PropertyField(maneuverProperty);
		//EditorGUILayout.LabelField(aircraft.hash.ToString());
		EditorGUI.indentLevel--;
	}

	public static Aircraft CreateAircraft(string aircraftName)
	{
		Aircraft newAircraft = CreateInstance<Aircraft>();
		newAircraft.aircraftName = aircraftName;
		SetHash(newAircraft);
		return newAircraft;
	}

	private static void SetHash(Aircraft aircraft)
	{
		aircraft.hash = Animator.StringToHash(aircraft.aircraftName);
	}
}