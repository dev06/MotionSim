using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MergeCube;

[RequireComponent( typeof( AudioSource ) )]
public class SplashScreenManager : MonoBehaviour
{
	public static SplashScreenManager instance;

	void Awake()
	{
		if ( instance == null )
			instance = this;
		else if ( instance != this )
			DestroyImmediate( this.gameObject );
	}

	public bool skipSplashScreen = false;
	public Callback OnSplashSequenceEnd;
	public Callback OnTitleMusicStartPoint;

	public CanvasGroup gameSplash;
	public float splashFadeoutInSec = .3f;
	[Range( 0f, 10f )]
	public float logoDuration = 3f;
	public Animator darkFader;

	public AudioClip logoSound;
	public GameObject loadingAnimaRef;
	bool isBlocked = true;
	bool isCubePageShow = false;

	public void StartSplashSequence()
	{
		if ( gameSplash != null )
		{
			gameSplash.alpha = 0f;
		}
		darkFader.Play( "FadeStayUp" );

		if ( skipSplashScreen )
		{
			EndSplashSequence();
		}
		else
		{
			StartCoroutine( BeginSplashSequencer() );
		}
	}

	IEnumerator BeginSplashSequencer()
	{
		darkFader.Play( "FadeIn" );

		if ( gameSplash != null )
		{
			gameSplash.alpha = 1;

			//Fade from black to user defined logo
			darkFader.Play( "FadeOut" );
			yield return new WaitForSeconds( 0.5f );

			//Get end user's audio selection if not null
			if ( logoSound != null )
			{
				GetComponent<AudioSource>().PlayOneShot( logoSound );
			}

			if ( OnTitleMusicStartPoint != null )
			{
				OnTitleMusicStartPoint.Invoke();
			}

			yield return new WaitForSeconds( logoDuration );

			bool skipCubePage = false;
			#if UNITY_EDITOR 
			skipCubePage = IntroSequencer.instance.skipAllPopInEditor;
			#endif 
			if ( !skipCubePage )
			{
				StartCubeCode();
				if ( loadingAnimaRef != null )
				{
					Destroy( loadingAnimaRef );
				}

				yield return new WaitUntil( () => !isBlocked );
			}

			EndSplashSequence();

			while ( gameSplash.alpha > 0 )
			{
				gameSplash.alpha -= Time.deltaTime * ( 1f / splashFadeoutInSec );
				yield return null;
			}
			if ( gameSplash != null )
				gameSplash.gameObject.SetActive( false );
			
		}
	}

	void EndSplashSequence()
	{
		if ( OnSplashSequenceEnd != null )
		{
			OnSplashSequenceEnd.Invoke();
		}
//		darkFader.Play("FadeOut");
	}

	public NoticePageManager Page_MergeCubeRequired;

	void StartCubeCode()
	{
//		MergeUserAccount.instance.autoSignInDoneCall += UpdateCubePageOpen;
//		Invoke ("Init", .1f);
		Init();
	}

	void Init()
	{
//		Debug.LogWarning( "Init Splash" );

		if ( PlayerPrefs.GetString( "CubePagePop" ) == "" )
		{
			isCubePageShow = true;
			//			Debug.LogWarning ("Should Open Cube");
			Page_MergeCubeRequired.gameObject.SetActive( true );
			Page_MergeCubeRequired.doneButton += CubePopDone;
			PlayerPrefs.SetString( "CubePagePop", "done" );
		}
		else
		{
			isBlocked = false;
			//			Debug.LogWarning ("ShouldOpenCodePage = "+false);
			Destroy( Page_MergeCubeRequired.gameObject );
		}

//		if (MergeUserAccount.instance.ShouldOpenCodePage)
//		{
//			isCubePageShow = true;
//			Debug.LogWarning("Should Open Cube");
//			Page_MergeCubeRequired.gameObject.SetActive(true);
//			Page_MergeCubeRequired.doneButton += CubeButtonClick;
//			MergeUserAccount.instance.enterCodeClose += CubePopDone;
//		}
//		else
//		{
//			isBlocked = false;
//			Debug.LogWarning("ShouldOpenCodePage = " + false);
//			DestroyPage_MergeCube();
//		}
	}

	bool isWebPageOpen = false;

	void UpdateCubePageOpen()
	{
//		MergeUserAccount.instance.autoSignInDoneCall -= UpdateCubePageOpen;
		if ( isCubePageShow )
		{
			CancelInvoke();
//			if ( !MergeUserAccount.instance.ShouldOpenCodePage )
//			{
//				Debug.LogWarning( "Auto Sign In, Close now." );
//				if ( isWebPageOpen )
//				{
//					MergeUserAccount.instance.CloseTheBrowser();
//					CubePopDone();
//				}
//				else
//				{
//					Page_MergeCubeRequired.doneButton -= CubeButtonClick;
//					Page_MergeCubeRequired.doneButton += CubeButtonClickSkipPop;
//					MergeUserAccount.instance.enterCodeClose -= CubePopDone;
//					MergeUserAccount.instance.autoSignInDoneCall -= UpdateCubePageOpen;
//				}
//			}
		}
	}

	void CubeButtonClick()
	{
		isWebPageOpen = true;
//		Debug.LogWarning( "CubeButtonClick" );
//		MergeUserAccount.instance.PopEnterCode();
//		MergeUserAccount.instance.autoSignInDoneCall -= UpdateCubePageOpen;
	}

	void CubeButtonClickSkipPop()
	{
//		Debug.LogWarning( "CubeButtonClick Skip" );
		isBlocked = false;
		DestroyPage_MergeCube();
	}

	void CubePopDone()
	{
		isCubePageShow = false;
		if ( isBlocked )
		{
//			Debug.LogWarning( "CubePopDone" );
			isBlocked = false;
			Page_MergeCubeRequired.doneButton -= CubeButtonClick;
//			MergeUserAccount.instance.enterCodeClose -= CubePopDone;
//			MergeUserAccount.instance.autoSignInDoneCall -= UpdateCubePageOpen;
			DestroyPage_MergeCube();
		}
	}

	void DestroyPage_MergeCube()
	{
		Destroy( Page_MergeCubeRequired.transform.parent.gameObject );
	}

	public void OpenMergeCubeUrl()
	{
		Application.OpenURL( @"https://mergecube.com/needamergecube" );
	}

	void Update()
	{
		if ( Input.GetKeyDown( KeyCode.R ) )
		{
			CubePopDone();
		}
	}
}
