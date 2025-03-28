﻿using FluentValidation.Results;

namespace Application.Abstract
{
    public static class ApiResponseBuilder
    {
        public static ApiResponse<T> Success<T>(T data, string message, int statusCode = 200)
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data,
                StatusCode = statusCode,
                Errors = null
            };
        }

        public static ApiResponse<T> Error<T>(string message, Dictionary<string, List<object>>? errors = null, int statusCode = 400)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Message = message,
                Data = default,
                StatusCode = statusCode,
                Errors = errors
            };
        }

        public static ApiResponse<T> ValidationError<T>(List<ValidationFailure> validationErrors, string message = "Validation failed")
        {
            var errors = new Dictionary<string, List<object>>();
            foreach (var error in validationErrors)
            {
                if (!errors.TryGetValue(error.PropertyName, out List<object>? value))
                {
                    value = [];
                    errors[error.PropertyName] = value;
                }
                value.Add(error.ErrorMessage);
            }

            return new ApiResponse<T>
            {
                IsSuccess = false,
                Message = message,
                Data = default,
                StatusCode = 400,
                Errors = errors
            };
        }
    }
}
