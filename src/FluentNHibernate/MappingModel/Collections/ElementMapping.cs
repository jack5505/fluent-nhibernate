using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentNHibernate.MappingModel.Collections
{
    public class ElementMapping : MappingBase
    {
        private readonly IList<ColumnMapping> columns = new List<ColumnMapping>();
        private readonly AttributeStore<ElementMapping> attributes = new AttributeStore<ElementMapping>();

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessElement(this);

            foreach (var column in columns)
                visitor.Visit(column);
        }

        public TypeReference Type
        {
            get { return attributes.Get(x => x.Type); }
            set { attributes.Set(x => x.Type, value); }
        }

        public void AddColumn(ColumnMapping mapping)
        {
            columns.Add(mapping);
        }

        public IEnumerable<ColumnMapping> Columns
        {
            get { return columns; }
        }

        public bool IsSpecified<TResult>(Expression<Func<ElementMapping, TResult>> property)
        {
            return attributes.IsSpecified(property);
        }

        public bool HasValue<TResult>(Expression<Func<ElementMapping, TResult>> property)
        {
            return attributes.HasValue(property);
        }

        public void SetDefaultValue<TResult>(Expression<Func<ElementMapping, TResult>> property, TResult value)
        {
            attributes.SetDefault(property, value);
        }
    }
}