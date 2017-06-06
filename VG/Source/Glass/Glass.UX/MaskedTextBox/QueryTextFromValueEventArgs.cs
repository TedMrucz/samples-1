using System;
using System.Collections.Generic;
using System.Text;

namespace MaskedTextBox
{
    public class QueryTextFromValueEventArgs : EventArgs
    {
        public QueryTextFromValueEventArgs(object value, string text)
        {
            this.value = value;
            this.text = text;
        }

        private object value;

        public object Value
        {
            get { return value; }
        }

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}
