using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {


	public class Turn : ActionTask {

        public float turnDuration = 0.5f; // How long the turn should take in seconds
        private float timer = 0f;

        private Quaternion startRotation;
        private Quaternion targetRotation;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            
            timer = 0f;
            startRotation = agent.transform.rotation;
            targetRotation = startRotation * Quaternion.Euler(0, 180f, 0);
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            
            timer += Time.deltaTime;

            // Calculate progress as a percentage from 0.0 to 1.0
            float percentComplete = timer / turnDuration;
            agent.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, percentComplete);//slowly rotate the agent towards the target rotation over time

            // Once the duration is reached, finish the action
            if (timer >= turnDuration)
            {
                // Snap to the exact final rotation just to be perfectly precise
                agent.transform.rotation = targetRotation;
                EndAction(true);
            }
            }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}