using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker.Entities
{
	public enum AppRuleAlgorithm
	{
		Exact,
		ExactInvariant,
		Near,
		NearInvariant,
		Regex,
		RegexInvariant,
		StartsWith,
		StartsWithInvariant,
		EndWith,
		EndWithInvariant,
	}
}
