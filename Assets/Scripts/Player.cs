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
            
			float _up = Input.GetKey(KeyCode.W)?1:0;
			float _down = Input.GetKey(KeyCode.S)?-1:0;
			
			float _left = Input.GetKey(KeyCode.A)?-1:0;
			float _right = Input.GetKey(KeyCode.D)?1:0;
			//change backpack
			bool up = Input.GetKeyDown(KeyCode.UpArrow);
			if (up)
			{
				bool upChange = up && Input.GetKey(KeyCode.LeftShift);
				if (upChange) {
					GetComponent<Eq>().ChangeSlot(Item.SLOT_UP);
					_up = 0;
				}
			}
			

			if (MoonzInput.GetKeyDown(MoonzInput.ARROW_DOWN, inputSuffix)){
				GetComponent<Eq>().ChangeSlot(Item.SLOT_DOWN);
				_down = 0;
			}

			if (MoonzInput.GetKeyDown(MoonzInput.ARROW_LEFT, inputSuffix)){
				GetComponent<Eq>().ChangeSlot(Item.SLOT_LEFT);
				_left = 0;
			}
			
			if (MoonzInput.GetKeyDown(MoonzInput.ARROW_RIGHT, inputSuffix)){
                GetComponent<Eq>().ChangeSlot(Item.SLOT_RIGHT);
				_right = 0;
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

			float h = Input.GetAxis("H"+inputSuffix);
            float v = Input.GetAxis("V"+inputSuffix);

            float angle;
            if (Mathf.Abs(h) + Mathf.Abs(v) > 0.5f) {
                GetComponent<Animator>().SetInteger("animId", 0);
                angle = Mathf.Atan2(h, v);
                sp.rotation = Quaternion.Euler(0, angle * 180 / Mathf.PI, 0);
            } else {
                GetComponent<Animator>().SetInteger("animId", 1);
            }

            sc.MoveForward(  (v + _up + _down)* critter.getSpeed() * Time.deltaTime);
            sc.MoveSide( (h + _left + _right) * critter.getSpeed() * Time.deltaTime);

            float fh = Input.GetAxis("FH"+inputSuffix);
            float fv = Input.GetAxis("FV"+inputSuffix);
            angle = Mathf.Atan2(fh, fv);

            if (Mathf.Abs(fh) + Mathf.Abs(fv) > 0.5 ){
                Vector3 shootDirection = Camera.main.transform.up * fv + Camera.main.transform.right * fh ;
                critter.Attack(sc.position + shootDirection);
                sp.rotation = Quaternion.Euler(0, angle * 180 / Mathf.PI, 0);
            }

			if (Input.GetKey(KeyCode.Space) || Input.GetKeyDown("joystick " + inputSuffix + " button 2")) {
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
