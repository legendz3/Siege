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

using System.Linq;
using System.Web;
using Siege.Provisions.UnitOfWork;

namespace Siege.Provisions.Web
{
    public class HttpUnitOfWorkStore : IUnitOfWorkStore
    {
        public void Dispose()
        {
            if (SessionExists())
            {
                var unitsOfWork =
                    HttpContext.Current.Session.Keys.OfType<string>().Where(
                        x => x.StartsWith("HttpUnitOfWorkStore.CurrentUnitOfWork_")).ToList();

                foreach (string key in unitsOfWork)
                {
                    var currentUnitOfWork = HttpContext.Current.Session[key] as IUnitOfWork;

                    if (currentUnitOfWork == null) continue;
                    currentUnitOfWork.Dispose();

                    HttpContext.Current.Session.Remove(key);
                }
            }
        }

        public IUnitOfWork CurrentFor<TDatabase>() where TDatabase : IDatabase
        {
            if (SessionExists())
            {
                return
                    HttpContext.Current.Session["HttpUnitOfWorkStore.CurrentUnitOfWork_" + typeof (TDatabase)]
                    as IUnitOfWork;
            }
            return null;
        }

        public void SetUnitOfWork<TDatabase>(IUnitOfWork unitOfWork)
            where TDatabase : IDatabase
        {
            if (SessionExists())
            {
                HttpContext.Current.Session["HttpUnitOfWorkStore.CurrentUnitOfWork_" + typeof (TDatabase)] =
                    unitOfWork;
            }
        }

        private bool SessionExists()
        {
            return HttpContext.Current.Session != null;
        }
    }
}