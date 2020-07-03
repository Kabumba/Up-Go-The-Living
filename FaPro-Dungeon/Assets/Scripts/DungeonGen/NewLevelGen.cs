using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevelGen : MonoBehaviour
{
    public RoomController rcPrefab;

    private GameObject player;

    public GameObject victoryScreen;

    public void OnTriggerEnter2D(Collider2D other)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (other.gameObject.tag == "Player")
        {
            if (GameController.floorNumber < 5)
            {
                CoroutineManager.Instance.StartCoroutine(DelayRoutine());
                GameObject.FindGameObjectWithTag("Player").SetActive(false);
                GameObject.FindGameObjectWithTag("BlackScreen").GetComponent<SpriteRenderer>().enabled = true;
                player.transform.position = new Vector2(0, 0);
                Destroy(GameObject.Find("RoomController"));
                RoomController rc = Instantiate(rcPrefab, transform.position, transform.rotation);
                rc.name = "RoomController";
                DungeonGenerator dg = rc.GetComponent<DungeonGenerator>();
                dg.NewFloor();
                GameController.floorNumber++;
            }
            else
            {
                player.SetActive(false);
                FindObjectOfType<VictoryScreen>().victoryScreenUI.SetActive(true);
            }
        }
    }

    IEnumerator DelayRoutine()
    {
        yield return new WaitForSeconds(1);
        GameObject.FindGameObjectWithTag("BlackScreen").GetComponent<SpriteRenderer>().enabled = false;
        CoroutineManager.Instance.StartCoroutine(DelayRoutine2());
    }

    IEnumerator DelayRoutine2()
    {
        yield return new WaitForSeconds(0.1f);
        player.SetActive(true);
    }


}
