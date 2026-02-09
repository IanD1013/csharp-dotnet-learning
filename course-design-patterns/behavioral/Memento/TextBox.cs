namespace Memento;

public class TextBox
{
    private string _text = string.Empty;

    public void SetText(string text)
    {
        _text = text;
    }
    
    public string GetText()
    {
        return _text;
    }
    
    public TextState Save()
    {
        return new TextState(_text);
    }
    
    public void Restore(TextState textState)
    {
        _text = textState.GetText();
    }


    public class TextState
    {
        private readonly string _text;

        internal TextState(string text)
        {
            _text = text;
        }
        
        internal string GetText()
        {
            return _text;
        }
    }
}