using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class JigsawPuzzle : MonoBehaviour
{
    public Texture2D sourceImage;
    public int rows ;
    public int columns ;
    private List<Image> puzzlePieces = new List<Image>();

    void Start()
    {
        SliceImage();
        ShufflePuzzlePieces();
        RepositionPuzzlePieces();
    }

    void SliceImage()
    {
        int pieceWidth = sourceImage.width / columns;
        int pieceHeight = sourceImage.height / rows;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
              
                Texture2D pieceTexture = new Texture2D(pieceWidth, pieceHeight);
                pieceTexture.SetPixels(sourceImage.GetPixels(x * pieceWidth, (rows - 1 - y) * pieceHeight, pieceWidth, pieceHeight));
                pieceTexture.Apply();

             
                GameObject puzzlePiece = new GameObject("PotonganGambar_" + x + "_" + y);
                puzzlePiece.transform.SetParent(transform);

               
                Image image = puzzlePiece.AddComponent<Image>();
                image.sprite = Sprite.Create(pieceTexture, new Rect(0, 0, pieceWidth, pieceHeight), new Vector2(2f,2f));
            
                RectTransform rectTransform = puzzlePiece.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(pieceWidth, pieceHeight);



                BoxCollider2D collider = puzzlePiece.AddComponent<BoxCollider2D>();
                collider.size = new Vector2(pieceWidth -25, pieceHeight -28);
              
                puzzlePieces.Add(image);
            }
        }
    }

   void ShufflePuzzlePieces()
{
    for (int i = 0; i < puzzlePieces.Count; i++)
    {
        Image temp = puzzlePieces[i];
        int randomIndex = Random.Range(i, puzzlePieces.Count);
        puzzlePieces[i] = puzzlePieces[randomIndex];
        puzzlePieces[randomIndex] = temp;
    }
}

void RepositionPuzzlePieces()
{
    
    List<Vector3> positions = new List<Vector3>();
    for (int i = 0; i < puzzlePieces.Count; i++)
    {
        float posX = (i % columns) * puzzlePieces[i].rectTransform.rect.width;
        float posY = (i / columns) * puzzlePieces[i].rectTransform.rect.height;
        positions.Add(new Vector3(posX, posY, 0f));
    }

  
    for (int i = 0; i < puzzlePieces.Count; i++)
    {
        puzzlePieces[i].rectTransform.localPosition = positions[i];
    }
}


}
