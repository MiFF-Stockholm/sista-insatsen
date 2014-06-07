using UnityEngine;
using System.Collections;

public class CharacterControlWorld : MonoBehaviour {
    public float MovementSpeed = 1;

    void FixedUpdate()
    {
        if (Globals.InDialog)
            return;

        float ew = Input.GetAxis("Horizontal");
        float sn = Input.GetAxis("Vertical");
        if (ew == 0.0f && sn == 0.0f)
            return;

        Vector3 movement = new Vector3(ew, sn);
        movement.Normalize();
        transform.position += movement * MovementSpeed * Time.fixedDeltaTime;
	}
}
