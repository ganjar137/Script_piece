using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PotonganSprite : MonoBehaviour
{
    public Texture2D sprites;
    [SerializeField] int rows;
    [SerializeField] int columns;
    public GridLayoutGroup gridGrup;

    public List<GameObject> piecePuzzle = new List<GameObject>();
    
    private void Start()
    {
        ImageSlice();
    }

    public void ImageSlice()
    {
        int widths = sprites.width / columns;
        int heights = sprites.height / rows;

        // Set cell size
        gridGrup.cellSize = new Vector2(300, 300);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                // Calculate starting positions
                int startX = x * widths;
                int startY = y * heights;

                // Create texture for the piece
                Texture2D piece = new Texture2D(widths, heights);
                
                // Get pixels from the original image horizontally
                piece.SetPixels(sprites.GetPixels(startX, sprites.height - (startY + heights), widths, heights));
                piece.Apply(); 

                // Create GameObject for the piece
                GameObject obj = new GameObject("piece : " + x + "-" + y);
                Image img = obj.AddComponent<Image>();
                img.sprite = Sprite.Create(piece, new Rect(0, 0, widths, heights), new Vector2(0.5f, 0.5f));
                obj.transform.SetParent(gridGrup.transform); // Set as child of GridLayoutGroup
                piecePuzzle.Add(obj);
            }
        }
    }
}
