using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {
    public Transform wall;

	// Use this for initialization
	void Start () {
        PersonFactory.instance.CreateGeneration();
        for(int i = -15; i < 15; i+=2) {
            Instantiate(wall, new Vector3(i, -15, 0), Quaternion.identity);
            Instantiate(wall, new Vector3(i, 15, 0), Quaternion.identity);
            Instantiate(wall, new Vector3(-15, i, 0), Quaternion.identity);
            Instantiate(wall, new Vector3(15, i, 0), Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
