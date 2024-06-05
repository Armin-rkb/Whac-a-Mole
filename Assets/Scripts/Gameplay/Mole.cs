using System.Collections;
using UnityEngine;

public class Mole : MonoBehaviour, IMole
{
    [SerializeField] private int score;
    [SerializeField] private float appearingTime = 1f;
    [SerializeField] private Collider2D hitBox;
    [SerializeField] private Animator animator;

    private Hole hole;
    private IEnumerator waitBeforeHidingCoroutine;
    private string curAnimationState;
    private bool isAppearing = false;

    private readonly string ANIMATION_STATE_HIT= "Mole_Hit";
    private readonly string ANIMATION_STATE_HIDE = "Mole_Hide";
    private readonly string ANIMATION_STATE_POPUP = "Mole_Popup";

    public int Score => score;

    void Update()
    {
        if (isAppearing)
            return;

        if (Input.touchCount > 0)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                    if (hit.collider != null && hit.collider == hitBox)
                    {
                        Hit();
                    }
                }
            }
        }
    }

    public void SetHole(Hole newHole)
    {
        hole = newHole;
    }

    public void Hide()
    {
        hitBox.enabled = false;

        ChangeAnimationState(ANIMATION_STATE_HIDE);

        hole.Vacate();
    }

    public void Hit()
    {
        if (waitBeforeHidingCoroutine != null)
        {
            StopCoroutine(waitBeforeHidingCoroutine);
        }
        GameScoreManager.Instance.AddScore(Score);  
        hitBox.enabled = false;
        ChangeAnimationState(ANIMATION_STATE_HIT);
        hole.Vacate();
    }

    public void PopUp()
    {
        ChangeAnimationState(ANIMATION_STATE_POPUP);

        waitBeforeHidingCoroutine = WaitBeforeHidingRoutine();
        StartCoroutine(waitBeforeHidingCoroutine);

        hitBox.enabled = true;
        isAppearing = true;
    }

    private void ChangeAnimationState(string newState)
    {
        if (curAnimationState == newState)
            return;

        animator.Play(newState);
        curAnimationState = newState;
    }

    private IEnumerator WaitBeforeHidingRoutine()
    { 
        yield return new WaitForSeconds(appearingTime);
        Hide();
    }
}
