/*
 * This is the display map for the player, to display the map, the position of every unit and such
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class P1_DisplayMap : MonoBehaviour {
	//This is the prefab for all the buildings game objecy
	public GameObject castleIconPrefab;
	public GameObject barrackIconPrefab;
	public GameObject towerPrefab;
	public GameObject wallPrefab;

	//This int is to check if a building is added or destroy to display on the map.
	public List<int> p1_buildingStatus = new List<int>{0,0,0,0};


	void Awake(){
		for (int i = 0; i < 4; i ++) {
			p1_buildingStatus[i] = PlayerController.p1_buildingList[i].Count;		
		}
	}
}
