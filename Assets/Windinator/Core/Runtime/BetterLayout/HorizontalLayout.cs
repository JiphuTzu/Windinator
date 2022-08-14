using UnityEngine;

public class HorizontalLayout : GenericLayout
{
    protected override void OnDirty(int childCount)
    {
        Rect parent = RectTransform.rect;

        Vector2 prefferedSize = default;
        Vector2 totalPrefferedSize = default;
        Vector2 totalFlexibleSpace = default;

        foreach (var child in Children)
        {
            totalFlexibleSpace += child.Flexible;
            totalPrefferedSize += Vector2.Max(default, child.PrefferedSize - child.MinSize);
            prefferedSize += child.PrefferedSize;
        }

        Vector2 paddingSize = new Vector2(Padding.x + Padding.z, Padding.y + Padding.w);
        Vector2 containerSize = parent.size - paddingSize;
        Vector2 usedSize = FitMinimum();

        usedSize = FitPreffered(totalPrefferedSize, containerSize - usedSize);

        Layout.PrefferedSize = prefferedSize + new Vector2(Padding.x + Padding.z, Padding.y + Padding.w);

        usedSize = FitFlexible(totalFlexibleSpace, containerSize - usedSize);

        Arrange(containerSize, usedSize);
    }

    void Arrange(Vector2 container, Vector2 usedSize)
    {
        float advance = Padding.x;

        switch (Alignment)
        {
            case TextAnchor.LowerLeft:
            case TextAnchor.LowerCenter:
            case TextAnchor.LowerRight:

            advance += container.x - usedSize.x;

            break;

            case TextAnchor.MiddleLeft:
            case TextAnchor.MiddleCenter:
            case TextAnchor.MiddleRight:

            advance += (container.x - usedSize.x) * 0.5f;

            break;
        }

        foreach(var layout in Children)
        {
            var child = layout.RectTransform;
            var size = layout.CachedSize;

            float height = layout.Flexible.y != 0 ? 
                Mathf.Max(layout.MinSize.y, container.y) : 
                Mathf.Max(layout.MinSize.y, Mathf.Min(layout.PrefferedSize.y, container.y));

            float posY = Padding.y;

            switch (Alignment)
            {
                case TextAnchor.UpperCenter:
                case TextAnchor.LowerCenter:
                case TextAnchor.MiddleCenter:

                posY += (container.y - height) * 0.5f;

                break;

                case TextAnchor.UpperRight:
                case TextAnchor.LowerRight:
                case TextAnchor.MiddleRight:

                posY += container.y - height;

                break;
            }

            child.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, posY, height);
            child.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, advance, size.x);
            advance += size.x;
        }
    }
}
