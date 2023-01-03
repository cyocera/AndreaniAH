﻿namespace ApiGeo
{
    public class BaseResponseEntity<T>
    {
        public int Code { get; set; } = 200;
        public int ErrorId { get; set; } = 0;
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
