using UnityEngine;
using System.Collections;

public class RandomPrizeBehaviour : MonoBehaviour {

    private float timeToChange = 0.1f;
    private float currentTime = 0f;
    private Color currentColor;
    private Color newColor;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(currentTime > timeToChange)
        {

            newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
            currentColor = gameObject.GetComponentInChildren<SpriteRenderer>().color;

            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(currentColor, newColor, 0.5f);

            currentTime = 0f;
        }
        currentTime += Time.deltaTime;
    }

	void OnMouseOver() {
		Destroy(gameObject);
		GameManager.GiveCoins(100);
	}
}
