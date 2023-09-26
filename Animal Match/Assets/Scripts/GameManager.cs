using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance; // 싱글 톤 방식
    private List<Card> allCards;
    private Card flippedCard; // 뒤집힌 카드 정보를 저장하기 위한 참조 변수
    private bool isFlipping = false;
    
    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    void Start() {
        Board board = FindObjectOfType<Board>(); // 보드 객체 찾아오기
        allCards = board.GetCards(); // 게임 매니저에서 20장의 카드에 저장하고 접근하기 위한 변수

        StartCoroutine("FlipAllCardsRoutine");


    }

    IEnumerator FlipAllCardsRoutine() {
        isFlipping = true;
        yield return new WaitForSeconds(0.5f);
        FlipAllCards();
        yield return new WaitForSeconds(3f);
        FlipAllCards();
        yield return new WaitForSeconds(0.5f);
        isFlipping = false;
    }
    
    void FlipAllCards() { // 모든 카드 한 번 뒤집는 메소드
        foreach (Card card in allCards) {
            card.FlipCard();
        }
    }

    public void CardCliked(Card card) {
        if(isFlipping) {
            return;
        }

        card.FlipCard();

        if(flippedCard == null) { // 아직 뒤집힌 카드가 없다면
            flippedCard = card; // 지금 뒤집힌 카드를 저장
        }
        else {
            StartCoroutine(CheckMatchRoutine(flippedCard, card));
        }
    }

    IEnumerator CheckMatchRoutine(Card card1, Card card2) {
        isFlipping = true;


        if(card1.cardID == card2.cardID) {
            card1.SetMatched();
            card2.SetMatched();
        }
        else {
            yield return new WaitForSeconds(1f);

            card1.FlipCard();
            card2.FlipCard();

            yield return new WaitForSeconds(0.4f);
        }

        isFlipping = false;
        flippedCard = null;
    }
}
