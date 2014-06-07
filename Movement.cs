using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public float MovementSpeed = 5f;
	public float distance = 1.0f;
    public float randomEncounterChance = 0.020f;

	private Animator animator;
	private float weight = 0.0f;
	private Vector3 start;
	private Vector3 currentPos;
	private Vector3 end;
	private bool isMoving = false;
	// Use this for initialization
	void Start () {
		animator = this.GetComponentInChildren<Animator>();

	}

	//A solid object is one wo has a "solid" gameobject. Why not tags? Becaouse Unity, thats why.
	//This function was called isDirectionSafeFromSolidObjects, but later change due to myself forgetting how negation works. 01:32
	private bool isDirSolid(Vector3 vector) {
		Vector3 dist = new Vector3(0, 0, 1.1f);
		Vector3 from = vector + dist;
		Vector3 to = vector - dist;
		RaycastHit hit = new RaycastHit();
		bool itHit = Physics.Raycast(from, to-from, out hit, 50f);
		//00:53
		if(itHit) {//then
			//WTF?!@_
			//Why the hell would "transform" contain a FindChild function()!? Isn't transform just the pos + rot etc!?
			//04:15
			Tags tag = hit.collider.transform.GetComponentInChildren<Tags>();

			return tag != null && tag.SolidTile;
		} else {
			return false;
		}
	}

	// Update is called once per frame
	void Update () {
		float vert = Input.GetAxisRaw("Vertical");
		float hort = Input.GetAxisRaw("Horizontal");
		if (Globals.InDialog) {
			return;
		}

		if (currentPos == end) {
            if (isMoving && Dialog.PlayedDialogs.Contains("intro") && Random.value < (randomEncounterChance * Globals.RandomEncounterGlobalFactor))
            {
                Globals.translateOnStartup = true;
                Globals.initialPosition = transform.position;
                Globals.battleCompletedDelegate = Globals.defaultBattleComplete;
                Globals.PreBattleLevelName = Application.loadedLevelName;
                Application.LoadLevel("scen-ooe_nik");
            }
            start = transform.position;
            isMoving = false;
            weight = 0;
            animator.SetFloat("Speed", 0.0f);
		}
		if(isMoving) {
			weight += Time.deltaTime * MovementSpeed;
			transform.position = Vector3.Lerp(start, end, weight);
			currentPos = transform.position;
		}
		if (!isMoving) {
			if(vert != 0) {
				move(new Vector3(0, vert, 0), vert, hort);
			} else if(hort != 0) {
				move(new Vector3(hort, 0, 0), vert, hort);
			}
		}			
	}

	void move(Vector3 to, float vert, float hort) {
		Vector3 moveTo = transform.position + (to * distance);
		if(isDirSolid(moveTo)) {
			return;
		}
		start = transform.position;
		end = moveTo;
		isMoving = true;
		if(vert == 1) {
			animator.SetInteger("Direction", 0);
			animator.SetFloat("Speed", 1.0f);
		} else if (vert == -1) {
			animator.SetInteger("Direction", 2);
			animator.SetFloat("Speed", 1.0f);
		} else if(hort == 1) {
			animator.SetInteger("Direction", 1);
			animator.SetFloat("Speed", 1.0f);
		} else if (hort == -1) {
			animator.SetInteger("Direction", 3);
			animator.SetFloat("Speed", 1.0f);
		}
	}

}

