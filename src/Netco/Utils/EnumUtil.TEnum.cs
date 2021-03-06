﻿using System;
using System.Collections.Generic;
using System.Linq;
using Netco.Monads;

namespace Netco.Utils
{
	/// <summary>
	/// Strongly-typed enumeration util
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	public static class EnumUtil< TEnum > where TEnum : struct, IComparable
	{
		/// <summary>
		/// Values of the <typeparamref name="TEnum"/>
		/// </summary>
		public static readonly TEnum[] Values;

		/// <summary>
		/// Values of the <typeparamref name="TEnum"/> without the default value.
		/// </summary>
		public static readonly TEnum[] ValuesWithoutDefault;

		// enum parsing performance is horrible!
		internal static readonly IDictionary< string, TEnum > CaseDict;
		internal static readonly IDictionary< string, TEnum > IgnoreCaseDict;

		internal static readonly string EnumPrefix = typeof( TEnum ).Name + "_";

		/// <summary>
		/// Efficient comparer for the enum
		/// </summary>
		public static readonly IEqualityComparer< TEnum > Comparer;

		static EnumUtil()
		{
			Values = GetValues();
			var def = default( TEnum );
			ValuesWithoutDefault = Values.Where( x => !def.Equals( x ) ).ToArray();
			Comparer = EnumComparer< TEnum >.Instance;

			IgnoreCaseDict = new Dictionary< string, TEnum >( StringComparer.InvariantCultureIgnoreCase );
			CaseDict = new Dictionary< string, TEnum >( StringComparer.InvariantCulture );

			foreach( var value in Values )
			{
				var item = value.ToString();
				IgnoreCaseDict[ item ] = value;
				CaseDict[ item ] = value;
			}
		}

		/// <summary>
		/// Converts the specified enum safely from the target enum. Matching is done
		/// via the efficient <see cref="Comparer"/> initialized with <see cref="MaybeParse.Enum{TEnum}(string)"/>
		/// </summary>
		/// <typeparam name="TSourceEnum">The type of the source enum.</typeparam>
		/// <param name="enum">The @enum to convert from.</param>
		/// <returns>converted enum</returns>
		/// <exception cref="ArgumentException"> when conversion is not possible</exception>
		public static TEnum ConvertSafelyFrom< TSourceEnum >( TSourceEnum @enum )
			where TSourceEnum : struct, IComparable
		{
			return EnumUtil< TSourceEnum, TEnum >.Convert( @enum );
		}

		private static TEnum[] GetValues()
		{
			var enumType = typeof( TEnum );

			if( !enumType.IsEnum )
				throw new ArgumentException( "Type is not an enum: '" + enumType.Name );

#if !SILVERLIGHT2

			return Enum
				.GetValues( enumType )
				.Cast< TEnum >()
				.ToArray();
#else
			return enumType
				.GetFields()
				.Where(field => field.IsLiteral)
				.ToArray(f => (TEnum) f.GetValue(enumType));
#endif
		}
	}
}