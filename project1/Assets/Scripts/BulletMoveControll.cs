using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletMoveControll : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    private Vector2 _direction;
    private GameObject LocateEnemy()
    {
        var results = new Collider2D[5];//���ڴ���Collider2D(��һ��Բ�η�Χ��Colliderȥ�����Χ����)������
        //transform.positionΪ�ӵ�λ�ã�5ΪԲ�ΰ뾶��resultsΪ�������
        Physics2D.OverlapCircleNonAlloc(transform.position,10, results);
        foreach (var result  in results)
        {
            if (result.CompareTag("Enemy")&&result!=null)
            {
                return result.gameObject;

            }
            
        }
        return null;
    }

    private Vector2 MoveDirection(Transform tar)
    {
        var direction = new Vector2(1, 0);

        if (tar != null)
        {

            direction=tar.position - transform.position;
            direction.Normalize();
        }


        return direction;

    }

    private void Awake()
    {
        var enemy = LocateEnemy();
        if (enemy != null) 
        {
            _direction = MoveDirection(enemy.transform);
        }
        else {
            _direction = MoveDirection(null);
        }

      
        transform.rotation=Quaternion.LookRotation(Vector3.forward,_direction);
    }


    private void FixedUpdate()
    {
        var targetPos = (Vector2)transform.position + _direction;
        rb.DOMove(targetPos, speed).SetSpeedBased();
    }
}
