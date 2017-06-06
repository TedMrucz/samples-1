// (c) Norbert Huffschmid
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.

using System.Windows;
using System.Windows.Input;

namespace SpinDotters
{
    public class ComboBox : DomainUpDown
    {
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (this.spinPanel.Expanded)
            {
                // determine selected index from click position
                Point mousePosition = e.GetPosition(this);
                double averageItemHeight = this.ActualHeight / this.Items.Count;
                this.SelectedIndex = (int) (mousePosition.Y / averageItemHeight);

                this.spinPanel.Expanded = false;
            }
            else
            {
                this.spinPanel.Expanded = true;
            }
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);

            this.spinPanel.Expanded = false;
        }
    }
}
