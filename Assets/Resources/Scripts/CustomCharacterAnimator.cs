using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCharacterAnimator : MonoBehaviour
{
	[SerializeField] private Animator animatorComp;
	private Animation playingAnimation;
	public enum Animation : byte
	{
		Idle,
		Walking,
		Running,
		Attack
	}

	public void SetAnimation(Animation animation)
	{
		if (animation == playingAnimation)
		{
			DebugLogging.SendWarning(gameObject: gameObject, $"{animation} already playing");
			return;
		}

		switch (animation)
		{ 
			case Animation.Idle:
				playingAnimation = Animation.Idle;
				animatorComp.Play("Idle");
				break;
			case Animation.Walking:
				playingAnimation = Animation.Walking;
				animatorComp.Play("Walking");
				break;
			case Animation.Running:
				playingAnimation = Animation.Running;
				animatorComp.Play("Running");
				break;
			case Animation.Attack:

				break;
		}
	}

	private void StateAnimationMachine()
	{
	}

	private void Update()
	{
		
	}
}
