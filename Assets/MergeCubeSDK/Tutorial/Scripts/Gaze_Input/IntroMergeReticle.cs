using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MergeCube;
public class IntroMergeReticle : MonoBehaviour {
	public static IntroMergeReticle instance;
	public Transform reticle;
	public Sprite fullScreenSprite;
	public Sprite vrScreenSprite;
	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		defaultScale = reticle.localScale;
		reticle.GetComponent<SpriteRenderer> ().sprite = (MergeCubeSDK.instance.viewMode == MergeCubeBase.ViewMode.HEADSET) ? vrScreenSprite : fullScreenSprite;
		MergeCubeSDK.instance.OnViewModeSwap += SetMode;
	}
	void OnDestroy(){
		MergeCubeSDK.instance.OnViewModeSwap -= SetMode;
	}
	public void SetMode(bool isVRMode){
		reticle.GetComponent<SpriteRenderer> ().sprite = isVRMode ? vrScreenSprite : fullScreenSprite;
	}
	public void ActiveIt(bool isActive){
		reticle.gameObject.SetActive (isActive);
	}

	Vector3 defaultScale;
	bool isHover = false;
	bool isPushed = false;
	//grow
	public void OnHoverAction()
	{
		isHover = true;
		if (!isPushed) {
			reticle.localScale = defaultScale * 1.3f;
		}
	}

	//shrink
	public void OffHoverAction()
	{
		isHover = false;
		if (!isPushed) {
			reticle.localScale = defaultScale;
		}
	}

	//pulse
	public void OnClickAction()
	{
		isPushed = true;
		reticle.transform.localScale = defaultScale * .8f;
	}

	//pulse
	public void OffClickAction()
	{
		isPushed = false;
		reticle.transform.localScale = isHover?defaultScale*1.3f:defaultScale;
	}
}
