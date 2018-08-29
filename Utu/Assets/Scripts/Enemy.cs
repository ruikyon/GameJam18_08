using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy instance;
    private int counter = 0, waveCounter = 0;
    private bool show = false, wait = true;
    [SerializeField]
    private int interval, waitTime;
    private float distance = 100; // 飛ばす&表示するRayの長さ
    private float duration = 3;   // 表示期間（秒）
    [SerializeField]
    private LineRenderer[] rays;
    private Vector3 origin;
    private int shotCount;
    [SerializeField]
    private float maxHeight, minHeight, maxWide, minWide, depth;
    private int point;

    // Use this for initialization
    void Start()
    {
        instance = this;
        origin = transform.position;
        counter = waitTime;

        for (int i = 0; i < rays.Length; i++)
        {
            rays[i].SetPosition(0, transform.position);
            rays[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waveCounter < 15)
        {
            counter--;
            if (counter == 0)
            {
                if (show)
                {
                    Judge();
                    wait = true;
                    show = false;
                    counter = waitTime;
                }
                else if (wait)
                {
                    LineShow();
                    wait = false;
                    show = true;
                    counter = interval;
                    waveCounter++;
                }
            }
        }
    }

    private void LineShow()//Debug.DrawRayでレイを表示
    {
        Debug.Log("wave: " + waveCounter);
        shotCount = waveCounter / 5 + 1;
        for (int i = 0; i < shotCount; i++)
        {
            var height = Random.Range(minHeight, maxHeight);
            var wide = Random.Range(minWide, maxWide);
            rays[i].gameObject.SetActive(true);
            rays[i].SetPosition(1, new Vector3(wide, height, depth));
        }
    }

    private void Judge() //レイを飛ばして判定
    {
        shotCount = waveCounter / 5 + 1;
        var flag = false;
        point = 0;
        for (int i = 0; i < shotCount; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(origin, rays[i].GetPosition(1) - origin, out hit, 20))
            {
                Debug.Log("hit");

                switch (hit.collider.tag)
                {
                    case "head":
                        point += 1000;
                        flag = true;
                        Hud.instance.HeadShot();
                        break;
                    case "body":
                        point += 500;
                        flag = true;
                        break;
                    case "leg":
                        point += 200;
                        flag = true;
                        break;
                    default:
                        point += 0;
                        break;
                }
            }
            rays[i].gameObject.SetActive(false);
        }
        if (flag)
        {
            Player.instance.Shoot(point);
        }
        GameController.instance.AddScore(point);
    }

    public int GetWave()
    {
        return waveCounter;
    }
}
