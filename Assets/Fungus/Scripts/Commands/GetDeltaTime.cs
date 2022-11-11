using UnityEngine;

namespace Fungus
{
    [CommandInfo("Property",
        "GetDeltaTime",
        "Place in Update to get the time in seconds since the last frame the computer renders. Use a Fungus Float var to store this value.")]
    [AddComponentMenu("")]

    public class GetDeltaTime : Command
    {
        [Tooltip("Use the arrow to the right and make a Fungus Float variable to be able to use the Time.deltaTime value")]
        [SerializeField] protected FloatData deltaTimeVar = new FloatData(0f);

        public override void OnEnter()
        {
            deltaTimeVar.Value = Time.deltaTime;

            Continue();
        }
    }

}




