using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){

		int textureHeight = 146;
		int textureWidth = 50;
		int x = (int)(Screen.width * 0.5f );
		int y = (int)(Screen.height * 0.95f - textureHeight);
		GUI.DrawTexture(new Rect(x, y, textureWidth, textureHeight), Resources.Load("life_bg", typeof(Texture)) as Texture);

		float healthPercent = GetComponent<Critter>().hp / GetComponent<Critter>().maxHp;
		int healthHeight = (int)(textureHeight * healthPercent);
		GUI.DrawTexture(new Rect(x, Screen.height * 0.95f - textureHeight * healthPercent , 50, healthHeight), Resources.Load("life_fg", typeof(Texture)) as Texture);

	}
}
