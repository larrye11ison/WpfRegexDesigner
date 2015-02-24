namespace WpfRegexDesigner.Model
{
    public interface IRegexInfo
    {
        int Index
        {
            get;
        }

        int Length
        {
            get;
        }

        string Value
        {
            get;
        }
    }
}