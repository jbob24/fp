﻿using System;
using System.Runtime.Serialization;

namespace FinanceProjector.TransactionProviders.OFX
{
    [Serializable]
    public class OFXParseException : Exception
    {
        public OFXParseException()
        {
        }

        public OFXParseException(string message)
            : base(message)
        {
        }

        public OFXParseException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected OFXParseException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}