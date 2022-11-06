using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FMODManager : MonoBehaviour {

	[System.Serializable]
	public class Event {
		public string eventName;
		public EventReference eventReference;
		public EventInstance eventInstance;

		public bool playing;

		[Range(0, 1)] public float Layer1Vol;
		[Range(0, 1)] public float Layer2Vol;
		[Range(0, 1)] public float Layer3Vol;
		[Range(0, 10)] public float Intensity;

		public void StartPlaying()
		{
			eventInstance = RuntimeManager.CreateInstance(eventReference);
			if (FMODManager.INSTANCE.debugMode) { Debug.Log("StartPlaying ran on " + eventName); }

			eventInstance.start();
			VerifyPlayState();
		}

		public void StopPlayingWithFadeOut()
		{
			if (FMODManager.INSTANCE.debugMode) { Debug.Log("StopPlayingWithFadeOut ran on " + eventName); }
			eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			VerifyPlayState();
		}

		public void StopPlayingImmediate()
		{
			if (FMODManager.INSTANCE.debugMode) { Debug.Log("StopPlayingImmediate ran on " + eventName); }
			eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			VerifyPlayState();
		}

		private void VerifyPlayState()
		{
			PLAYBACK_STATE playbackState;
			eventInstance.getPlaybackState(out playbackState);

			playing = playbackState == PLAYBACK_STATE.PLAYING;
		}

		public void UpdateParam(string _parameterName, float _newValue)
		{
			if (FMODManager.INSTANCE.debugMode) { Debug.Log("UpdateParam " + _parameterName + " for event " + eventName); }
			RuntimeManager.StudioSystem.setParameterByName(_parameterName, _newValue);

			if (_parameterName == "Layer1Vol") {
				Layer1Vol = _newValue;

			} else if (_parameterName == "Layer2Vol") {
				Layer2Vol = _newValue;

			} else if (_parameterName == "Layer3Vol") {
				Layer3Vol = _newValue;

			} else if (_parameterName == "Intensity") {
				Intensity = _newValue; ;

			} else { Debug.LogError("FMODManager UpdateParam This should never happen"); }
		}
	}

	public List<Event> events;

	public static FMODManager INSTANCE;

	public bool debugMode;

	public void Awake()
	{
		RuntimeManager.WaitForAllSampleLoading();
		INSTANCE = this;
	}

	public void PlayOneShotEvent(EventReference _eventReference)
	{
		RuntimeManager.PlayOneShot(_eventReference);
	}

	public Event GetEvent(EventReference _eventReference, string _eventName = "")
	{
		bool _alreadyInList = false;
		Event _currentEvent = new Event();

		foreach (Event _event in events) {
			if (_event.eventReference.Path == _eventReference.Path) {
				_alreadyInList = true;
				_currentEvent = _event;
			}

			break;
		}

		if (!_alreadyInList) {
			Event _newEvent = new Event {
				eventReference = _eventReference,
				eventName = _eventReference.Path,
			};

			events.Add(_newEvent);
			_currentEvent = _newEvent;
		}

		return _currentEvent;
	}

	public void LoadEvent(string _eventName, bool _isPlaying, float _intensity,
		float _layer1Vol, float _layer2Vol, float _layer3Vol)
	{
		Event _newEvent = new Event { };

		_newEvent.eventReference = RuntimeManager.PathToEventReference(_eventName);
		_newEvent.UpdateParam("Layer1Vol", _layer1Vol);
		_newEvent.UpdateParam("Layer2Vol", _layer2Vol);
		_newEvent.UpdateParam("Layer3Vol", _layer3Vol);
		_newEvent.UpdateParam("Intensity", _intensity);

		events.Add(_newEvent);

		if (_isPlaying) { _newEvent.StartPlaying(); }
	}
}
