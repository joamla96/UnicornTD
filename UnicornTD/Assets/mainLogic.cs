using UnityEngine;
using UnityEngine.UI;
using System.Collections;

class mainLogic : MonoBehaviour {

	public GameObject MobBasic;
	public GameObject TowerPrefab;
	public GameObject CoinsPrefab;
	public Vector3 spawnValues;
	public int startWait = 5;
	public float waveWait = 20f;
	public WaveClass[] waves;
	public Text waveText;
	public Text waveCounterText;
	public Text resourceText;
	public int resource;
	public double timeRemaining = 20;
	private bool spawningDone = true;
	private string currentWave = "Prepare !";

    private float currentTime = 0;
    private float timeToSpawnOnEnd = 0.1f;


	// Use this for initialization

	void Start() {

		timeRemaining = (float)startWait;

		StartCoroutine(SpawnWaves());
	}

	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds(startWait);

		foreach (WaveClass w in waves) {

			currentWave = w.cName;
			Debug.Log(w.cName);
			for (int i = 0; i < w.cEnemyCount; i++) {
				spawnmob();
				yield return new WaitForSeconds(w.cCadence);
			}
			spawningDone = true;
			yield return new WaitForSeconds(20);
		}


	}
	void Awake() {}
	// Update is called once per frame
	void Update() {

		int RandomNr = (int)Random.Range(1,12000);
		if(RandomNr == 1) {
			GameObject Coin = Instantiate(CoinsPrefab, new Vector3(Random.Range(-5,5), 1, Random.Range(-4, 4)), TowerPrefab.transform.rotation) as GameObject;
		}

        if (GameManager.instance.currentState == GameManager.GameState.START)
        {
            //return the amount of resources
            resourceText.text = GameManager.Points.ToString();

            if (spawningDone)
            {

                timeRemaining -= Time.deltaTime;
                //return time left for next wave
                waveText.text = "Next wave: " + (double)System.Math.Round(timeRemaining, 0) + "s";

                if (timeRemaining < 0)
                {
                    spawningDone = false;
                }

            }
            else
            {

                waveText.text = currentWave;
            }
            //Count the amount of waves and waves left
            waveCounterText.text = "2";



            if (Input.GetKeyUp("b"))
            {
                if (GameManager.TakeCoins(5))
                    buildtower();
            }

            if (Input.GetKeyUp("s"))
            {
                spawnmob();
            }

            if (GameManager.instance.currentState == GameManager.GameState.END)
            {
                Debug.Log("GOT IN");
                if (currentTime > timeToSpawnOnEnd)
                {
                    spawnmob();
                    currentTime = 0;
                }
                currentTime += Time.deltaTime;
            }
        }
	}

	private void spawnmob() { // spawnmob(difficultylevel)
		GameObject mob = Instantiate(MobBasic, new Vector3(-15, 4, Random.Range(-5f, 5f)), transform.rotation) as GameObject;
	}
    

	public void buildtower() {
		GameObject Tower;
		Vector3 Mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Tower = Instantiate(TowerPrefab, new Vector3(Mouse.x, 1, Mouse.z), TowerPrefab.transform.rotation) as GameObject;
	}
}
