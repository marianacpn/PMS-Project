namespace Shared.Support.DB
{
    public class CommandParameter
    {
        public CommandParameter()
        {
        }

        public CommandParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }

        public object Value { get; set; }
    }
}
