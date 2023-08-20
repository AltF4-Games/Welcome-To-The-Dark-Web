using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoorScript_ : MonoBehaviour {
	private Transform[] Childs;
	private Transform Joint01;
	private Transform Joint02;

	public enum OpenStyle
	{
		BUTTON, 
		AUTOMATIC
	}
	
	[Serializable]
	public class DoorControls
	{
		public float openingSpeed = 1;
		public float closingSpeed = 1.3f;
		[Range(0,1)]
		public float closeStartFrom = 0.6f;
		public OpenStyle openMethod;
		public bool autoClose = false;
		public bool closetScare = false;
	}
	[Serializable]
	public class AnimNames
	{
		public string OpeningAnim = "Door_open";
		public string LockedAnim = "Door_locked";
	}
	[Serializable]
	public class DoorSounds 
	{
		public bool enabled = true;
		public AudioClip open;
		public AudioClip close;
		public AudioClip closed;
		[Range(0, 1.0f)]
		public float volume = 1.0f;
		[Range(0, 0.4f)]
		public float pitchRandom = 0.2f;
	}

	[Serializable]
	public class KeySystem
	{
		public bool enabled = false;
		[HideInInspector]
		public bool isUnlock = false;
		[Tooltip("If you have a padlock model, you can put the prefab here.")]
		public GameObject LockPrefab;
	}
	
	[Tooltip("player's head with collider in trigger mode. Type your tag here (usually it is MainCamera)")]
	public string PlayerHeadTag = "MainCamera";
	[Tooltip("Empty gameObject in the door knobs area. It needed to open the door if "+ "'"+ "Open by button"+"'"+" type is selected. If you don't want to put this object in this slot manually, you can simply create the object with the name " +"'"+"doorKnob"+"'" +" and put it in the door prefab.")]
	public Transform knob;
	
	public DoorControls controls = new DoorControls();
	public AnimNames AnimationNames = new AnimNames();
	public DoorSounds doorSounds = new DoorSounds();
	public KeySystem keySystem = new KeySystem();

	Transform player;
	bool Opened = false;
	bool inZone = false; 
	Canvas TextObj;
	Text theText;
	AudioSource SoundFX;
	Animation doorAnimation;
	Animation LockAnim;
	
	private void Start ()
	{
		Childs = GetComponentsInChildren<Transform> ();
		foreach (Transform Child in Childs) {
			if (Child.name == "Joint01") {
				Joint01 = Child.transform;
			}
			else if(Child.name == "Joint02"){
				Joint02 = Child.transform;
			}
		}

		foreach(Transform Child in Childs){
			if(Child.name == "Door_bottom01"){
				Child.parent = Joint01;
			}else if(Child.name == "Door_bottom02"){
				Child.parent = Joint02;
			}
		}

		if(controls.openMethod == OpenStyle.AUTOMATIC)
			controls.autoClose = true;

		if(PlayerHeadTag == "")
			Debug.LogError("You need to set a tag!");

		if (GameObject.FindWithTag (PlayerHeadTag) != null) {
			player = GameObject.FindWithTag (PlayerHeadTag).transform;
		} 
		else {
			Debug.LogWarning(gameObject.name + ": You need to set your player's camera tag to " + "'"+PlayerHeadTag+"'." + " The " + "'" + gameObject.name+"'" +" can't open/close if you don't set this tag");
		}

		AddLock();
		AddAudioSource();
		DetectDoorKnob ();
		doorAnimation = GetComponent<Animation>();
	}


	void AddLock(){
		if(!keySystem.enabled)
			return;

		if(keySystem.LockPrefab == null){
			Debug.LogWarning(gameObject.name + ": you can set a padlock prefab if you want.");
		}
		else {
			LockAnim = keySystem.LockPrefab.GetComponent<Animation> ();
			keySystem.enabled = true;
		}
	}

	void AddAudioSource()
	{
		GameObject go = new GameObject("SoundFX");
		go.transform.position = transform.position;
		go.transform.rotation = transform.rotation;
		go.transform.parent = transform;
		SoundFX = go.AddComponent<AudioSource>();
		SoundFX.volume = doorSounds.volume;
		SoundFX.spatialBlend = 1;
		SoundFX.playOnAwake = false;
		SoundFX.clip = doorSounds.open; 
	}

	void DetectDoorKnob()
	{
		if(knob == null){
		Transform[] children = GetComponentsInChildren<Transform>(true);
		
		foreach(Transform child in children){
			if(child.name == "doorKnob"){
				knob = child;
			}
		}
		}
	}
	
	private void Update () {
		if (!doorAnimation.isPlaying && SoundFX.isPlaying) {
			SoundFX.Stop();
		}
	}
	
	private void OpenLockDoor()
	{
		if(keySystem.LockPrefab != null){
			LockAnim.Play("Lock_open");
			Invoke("OpenDoor", 1);
		} 
		else
		{
			OpenDoor();
		}
	}
	
	public void Unlock()
	{
		keySystem.isUnlock = true;
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag != PlayerHeadTag)
			return;
		if (inZone) {
			print("WARN");
		}
		inZone = true;
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag != PlayerHeadTag) 
			return;


		if(Opened && controls.autoClose)
			CloseDoor();
		
		inZone = false;
	}


	#region AUDIO
	/*
	 * 	AUDIO
	 */ 
	void PlaySFX(AudioClip clip)
	{
		if(!doorSounds.enabled)
			return;

		SoundFX.pitch = UnityEngine.Random.Range (1-doorSounds.pitchRandom, 1+doorSounds.pitchRandom);
		SoundFX.clip = clip;
		SoundFX.Play();
	}
	
	void PlayClosedFXs(){
		if (doorSounds.closed != null) {
			SoundFX.clip = doorSounds.closed;
			SoundFX.Play();
			if(doorAnimation[AnimationNames.LockedAnim] != null){
				doorAnimation.Play(AnimationNames.LockedAnim);
				doorAnimation[AnimationNames.LockedAnim].speed = 1;
				doorAnimation [AnimationNames.LockedAnim].normalizedTime = 0;
			}


		}
	}

	void CloseSound()
	{
		if(doorAnimation[AnimationNames.OpeningAnim].speed < 0 && doorSounds.close != null)
			PlaySFX(doorSounds.close);
	}
	#endregion
	
	public void ToggleDoor()
    {
		if (controls.closetScare) FindObjectOfType<HallucinationScare>().TriggerScare();

		if (!Opened) OpenDoor();
		else CloseDoor();
    }

	public void OpenDoor()
	{
		doorAnimation.Play(AnimationNames.OpeningAnim);
		doorAnimation[AnimationNames.OpeningAnim].speed = controls.openingSpeed;
		doorAnimation [AnimationNames.OpeningAnim].normalizedTime = doorAnimation [AnimationNames.OpeningAnim].normalizedTime;

		if(doorSounds.open != null)
			PlaySFX(doorSounds.open);
		
		Opened = true;

		keySystem.enabled = false;
	}

	public void CloseDoor()
	{
		if (doorAnimation[AnimationNames.OpeningAnim].normalizedTime < 0.98f && doorAnimation [AnimationNames.OpeningAnim].normalizedTime > 0) 
		{
			doorAnimation[AnimationNames.OpeningAnim].speed = -controls.closingSpeed;
			doorAnimation[AnimationNames.OpeningAnim].normalizedTime = doorAnimation [AnimationNames.OpeningAnim].normalizedTime;
			doorAnimation.Play (AnimationNames.OpeningAnim);
		} 
		else 
		{
			doorAnimation[AnimationNames.OpeningAnim].speed = -controls.closingSpeed;
			doorAnimation[AnimationNames.OpeningAnim].normalizedTime = controls.closeStartFrom;
			doorAnimation.Play (AnimationNames.OpeningAnim);
		}
		if(doorAnimation[AnimationNames.OpeningAnim].normalizedTime > controls.closeStartFrom){
			doorAnimation[AnimationNames.OpeningAnim].speed = -controls.closingSpeed;
			doorAnimation[AnimationNames.OpeningAnim].normalizedTime = controls.closeStartFrom;
			doorAnimation.Play (AnimationNames.OpeningAnim);
		}
		Opened = false;
	}
	
}