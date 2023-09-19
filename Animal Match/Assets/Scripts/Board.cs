using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    [SerializeField]
    private GameObject cardPrefab;

    [SerializeField]
    private Sprite[] cardSprites; // 카드 동물 이미지를 리스트로 저장하는 변수
    

    private List<int> cardIDList = new List<int>(); // 카드 쌍을 이루도록 카드에 ID 번호를 부여하기 위한 카드ID 리스트 변수


    void Start() {
        GenerateCardID();
        InitBoard();
    }

    void GenerateCardID() {
        for(int i = 0; i < cardSprites.Length; i++) { // 카드 별 ID 부여
            cardIDList.Add(i);
            cardIDList.Add(i);
        }
    }

    void InitBoard() {
        float spaceY = 1.8f; // 세로의 간격
        float spaceX = 1.3f; // 가로의 간격

        int rowCount = 5; // 세로 줄 수
        int colCount = 4; // 가로 줄 수

        int cardIndex = 0; // cardSprites가 배열이기 때문에 그에 따른 인덱스를 가져온다.

        for(int row = 0; row < rowCount; row++) {
            for(int col = 0; col < colCount; col++) {
                float posX = (col - (colCount / 2)) * spaceX + (spaceX / 2); // 가로 간격을 계산하여 카드를 배치 하기 위한 계산식
                float posY = (row - (int)(rowCount / 2)) * spaceY; // 세로 간격을 계산하여 카드를 배치 하기 위한 계산식

                Vector3 pos = new Vector3(posX, posY, 0f); // 계산된 가로 세로 간격을 저장
                GameObject cardObject = Instantiate(cardPrefab, pos, Quaternion.identity); // 오브젝트화 시키기
                Card card = cardObject.GetComponent<Card>(); // 카드 컴포넌트 가져오기

                int cardID = cardIDList[cardIndex++]; // 저장된 값을 하나씩 가져온다. 0,0,1,1,2,2,3,3, ...

                card.SetCardID(cardID);
                card.SetAnimalSprite(cardSprites[cardID]);
            }
        }
    }
}
