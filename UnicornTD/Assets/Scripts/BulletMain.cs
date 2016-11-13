using UnityEngine;
using System.Collections;

public class BulletMain : MonoBehaviour {


	public int Damage = 50;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider Col) {
		if(Col.gameObject.tag == "Enemy") {
			Col.gameObject.GetComponent<UnicornBehaviour>().TakeHp(Damage);
			Destroy(gameObject);
		}
	}

	public void setDamage(int NewDamage) {
		Damage = NewDamage;
	}
}
