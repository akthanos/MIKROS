using FMODUnity;
using Fungus;
using UnityEngine;


namespace Fungus {
	[CommandInfo("_FMOD",
			"FMOD Params",
			"Updates FMOD event parameters. Mouse over parameters for tooltips.")]
	[AddComponentMenu("")]
	public class ControlFMODParams : Command {

		[SerializeField]
		public enum Parameter {
			None,
			hHealth,
			hTOD,
			hDelirium,
		}

		public EventReference eventReference;

		[Tooltip("The parameter of the event to update.")]
		public Parameter parameter;

		[Tooltip("Value to which you want to set the selected parameter to.\n\n" +
		"Volumes range from 0 to 1, Intensity ranges from 0 to 10.")]
		public float value;

		private FMODManager.Event fmodEvent;

		public override void OnEnter() {
			fmodEvent = FMODManager.INSTANCE.GetEvent(eventReference);
			fmodEvent.UpdateParam(parameter.ToString(), value);

			Continue();
		}

		public override Color GetButtonColor() { return new Color32(24, 71, 54, 255); }

		public override string GetSummary() {
			string _summary = "Error: Event is null!";

			if (eventReference.Path != "") {
				_summary = "Error: Parameter is none!";

				if (parameter != Parameter.None) {
					_summary = eventReference.Path + ": " + parameter.ToString() + " > " + value;

				}
			}

			return _summary;
		}
	}
}
