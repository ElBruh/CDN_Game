﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {
  public ParticleSystem ps;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public void PlayParticles()
  {
    ps.Play();
  }
  public void StopParticles()
  {
    ps.Stop();
  }
}
