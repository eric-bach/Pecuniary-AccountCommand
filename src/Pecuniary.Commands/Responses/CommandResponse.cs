using System;

namespace Pecuniary.Commands.Responses
{
    public class CommandResponse
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public string Error { get; set; }
    }
}
