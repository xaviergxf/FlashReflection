namespace FlashReflection.Exceptions
{
    using System;

    public class WrongObjectReflectionException : ReflectionException
    {
        private Type expected;

        private Type actual;

        public WrongObjectReflectionException(Type expected, Type actual)
        {
            this.expected = expected;
            this.actual = actual;
        }

        public WrongObjectReflectionException(string message, Type expected, Type actual)
            : base(message)
        {
            this.expected = expected;
            this.actual = actual;
        }

        public WrongObjectReflectionException(string message, Exception innerException, Type expected, Type actual)
            : base(message, innerException)
        {
            this.expected = expected;
            this.actual = actual;
        }

        public Type Expected
        {
            get
            {
                return this.expected;
            }
        }

        public Type Actual
        {
            get
            {
                return this.actual;
            }
        }
    }
}