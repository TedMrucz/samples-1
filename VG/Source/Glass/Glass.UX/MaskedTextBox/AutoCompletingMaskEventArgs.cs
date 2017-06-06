using System.ComponentModel;

namespace MaskedTextBox
{
    public class AutoCompletingMaskEventArgs : CancelEventArgs
    {
        public AutoCompletingMaskEventArgs(MaskedTextProvider maskedTextProvider, int startPosition, int selectionLength, string input)
        {
            this.autoCompleteStartPosition = -1;
            this.maskedTextProvider = maskedTextProvider;
            this.startPosition = startPosition;
            this.selectionLength = selectionLength;
            this.input = input;
        }

        private MaskedTextProvider maskedTextProvider;
        public MaskedTextProvider MaskedTextProvider
        {
            get { return this.maskedTextProvider; }
        }

        private int startPosition;
        public int StartPosition
        {
            get { return this.startPosition; }
        }

        private int selectionLength;
        public int SelectionLength
        {
            get { return this.selectionLength; }
        }

        private string input;
        public string Input
        {
            get { return this.input; }
        }

        private int autoCompleteStartPosition;
        public int AutoCompleteStartPosition
        {
            get { return this.autoCompleteStartPosition; }
            set { this.autoCompleteStartPosition = value; }
        }

        private string autoCompleteText;
        public string AutoCompleteText
        {
            get { return this.autoCompleteText; }
            set { this.autoCompleteText = value; }
        }
    }
}
