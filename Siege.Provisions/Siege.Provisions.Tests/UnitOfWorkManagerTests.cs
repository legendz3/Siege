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
using Rhino.Mocks;

namespace Siege.Provisions.Tests
{
    [TestFixture]
    public class UnitOfWorkManagerTests
    {
        private MockRepository mocks;
        private UnitOfWorkManager unitOfWorkManager;
        private IUnitOfWorkStore store;
        private IUnitOfWorkFactory factory;
        private IUnitOfWork instance;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            instance = mocks.DynamicMock<IUnitOfWork>();
            store = mocks.DynamicMock<IUnitOfWorkStore>();
            factory = mocks.DynamicMock<IUnitOfWorkFactory>();
            unitOfWorkManager = mocks.Stub<UnitOfWorkManager>();
            unitOfWorkManager.Add(new NullPersistenceModule(factory, store));
        }

        [Test]
        public void CallingCurrentFirstTimeShouldCreateNewUnitOfWork()
        {
            using (mocks.Record())
            {
                store.Expect(s => s.CurrentFor<NullPersistenceModule>()).Return(null).Repeat.Twice();
                factory.Expect(f => f.Create());
                store.Expect(s => s.SetUnitOfWork<NullPersistenceModule>(null)).IgnoreArguments();
            }

            using (mocks.Playback())
            {
                var unitOfWork = unitOfWorkManager.For<NullPersistenceModule>();
            }
        }

        [Test]
        public void CallingCurrentAfterFirstTimeShouldNotCreateNewUnitOfWork()
        {
            using (mocks.Record())
            {
                store.Expect(s => s.CurrentFor<NullPersistenceModule>()).Return(instance).Repeat.Twice();
                factory.Expect(f => f.Create()).Repeat.Never();
                store.Expect(s => s.SetUnitOfWork<NullPersistenceModule>(null)).IgnoreArguments().Repeat.Never();
            }

            using (mocks.Playback())
            {
                var unitOfWork = unitOfWorkManager.For<NullPersistenceModule>();
            }
        }

        [Test]
        public void AddingModuleTwiceShouldOnlyAddOnce()
        {
            using (mocks.Record())
            {
                store.Expect(s => s.CurrentFor<NullPersistenceModule>()).Return(instance).Repeat.Twice();
                factory.Expect(f => f.Create()).Repeat.Never();
                store.Expect(s => s.SetUnitOfWork<NullPersistenceModule>(null)).IgnoreArguments().Repeat.Never();
            }

            using (mocks.Playback())
            {
                unitOfWorkManager.Add(new NullPersistenceModule(factory, store));
                var unitOfWork = unitOfWorkManager.For<NullPersistenceModule>();
            }
        }
    }
}