using UnityEngine;
using System.Collections.Generic;

public class ImageSlicer : MonoBehaviour
{
    public Texture2D sourceImage;
    public int rows = 3;
    public int columns = 3;

    private List<GameObject> puzzlePieces = new List<GameObject>();

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
                Texture2D piece = new Texture2D(pieceWidth, pieceHeight);
                piece.SetPixels(sourceImage.GetPixels(x * pieceWidth, (rows - 1 - y) * pieceHeight, pieceWidth, pieceHeight));
                piece.Apply();

                GameObject puzzlePiece = new GameObject("PuzzlePiece_" + x + "_" + y);
                SpriteRenderer spriteRenderer = puzzlePiece.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = Sprite.Create(piece, new Rect(0, 0, piece.width, piece.height), new Vector2(0.5f, 0.5f));

            
                BoxCollider2D collider = puzzlePiece.AddComponent<BoxCollider2D>();
              
                collider.size = new Vector2(pieceWidth, pieceHeight);

                puzzlePiece.transform.localScale = new Vector3(1f / columns, 1f / rows, 1f);

                float posX = (float)x / columns - 0.5f + puzzlePiece.transform.localScale.x / 2f;
                float posY = (float)y / rows - 0.5f + puzzlePiece.transform.localScale.y / 2f;
                puzzlePiece.transform.position = new Vector3(posX, posY, 0f);

                puzzlePiece.transform.parent = transform;

                puzzlePieces.Add(puzzlePiece);
            }
        }
    }

    void ShufflePuzzlePieces()
    {
        for (int i = puzzlePieces.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            GameObject temp = puzzlePieces[i];
            puzzlePieces[i] = puzzlePieces[j];
            puzzlePieces[j] = temp;
        }
    }

    void RepositionPuzzlePieces()
    {
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            int x = i % columns;
            int y = i / columns;

            float posX = (float)x / columns - 0.5f + puzzlePieces[i].transform.localScale.x / 2f;
            float posY = (float)y / rows - 0.5f + puzzlePieces[i].transform.localScale.y / 2f;
            puzzlePieces[i].transform.position = new Vector3(posX, posY, 0f);
        }
    }
}
