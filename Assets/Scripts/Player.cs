using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public string inputSuffix;

    protected SC sc;
    protected SP sp;
    protected Critter critter;
    protected Eq eq;
    public GameObject bubble;

    // Use this for initialization
    void Start () {
        sc = GetComponent<SC>();
        sp = GetComponent<SP>();
        eq = GetComponent<Eq>();
        critter = GetComponent<Critter>();

        bubble = Instantiate(Resources.Load("ShieldBubble")) as GameObject;
        bubble.transform.parent = transform;
        bubble.transform.localPosition = new Vector3(0,0.2f,0);
        bubble.SetActive(false);
    }

    // Update is called once per frame
    void Update () {

        if (sc && critter) {
            
			//change backpack
			if (MoonzInput.GetKeyDown(MoonzInput.ARROW_UP, inputSuffix)) {
                GetComponent<Eq>().ChangeSlot(Item.SLOT_UP);
			}
			
			if (MoonzInput.GetKeyDown(MoonzInput.ARROW_DOWN, inputSuffix)){
				GetComponent<Eq>().ChangeSlot(Item.SLOT_DOWN);
			}

			if (MoonzInput.GetKeyDown(MoonzInput.ARROW_LEFT, inputSuffix)){
				GetComponent<Eq>().ChangeSlot(Item.SLOT_LEFT);
			}
			
			if (MoonzInput.GetKeyDown(MoonzInput.ARROW_RIGHT, inputSuffix)){
                GetComponent<Eq>().ChangeSlot(Item.SLOT_RIGHT);
            }

            if (MoonzInput.GetKeyDown(MoonzInput.B, inputSuffix) && eq.GetShield() != null) {
                critter.shieldActive = !critter.shieldActive;
            }
            bubble.SetActive(critter.shieldActive && eq.GetShield() != null);

			if (MoonzInput.GetKeyDown(MoonzInput.A, inputSuffix) ){
				if (eq.downSlot != null){
					critter.UseBuff(eq.downSlot);
					eq.RemoveItem(eq.downSlot.GetComponent<Item>());
				}
			}

			float h = MoonzInput.GetAxis("H",inputSuffix);
            float v = MoonzInput.GetAxis("V",inputSuffix);

            float angle;
            if (Mathf.Abs(h) + Mathf.Abs(v) > 0.5f) {
                GetComponent<Animator>().SetInteger("animId", 0);
                angle = Mathf.Atan2(h, v);
                sp.rotation = Quaternion.Euler(0, angle * 180 / Mathf.PI, 0);
            } else {
                GetComponent<Animator>().SetInteger("animId", 1);
            }

			sc.MoveForward(  v * critter.getSpeed() * Time.deltaTime);
			sc.MoveSide( h * critter.getSpeed() * Time.deltaTime);
            
            float fh = MoonzInput.GetAxis("FH",inputSuffix);
            float fv = MoonzInput.GetAxis("FV",inputSuffix);
            angle = Mathf.Atan2(fh, fv);

            if (Mathf.Abs(fh) + Mathf.Abs(fv) > 0.5 ){
                Vector3 shootDirection = Camera.main.transform.up * fv + Camera.main.transform.right * fh ;
                critter.Attack(sc.position + shootDirection);
                sp.rotation = Quaternion.Euler(0, angle * 180 / Mathf.PI, 0);
            }

			if (MoonzInput.GetKeyDown(MoonzInput.RB, inputSuffix) || MoonzInput.GetKeyDown(MoonzInput.X, inputSuffix)) {
                PickDropIfAny();
            }

        }
    }

    void PickDropIfAny() {
        GameObject dropObj = World.GetNearestDrop(sc);
        if (dropObj != null && sc.IsColliding(dropObj)) {
            Drop drop = dropObj.GetComponent<Drop>();
			Debug.Log("taking drop");
            if (drop.itemPrefab != null) {
                SendMessage("AddItemToEq", Instantiate(drop.itemPrefab));
            }
            drop.TriggerTakeEffect();
            World.RemoveDrop(dropObj);
        }
    }

    void LetMeDie() {
        World.RemovePlayer(gameObject);



    }
}
