using System;
using StereoKit;


namespace StereoKitUtilities
{
	public class Grabbable
	{
		public Model _model;
		public Color _color;
		public Vec3 _position;
		bool _grabbing;

		Bounds _bounds;
		Vec3 _offset;

		Action _onGrabStart;
		Action _grabOngoing;
		Action _onGrabEnd;

		/// <summary>
		/// Creates a Grabbable object refrencing a given Model and optional callback function onGrabStart, garbOngoing, onGrabEnd
		/// </summary>
		/// <param name="model">The Model to make grabbable</param>
		/// <param name="onGrabStart">The onGrabStart callback function</param>
		/// <param name="grabOngoing">The grabOngoing callback function</param>
		/// <param name="onGrabEnd">The onGrabEnd callback function</param>
		public Grabbable(ref Model model, Action onGrabStart = null, Action grabOngoing = null, Action onGrabEnd = null)
		{
			_model = model;
			_bounds = model.Bounds;
			_color = Color.White;
			_position = Vec3.Zero;
			_offset = Vec3.Zero;
			_grabbing = false;
			_onGrabStart = onGrabStart;
			_grabOngoing = grabOngoing;
			_onGrabEnd = onGrabEnd;
		}

		/// <summary>
		/// Sets a callback function for onGrabStart
		/// </summary>
		/// <param name="onGrabStart">The callback function</param>
		public void SetOnGrabStart(Action onGrabStart)
		{
			_onGrabStart = onGrabStart;
		}

		/// <summary>
		/// Sets a callback function for grabOngoing
		/// </summary>
		/// <param name="grabOngoing">The callback function</param>
		public void SetGrabOngoing(Action grabOngoing)
		{
			_grabOngoing = grabOngoing;
		}

		/// <summary>
		/// Sets a callback function for onGrabEnd
		/// </summary>
		/// <param name="onGrabEnd">The callback function</param>
		public void SetOnGrabEnd(Action onGrabEnd)
		{
			_onGrabEnd = onGrabEnd;
		}

		/// <summary>
		/// Call every frame to show the updated Model
		/// </summary>
		public void Update()
		{
			Hand hand;
			Pose fingertip;

			for (int i = 0; i < (int)Handed.Max; i++) // For each hand
			{
				hand = Input.Hand((Handed)i); // Get the current hand
				if (hand.IsTracked)
				{
					for (int j = 1; j < 5; j++) // For each finger (excluding thumb)
					{
						fingertip = hand[(FingerId)j, JointId.Tip].Pose; // Get the fingertips pose information
						if (_bounds.Contains(fingertip.position)) // If the fingertip is in the models boundary
						{
							if (hand.IsJustPinched || hand.IsJustGripped) // If the user jsut pinched or just grabbed...
							{
								_grabbing = true; // ...the user is currently grabbing
								_offset = _position - fingertip.position;
								if (_onGrabStart != null)
									_onGrabStart();
							}
							if (hand.IsJustUnpinched || hand.IsJustUngripped) // If the user stopped pinching or grabbing...
							{
								_grabbing = false; // ...the user is no longer grabbing
								if (_onGrabEnd != null)
									_onGrabEnd();
							}
							if (_grabbing == true) // If we are currently grabbing
							{
								if (_grabOngoing != null)
									_grabOngoing();
								_position = fingertip.position + _offset; // Update the objects position relative to the grabbed position
							}
							break;
						}
						else
						{
							_grabbing = false;
						}
					}
				}
			}
			_bounds.center = _position; // Update the position of the models boundary
			_model.Draw(Matrix.T(_position), _color); // Draw the model at its new position
		}
	}
}
