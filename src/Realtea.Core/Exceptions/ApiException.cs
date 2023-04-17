using System;
using Realtea.Core.Enums;

namespace Realtea.Core.Exceptions
{
	public class ApiException : Exception
	{
		public ApiException(string message, FailureType failureType) : base(message)
		{
			FailureType = failureType;
		}

		public FailureType FailureType { get; }
	}
}

