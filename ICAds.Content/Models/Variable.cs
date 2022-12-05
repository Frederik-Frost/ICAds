using System;
namespace ICAds.Content.Models
{
	public class Variable
	{
		public string Value { get; set; }

        string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = "{" + value + "}";
            }
        }

        public Variable(string name, string value)
		{
            Name = name;
            Value = value;
		}
	}
}

