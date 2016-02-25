using UnityEngine;using System.Collections;public class BallManager : MonoBehaviour {    public int currentBallCount;    private int floorBallCount;    private int flyingBallCount;    private float ballStartPositionX;
    private float ballStartPositionY;
    private Ball ballPrefab;    private float ballFireIntervalSecond;    private Ball startBall;    private Ball nextBall;
    void Start () {        floorBallCount = currentBallCount;        flyingBallCount = 0;        ballStartPositionX = 0f;        ballStartPositionY = -2.8f;        ballFireIntervalSecond = 0.1f;        ballPrefab = Resources.Load("Prefab/Ball", typeof(Ball)) as Ball;        startBall = Instantiate(ballPrefab, new Vector3(ballStartPositionX, ballStartPositionY), Quaternion.identity) as Ball;	}		void Update () {        if (floorBallCount != currentBallCount)        {            return;        }	    if (Input.GetMouseButton(0))        {            Vector3 toPosition = Input.mousePosition;            //Debug.Log("X : " + toPosition.x + " Y : " + toPosition.y);        }        if (Input.GetMouseButtonUp(0))        {            Vector3 toPosition = Input.mousePosition;            toPosition.z = 1;            toPosition = Camera.main.ScreenToWorldPoint(toPosition);            toPosition.z = 0;            Debug.Log("Mouse Released");            StartCoroutine(FireBalls(toPosition));                    }	}    IEnumerator FireBalls(Vector3 t)
    {
        Debug.Log("S : " + startBall.transform.position);
        Debug.Log("Fire Ball to " + t);
        //Instantiate(ballPrefab, pos, Quaternion.identity);
                
        float degree = Vector3.Angle(startBall.transform.position, t);
        Debug.Log("Degree " + degree);

        Vector3 s = startBall.transform.position;
        Vector3 v = (t - s).normalized * 5;
            //Vector3.Lerp(startBall.transform.position, pos, 1f).normalized * 3;

        Debug.Log("V : " + v);

        for (int i = 0; i < currentBallCount; i++)
        {
            Ball b = Instantiate(startBall) as Ball;
            Rigidbody2D rb =  b.GetComponent<Rigidbody2D>();

            rb.velocity = v;

            //floorBallCount--;
            //flyingBallCount++;

            yield return new WaitForSeconds(ballFireIntervalSecond);
        }

        //startBall = null;
    }}