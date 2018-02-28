using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBuffs : ScriptableObject
{
	public Buff[] buffs;

	private static AllBuffs instance;

	private const string loadPath = "ScriptableCollections/AllBuffs";

	public static AllBuffs Instance
	{
		get
		{
			if (!instance)
			{
				instance = FindObjectOfType<AllBuffs>();
			}
			if(!instance)
			{
				instance = Resources.Load<AllBuffs>(loadPath);
			}
			if(!instance)
			{
				Debug.LogError("AllBuffs has not been created yet. Go to Assets > Create > AllBuffs");
			}
			return instance;
		}
		set
		{
			instance = value;
		}
	}
}
