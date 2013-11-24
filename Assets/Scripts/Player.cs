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
			
			bool downChange = Input.GetKeyDown(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftShift);
			if (downChange){
				GetComponent<Eq>().ChangeSlot(Item.SLOT_DOWN);
				_down = 0;
			}
			
			bool leftChange = Input.GetKeyDown("joystick " + inputSuffix + " button 7") || (Input.GetKeyDown (KeyCode.LeftArrow) &&Input.GetKey(KeyCode.LeftShift));
			if (leftChange){
				GetComponent<Eq>().ChangeSlot(Item.SLOT_LEFT);
				_left = 0;
			}
			
			bool rightChange = Input.GetKeyDown("joystick " + inputSuffix + " button 5") || Input.GetKeyDown (KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift);
            if (rightChange){
                GetComponent<Eq>().ChangeSlot(Item.SLOT_RIGHT);
				_right = 0;
            }

			bool shieldToggled= Input.GetKeyDown("joystick " + inputSuffix + " button 12") || Input.GetKeyDown("joystick " + inputSuffix + " button 3");
            if (shieldToggled && eq.GetShield() != null) {
                critter.shieldActive = !critter.shieldActive;
            }
            bubble.SetActive(critter.shieldActive && eq.GetShield() != null);

			float h = Input.GetAxis("H"+inputSuffix);
            float v = Input.GetAxis("V"+inputSuffix);

            sc.MoveForward(  (v + _up + _down)* critter.speed * Time.deltaTime);
            sc.MoveSide( (h + _left + _right) * critter.speed * Time.deltaTime);

            float fh = Input.GetAxis("FH"+inputSuffix);
            float fv = Input.GetAxis("FV"+inputSuffix);
            float angle = Mathf.Atan2(fh, fv);

			float upShot = Input.GetKey(KeyCode.UpArrow)?1:0;
			float downShot = Input.GetKey(KeyCode.DownArrow)?-1:0;

			float leftShot = Input.GetKey(KeyCode.LeftArrow)?-1:0;
			float rightShot = Input.GetKey(KeyCode.RightArrow)?1:0;

            if (Mathf.Abs(fh) + Mathf.Abs(fv) > 0.5 || leftShot != 0 || rightShot != 0 || upShot != 0 || downShot != 0){
                Vector3 shootDirection = Camera.main.transform.up * (fv +upShot + downShot)+ Camera.main.transform.right * (fh + leftShot + rightShot);
                critter.Attack(sc.position + shootDirection);
                sp.rotation = Quaternion.Euler(0, angle * 180 / Mathf.PI, 0);
            }

			if (Input.GetButtonDown("PickItem") || Input.GetKey(KeyCode.Space) || Input.GetKeyDown("joystick " + inputSuffix + " button 2")) {
                PickDropIfAny();
            }

        }
    }

    void PickDropIfAny() {
        GameObject dropObj = World.GetNearestDrop(sc);
        if (dropObj != null && sc.IsColliding(dropObj)) {
            Drop drop = dropObj.GetComponent<Drop>();
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
