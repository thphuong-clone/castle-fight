using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class TestCortana : MonoBehaviour
{
	//The display UI to display cortana text
	[SerializeField]Text cortanaText;
	
	//The command, use it to resolve
	public string command;
	//[Unit Type] + [Order type] + [Position 1] + [Position 2]
	// Swordsman Attack Top Right


	//First one
	List<string> swordManVariation = new List<string>
	{"sword","samurai","soldier","infantry"};
	List<string> archerVariation = new List<string>
	{"archer","bow","arrow","ranger","range"};
	List<string> horseManVariation = new List<string>
	{"horse","fast","cavalry","knight"};
	List<string> gladiatorVariation = new List<string>
	{"gladiator","defender","heavy","shield"};
	List<string> cannonVariation = new List<string>
	{"cannon","explosion","catapult"};

	//second one
	List<string> attackVariation = new List<string>
	{"attack","charge","push","kill"};
	List<string> moveVariation = new List<string>
	{"move","defend","pull","retreat"};

	//third one
	List<string> topVariation = new List<string>
	{"north","up","top","high"};
	List<string> bottomVariation = new List<string>
	{"south","bottom","down","low"};

	//last one
	List<string> upVariation = new List<string>
	{"north","up","top","high"};
	List<string> downVariation = new List<string>
	{"south","bottom","down","low"};
	List<string> leftVariation = new List<string>
	{"left","west"};
	List<string> rightVariation = new List<string>
	{"right","east"};


    void Start()
    {
        cortanaText.text = "Press the button and tell me something (✿◕‿◕)";
        cortanaText.alignment = TextAnchor.MiddleCenter;
    }

    void Update()
    {
        if (Cortana.Interop.receivingVoice)
        {
            cortanaText.text = "Listening";
        }
        else
        {
            if (!string.IsNullOrEmpty(Cortana.Interop.cortanaReceivedText))
            {
                cortanaText.text = Cortana.Interop.cortanaReceivedText;
				command = Cortana.Interop.cortanaReceivedText;
				commandResolver(command);
            }
        }
    }

	//this function will analyze the voice of cortana
	public void commandResolver(string _command){
		string[] part = new string[4];
		int count = 0;
		char[] c = _command.ToCharArray();
		int cached = 0;
		//first, find the number of words,
		int spaceCount = 0;
		for (int i = 0; i < c.Length; i ++) {
			if (c[i] == ' ')
				spaceCount ++;
		}
		if (spaceCount != 3){
			Debug.LogWarning("Not enough words");
			return;
		}
		//if there are enough words, divide those into 4 strings
		//divide the command string into 4 parts.
		for (int i = 0; i < c.Length; i ++) {
			if (c[i] == ' '){
				//add the word
				for (int j = cached; j < i; j ++){
					part[count] += c[j];
				}
				cached = i + 1 ;
				count ++;
				if (count == 3){
					for (int k = cached; k < c.Length ; k ++){
						part[3] += c[k];
					}
				}
			}
		}
		//all the words are stored now in part 0 -> 4, start working here
		int[] commandCase = new int[4];
		for (int i = 0; i < 4; i ++) {
			commandCase[i] = 0;	
		}

		//for the part[0]
		if (swordManVariation.Contains (part [0])) {
			commandCase[0] = 0;	
		}
		else{
			if (archerVariation.Contains(part[0])){
				commandCase[0] = 1;
			}
			else{
				if (horseManVariation.Contains(part[0])){
					commandCase[0] = 2;
				}
				else{
					if (gladiatorVariation.Contains(part[0])){
						commandCase[0] = 3;
					}
					else{
						if (cannonVariation.Contains(part[0])){
							commandCase[0] = 4;
						}
						else{
							Debug.Log("can't recognize command at part 1");
							return;
						}
					}
				}
			}
		}
		//Part[1]
		if (attackVariation.Contains (part [1])) {
			commandCase[1] = 2;	
		}
		else{
			if (moveVariation.Contains(part[1])){
				commandCase[1] = 1;
			}
			else{
				Debug.Log("can't recognize command at part 2");
				return;
			}
		}
		//part[2]
		if (topVariation.Contains (part [2])) {
			commandCase[2] = 1;	
		}
		else{
			if (bottomVariation.Contains(part[2])){
				commandCase[2] = -1;
			}
			else{
				Debug.Log("can't recognize command at part 3");
				return;
			}
		}

		//part[3]
		if (upVariation.Contains(part[3])){
			commandCase[3] = 1;
		}
		else{
			if (downVariation.Contains(part[3])){
				commandCase[3] = 2;
			}
			else{
				if (leftVariation.Contains(part[3])){
					commandCase[3] = 3;
				}
				else{
					if (rightVariation.Contains(part[3])){
						commandCase[3] = 4;
					}
					else{
						Debug.Log("can't recognize command at part 4");
						return;
					}
				}
			}
		}

		//issue commands
		//this is the list of the soldier that is going to be commanded
		List<Soldier> ls = PlayerController.p1_listOfSoldierLists [commandCase [0]];
		//type of order, attack or move
		//Vector3 v = PlayerController.p1_soldierOrder [commandCase [0]];
		//set the position
		Vector2 dPos = Vector2.zero;
		switch (commandCase [3]) {
		case 1://up
			dPos = new Vector2(UnityEngine.Random.Range(-0.5f,0.5f),5.5f);
			break;
		case 2://down
			dPos = new Vector2(UnityEngine.Random.Range(-0.5f,0.5f),0.5f);
			break;
		case 3://left
			dPos = new Vector2(UnityEngine.Random.Range(-4.25f,-3.75f), UnityEngine.Random.Range(-0.5f,0.5f));
			break;
		case 4://right
			dPos = new Vector2(UnityEngine.Random.Range(3.75f,4.25f), UnityEngine.Random.Range(-0.5f,0.5f));
			break;
		}
		dPos = new Vector2 (dPos.x, dPos.y * commandCase [2]);
		//set up the position and the command
		PlayerController.p1_soldierOrder [commandCase [0]] = new Vector3 (dPos.x,dPos.y,commandCase[1]);
		//now order every soldier
		foreach (Soldier s in ls) {
			s.Deploy(PlayerController.p1_soldierOrder[commandCase[0]]);
		}
	}

}

