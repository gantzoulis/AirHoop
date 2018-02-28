using UnityEngine;

public class AllAircrafts : ScriptableObject 
{
	public Aircraft[] aircrafts;

	private static AllAircrafts instance;

	private const string loadPath = "ScriptableCollections/AllAircrafts";

	public static AllAircrafts Instance
	{
		get
		{
			if (!instance)
			{
				instance = FindObjectOfType<AllAircrafts>();
			}
			if(!instance)
			{
				instance = Resources.Load<AllAircrafts>(loadPath);
			}
			if(!instance)
			{
				Debug.LogError("AllAircrafts has not been created yet. Go to Assets > Create > AllAircrafts");
			}
			return instance;
		}
		set
		{
			instance = value;
		}
	}
}