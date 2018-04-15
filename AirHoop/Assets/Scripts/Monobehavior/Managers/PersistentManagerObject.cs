using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentManagerObject : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
