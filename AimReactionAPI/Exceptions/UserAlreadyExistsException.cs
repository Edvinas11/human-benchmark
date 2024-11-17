using System;

namespace AimReactionAPI.Exceptions {
    public class UserAlreadyExistsException : Exception {
        public UserAlreadyExistsException(string message) : base(message) {

        }
    }
}
