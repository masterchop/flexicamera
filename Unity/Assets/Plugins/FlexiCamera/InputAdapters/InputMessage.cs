using UnityEngine;
using System.Collections.Generic;

namespace FlexiCamera.InputAdapters
{
	public class InputMessage
	{
		#region Enums
		
		public enum InputTypes
		{
			// Discrete gestures
			OneFingerTap,
			OneFingerLongTap,
			OneFingerDrag,
			TwoFingerDrag,
			TwoFingerTwist,
			TwoFingerPinch,
			
			// Continuous gestures
			FingerDown,
			FingerUp,
			FingerStationary,
			FingerMoving
		}
		
		public enum MessageTypes
		{
			Transient,
			Begin,
			Update,
			End,
			Failed
		}
		
		#endregion
		
		#region Public Properties
		
		/// <summary>
		/// Gets the input type of this message
		/// </summary>
		public InputTypes InputType {
			get;
			protected set;
		}
		
		/// <summary>
		/// Gets the message type of this message
		/// </summary>
		public MessageTypes MessageType {
			get;
			protected set;
		}
		
		/// <summary>
		/// Gets the positions of each finger utilised in gesture that triggered this message 
		/// </summary>
		public List<Vector2> FingerPositions {
			get;
			protected set;
		}
			
		/// <summary>
		/// Gets the change in position of each finger utilised in gesture that triggered this message 
		/// </summary>
		public List<Vector2> FingerDeltas {
			get;
			protected set;
		}
		
		/// <summary>
		/// Gets the normalised positions of each finger utilised in gesture that triggered this message 
		/// </summary>
		public List<Vector2> NormalisedFingerPositions {
			get;
			protected set;
		}
			
		/// <summary>
		/// Gets the normalised change in position of each finger utilised in gesture that triggered this message 
		/// </summary>
		public List<Vector2> NormalisedFingerDeltas {
			get;
			protected set;
		}
		
		/// <summary>
		/// Gets data associated with the gesture such as twist or pinch amounts
		/// </summary>
		public List<float> 	 GestureData {
			get;
			protected set;
		}
		
		/// <summary>
		/// Gets whether the message should propogate to the next controller in the list
		/// </summary>
		public bool Propogate {
			get;
			protected set;
		
		}
		
		/// <summary>
		/// Gets the amount of times that controller was activated by this message
		/// </summary>
		public int UseCount {
			get; 
			protected set;
		}
		
		#endregion
		
		#region Constuctors
		
		public InputMessage(
			InputTypes inputType, MessageTypes messageType, 
			List<Vector2> fingerPositions, List<Vector2> fingerDeltas = null, 
			List<float> gestureData = null
		)
		{
			this.InputType = inputType;
			this.MessageType = messageType;
			this.FingerPositions = fingerPositions;
			this.FingerDeltas = fingerDeltas;
			this.GestureData = gestureData;
			this.Propogate = true;
			GenerateNormalisedLists();
		
		}
		
		public InputMessage(
			InputTypes inputType, MessageTypes messageType, 
			Vector2 fingerPosition, Vector2 fingerDelta = default(Vector2), 
			float gestureData = default(float)
		)
		{
			this.InputType = inputType;
			this.MessageType = messageType;
			this.FingerPositions = new List<Vector2>() { fingerPosition };
			this.FingerDeltas = new List<Vector2>() { fingerDelta };
			this.GestureData = new List<float>() { gestureData };
			this.Propogate = true;
			GenerateNormalisedLists();
		}
		
		#endregion
		
		#region Protected Methods
		
		protected void GenerateNormalisedLists()
		{
			NormalisedFingerDeltas = new List<Vector2>();	
			NormalisedFingerPositions = new List<Vector2>();	
			
			foreach (Vector2 p in this.FingerDeltas)
			{
				NormalisedFingerDeltas.Add(new Vector2(p.x / Screen.width, p.y / Screen.width));
			}
			
			foreach (Vector2 p in this.FingerPositions)
			{
				NormalisedFingerPositions.Add(new Vector2(p.x / Screen.width, p.y / Screen.width));
			}
		}
		
		#endregion
		
		#region Public Methods
		
		public void StopPropagation()
		{
			this.Propogate = false;
		}
		
		public void Use()
		{
		
		
		}
		
		#endregion
		
		
	}
}

