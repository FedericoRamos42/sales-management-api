using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utils.Result
{
    public class Result<T> where T : class
    {
        public T? Value { get; set; }
        public bool IsSucces {  get; set; }
        public List<string> Errors { get; set; } = [];

        public Result(T value)
        {
            Value = value;
            Errors = [];
            IsSucces = true;
        }

        public Result(List<string> errors)
        {
            Value = default!;
            Errors = errors;
            IsSucces = false;
        }

        public static Result<T> Succes(T value)
        {
            return new Result<T>(value);
        }

        public static Result<T> Failure(string error) 
        {
            List<string> errors = new List<string>();
            errors.Add(error);
            return new Result<T>(errors);
        }

        public static Result<T> Failure(List<string> errors)
        {
            return new Result<T>(errors);
        }

    }
}
