using UnityEngine;

public class MoonzInput
{
	public const string ARROW_LEFT = "arrow_left";
	public const string ARROW_RIGHT = "arrow_right";
	public const string ARROW_UP = "arrow_up";
	public const string ARROW_DOWN = "arrow_down";
	public const string A = "a";
	public const string B = "b";
	public const string C = "c";
	public const string D = "d";
	public const string RB = "RB";
	public const string X = "X";

	public const string DEFAULT_MODE = "default";

	public static string mode = MoonzInput.DEFAULT_MODE;

	public static bool GetKeyDown(string keyCode, string inputSuffix) {
		switch(keyCode) {
			case ARROW_LEFT: {
				return Input.GetKeyDown("joystick " + inputSuffix + " button 7") || 
					(Input.GetKeyDown (KeyCode.LeftArrow) &&Input.GetKey(KeyCode.LeftShift)) ||
					Input.GetAxis("ChooseItemX"+inputSuffix)==-1;
			}
			case ARROW_RIGHT:{
				return Input.GetKeyDown("joystick " + inputSuffix + " button 5") || 
					Input.GetKeyDown (KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift) ||
					Input.GetAxis("ChooseItemX"+inputSuffix)==1;
			}
			case ARROW_DOWN:{
				return Input.GetKeyDown("joystick " + inputSuffix + " button 6") || 
					(Input.GetKeyDown(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftShift)) ||
					Input.GetAxis("ChooseItemY"+inputSuffix)==-1;
					;
			}
			case B: {
				return Input.GetKeyDown("joystick " + inputSuffix + " button 12") || Input.GetKeyDown("joystick " + inputSuffix + " button 3") ||
				Input.GetKeyDown("joystick " + inputSuffix + " button 1") // to jest B na win 
				;
			}
			case A: {
				return Input.GetKeyDown("joystick " + inputSuffix + " button 0");				
			}
		}

		if (mode == DEFAULT_MODE){
			if (keyCode == MoonzInput.X) return Input.GetKeyDown("joystick "+inputSuffix+" button 2");
			if (keyCode == RB) return Input.GetKeyDown("joystick "+inputSuffix+" button 5");
		}
		else{
        	if (keyCode == ARROW_UP) return Input.GetKeyDown("joystick "+inputSuffix+" button 5");
			if (keyCode == ARROW_DOWN) return Input.GetKeyDown("joystick "+inputSuffix+" button 6");
			if (keyCode == A) return Input.GetKeyDown("joystick "+inputSuffix+" button 16");
			if (keyCode == B) return Input.GetKeyDown("joystick "+inputSuffix+" button 17");
			if (keyCode == C) return Input.GetKeyDown("joystick "+inputSuffix+" button 18");
			if (keyCode == D) return Input.GetKeyDown("joystick "+inputSuffix+" button 19");
			if (keyCode == RB) return Input.GetKeyDown("joystick "+inputSuffix+" button 14");
		}
        

        return false;
    }

    public static float GetAxis(string axis, string inputSuffix) {
        if (mode == "default") {
            return Input.GetAxis(axis+inputSuffix);
        } else {
			if (axis == "FV") return Input.GetAxis("MacFV");
			if (axis == "FH") return Input.GetAxis("MacFH");
            return Input.GetAxis(axis+"1");
        }
    }

}

