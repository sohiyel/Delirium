// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;



[RequireComponent(typeof(Collider))]
public class tele6 : MonoBehaviour, IGvrGazeResponder {
	private Vector3 startingPosition;
	private float delay = 0.0f; 
	private bool gazed = false;
	public float xPoint;
	public float yPoint;
	public float zPoint;


	void Start() {
		print ("Start");
		startingPosition = transform.localPosition;
		SetGazedAt(false);
		//delay = Time.time + 2.0f;
		//print(delay);
		//print ("       ");
	}
	//void Update()
	//{
	/*GameObject me = GameObject.Find ("My");	
		Ray ray = new Ray (me.transform.position, me.transform.rotation* Vector3.forward);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			if (hit.collider.tag == "Death") {
				print ("HHHHHHHH");
			}
		}*/



	//}

	void LateUpdate() {
		print ("LateUpdate");
		GvrViewer.Instance.UpdateState();
		if (GvrViewer.Instance.BackButtonPressed) {
			Application.Quit();
		}
		if (gazed == false) {
			delay = Time.time + 2.0f;
		}
		if (Time.time > delay) {			
			TeleportRandomly ();
		}
	}

	public void SetGazedAt(bool gazedAt) {		
		print ("SetGazedAt");
//		GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
		//print (GetComponent<Transform>().gameObject.transform.position);

		if (gazedAt) {
			gazed = true;
		}
		else
			gazed = false;
	}

	public void Reset() {
		print ("Reset");
		transform.localPosition = startingPosition;
		//delay = Time.time + 2.0f;
	}

	public void ToggleVRMode() {
		print ("ToggleVRMode");
		GvrViewer.Instance.VRModeEnabled = !GvrViewer.Instance.VRModeEnabled;
	}

	public void ToggleDistortionCorrection() {
		GvrViewer.Instance.DistortionCorrectionEnabled =
			!GvrViewer.Instance.DistortionCorrectionEnabled;
	}

	#if !UNITY_HAS_GOOGLEVR || UNITY_EDITOR
	public void ToggleDirectRender() {
		GvrViewer.Controller.directRender = !GvrViewer.Controller.directRender;

	}
	#endif  //  !UNITY_HAS_GOOGLEVR || UNITY_EDITOR

	public void TeleportRandomly() {
		print ("TeleportRandomly");
		//Vector3 direction = Random.onUnitSphere;
		//direction.y = Mathf.Clamp(direction.y, 0.5f, 1f);
		//float distance = 2 * Random.value + 1.5f;
		GameObject me = GameObject.Find ("Capsule");	
		//GameObject p11 = GameObject.Find ("Maze01");	
		me.transform.position = new Vector3(xPoint,yPoint,zPoint);
		if (xPoint == 0 && yPoint == 0 && zPoint == 0)
			Application.LoadLevel ("Win");
		//transform.localPosition = direction * distance;
		//	transform.gameObject.Capsule = transform.localPosition;
	}
	#region IGvrGazeResponder implementation

	/// Called when the user is looking on a GameObject with this script,
	/// as long as it is set to an appropriate layer (see GvrGaze).
	public void OnGazeEnter() {	
		print ("OnGazeEnter");
		//delay = Time.time + 2.0f;
		SetGazedAt(true);
		gazed = true;
	}

	/// Called when the user stops looking on the GameObject, after OnGazeEnter
	/// was already called.
	public void OnGazeExit() {
		print ("OnGazeExit");
		//delay = Time.time + 2.0f;
		SetGazedAt(false);
		gazed = false;
	}

	/// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
	public void OnGazeTrigger() {
		//TeleportRandomly();
	}

	#endregion
}
