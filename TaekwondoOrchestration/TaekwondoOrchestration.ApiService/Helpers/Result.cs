using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Helpers
{
    public class Result
    {
        public bool Success { get; protected set; }
        public List<string> Errors { get; protected set; } = new();
        public bool Failure => !Success;

        protected Result() { }

        public static Result Ok() =>
            new Result { Success = true };

        public static Result Fail(string error) =>
            new Result { Success = false, Errors = new List<string> { error } };

        public static Result Fail(List<string> errors) =>
            new Result { Success = false, Errors = errors };
    }

    public class Result<T> : Result
    {
        public T? Value { get; private set; }

        private Result() { }

        public static Result<T> Ok(T value) =>
            new Result<T> { Success = true, Value = value };

        public static new Result<T> Fail(string error) =>
            new Result<T> { Success = false, Errors = new List<string> { error } };

        public static new Result<T> Fail(List<string> errors) =>
            new Result<T> { Success = false, Errors = errors };
    }
}
