using System;
using System.Collections.Generic;
using System.Text;

namespace bitKeeper.Exceptions
{
    [Serializable]
    public class WrongEncryptionKeyException : Exception
    {
        public WrongEncryptionKeyException() { }
        public WrongEncryptionKeyException(string message) : base(message) { }
        public WrongEncryptionKeyException(string message, Exception inner) : base(message, inner) { }
        protected WrongEncryptionKeyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
