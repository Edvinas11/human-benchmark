using System;

namespace AimReactionAPI.Exceptions {
    public class UserNotFoundException : Exception {
        public UserNotFoundException(string message) : base(message) {

        }
    }
}
