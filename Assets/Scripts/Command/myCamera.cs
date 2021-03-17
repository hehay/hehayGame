using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class myCamera:MonoBehaviour
    {
        private Transform player;
        private Vector3 dir;
        private float dis;
        public float maxDis;
        public float minDis;
        // Use this for initialization
        void Start()
        {
            dis = maxDis;
        }

    public void SetPlay(GameObject play)
    {
        player = GameData.play.transform;
        dir = (transform.position - player.position).normalized;

    }
    private void Update()
    {
        if(player==null) return;
        Vector3 pos = player.transform.position + Vector3.up*1f;
        RaycastHit[] hits;
        hits = Physics.RaycastAll(pos, (transform.position - pos).normalized, maxDis);
        if (hits.Length > 0)
        {
            RaycastHit startHit = hits[0];
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.CompareTag(TAGS.Ground))
                {
                    if (hits[i].distance < startHit.distance)
                    {
                        startHit = hits[i];
                    }
                }
            }
            if (startHit.collider.CompareTag(TAGS.Ground))
                dis = Vector3.Distance(startHit.point, pos);

            if (dis > maxDis)
            {
                dis = maxDis;
            }
            else if (dis < minDis)
            {
                dis = minDis;
            }
        }
        else
        {
            if (dis != maxDis) dis = maxDis;
        }
        Vector3 positon = (dir*dis) + pos;
        transform.position = Vector3.Lerp(transform.position, positon, 10*Time.deltaTime);
    }

    }

