using UnityEngine;
using System.Collections;

public class DeleteOnBoss2 : MonoBehaviour {
    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(!Globals.boss2Defeated);
    }
}
