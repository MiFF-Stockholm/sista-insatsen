using UnityEngine;
using System.Collections;
using System.Threading;

public class EnemyStats : MonoBehaviour {
	//public float lerpSmooth = 0.9f;
	//public int attackRange = 10;

	public int health = 20;
	public bool isDead = false;
	public int battleProgress = 50;
	public int damage = 7;

	public bool attacking;
	private Vector3 attackEnd;
	public Vector3 attackStart;
	private float attackOffset = 4f;
	private int startHealth = 0;
	private PlayerStats attackedPlayer;

	AttackCompleteDelegate attackCompleteDelegate;

	public delegate void AttackCompleteDelegate();

	void Start () {
		attackStart = transform.position;
	}

	void Update() {
		if (health < 0 && !isDead) {
			//run death animation/effect
			transform.renderer.enabled = false;
			isDead = true;
		} else {
			if(startHealth == 0) {
				startHealth = health;
			}
			// DOn't JUDGE MEEE!
			gameObject.renderer.material.color = new Color32((byte)(0xFF - (byte)((health/startHealth)*0xFF*0.5)), (byte)(0xf0*0.5), (byte)(0xf0*0.5), 0xff);
			// Okey... they are kind of grey tinted now... CLOSE ENOUFH!

			Vector3 newPosition = attackStart;
			if(attacking) {
				newPosition = attackEnd;
				
				if(Vector3.Distance(transform.position, attackEnd) < attackOffset)
				{
					attacking = false;
					attackedPlayer.healthPoints -= Random.Range(damage/2, damage);
					Globals.Play(Resources.Load<AudioClip>("sounds/music/Combat03_Attack hit"));
					//TODO Display blood?!?!?
				}
			}
			transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);

			
			if(Vector3.Distance(transform.position, attackStart) < attackOffset && !attacking) {
				if(Monitor.TryEnter(this)) {
					if(attackCompleteDelegate != null) {
							AttackCompleteDelegate tmpDel = attackCompleteDelegate;
							attackCompleteDelegate = null;
							tmpDel();
					}					
					Monitor.Exit(this);
				}
			}
		}
	}

	public void startAttackAnimation(PlayerStats player)
	{        

		attackedPlayer = player;

		GameObject pObj = GameObject.Find("player" + (player.index + 1));
		attackEnd = pObj.transform.position;

		this.attacking = true;
	}

	public void startAttackAnimation(PlayerStats player, AttackCompleteDelegate attackCompleteDelegate) {
		startAttackAnimation(player);
		this.attackCompleteDelegate = attackCompleteDelegate;
	}
}
