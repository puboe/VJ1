using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public int pool = 15;
	public GameObject barrelPrefab;
	public List<Barrel> barrels = new List<Barrel>();

	void Awake (){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		GeneratePool ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void GeneratePool (){
		for (int i = 0; i<pool; i++) {
			Barrel barrel = ((GameObject)Instantiate(barrelPrefab, new Vector3(-2.6f,2.22f,0), Quaternion.identity)).GetComponent<Barrel>();
			barrels.Add (barrel);
		}
	}

	public void Roll(){
		for (int i = 0; i<pool; i++) {
			if (barrels [i].onPool){
				barrels [i].transform.position = new Vector2 (-1.588f, 2.225f);
				barrels [i].move = true;
				barrels [i].onPool = false;
				barrels [i].GetComponent<Rigidbody>().isKinematic = false;
				barrels [i].GetComponent<Rigidbody>().detectCollisions = true;
				break;
			}
		}
	}
}
