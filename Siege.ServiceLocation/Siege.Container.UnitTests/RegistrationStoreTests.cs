﻿/*   Copyright 2009 - 2010 Marcus Bratton

     Licensed under the Apache License, Version 2.0 (the "License");
     you may not use this file except in compliance with the License.
     You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

     Unless required by applicable law or agreed to in writing, software
     distributed under the License is distributed on an "AS IS" BASIS,
     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     See the License for the specific language governing permissions and
     limitations under the License.
*/

using NUnit.Framework;
using Siege.ServiceLocation.Extensions.ExtendedSyntax;
using Siege.ServiceLocation.UnitTests.TestClasses;

namespace Siege.ServiceLocation.UnitTests
{
	public abstract partial class SiegeContainerTests
	{
		[Test]
		public void Should_Identify_Constructor_Candidates()
		{
			locator.Register(Given<MultiConstructorType>.Then<MultiConstructorType>());

			var candidates = locator.Store.RegistrationStore.GetCandidatesForType<MultiConstructorType>();

			Assert.AreEqual(6, candidates.Count);
		}
	}
}