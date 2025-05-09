namespace BookLendingSystem.Application.Common
{
    public class ResultBehaviour<T>
    {
        public bool Succeeded { get; set; }
        public string[] Messages { get; set; }
        public T? Data { get; set; }

        public ResultBehaviour(bool succeeded, IEnumerable<string> messages, T? data = default)
        {
            Succeeded = succeeded;
            Messages = messages.ToArray();
            Data = data;
        }

        public static ResultBehaviour<T> Success(T? data = default)
        {
            return new ResultBehaviour<T>(true, Array.Empty<string>(), data);
        }

        public static ResultBehaviour<T> Success(string message, T? data = default)
        {
            return new ResultBehaviour<T>(true, new List<string> { message }, data);
        }

        public static ResultBehaviour<T> Failure(IEnumerable<string> errors)
        {
            return new ResultBehaviour<T>(false, errors);
        }

        public static ResultBehaviour<T> Failure(string error)
        {
            return Failure(new List<string> { error });
        }

    }

}
