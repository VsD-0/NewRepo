using Microsoft.AspNetCore.Mvc;

namespace ManagementDocument.API.Exceptions
{
    public class CustomProblemDetails : ProblemDetails
    {
        public List<CustomError> Errors { get; set; }
    }
}
