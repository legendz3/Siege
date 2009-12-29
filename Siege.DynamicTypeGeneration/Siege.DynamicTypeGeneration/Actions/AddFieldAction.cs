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

using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Siege.DynamicTypeGeneration.Actions
{
    public class AddFieldAction : ITypeGenerationAction
    {
        private FieldInfo field;
        private FieldBuilder fieldBuilder;

        public FieldInfo Field
        {
            get { return fieldBuilder; }
        }

        public FieldInfo Source
        {
            get { return field; }
        }

        public AddFieldAction(BuilderBundle bundle, FieldInfo field)
            : this(bundle, field.Name, field.FieldType)
        {
            this.field = field;
        }

        public AddFieldAction(BuilderBundle bundle, string fieldName, Type fieldType)
        {
            this.field = fieldBuilder = bundle.TypeBuilder.DefineField(fieldName, fieldType, FieldAttributes.Public);
        }

        public void Execute()
        {
        }
    }
}