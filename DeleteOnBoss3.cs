using UnityEngine;
using System.Collections;

public class DeleteOnBoss3 : MonoBehaviour {
    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(!Globals.boss3Defeated);
    }
}
