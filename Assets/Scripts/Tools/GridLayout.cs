using UnityEngine;
using System.Collections;

// 网格布局
public class GridLayout : MonoBehaviour {
    public enum RestrictType { rowCount, columnCount };
    public RestrictType restrict;
    public int restrictCount;

    public enum AlignType { NorthWest, North, NorthEast };
    public AlignType align;

    public int cellWidth=100, cellHeight=100;
    public int spaceWidth, spaceHeight;

    private int itemCount = 0;
    //private float positionX, positionY, positionZ;
    private float width, height;
    private UISprite uiSprite;

	// Use this for initialization
	void Start () {
        //positionX = transform.localPosition.x;
        //positionY = transform.localPosition.y;
        //positionZ = transform.localPosition.z;

        uiSprite = gameObject.GetComponent<UISprite>();
        width = uiSprite.localSize.x;
        height = uiSprite.localSize.y;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.childCount != itemCount)
        {
            rePaint();
            itemCount = transform.childCount;
        }
	}

    // 重新排列
    public void rePaint()
    {
        //Debug.Log("width: " + width);
        //Debug.Log("height: " + height);

        itemCount = transform.childCount;
        int childIndex = 0;
        float x = 0, y = 0;
        Transform t;

        renewXY(ref x, ref y);
        while (childIndex < itemCount)
        {
            //print("x : " + x + "  y : " + y);
            t = transform.GetChild(childIndex);
            t.localPosition = new Vector3(x, y, 0);
            t.localScale = Vector3.one;
            t.GetComponent<UISprite>().width = cellWidth;
            t.GetComponent<UISprite>().height = cellHeight;

            childIndex++;
            if (restrict == RestrictType.rowCount){
                y -= cellHeight + spaceHeight;

                if (childIndex / restrictCount > 1 && childIndex % restrictCount == 0)
                {
                    x += (cellWidth + spaceWidth) * (align == AlignType.NorthEast ? -1 : 1);
                    y = 0;

                    // 扩充容器大小
                    if (x + cellWidth > width)
                        transform.GetComponent<UISprite>().width = int.Parse((x + cellWidth) + "");
                }
            }
            else if (restrict == RestrictType.columnCount)
            {
                x += cellWidth + spaceWidth;

                if (childIndex / restrictCount >= 1 && childIndex % restrictCount == 0)
                {
                    renewXY(ref x, ref y);
                    y -= (cellHeight + spaceHeight) * (childIndex / restrictCount);

                    // 扩充容器大小
                    if (-y + cellWidth > height)
                        transform.GetComponent<UISprite>().height = int.Parse((-y + cellHeight) + "");
                }
            }
        }
    }

    private void renewXY(ref float x, ref float y)
    {
        switch (align){
            case AlignType.NorthWest:
                x = 0;
                break;
            case AlignType.North:
                if (restrict == RestrictType.columnCount)
                    x = itemCount > restrictCount 
                        ? (width - restrictCount * cellWidth) / (restrictCount + 1)
                        : (width - itemCount * cellWidth) / (itemCount + 1);
                else if (restrict == RestrictType.rowCount)
                {
                    int column = itemCount / restrictCount + (itemCount % restrictCount > 0 ? 1 : 0);
                    x = (width - column * cellWidth) / (column + 1);
                }
                break;
            case AlignType.NorthEast:
                x = width - cellWidth;
                break;
        }
                y = 0;
    }
}
