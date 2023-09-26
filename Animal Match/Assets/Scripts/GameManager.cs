using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance; // 싱글 톤 방식

    private List<Card> allCards;

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
        yield return new WaitForSeconds(0.5f);
        FlipAllCards();
        yield return new WaitForSeconds(3f);
        FlipAllCards();
        yield return new WaitForSeconds(0.5f);
    }
    
    void FlipAllCards() { // 모든 카드 한 번 뒤집는 메소드
        foreach (Card card in allCards) {
            card.FlipCard();
        }
    }
}
