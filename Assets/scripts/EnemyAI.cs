using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int target;
    public GameObject exit;
    public GameObject[] wayPoints;
    public float navigation;
    private Transform _enemy;
    private float _navigationTime;

    private void Start()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("Corner");
        exit = GameObject.FindGameObjectWithTag("finish");
        _enemy = GetComponent<Transform>();
    }

    private void Update()
    {
        if (wayPoints == null) return;
        _navigationTime += Time.deltaTime;
        if (!(_navigationTime > navigation)) return;
        _enemy.position = Vector3.MoveTowards(_enemy.position, target < wayPoints.Length ? wayPoints[target].transform.position : exit.transform.position, _navigationTime);
        _navigationTime = 0;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Corner"))
        {
            target+=1;
        }
        else if(collision.CompareTag("finish"))
        {
            Destroy(this);
        }
    }
}
