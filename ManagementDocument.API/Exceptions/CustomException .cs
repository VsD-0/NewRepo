namespace ManagementDocument.API.Exceptions
{
    public class CustomException : Exception
    {
        public List<CustomError> Errors { get; }

        public CustomException(List<CustomError> errors)
        {
            Errors = errors;
        }
    }
}
