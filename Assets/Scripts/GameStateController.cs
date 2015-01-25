using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	private int numOfTeamMembers = 4;
	public int NumOfTeamMembers {get { return numOfTeamMembers; } set { numOfTeamMembers = value; } }

    public bool clickBeginThisFrame = false;

    public int maxTimeDisplacement = 5;

    private List<Player> robbers;
	
	// Use this for initialization
	void Start () 
	{
        robbers = new List<Player> ();
        GameObject[] tmprob = GameObject.FindGameObjectsWithTag ("Player");
        foreach (GameObject go in tmprob) {
            robbers.Add (go.GetComponent<Player>());
        }
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

	// When an UI button is pushed, act accordingly.
	public void ButtonClick (int button )
	{	
		// Depending on what button was pushed.
		switch (button) 
		{
			case (int)ButtonClicked.GoBack:
				
				if ( currentGameState == GameState.Setup)
					currentClick -= 1;
				
				break;
			case (int)ButtonClicked.GoFoward:
				
				if ( currentGameState == GameState.Setup)
					currentClick += 1;

				break;
			case (int)ButtonClicked.Execute:
				
				// hide some buttons
				GameObject obj = GameObject.Find("Execute");
				obj.SetActive(false);
			
				obj = GameObject.Find("goForward");
				obj.SetActive(false);

				obj = GameObject.Find("goBack");
				obj.SetActive(false);

				// Stars game execution phase.
				if (currentGameState == GameState.Setup) 
				{
					currentClick = 0; 
					currentGameState = GameState.Execution;
					lastClickTime = Time.time;
					clickBeginThisFrame = true;
				}
				
				break;

			case (int)ButtonClicked.TeamMember1:
				robbers[2].isSelected = true;
				robbers[1].isSelected = false;
				robbers[0].isSelected = false;

			break;

			case (int)ButtonClicked.TeamMember2:
				robbers[2].isSelected = false;
				robbers[1].isSelected = true;
				robbers[0].isSelected = false;
			
				break;
			case (int)ButtonClicked.TeamMember3:
				robbers[2].isSelected = false;
				robbers[1].isSelected = false;
				robbers[0].isSelected = true;
			
				break;

		}


			
	}
	


}
