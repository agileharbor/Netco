using System;

namespace Netco.Monads
{
	/// <summary> Helper class for creating <see cref="Result{T}"/> instances </summary>
	public static class Result
	{
		/// <summary> Creates success result </summary>
		/// <typeparam name="TValue">The type of the result.</typeparam>
		/// <param name="value">The item.</param>
		/// <returns>new result instance</returns>
		/// <seealso cref="Result{T}.CreateSuccess"/>
		public static Result< TValue > CreateSuccess< TValue >( TValue value )
		{
			return Result< TValue >.CreateSuccess( value );
		}

		/// <summary> Creates success result </summary>
		/// <typeparam name="TValue">The type of the result.</typeparam>
		/// <param name="value">The item.</param>
		/// <returns>new result instance</returns>
		/// <seealso cref="Result{T}.CreateSuccess"/>
		[ Obsolete( "Use CreateSuccess instead" ) ]
		public static Result< TValue > Success< TValue >( TValue value )
		{
			return CreateSuccess( value );
		}
	}
}