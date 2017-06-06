using System;
using System.Collections.Generic;
using System.Text;

namespace MaskedTextBox
{
    public class QueryValueFromTextEventArgs : EventArgs
    {
        public QueryValueFromTextEventArgs(string text, object value)
        {
            this.text = text;
            this.value = value;
        }

        private string text;

        public string Text
        {
            get { return text; }
        }

        private object value;

        public object Value
        {
            get { return value; }
            set { this.value = value; }
        }

        private bool hasParsingError;

        public bool HasParsingError
        {
            get { return hasParsingError; }
            set { hasParsingError = value; }
        }
    }
}
