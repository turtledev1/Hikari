﻿using UnityEngine;
using System.Collections;

public class PlayerStateManager : Singleton<PlayerStateManager>
{
	public enum PlayerState
	{
		Alive,
		Dead
	}

	public PlayerState currentState { get; set; }

	void Start()
	{
		currentState = PlayerState.Alive;
		SoundManager.Instance.LevelStart();
	}

	public void Death()
	{
		currentState = PlayerState.Dead;

		gameObject.SetActive(false);
		TimerManager.Instance.StopTimer();
		MenuManager.Instance.Death();
		SoundManager.Instance.PlayerDeath();

		CameraManager.Instance.ToggleCameraMoving(false);
	}

	public void Respawn()
	{
		Vector3 position = GameObject.FindGameObjectWithTag("Respawn").transform.position;

		gameObject.transform.position = position;
		GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
		currentState = PlayerState.Alive;
		gameObject.SetActive(true);

		MenuManager.Instance.CloseAllMenus();
		TimerManager.Instance.RestartTimer();
		SoundManager.Instance.LevelStart();

		CameraManager.Instance.ToggleCameraMoving(true);
	}
}
