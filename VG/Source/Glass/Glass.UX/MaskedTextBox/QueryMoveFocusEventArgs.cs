using System.Windows;
using System.Windows.Input;

namespace MaskedTextBox
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")]
    public delegate void QueryMoveFocusEventHandler(object sender, QueryMoveFocusEventArgs e);

    public class QueryMoveFocusEventArgs : RoutedEventArgs
    {
        //default CTOR private to prevent its usage.
        private QueryMoveFocusEventArgs()
        {
        }

        //internal to prevent anybody from building this type of event.
        internal QueryMoveFocusEventArgs(FocusNavigationDirection direction, bool reachedMaxLength)
            : base(AutoSelectTextBox.QueryMoveFocusEvent)
        {
            this.navigationDirection = direction;
            this.reachedMaxLength = reachedMaxLength;
        }

        private FocusNavigationDirection navigationDirection;
        public FocusNavigationDirection FocusNavigationDirection
        {
            get { return this.navigationDirection; }
        }

        private bool reachedMaxLength;
        public bool ReachedMaxLength
        {
            get { return this.reachedMaxLength; }
        }

        private bool canMove = true; //defaults to true... if nobody does nothing, then its capable of moving focus.
        public bool CanMoveFocus
        {
            get { return this.canMove; }
            set { this.canMove = value; }
        }
    }
}
