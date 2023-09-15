using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // 다운받은 에셋 DOTweening 사용

public class Card : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer cardRenderer;

    [SerializeField]
    private Sprite animalSprite; // 카드 앞면, 동물 이미지

    [SerializeField]
    private Sprite backSprite; // 카드 뒷면

    private bool isFlipped = false; // 카드가 뒤집힌 상태인지 저장하는 변수
    private bool isFlipping = false; // 카드가 뒤집히고 있는 상태인지 저장하는 변수

    void Start() {
        
    }


    void Update() {
        
    }

    public void FlipCard() {
        // 이 변수가 필요한 이유는 만약 뒤집히고 있는 중 마우스를 계속 누르게 되면 localScale의 값 중 x의 값이 1이 아닌 클릭했을 당시의 크기로 저장된다.
        // 따라서 카드의 크기가 계속해서 줄어드는 오류가 발생한다.
        isFlipping = true; // 카드가 뒤집히고 있는 중이다.

        // 카드의 뒤집기 모션을 자연스럽게 만들기 위해 Scale값을 0으로 줄였다가 다시 늘리는 작업을 수행
        Vector3 originalScale = transform.localScale; // 현재 처음 저장되어 있는 카드의 크기(Scale)를 저장하는 변수
        Vector3 targetScale = new Vector3(0f, originalScale.y, originalScale.z); // 카드의 크기(Scale) 중 x값만 0으로 변경하는 변수

        transform.DOScale(targetScale, 0.2f).OnComplete(() => { // 현재 Scale을 0.2초 동안 targetScale의 크기로 만든다. 만약 그 과정이 Complete 됐을 경우
            isFlipped = !isFlipped; // 처음엔 뒷면(false)에서 !isFlipped가 되어 true가 저장된다.

            if (isFlipped) {// 만약 isFlipped가 true(뒷면)라면
                cardRenderer.sprite = animalSprite; // 카드 앞면(동물 그림)으로 변경
            }
            else {// 만약 isFlipped가 false(앞면)라면
                cardRenderer.sprite = backSprite; // 카드를 뒷면으로 변경
            }

            transform.DOScale(originalScale, 0.2f).OnComplete(() => { // 현재 Scale을 0.2초 동안 originalScale의 크기로 만든다. 만약 그 과정이 Complete 됐을 경우
                isFlipping = false; // 카드가 뒤집히고 있는 상태가 아닌것으로 변경
            });
        });
    }

    void OnMouseDown() {
        if(isFlipping == false) { // 만약 카드가 뒤집히고 있는 상태가 아니라면
            FlipCard(); // 카드를 뒤집는다.
        }
    }
}
