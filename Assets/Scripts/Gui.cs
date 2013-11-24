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

		bool rightPlayer = GetComponent<Player>().inputSuffix=="2";

		int x1 = (int)(Screen.width * 0.05f );
		int textureHeight = 146;
		int textureWidth = 50;
		int rectW = (int)(Screen.width * 0.05f);
		int offset = (int)(Screen.width * 0.01f);

		int playerGuiWidth = textureWidth + 3 * rectW + 2 * offset;
		
		if (rightPlayer)
		{
			x1 = (int)(Screen.width  - playerGuiWidth);
		}

		int y1 = (int)(Screen.height * 0.95f - textureHeight);

		GUI.DrawTexture(new Rect(x1, y1, textureWidth, textureHeight), Resources.Load("life_bg", typeof(Texture)) as Texture);

		float healthPercent = GetComponent<Critter>().hp / GetComponent<Critter>().maxHp;
		int healthHeight = (int)(textureHeight * healthPercent);
		GUI.DrawTexture(new Rect(x1, Screen.height * 0.95f - textureHeight * healthPercent , 50, healthHeight), Resources.Load("life_fg", typeof(Texture)) as Texture);
		
		string slotName = "slot";

		Eq eq = GetComponent<Eq>();
		//up slot
		GUI.DrawTexture(new Rect(offset + x1 + textureWidth + rectW, y1, rectW, rectW), Resources.Load(slotName, typeof(Texture))as Texture);
		GameObject upSlot = eq.upSlot;
		if (upSlot != null) {
			Item upHandItem = upSlot.GetComponent<Item>();
			GUI.DrawTexture(new Rect(offset + x1 + textureWidth + rectW, y1, rectW, rectW), upHandItem.texture);
		}

		// left slot
		GUI.DrawTexture(new Rect(offset + x1 + textureWidth, y1 + rectW , rectW, rectW), Resources.Load(slotName, typeof(Texture))as Texture);
		GameObject leftSlot = eq.leftSlot;
		if (leftSlot != null) {
			Item leftItem = leftSlot.GetComponent<Item>();
			GUI.DrawTexture(new Rect(offset + x1 + textureWidth, y1 + rectW, rectW, rectW), leftItem.texture);
		}

		//right slot
		GUI.DrawTexture(new Rect(offset + x1 + textureWidth + 2 * rectW, y1 + rectW , rectW, rectW), Resources.Load(slotName, typeof(Texture))as Texture);

		GameObject rightSlot = eq.rightSlot;
		if (rightSlot != null) {
			Item rightItem = rightSlot.GetComponent<Item>();
			GUI.DrawTexture(new Rect(offset + x1 + textureWidth + 2 * rectW, y1 + rectW , rectW, rectW), rightItem.texture);
			GUI.Label(new Rect(offset + x1 + textureWidth + 2 * rectW, y1 + rectW , rectW, rectW), (int)(rightItem.shieldHp) + " /" + (int)(rightItem.shieldTotalHp));
		}

		//down slot
		GUI.DrawTexture(new Rect(offset + x1 + textureWidth + rectW, y1 + 2 * rectW , rectW, rectW), Resources.Load(slotName, typeof(Texture))as Texture);
        GameObject downSlot = eq.downSlot;
		if (downSlot != null){
            Item downItem = downSlot.GetComponent<Item>();
			GUI.DrawTexture(new Rect(offset + x1 + textureWidth + rectW, y1 + 2 * rectW , rectW, rectW), downItem.texture);
		}

        GUI.Label(new Rect(50, 50, 300, 70), World.score.ToString());


	}
}
