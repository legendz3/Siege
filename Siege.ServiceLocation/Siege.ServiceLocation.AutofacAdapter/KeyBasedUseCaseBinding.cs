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

using Autofac;
using Autofac.Builder;
using Siege.ServiceLocation.Bindings;
using Siege.ServiceLocation.UseCases;

namespace Siege.ServiceLocation.AutofacAdapter
{
    public class KeyBasedUseCaseBinding<TService> : IKeyBasedUseCaseBinding<TService>
    {
        private IContainer container;

        public KeyBasedUseCaseBinding(IContainer container)
        {
            this.container = container;
        }

        public void Bind(IUseCase useCase, IFactoryFetcher locator)
        {
            Bind((IKeyBasedUseCase<TService>)useCase);
        }

        private void Bind(IKeyBasedUseCase<TService> useCase)
        {
            var builder = new ContainerBuilder();

            builder.Register(useCase.GetBoundType()).Named(useCase.Key).FactoryScoped();

            builder.Build(container);
        }
    }
}