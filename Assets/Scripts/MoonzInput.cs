using UnityEngine;

public class MoonzInput
{
	public const string ARROW_LEFT = "arrow_left";
	public const string ARROW_RIGHT = "arrow_right";
	public const string ARROW_DOWN = "arrow_down";
	public const string B = "b";
	public const string A = "a";

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
		return false;
	}

}

