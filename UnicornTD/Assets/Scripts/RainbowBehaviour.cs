using UnityEngine;
using System.Collections;

public class RainbowBehaviour : MonoBehaviour {

    public double hp = 100;
    private double attackLvl = 1;
    private double deffenseLvl = 1;
    private int unicornHitDamage = 5;

    public Sprite[] spritesLeft = new Sprite[4];
    public Sprite[] spritesRight = new Sprite[4];

    private SpriteRenderer rightCloud;
    private SpriteRenderer leftCloud;

    private bool gotHit = false;
    private float currentTime = 0;
    private float timeToChange = 0.5f;

    // Use this for initialization
    void Start () {
        rightCloud = transform.FindChild("CloudRight").gameObject.GetComponent<SpriteRenderer>();
        leftCloud = transform.FindChild("CloudLeft").gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        if(gotHit)
        {
            rightCloud.sprite = spritesRight[2];
            leftCloud.sprite = spritesLeft[2];
            if(currentTime> timeToChange)
            {
                rightCloud.sprite = spritesRight[0];
                leftCloud.sprite = spritesLeft[0];
                gotHit = false;
                currentTime = 0f;
            }
            currentTime += Time.deltaTime;
        }
        
	    if(hp <= 0)
        {
			GameManager.instance.ChangeState(GameManager.GameState.END);
            rightCloud.sprite = spritesRight[3];
            leftCloud.sprite = spritesLeft[3];
        }
        else if(hp<=50)
        {
            rightCloud.sprite = spritesRight[1];
            leftCloud.sprite = spritesLeft[1];
        }
        
	}

    public double GetAttackLvl()
    {
        return attackLvl;
    }
    public double GetDeffenseLvl()
    {
        return deffenseLvl;
    }

    public void UpgradeAttackLvl(int newLvl)
    {
        attackLvl = newLvl;
    }
    public void UpgradeDeffenseLvl(int newLvl)
    {
        deffenseLvl = newLvl;
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
			//Activate Explode method on unicorn
			coll.gameObject.GetComponent<UnicornBehaviour>().Explode();
            hp -= unicornHitDamage * deffenseLvl;
            gotHit = true;
            currentTime = 0;
        }
            

    }
}
