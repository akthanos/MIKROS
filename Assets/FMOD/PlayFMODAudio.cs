using FMODUnity;
using Fungus;
using UnityEngine;


namespace Fungus {
	[CommandInfo("_FMOD",
			"FMOD Play",
			"Triggers play/stop of an event in FMOD. Mouse over parameters for tooltips.")]
	[AddComponentMenu("")]
	public class PlayFMODAudio : Command {

		[SerializeField]
		public enum Action {
			None,
			Play,
			StopWithFadeOut,
			StopImmediate,
		}

		public EventReference eventReference;

		[Tooltip("Leave Action at 'none' if you only want to update parameters. It stops or plays an event otherwise.")]
		public Action action;

		private FMODManager.Event fmodEvent;

		public override void OnEnter() {
			fmodEvent = FMODManager.INSTANCE.GetEvent(eventReference);

			if (action == Action.Play) {
				fmodEvent.StartPlaying();

			} else if (action == Action.StopWithFadeOut) {
				fmodEvent.StopPlayingWithFadeOut();

			} else if (action == Action.StopImmediate) {
				fmodEvent.StopPlayingImmediate();
			}

			Continue();
		}

		public override Color GetButtonColor() { return new Color32(24, 71, 54, 255); }

		public override string GetSummary() {
			string _summary = "Error: Event is null!";

			if (eventReference.Path != "") {
				_summary = "Error: Action is none!";

				if (action != Action.None) {
					_summary = action.ToString() + ": " + eventReference.Path;
				}
			}

			return _summary;
		}

	}
}
