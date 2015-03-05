namespace WhoCanHelpMe.Domain
{
    using System;

    public class Assertion
    {
        public Assertion(string category, string tag)
        {
            this.Category = category;
            this.Tag = tag;
            this.AddedOn = DateTime.UtcNow;
            this.Id = Guid.NewGuid();
        }

        public DateTime AddedOn { get; set; }

        public string Category { get; set; }

        public Guid Id { get; set; }

        public string Tag { get; set; }
    }
}