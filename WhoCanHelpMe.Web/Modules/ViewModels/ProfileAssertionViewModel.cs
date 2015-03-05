namespace WhoCanHelpMe.Web.Modules.ViewModels
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{Category} {Tag}")]
    public class ProfileAssertionViewModel
    {
        public Guid Id { get; set; }

        public string Category { get; set; }

        public string Tag { get; set; }
    }
}