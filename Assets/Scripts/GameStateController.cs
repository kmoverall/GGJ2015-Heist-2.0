using UnityEngine;
using System.Collections;



public class GameStateController : MonoBehaviour 
{
	
	public enum GameState { Setup, Execution };
	public enum ButtonClicked { GoBack, GoFoward, Execute, TeamMember1, TeamMember2, TeamMember3, TeamMember4 };
	
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

    public bool clickBeginThisFrame = false;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (clickBeginThisFrame) {
            clickBeginThisFrame = false;
        }

		// Keep track of what curent "Click" is once execution has begun.
		if (currentGameState == GameState.Execution && (Time.time - lastClickTime) > secondsPerClick) 
		{
			currentClick += 1;            
            lastClickTime = Time.time;
            clickBeginThisFrame = true;
            Debug.Log (currentClick);
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
            if (Input.GetKeyUp (KeyCode.Return)) {
                currentGameState = GameState.Execution;
                lastClickTime = Time.time;
                clickBeginThisFrame = true;
            }
        }
	}

	public void ButtonClick (int button )
	{	


		switch (button) 
		{
			case (int)ButtonClicked.GoBack:
				
				break;
			case (int)ButtonClicked.GoFoward:
				
				break;
			case (int)ButtonClicked.Execute:
				
				break;
			case (int)ButtonClicked.TeamMember1:
				
				break;
			case (int)ButtonClicked.TeamMember2:
				
				break;
			case (int)ButtonClicked.TeamMember3:
				
				break;
			case (int)ButtonClicked.TeamMember4:
				
				break;
		}


			if (currentClick == 0 && currentGameState == GameState.Setup) 
			{
				currentGameState = GameState.Execution;
				lastClickTime = Time.time;
				clickBeginThisFrame = true;
			}
	}
	


}
