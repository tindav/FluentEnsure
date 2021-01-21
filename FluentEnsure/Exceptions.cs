using System;

namespace FluentEnsure
{
    public static class Exceptions
    {
        public static Exception GetDefaultException() => new Exception("The rule was not respected.");
    }
}
