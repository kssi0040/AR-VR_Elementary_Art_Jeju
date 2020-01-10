using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	public enum STEP {

		NONE = -1,

		PLAY = 0,		// 게임 실행 중.
		CLEAR,			// 클리어. 

		NUM,
	};

	public STEP	step      = STEP.PLAY;
	public STEP	next_step = STEP.NONE;

	private float		step_timer = 0.0f;

	// -------------------------------------------------------- //

	public GameObject		pazzlePrefab = null;

	public	PazzleControl	pazzle_control = null;

	public GameObject		retry_button = null;
	public GameObject		complete_image = null;

	// -------------------------------------------------------- //

	public enum SE {

		NONE = -1,

		GRAB = 0,		// 조각을 집었을 때..
		RELEASE,		// 조각을 놓았을 때（정답이 아닐 경우）.

		ATTACH,			// 조각을 놓았을 때（정답인 경우）.

		COMPLETE,		// 퍼즐을 완성했을 때의 효과. 

		BUTTON,			// GUI 버튼.   

		NUM,
	};

	public AudioClip[]	audio_clips;

	// -------------------------------------------------------- //

	void 	Start()
	{
		this.pazzle_control = (Instantiate(this.pazzlePrefab) as GameObject).GetComponent<PazzleControl>();
	}

	void 	Update()
	{
	
		// ---------------------------------------------------------------- //

		this.step_timer += Time.deltaTime;

		// ---------------------------------------------------------------- //
		// 상태 변화 점검.

		switch(this.step) {

			case STEP.PLAY:
			{
				if(this.pazzle_control.isCleared()) {

					this.next_step = STEP.CLEAR;
				}
			}
			break;

			case STEP.CLEAR:
			{
                /*
				if(this.step_timer >this.audio_clips[(int)SE.COMPLETE].length + 0.5f) {         // 클리어 조건을 달성한 if문?

					if(Input.GetMouseButtonDown(0)) {       // 마우스 왼쪽 버튼을 눌렀을 때

                            //UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");         // 퍼즐 조각 완성 시 마우스 왼쪽 클릭으로 TitleScene로감
                            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene0");           // 퍼즐 조각 완성 시 마우스 왼쪽 클릭으로 초기화면으로 감
					}
				}
                 * */
			}
			break;
		}


		// ---------------------------------------------------------------- //
		// 변화 시 초기화.

		if(this.next_step != STEP.NONE) {

			switch(this.next_step) {

				case STEP.CLEAR:
				{
                    // SetActive(bool값) : 객체를 활성화 또는 비활성화 시킴
					this.retry_button.SetActive(false);
					this.complete_image.SetActive(true);    // 조각 완성 시 CompleteImage 오브젝트를 불러와서 실행
				}
				break;
			}

			this.step      = this.next_step;
			this.next_step = STEP.NONE;

			this.step_timer = 0.0f;
		}

		// ---------------------------------------------------------------- //
		// 실행 처리.

		switch(this.step) {

			case STEP.PLAY:
			{
			}
			break;
		}
	}

	public void	playSe(SE se)
	{
		this.GetComponent<AudioSource>().PlayOneShot(this.audio_clips[(int)se]);
	}

	// 다시하기 버튼을 눌렀을 때의 처리.
	public void	OnRetryButtonPush()
	{
        Debug.Log("this.pazzle_control.isCleared(): " + this.pazzle_control.isCleared());


        //if (!this.pazzle_control.isCleared()) {

			this.playSe(GameControl.SE.BUTTON);

			this.pazzle_control.beginRetryAction();
		//}
	}
}
