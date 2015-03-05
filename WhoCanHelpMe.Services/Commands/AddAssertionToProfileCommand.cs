namespace WhoCanHelpMe.Services.Commands
{
    using System;

    using WhoCanHelpMe.Common.Commands;

    public class AddAssertionToProfileCommand : ICommand
    {
        public Guid UserId { get; set; }

        public string Category { get; set; }

        public string Tag { get; set; }
    }
}