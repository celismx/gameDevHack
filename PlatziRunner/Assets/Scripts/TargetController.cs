using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{

    public float coinSpeed = 300.0f;
    public float coinSpeed2 = 0.0f;

    public GameObject player_;

    private Rigidbody2D objectiveRigidBody;
    public bool pause = false;
    private bool moving = false;
    private float coinHorizontal;
    public Vector2 lastMovementCoin = Vector2.zero;

    bool forceStop = false;

    private const string movingState = "moving";

    public static bool playerCreated;

    private void Start()
    {
        objectiveRigidBody = GetComponent<Rigidbody2D>();

        lastMovementCoin = new Vector2(1, 0);
        coinSpeed = 230.0f; //Crete a method to modify this depending on the level dificulty
    }

    void Update()
    {

        moving = false;

        if (GameManager.sharedInstance.currentGameState == GameState.inGame && !forceStop)
        {
            moving = true;
            coinHorizontal = Random.Range(0.9f, 1.3f);
            coinSpeed2 = coinSpeed * coinHorizontal;
            lastMovementCoin = new Vector2(1, 0);

            objectiveRigidBody.velocity = lastMovementCoin.normalized * coinSpeed2 * Time.deltaTime;
        }


        if (!moving)
        {
            objectiveRigidBody.velocity = Vector2.zero;
        }
    }

    private void OnBecameInvisible()
    {
        forceStop = true;
    }

    private void OnBecameVisible()
    {
        StartCoroutine(BecomeVisible());
    }

    IEnumerator BecomeVisible()
    {
        Debug.Log("Start OnBecameVisible");
        // Así la moneda esperará un poco antes de continuar su camino. Este parámetro debemos configurarlo para que no se note que espera, pero la moneda lo haga igualmente y el jugador nunca la pierda.
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Visible");
        forceStop = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            // El jugador ha obtenido la moneda y ha ganado el nivel.
            GameManager.sharedInstance.Win();
            Time.timeScale = 0.0f;
            Destroy(gameObject);
        }
    }
}