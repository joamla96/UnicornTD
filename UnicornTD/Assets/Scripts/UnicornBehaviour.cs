using UnityEngine;
using System.Collections;

public class UnicornBehaviour : MonoBehaviour {

    public double hp = 100;
	public AudioClip Death;

	private bool killable = true;

    void Update()
    {
        if(hp<= 0)
        {
            Explode();
        }
    }

    public void TakeHp(double hpToTake)
    {
        hp -= hpToTake;
    }

    public void Explode()
    {
		if (killable) {
			killable = false;
			GameManager.GiveCoins(5);
			GameObject shower = transform.FindChild("Shower").gameObject;
			shower.SetActive(true);
			shower.gameObject.GetComponent<ParticleSystem>().Play();
			gameObject.transform.FindChild("unicorn_walking1").GetComponent<SpriteRenderer>().enabled = false;
			Destroy(gameObject, 0.5f);
			GetComponent<AudioSource>().PlayOneShot(Death);
		}
	}

}
