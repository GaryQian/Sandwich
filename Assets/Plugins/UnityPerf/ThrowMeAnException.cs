using System;
using UnityEngine;
using UnityEngine.CrashLog;
using System.Collections;

public class ThrowMeAnException : MonoBehaviour 
{

    void Awake()
    {
		CrashReporting.Init("UPID goes here");
    }

	public void CrashForMe()
	{
		throw new Exception("Button press exception");
	}

}
