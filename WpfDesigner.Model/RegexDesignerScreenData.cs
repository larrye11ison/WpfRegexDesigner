using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml.Serialization;

namespace WpfRegexDesigner.Model
{
    [Serializable]
    public class RegexDesignerScreenData : PropertyChangeNotificationBase
    {
        private RegexDesignerApplicationOptions _applicationOptions = new RegexDesignerApplicationOptions();
        private BrushConverter _brushConverter;
        private FontWeightConverter _fontWeightConverter;
        private string _inputText = "string.MT";

        private string _InputTextResults = string.Empty;

        private FlowDocument _inputTextResultsFlowDocument = new FlowDocument();

        private ScreenLayoutOptions _LayoutOptions = null;

        private int _MatchedTextIndex = 0;

        private int _MatchedTextLength;

        private RegexMatchCollection _Matches = RegexMatchCollection.Empty;

        private RegexOptionsValueList _Options = new RegexOptionsValueList();

        private string _RegularExpression = string.Empty;

        private string _ReplacementExpression = string.Empty;

        private string _ReplacementResult = string.Empty;

        public RegexDesignerApplicationOptions ApplicationOptions
        {
            get { return _applicationOptions; }
            set { _applicationOptions = value; }
        }

        public string InputText
        {
            get { return _inputText; }
            set
            {
                var victor = value ?? string.Empty;
                if (_inputText.Equals(victor))
                {
                    return;
                }
                _inputText = victor;
                SendChangeNotification("InputText");
            }
        }

        [XmlIgnore]
        public string InputTextResults
        {
            get { return _InputTextResults; }
            private set
            {
                var victor = value ?? string.Empty;
                if (_InputTextResults.Equals(victor))
                {
                    return;
                }
                _InputTextResults = victor;
                SendChangeNotification("InputTextResults");
            }
        }

        public FlowDocument InputTextResultsFlowDocument
        {
            get
            {
                return _inputTextResultsFlowDocument;
            }
        }

        public ScreenLayoutOptions LayoutOptions
        {
            get
            {
                if (_LayoutOptions == null)
                {
                    _LayoutOptions = new ScreenLayoutOptions();
                }
                return _LayoutOptions;
            }
            set { _LayoutOptions = value; }
        }

        public int MatchedTextIndex
        {
            get { return _MatchedTextIndex; }
        }

        public int MatchedTextLength
        {
            get { return _MatchedTextLength; }
        }

        [XmlIgnore]
        public RegexMatchCollection Matches
        {
            get { return _Matches; }
            private set
            {
                var victor = value ?? RegexMatchCollection.Empty;
                if (_Matches.Equals(victor))
                {
                    return;
                }
                _Matches = victor;
                SendChangeNotification("Matches");
            }
        }

        //[XmlIgnore]
        public RegexOptionsValueList Options
        {
            get { return _Options; }
            set
            {
                if (_Options.Equals(value))
                {
                    return;
                }
                _Options = value;
                SendChangeNotification("Options");
            }
        }

        public string RegularExpression
        {
            get { return _RegularExpression; }
            set
            {
                var victor = value ?? string.Empty;
                if (_RegularExpression.Equals(victor))
                {
                    return;
                }
                _RegularExpression = victor;
                SendChangeNotification("RegularExpression");
            }
        }

        public string ReplacementExpression
        {
            get { return _ReplacementExpression; }
            set
            {
                string victor = value ?? string.Empty;
                if (_ReplacementExpression.Equals(victor))
                {
                    return;
                }
                _ReplacementExpression = victor;
                SendChangeNotification("ReplacementExpression");
            }
        }

        [XmlIgnore]
        public string ReplacementResult
        {
            get { return _ReplacementResult; }
            private set
            {
                var victor = value ?? string.Empty;
                if (_ReplacementResult.Equals(victor))
                {
                    return;
                }
                _ReplacementResult = victor;
                SendChangeNotification("ReplacementResult");
            }
        }

        private string LastExecutedInputText { get; set; }

        public void Execute()
        {
            LastExecutedInputText = InputText;
            Regex regex = new Regex(RegularExpression, Options.Value);
            MatchCollection matchCollection = regex.Matches(InputText);
            this.Matches = new WpfRegexDesigner.Model.RegexMatchCollection(matchCollection, regex);
            this.ReplacementResult = regex.Replace(InputText, ReplacementExpression);
            _MatchedTextIndex = -1; _MatchedTextLength = -1;
            SetMatchedTextProperties(0, 0);
        }

        public void SetMatchedTextProperties(int matchedTextIndex, int matchedTextLength)
        {
            if (_MatchedTextLength == matchedTextLength && _MatchedTextIndex == matchedTextIndex)
            {
                return;
            }
            _MatchedTextIndex = matchedTextIndex;
            _MatchedTextLength = matchedTextLength;
            ResetFlowDocumentText();
            SendChangeNotification("InputTextResultsFlowDocument");
        }

        private void ResetFlowDocumentText()
        {
            Paragraph para = new Paragraph();

            if (MatchedTextLength == 0)
            {
                // If the length is zero, there's nothing selected. Just dump ALL
                // the text into the document and bail.
                Run run = new Run(LastExecutedInputText);
                para.Inlines.Add(run);
            }
            else
            {
                // Cut the last exec'd input text into three pieces - before the matched text (begin),
                // the matched text itself (middle), and after the matched text (end). The "middle" part is
                // the one that we'll apply style stuff to so that it stands out from the rest of the text.
                Run begin = new Run(LastExecutedInputText.Substring(0, MatchedTextIndex));
                Run middle = new Run(LastExecutedInputText.Substring(MatchedTextIndex, MatchedTextLength));

                _fontWeightConverter = new FontWeightConverter();
                _brushConverter = new BrushConverter();

                middle.FontWeight = (FontWeight)_fontWeightConverter.ConvertFrom("Bold");
                middle.Foreground = new SolidColorBrush(_applicationOptions.MatchedTextForegroundColor);
                middle.Background = new SolidColorBrush(_applicationOptions.MatchedTextBackgroundColor);
                Run end = new Run(LastExecutedInputText.Substring(MatchedTextIndex + MatchedTextLength));

                para.Inlines.AddRange(new Run[] { begin, middle, end });
            }

            _inputTextResultsFlowDocument.Blocks.Clear();
            _inputTextResultsFlowDocument.Blocks.Add(para);
        }
    }
}