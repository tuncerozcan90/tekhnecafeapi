﻿namespace TekhneCafe.Core.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message = "Forbidden Access!") : base(message)
        {

        }
    }
}
