using UnityEngine;
using System.Collections;

public class main : MonoBehaviour {


	public Shader grayShader;

	private GameObject[] _tempTexutreAll;

	public GameObject _plane;
	// default cut the pitcure to four pieces.
	public int count = 1;


	private float[,] points = 
	// left Bottom    left Top       right Top       right bottom
	{ {0.5f,0.5f,0f,0f},{0.5f,0.5f,0f,0.5f},{0.5f,0.5f,0.5f,0.5f},{0.5f,0.5f,0.5f,1f}};

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
			_tempTexutreAll [num] = temp;

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
	}
	// Update is called once per frame
	void Update () {
	
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
