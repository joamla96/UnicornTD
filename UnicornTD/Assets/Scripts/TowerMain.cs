using UnityEngine;
using System.Collections;

public class TowerMain : MonoBehaviour {

	public int RotationSpeed = 10;
	public GameObject bulletGo;

	//public Sprite Tower1;
	public Sprite Tower2;
	public Sprite Tower3;
	public AudioClip ShootSound;
	public AudioClip UpgradeSound;

	private Quaternion _lookRotation;
	private Vector3 _direction;
	private GameObject CloseEnemy;
	private float timeToShoot = 1.0f;
	private int AttackDamage = 10;
	private float currentTime = 0.0f;
	private int Level = 1;

	private Transform FirePoint;

	// Use this for initialization
	void Start () {
	
	}
	
	void Awake() {

	}

	void OnMouseOver() {
		if(Input.GetKeyUp("u")) {
			UpgradeTower();
		}
	}

	// Update is called once per frame
	void Update () {
		CloseEnemy = FindClosestEnemy();
		if (CloseEnemy != null) {
			//find the vector pointing from our position to the target
			_direction = (CloseEnemy.transform.position - transform.position).normalized;

			//create the rotation we need to be in to look at the target
			_lookRotation = Quaternion.LookRotation(_direction);
			Quaternion TowerPointing = Quaternion.Euler(90, _lookRotation.eulerAngles.y + 90, _lookRotation.eulerAngles.z);

			//Debug.Log("Direction: " + _direction);
			//Debug.Log("Look" + _lookRotation);

			//rotate us over time according to speed until we are in the required rotation
			transform.rotation = Quaternion.Slerp(transform.rotation, TowerPointing, Time.deltaTime * RotationSpeed);

			if (currentTime >= timeToShoot) {

				Shoot(CloseEnemy);
				currentTime = 0f;
			} else {
				currentTime += Time.deltaTime;
			}
		}
	}

	GameObject FindClosestEnemy() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject closest = null;
		float distance = 10;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}

	void Shoot(GameObject Enemy) {
		FirePoint = transform.FindChild("FirePoint");
		Vector3 EnemyPosition = new Vector3(Enemy.transform.position.x, 0, Enemy.transform.position.z);
		var heading = EnemyPosition - FirePoint.position;
		var distance = heading.magnitude;
		var direction = heading / distance;

		Debug.Log(FirePoint);


		GameObject BulletInstance = Instantiate(bulletGo, FirePoint.position, FirePoint.rotation) as GameObject;
		BulletInstance.gameObject.GetComponent<Rigidbody>().AddForce(direction * 500, ForceMode.Force);
		BulletInstance.gameObject.GetComponent<BulletMain>().Damage = AttackDamage;
		GetComponent<AudioSource>().PlayOneShot(ShootSound);

		//Debug.DrawLine(FirePoint.position, EnemyPosition);
	}

	public void UpgradeTower() {
		switch(Level) {
			case 1:
				Debug.Log("Level 1 => 2");
				if (GameManager.TakeCoins(15)) {
					AttackDamage = 20;
					timeToShoot = 0.5f;
					GetComponentInChildren<SpriteRenderer>().sprite = Tower2;
					Level++;
					GetComponent<AudioSource>().PlayOneShot(UpgradeSound);
				} else SendMessage("Not Enought LSD");
				break;

			case 2:
				Debug.Log("Level 2 => 3");
				if (GameManager.TakeCoins(50)) {
					AttackDamage = 30;
					timeToShoot = 0.25f;
					GetComponentInChildren<SpriteRenderer>().sprite = Tower3;
					Level++;
					GetComponent<AudioSource>().PlayOneShot(UpgradeSound);
				} else SendMessage("Not Enought LSD");
				break;

			case 3:
				SendMessage("This tower is fully upgraded!");
				break;

			default: SendMessage("Error on Tower Upgrade"); break;
		}
	}

}
