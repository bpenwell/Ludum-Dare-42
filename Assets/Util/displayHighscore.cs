﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayHighscore : MonoBehaviour {

	public waveManager holdsWave;
	// Use this for initialization
	void Start () {
		GetComponent<Text>().text = holdsWave.CurrentWave.ToString();
	}
}
