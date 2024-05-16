﻿namespace ReportAndSolveAPI.Models
{
    public class ServiceResponse<T>  
    {
        public T? Data { get; set; }

        public bool Succes { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
