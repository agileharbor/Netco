using System;
using System.Threading.Tasks;

namespace Netco.ActionPolicyServices
{
	/// <summary>
	/// Same as <see cref="ActionPolicy"/>, but indicates that this policy
	/// holds some state and thus must have syncronized access.
	/// </summary>
	[ Serializable ]
	public sealed class ActionPolicyWithStateAsync : ActionPolicyAsync
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionPolicyWithState"/> class.
		/// </summary>
		/// <param name="policy">The policy.</param>
		public ActionPolicyWithStateAsync( Func< Func< Task >, Task > policy ) : base( policy )
		{
		}
	}
}