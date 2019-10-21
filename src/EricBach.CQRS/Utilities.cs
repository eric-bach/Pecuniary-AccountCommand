using System;

namespace EricBach.CQRS
{
    public static class Utilities
    {
        public static dynamic ChangeTo(dynamic source, Type dest)
        {
            return Convert.ChangeType(source, dest);
        }
    }
}
