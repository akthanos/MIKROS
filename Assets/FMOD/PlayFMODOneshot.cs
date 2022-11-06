using FMODUnity;
using Fungus;
using UnityEngine;


namespace Fungus {
	[CommandInfo("_FMOD",
			"FMOD Oneshot",
			"Triggers play/stop oneshot events in FMOD.")]
	[AddComponentMenu("")]
	public class PlayFMODOneshot : Command {
		public EventReference eventReference;

		public override void OnEnter() {
			FMODManager.INSTANCE.PlayOneShotEvent(eventReference);
			Continue();
		}

		public override Color GetButtonColor() { return new Color32(255, 87, 51, 255); }

		public override string GetSummary() {
			string _summary = "Error: Event is null!";

			if (eventReference.Path != "") { _summary = "Play oneshot " + eventReference.Path; }

			return _summary;
		}
	}
}
