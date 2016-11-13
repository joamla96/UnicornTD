using UnityEngine;
using System.Collections;

public class navmeshtest : MonoBehaviour {

	public Transform target;
	NavMeshAgent agent;

	void Awake() {
		target = GameObject.Find("Target").transform;
	}

	// Use this for initialization
	void Start() {
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(target.position);
	}

	// Update is called once per frame
	void Update() {
	}
}
