using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Exceptions
{
    /// <inheritdoc />
    /// <summary>Exception representing a response from Twitch to attempting to access a resource a user is not allowed to.</summary>
    public class ForbiddenResourceException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public ForbiddenResourceException(string data)
            : base(data)
        {
        }
    }
}
