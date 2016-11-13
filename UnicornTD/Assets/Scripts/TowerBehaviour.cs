using UnityEngine;
using System.Collections;

public class TowerBehaviour : MonoBehaviour {

    private int attackDamage = 10;

    public void UpgradeTower(int newAttackValue)
    {
        attackDamage = newAttackValue;
    }
    
    // Use this for initialization
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Unicorn")
            coll.gameObject.GetComponent<UnicornBehaviour>().TakeHp(attackDamage);

    }
}
