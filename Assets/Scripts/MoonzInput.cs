using UnityEngine;

public class MoonzInput
{
	public const string ARROW_LEFT = "arrow_left";
	public const string ARROW_RIGHT = "arrow_right";
	public const string ARROW_UP = "arrow_up";
	public const string ARROW_DOWN = "arrow_down";
	public const string A = "a";
	public const string B = "b";
	public const string X = "c";
	public const string Y = "d";
	public const string RB = "RB";
	public const string START = "start";

	public const string DEFAULT_MODE = "default";
	public const string INPUT_MANAGER_MODE= "input_manager";

	public static string mode = MoonzInput.INPUT_MANAGER_MODE;

	public static bool GetKeyDown(string keyCode, string inputSuffix) {
		if (mode == DEFAULT_MODE){
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
				case START:{
					return Input.GetKeyDown("joystick " + inputSuffix + " button 7");
				}
            }
			if (keyCode == MoonzInput.X) return Input.GetKeyDown("joystick "+inputSuffix+" button 2");
			if (keyCode == RB) return Input.GetKeyDown("joystick "+inputSuffix+" button 5");
		}
		else if (mode == INPUT_MANAGER_MODE) {
            switch (keyCode) {
                case ARROW_LEFT: return Input.GetButtonDown("ArrowLeft" + inputSuffix);
                case ARROW_UP: return Input.GetButtonDown("ArrowUp" + inputSuffix);
                case ARROW_RIGHT: return Input.GetButtonDown("ArrowRight" + inputSuffix);
                case ARROW_DOWN: return Input.GetButtonDown("ArrowDown" + inputSuffix);
                case A: return Input.GetButtonDown("ButtonA" + inputSuffix);
                case B: return Input.GetButtonDown("ButtonB" + inputSuffix);
                case X: return Input.GetButtonDown("ButtonX" + inputSuffix);
                case Y: return Input.GetButtonDown("ButtonY" + inputSuffix);
                case RB: return Input.GetButtonDown("ButtonRB" + inputSuffix);
                case START: return Input.GetButtonDown("ButtonStart" + inputSuffix);
            }

        } 

        /* Mac
        	if (keyCode == ARROW_UP) return Input.GetKeyDown("joystick "+inputSuffix+" button 5");
			if (keyCode == ARROW_DOWN) return Input.GetKeyDown("joystick "+inputSuffix+" button 6");
        	if (keyCode == ARROW_LEFT) return Input.GetKeyDown("joystick "+inputSuffix+" button 7");
			if (keyCode == ARROW_RIGHT) return Input.GetKeyDown("joystick "+inputSuffix+" button 8");
			if (keyCode == A) return Input.GetKeyDown("joystick "+inputSuffix+" button 16");
			if (keyCode == B) return Input.GetKeyDown("joystick "+inputSuffix+" button 17");
			if (keyCode == X) return Input.GetKeyDown("joystick "+inputSuffix+" button 18");
			if (keyCode == Y) return Input.GetKeyDown("joystick "+inputSuffix+" button 19");
			if (keyCode == RB) return Input.GetKeyDown("joystick "+inputSuffix+" button 14");
			if (keyCode == START) return Input.GetKeyDown("joystick "+inputSuffix+" button 9");
        */
        return false;
    }

    public static float GetAxis(string axis, string inputSuffix) {
        if (mode == DEFAULT_MODE) {
            switch (axis) {
                case "V": return Input.GetAxis("V" + inputSuffix);
                case "H": return Input.GetAxis("H" + inputSuffix);
                case "FV": return Input.GetAxis("FV" + inputSuffix);
                case "FH": return Input.GetAxis("FH" + inputSuffix);
            }
        } else if (mode == INPUT_MANAGER_MODE) {
            switch (axis) {
                case "V": return Input.GetAxis("VerticalMoveAxis" + inputSuffix);
                case "H": return Input.GetAxis("HorizontalMoveAxis" + inputSuffix);
                case "FV": return Input.GetAxis("VerticalFireAxis" + inputSuffix);
                case "FH": return Input.GetAxis("HorizontalFireAxis" + inputSuffix);
            }
        } else {
			if (axis == "FV") return Input.GetAxis("MacFV");
			if (axis == "FH") return Input.GetAxis("MacFH");
            return Input.GetAxis(axis+"1");
        }
        return 0;
    }

}

