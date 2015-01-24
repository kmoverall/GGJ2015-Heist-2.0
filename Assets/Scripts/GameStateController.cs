using UnityEngine;
using System.Collections;



public class GameStateController : MonoBehaviour 
{
	
	public enum GameState { Setup, Execution };
	
	// Constant for the number of seconds that elapse before a "Click" happens.
	public const float secondsPerClick = 5.0f;
	private float lastClickTime;
	
	// Current "Click", Denotes number of discrete time periods before or after start of game.
	private int currentClick = 0;
	public int CurrentClick { get { return currentClick; } }

	private GameState currentGameState = GameState.Setup;
	public GameState CurrentGameState { get { return currentGameState; } }

	private float totalMoney = 0.0f;
	public float TotalMoney { get { return totalMoney; } set { totalMoney = value; } }

	private int numOfTeamMembers = 0;
	public int NumOfTeamMembers {get { return numOfTeamMembers; } set { numOfTeamMembers = value; } }

	
	// Use this for initialization
	void Start () 
	{
		
		lastClickTime = Time.time;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Keep track of what curent "Click" is once execution has begun.
		if (currentGameState == GameState.Execution && (Time.time - lastClickTime) > secondsPerClick) 
		{
			currentClick += 1;            
			lastClickTime = Time.time;	  
		}

        if (currentGameState == GameState.Setup) {
            if (Input.GetKeyUp (KeyCode.A)) {
                currentClick--;
            }
            if (Input.GetKeyUp (KeyCode.D)) {
                currentClick++;
            }
            if (Input.GetKeyUp (KeyCode.Space)) {
                currentClick = 0;
            }
        }
	}
	
	void OnGUI() 
	{
		GUILayout.Label ("Curent Click: " + currentClick + "  Time:" + lastClickTime.ToString("0"));

		if (currentClick == 0) 
		{
			if (GUI.Button (new Rect (10, 10, 70, 70), "Execute")) 
				currentGameState = GameState.Execution;

			//if (GUI.Button (new Rect (10, 100, 70, 70), "Move"))
			//	Debug.Log("clicked move buton");
			
			//if (GUI.Button (new Rect (100, 100, 70, 70), "Acion"))
				//Debug.Log("Clicked Action Button");

		}

	}
}
