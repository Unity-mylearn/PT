﻿using UnityEngine;
using System.Collections;
using System.Reflection;
using UnityEditor;

public class main : MonoBehaviour {


	private bool isGameStart = false;
	public Shader grayShader;

	public GameObject nullGameObject;
	private GameObject[] _tempTexutreAll;

	public GameObject _plane;
	// default cut the pitcure to four pieces.
	public int count = 1;


	private float[,] points = 
	// lefttop right son bottom son   left bottom       right Top                     right bottom
	{ {0.5f,0.5f,0f,0.5f},{0.5f,0.5f,0f,0f},{0.5f,0.5f,0.5f,0.5f},{0.5f,0.5f,0.5f,1f}};

	private string[,] sons = {
		{ "2","1"}, { "3","-1"}, {"-1","3"}, { "-1","-1"}
	};

	// Use this for initialization
	void Start () {
		_tempTexutreAll = new GameObject[4];
		int num = 0;
		while (num < 4) {
			GameObject temp = Instantiate(_plane);
			temp.GetComponent<Renderer> ().material.mainTextureScale = new Vector2 (points[num,0],points[num,1]);
			temp.GetComponent<Renderer> ().material.mainTextureOffset = new Vector2 (points[num,2], points[num,3]);
			temp.name = "piece" + num;
			temp.transform.localScale = new Vector3 (0.4f,0.4f,0.4f);
			temp.tag = ""+num;
			_tempTexutreAll [num] = temp;
			_tempTexutreAll [num].GetComponent<son> ().horizontalObject = nullGameObject;
			_tempTexutreAll [num].GetComponent<son> ().verticalObject = nullGameObject;
			num++;
		}
		StartGame ();
	}


	void StartGame(){
		_plane.GetComponent<Renderer> ().material.shader = grayShader;
		for (int i = 0; i < _tempTexutreAll.Length; i++) {
			Debug.Log (_tempTexutreAll[i].ToString ());
			_tempTexutreAll [i].transform.localPosition = new Vector3 (Random.Range(-4,4),Random.Range(-1,1),0);
		}
		isGameStart = true;
	}
	// Update is called once per frame
	void Update () {
		// check if success. so how to check?
		// TODO
		if (isGameStart) {
			doCheck ();
		}
	}

	private void doCheck(){
		// 0.x = 1.x && 2.x = 3.x && 0.y = 2.y && 1.y = 3.y   => success
		// 0.x - 2.x > 0.5f && 1.x - 3.x > 0.5f && 0.y - 1.y > 0.5f && 2.y - 3.y > 0.5f
		//0 2   
		//1 3
		if (Mathf.Abs(Mathf.Abs(_tempTexutreAll [0].transform.localPosition.x) -  Mathf.Abs(_tempTexutreAll [1].transform.localPosition.x))<=0.5f
			&& Mathf.Abs(Mathf.Abs(_tempTexutreAll [2].transform.localPosition.x) -  Mathf.Abs(_tempTexutreAll [3].transform.localPosition.x))<=0.5f
			&& Mathf.Abs(Mathf.Abs(_tempTexutreAll [0].transform.localPosition.y) -  Mathf.Abs(_tempTexutreAll [2].transform.localPosition.y))<=0.5f
			&& Mathf.Abs(Mathf.Abs(_tempTexutreAll [1].transform.localPosition.y) -  Mathf.Abs(_tempTexutreAll [3].transform.localPosition.y))<=0.5f
		
//			&& Mathf.Abs(Mathf.Abs(_tempTexutreAll [0].transform.localPosition.x) -  Mathf.Abs(_tempTexutreAll [2].transform.localPosition.x))>=0.5f
//			&& Mathf.Abs(Mathf.Abs(_tempTexutreAll [0].transform.localPosition.x) -  Mathf.Abs(_tempTexutreAll [2].transform.localPosition.x))<=1f
//			&& Mathf.Abs(Mathf.Abs(_tempTexutreAll [1].transform.localPosition.x) -  Mathf.Abs(_tempTexutreAll [3].transform.localPosition.x))>=0.5f
//			&& Mathf.Abs(Mathf.Abs(_tempTexutreAll [1].transform.localPosition.x) -  Mathf.Abs(_tempTexutreAll [3].transform.localPosition.x))<=1f

//			&& Mathf.Abs(Mathf.Abs(_tempTexutreAll [0].transform.localPosition.y) -  Mathf.Abs(_tempTexutreAll [1].transform.localPosition.y))>=0.5f
//			&& Mathf.Abs(Mathf.Abs(_tempTexutreAll [0].transform.localPosition.y) -  Mathf.Abs(_tempTexutreAll [1].transform.localPosition.y))<=1f
//			&& Mathf.Abs(Mathf.Abs(_tempTexutreAll [2].transform.localPosition.y) -  Mathf.Abs(_tempTexutreAll [3].transform.localPosition.y))>=0.5f
//			&& Mathf.Abs(Mathf.Abs(_tempTexutreAll [2].transform.localPosition.y) -  Mathf.Abs(_tempTexutreAll [3].transform.localPosition.y))<=1f
		
		) {

			Debug.Log ("Success");
		}
	}

	void FixedUpdate(){
		if (Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			RaycastHit hitInfo;
			if (Physics.Raycast (ray, out hitInfo)) {
				Debug.DrawLine (ray.origin, hitInfo.point);
				GameObject gameobj = hitInfo.collider.gameObject;
				if (gameobj.name.Contains ("piece")) {
					for (int i = 0; i < _tempTexutreAll.Length; i++) {
						if (_tempTexutreAll [i].gameObject.name == gameobj.name) {
							continue;
						} else {
							_tempTexutreAll [i].GetComponent<MeshCollider> ().enabled = false;
						}
					}
					gameobj.transform.localPosition = hitInfo.point;
				}
			}
		} else if (Input.GetMouseButtonUp (0)) {
			for (int i = 0; i < _tempTexutreAll.Length; i++) {
				_tempTexutreAll [i].GetComponent<MeshCollider> ().enabled = true;
			}
		}
	}

	private void Cutoff(GameObject temp){
		
	}



}
