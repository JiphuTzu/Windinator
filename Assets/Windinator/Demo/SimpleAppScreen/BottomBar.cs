using Riten.Windinator;
using Riten.Windinator.LayoutBuilder;

public class BottomBar : LayoutBaker
{
    public override Layout.Element Bake()
    {
        return new Layout.Rectangle(
            shape: new ShapeProperties()
            {
                Color = Colors.Primary.ToColor(),
                Shadow = new ShadowProperties()
                {
                    Size = 10f,
                    Blur = 20f
                }
            },
            child: new Layout.Horizontal(
                new Layout.Element[] {
                    new MaterialUI.Icon(MaterialIcons.home, color: Colors.OnPrimary.ToColor()),
                    new MaterialUI.Icon(MaterialIcons.apple, color: Colors.OnPrimary.ToColor()),
                    new MaterialUI.Icon(MaterialIcons.ornament, color: Colors.OnPrimary.ToColor()),
                    new MaterialUI.Icon(MaterialIcons.settings_helper, color: Colors.OnPrimary.ToColor()),
                },
                Padding: new UnityEngine.Vector4(10, 10, 10, 10),
                spacing: 20f,
                alignment: UnityEngine.TextAnchor.MiddleCenter
            ),
            flexibleWidth: 1f
        );
    }
}
