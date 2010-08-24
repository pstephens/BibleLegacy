#region Copyright Notice

/* Copyright 2009-2010 Peter Stephens

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

#endregion

using System;
using System.Collections.Generic;

namespace Builder.Model
{
    public abstract class ServiceProvider<TRelated> : IServiceProvider<TRelated> where TRelated : class
    {
        private readonly Dictionary<Type, IService<TRelated>> services =
            new Dictionary<Type, IService<TRelated>>();

        public T GetService<T>() where T : class, IService<TRelated>, new()
        {
            IService<TRelated> service;
            if (!services.TryGetValue(typeof(T), out service))
            {
                service = new T {Related = this as TRelated};
                if(service.Related == null)
                    throw new InvalidOperationException("ServiceProvider (Related) must implement TRelated.");
                services.Add(typeof (T), service);
            }

            return (T) service;
        }
    }
}