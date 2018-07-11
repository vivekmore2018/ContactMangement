using System.Collections.Generic;

namespace CM.Interface.Business
{
    public class BusinessResult<T> where T : class
    {
        private T value;

        public List<string> Errors { get; set; }

        public BusinessResult(T value, List<string> errors) : this(value)
        {
            Errors = errors;
        }

        public BusinessResult(T value) : this()
        {
            Value = value;
        }

        public BusinessResult()
        {
            Errors = new List<string>();
        }

        public Status Status
        {
            get { return Errors.Count > 0 ? Status.Failure : Status.Success; }
        }

        public T Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }

    public enum Status
    {
        Success = 0,
        Failure = -1
    }
}
