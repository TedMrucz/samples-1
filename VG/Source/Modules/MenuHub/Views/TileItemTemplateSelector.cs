using System;
using System.Windows;
using System.Windows.Controls;
using VGTrader.Common;

namespace MenuHub.Views
{
    public class TileItemDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            //if (element != null && item != null && item is ITileBarBtnModel)
            //{
            //    ITileBarBtnModel tileElement = item as ITileBarBtnModel;

            //    switch (tileElement.ItemDataTemplate)
            //    {
            //        case 0:
            //            return element.FindResource("TileBtnRoundDataTemplate") as DataTemplate;
            //        case 1:
            //            return element.FindResource("TileBtnSpinDataTemplate") as DataTemplate;
            //        default:
            //            return element.FindResource("TileBtnDataTemplate") as DataTemplate;
            //    }
            //}

			

            return null;
        }
    }
}

