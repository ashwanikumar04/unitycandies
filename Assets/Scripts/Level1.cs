using UnityEngine;
using System.Collections;

public class Level1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(GameObject.Find("Root"));
		LoadingScreenController.Instance.Hide();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
