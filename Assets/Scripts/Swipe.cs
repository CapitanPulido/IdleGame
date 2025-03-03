using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Swipe : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private GameObject CanvaMenu;
    private Animator animation;
    private void Start()
    {
        animation = GetComponent<Animator>();
    }
    private void Update()
    {
        if(Input.touchCount>0&&Input.GetTouch(0).phase==TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            if (endTouchPosition.x < startTouchPosition.x)
            {
                animation.Play("New Animation");
            }

            if (endTouchPosition.x > startTouchPosition.x)
            {
                CanvaMenu.SetActive(false);
            }
        }
    }
}
