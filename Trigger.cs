using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {
    public GameObject Target;
    public string EnterMessage;
    public string ExitMessage;

    void OnTriggerEnter2D(Collider2D other)
    {
        Target.SetActive(true);
    }
}
