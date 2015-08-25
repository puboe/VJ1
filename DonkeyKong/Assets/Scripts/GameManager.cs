using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public int pool = 15;
	public GameObject barrelPrefab;
	public List<Barrel> barrels;

	void Awake (){
		instance = this;
		barrels = new List<Barrel>(pool);
	}

	// Use this for initialization
	void Start () {
		GeneratePool ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void GeneratePool (){
		for (int i = 0; i < pool; i++) {
			Barrel barrel = ((GameObject)Instantiate(barrelPrefab, new Vector3(999,999,0), Quaternion.identity)).GetComponent<Barrel>();
			barrels.Add (barrel);
		}
	}

	public void Roll(){
		foreach (Barrel barrel in barrels) {
			if (barrel.onPool){
				barrel.transform.position = new Vector2 (-1.588f, 2.225f);
				barrel.move = true;
				barrel.onPool = false;
				barrel.GetComponent<Rigidbody>().isKinematic = false;
				barrel.GetComponent<Rigidbody>().detectCollisions = true;
				break;
			}
		}
	}
}
