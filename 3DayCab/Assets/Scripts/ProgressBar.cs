using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

	public Slider slider;

	private void Start()
	{
		slider = GetComponent<Slider>();
		slider.maxValue = GameManager.managerInstance.stepsCountMax;
		
	}

	private void Update()
	{
		slider.value = GameManager.managerInstance.stepsCount;
	}
}
