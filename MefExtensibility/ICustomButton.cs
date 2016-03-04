using System;
using System.Windows.Media;

namespace MefExtensibility
{
    public interface ICustomButton
    {
        Brush GetColor();
        Action ClickAction { get; set; }
    }
}
