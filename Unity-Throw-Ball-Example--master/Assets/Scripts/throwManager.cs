using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class throwManager : MonoBehaviour {

	private Vector3 startPos; //mouse slide movement start pos
	private Vector3 endPos; //mouse slide movement end pos
	public float zDistance=30.0f;//z distance
	private bool isThrown;
	private float deltaTime;
	private float startTime;
	private float endTime;
	public GameObject ball_instance;
	private GameObject ball;
	public List<GameObject> oldBalls;
	private Vector3 throwDir;
	private GameObject MainCamera;
	private PLayerMovement pLayerMovement;
	public Serializing serializing;
	public UIController uIController;
	public Vector3 offset;
	public bool isStart = true;
	private void Awake()
    {
		print(Random.rotation);
		
	}
    void Start(){
		
	}
	public void Initialized()
    {
		uIController.UpdateLevelUI();
		uIController.UpdateTargetScoreUI();
		NewBall();
		isStart = false;
		isThrown = false;
		MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		pLayerMovement = GetComponent<PLayerMovement>();
		pLayerMovement.superCast.AddListener(SuperCast);
	}

	void Update () {

		if (Input.GetKeyDown(KeyCode.R)) {
			//SceneManager.LoadScene (0); // Reset scene on pressing R
		}

        if (!isStart){
			StartCoroutine(check(6));
        }
		
		/*while (!ball.GetComponent<Ball>().isCrached && throwDir != null)
        {
			var t = Time.deltaTime;
			ball.transform.Rotate(throwDir.x * t, throwDir.y * t, throwDir.z * t);
		}*/
		/*if (isThrown)
		{
			isThrown = false;
			StartCoroutine(check());
		}*/


		Debug.Log(1);
	}

	IEnumerator ResetBall()
    {
		
		oldBalls.Add(ball);
		ball.transform.SetParent(null);
		ball = null;
		yield return new WaitForSeconds(1);
		NewBall();
		yield return new WaitForSeconds(5);
		GameObject.Destroy(oldBalls[0]);
		oldBalls.Remove(oldBalls[0]);
		Debug.Log(3);
	}
	

	IEnumerator check(float strenght)
    {
		Debug.Log(0);
		if (Input.GetMouseButtonDown(0))
		{
			//get start mouse position
			
			Vector3 mousePos = Input.mousePosition * -1.0f;
			mousePos.z = zDistance; //add z distance

			startPos = Camera.main.ScreenToWorldPoint(mousePos);

			startTime = Time.time;
			//Print start Pos for debugging
		}

		if (Input.GetMouseButtonUp(0))
		{
			//get release mouse position
			Vector3 mousePos = Input.mousePosition * -1.0f;
			mousePos.z = zDistance; //add z distance
			ball.GetComponent<Rigidbody>().isKinematic = false;
			// convert mouse position to world position
			endPos = Camera.main.ScreenToWorldPoint(mousePos);
			endPos.z = Camera.main.nearClipPlane; //removing this breaks stuff,no idea why though

			endTime = Time.time;
	
			deltaTime = endTime - startTime;
			Debug.Log($"{endTime} - {startTime} = {deltaTime}");
			//Print start Pos for debugging
			//Debug.Log(endPos);

			throwDir = (startPos - endPos).normalized;//get throw direction based on start and end pos
			ball.GetComponent<Ball>().model.transform.rotation = new Quaternion(0, 0, 0,0);
			if (deltaTime > 0.1f)
            {
				ball.GetComponent<Rigidbody>().AddForce((throwDir * (startPos - endPos).sqrMagnitude / (deltaTime * strenght)));//add force to throw direction*magnitude
				isThrown = true;
				Debug.Log(2);
				//StartNew();

				while (!ball.GetComponent<Ball>().isCrached)
				{
					var t = Time.deltaTime;
					Debug.Log("TRY");
					Quaternion q1 = Quaternion.AngleAxis(10f * Time.deltaTime, throwDir);
					/*Quaternion q2 = Quaternion.AngleAxis(20f * Time.deltaTime, Vector3.forward);
					Quaternion q3 = Quaternion.AngleAxis(30f * Time.deltaTime, Vector3.up);
					Quaternion q = q1 * q2 * q3;*/
					//ball.transform.rotation = q1;
					/*Vector3 normthrowDir = throwDir.normalized;
					float halfAngel = 
					ball.transform.rotation = new Quaternion(throwDir.normalized);*/
					ball.GetComponent<Ball>().model.transform.Rotate(throwDir* (startPos - endPos).sqrMagnitude * Time.deltaTime);

					yield return null;
				}
				//StartCoroutine(RotateBall(throwDir));
				StartCoroutine(ResetBall());
				yield return null;
			}
            else
            {
				ball.GetComponent<Rigidbody>().AddForce(throwDir* (startPos - endPos).sqrMagnitude /100 );
				StartCoroutine(ResetBall());
			}
			
			
			
			
		}

	}

	void StartNew()
    {
		
	}
	void NewBall()
    {
		ball = Instantiate(ball_instance, transform.position, Random.rotation) as GameObject;
		ball.transform.SetParent(transform);
		/*isThrown = false;*/
		ball.GetComponent<Rigidbody>().isKinematic = true;
	}

	IEnumerator SuperCastCamera(float timeSup)
    {
		Camera.main.transform.SetParent(ball.transform);
		Camera.main.transform.position += offset;
		Camera.main.transform.LookAt(ball.transform.position);
		yield return new WaitForSeconds(4);
		if(serializing.playerData.score >= serializing.worldData.target)
        {
			uIController.NextLevel();

		}
        else
        {
			uIController.RestartLevel();
		}

		/*while(timeSup >= 0)
        {
			*//*if (isThrown) {
				Camera.main.transform.LookAt(ball.transform.position);
				Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, ball.transform.position + offset, Time.deltaTime);
				timeSup -= Time.deltaTime;
				Debug.Log(timeSup);
				yield return null;
			}*//*
			
		}*/

	}

	void SuperCast()
	{
		//
		StopAllCoroutines();
		StartCoroutine(SuperCastCamera(5));
		StartCoroutine(check(1f));

	}
}
