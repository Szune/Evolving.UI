using System;

namespace E.UI.Attributes
{
    /// <summary>
    /// Refreshes the value of the marked member at the interval specified in milliseconds.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class UIRefreshAttribute : Attribute
    {
        public long Interval { get; set; }

        public UIRefreshAttribute(long interval = 5000)
        {
            Interval = interval;
            throw new NotImplementedException();
        }
    }
}
