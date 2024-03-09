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
                // Memotong potongan gambar
                Texture2D pieceTexture = new Texture2D(pieceWidth, pieceHeight);
                pieceTexture.SetPixels(sourceImage.GetPixels(x * pieceWidth, (rows - 1 - y) * pieceHeight, pieceWidth, pieceHeight));
                pieceTexture.Apply();

                // Membuat GameObject baru untuk potongan gambar
                GameObject puzzlePiece = new GameObject("PotonganGambar_" + x + "_" + y);
                puzzlePiece.transform.SetParent(transform);

                // Menambahkan Image untuk menampilkan potongan gambar
                Image image = puzzlePiece.AddComponent<Image>();
                image.sprite = Sprite.Create(pieceTexture, new Rect(0, 0, pieceWidth, pieceHeight), new Vector2(2f,2f));
                // Mengatur ukuran dari RectTransform
                RectTransform rectTransform = puzzlePiece.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(pieceWidth, pieceHeight);



                BoxCollider2D collider = puzzlePiece.AddComponent<BoxCollider2D>();
                collider.size = new Vector2(pieceWidth -25, pieceHeight -28);
                // Menambahkan potongan gambar ke dalam list
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
    // Membuat list dari semua posisi yang mungkin
    List<Vector3> positions = new List<Vector3>();
    for (int i = 0; i < puzzlePieces.Count; i++)
    {
        float posX = (i % columns) * puzzlePieces[i].rectTransform.rect.width;
        float posY = (i / columns) * puzzlePieces[i].rectTransform.rect.height;
        positions.Add(new Vector3(posX, posY, 0f));
    }

    // Memberikan setiap potongan puzzle posisi berikutnya dari list posisi yang diacak
    for (int i = 0; i < puzzlePieces.Count; i++)
    {
        puzzlePieces[i].rectTransform.localPosition = positions[i];
    }
}


}
